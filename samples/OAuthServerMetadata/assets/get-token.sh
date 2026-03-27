#!/usr/bin/env bash
set -euo pipefail

KC_URL="${KC_URL:-http://localhost:8080}"
REALM="Test"
CLIENT_ID="test-client"
USERNAME="${1:-test}"
PASSWORD="${2:-test}"

RESPONSE=$(curl -sf -X POST "$KC_URL/realms/$REALM/protocol/openid-connect/token" \
  -d "client_id=$CLIENT_ID" \
  -d "username=$USERNAME" \
  -d "password=$PASSWORD" \
  -d "grant_type=password" \
  -d "scope=openid")

TOKEN=$(echo "$RESPONSE" | jq -r '.access_token')

echo "TOKEN=$TOKEN"
echo ""
echo "# Decoded payload:"
echo "$TOKEN" | cut -d'.' -f2 | base64 -d 2>/dev/null | jq . 2>/dev/null || true
