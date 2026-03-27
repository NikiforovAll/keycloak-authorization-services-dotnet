#!/usr/bin/env bash
set -uo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_DIR="$(dirname "$SCRIPT_DIR")"
PORT=5098
BASE_URL="http://localhost:$PORT"

ADMIN_TOKEN=$(bash "$SCRIPT_DIR/get-token.sh" testadminuser test 2>/dev/null | sed 's/TOKEN=//')
READER_TOKEN=$(bash "$SCRIPT_DIR/get-token.sh" test test 2>/dev/null | sed 's/TOKEN=//')

PASS=0
FAIL=0

assert_status() {
    local label="$1" expected="$2" url="$3" token="$4"
    local actual
    actual=$(curl -s -o /dev/null -w "%{http_code}" "$url" -H "Authorization: Bearer $token")
    if [ "$actual" = "$expected" ]; then
        echo "  ✅ $label: $actual"
        ((PASS++))
    else
        echo "  ❌ $label: expected $expected, got $actual"
        ((FAIL++))
    fi
}

assert_claim() {
    local label="$1" claim="$2" url="$3" token="$4"
    local body
    body=$(curl -sf "$url" -H "Authorization: Bearer $token")
    if echo "$body" | grep -q "$claim"; then
        echo "  ✅ $label: found '$claim'"
        ((PASS++))
    else
        echo "  ❌ $label: '$claim' not found in response"
        ((FAIL++))
    fi
}

run_test() {
    local label="$1"
    shift
    echo ""
    echo "🔄 Testing: $label"

    dotnet run --project "$PROJECT_DIR" -p:WarningLevel=0 -- "$@" > /dev/null 2>&1 &
    local pid=$!

    for i in $(seq 1 20); do
        curl -sf "$BASE_URL/me" > /dev/null 2>&1 && break
        sleep 1
    done

    assert_status "/admin (admin)" "200" "$BASE_URL/admin" "$ADMIN_TOKEN"
    assert_status "/admin (reader)" "403" "$BASE_URL/admin" "$READER_TOKEN"
    assert_claim "/roles (admin has Admin)" "Admin" "$BASE_URL/roles" "$ADMIN_TOKEN"

    if echo "$@" | grep -q "CustomTransform"; then
        assert_claim "/me (custom_marker)" "custom_marker" "$BASE_URL/me" "$ADMIN_TOKEN"
    fi

    kill $pid 2>/dev/null || true
    wait $pid 2>/dev/null || true
    sleep 2
}

echo "🧪 Token Introspection E2E Tests"
echo "================================"

run_test "default (introspection + roles)"
run_test "with custom claims transformation" --CustomTransform true

echo ""
echo "================================"
echo "Results: $PASS passed, $FAIL failed"

if [ "$FAIL" -gt 0 ]; then
    exit 1
fi
