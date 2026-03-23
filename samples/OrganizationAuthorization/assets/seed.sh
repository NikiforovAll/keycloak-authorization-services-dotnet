#!/usr/bin/env bash
set -euo pipefail

KC_URL="${KC_URL:-http://localhost:8080}"
REALM="Test"
ADMIN_USER="admin"
ADMIN_PASS="admin"

echo "⏳ Waiting for Keycloak at $KC_URL ..."
until curl -sf "$KC_URL/realms/master" > /dev/null 2>&1; do
  sleep 2
done
echo "✅ Keycloak is ready"

# Get admin token
TOKEN=$(curl -sf -X POST "$KC_URL/realms/master/protocol/openid-connect/token" \
  -d "client_id=admin-cli" \
  -d "username=$ADMIN_USER" \
  -d "password=$ADMIN_PASS" \
  -d "grant_type=password" | jq -r '.access_token')

AUTH="Authorization: Bearer $TOKEN"

# Add 'organization' scope as default scope for test-client
echo "🔧 Adding 'organization' scope to test-client..."
ORG_SCOPE_ID=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/client-scopes" | jq -r '.[] | select(.name=="organization") | .id')
CLIENT_UUID=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/clients?clientId=test-client" | jq -r '.[0].id')

if [ -n "$ORG_SCOPE_ID" ] && [ "$ORG_SCOPE_ID" != "null" ]; then
  curl -sf -X PUT "$KC_URL/admin/realms/$REALM/clients/$CLIENT_UUID/default-client-scopes/$ORG_SCOPE_ID" \
    -H "$AUTH" || true
  echo "   ✅ organization scope added to test-client"
else
  echo "   ⚠️  organization scope not found (orgs may not be enabled)"
fi

# Get test user ID
USER_ID=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/users?username=test&exact=true" | jq -r '.[0].id')
echo "📋 Test user ID: $USER_ID"

create_org() {
  local alias=$1
  local name=$2

  echo "🏢 Creating organization: $alias" >&2
  curl -sf -X POST "$KC_URL/admin/realms/$REALM/organizations" \
    -H "$AUTH" \
    -H "Content-Type: application/json" \
    -d "{\"name\": \"$name\", \"alias\": \"$alias\", \"enabled\": true, \"domains\": []}" \
    -o /dev/null -w "" || true

  local org_id
  org_id=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/organizations" | jq -r ".[] | select(.alias==\"$alias\") | .id")
  echo "   ID: $org_id" >&2
  echo "$org_id"
}

add_member() {
  local org_id=$1
  local user_id=$2

  echo "   ➕ Adding member to org $org_id" >&2
  curl -sf -X POST "$KC_URL/admin/realms/$REALM/organizations/$org_id/members" \
    -H "$AUTH" \
    -H "Content-Type: application/json" \
    -d "\"$user_id\"" \
    -o /dev/null -w "" || true
}

# Create organizations
ACME_ID=$(create_org "acme-corp" "Acme Corporation")
PARTNER_ID=$(create_org "partner-inc" "Partner Inc")
STARTUP_ID=$(create_org "startup-co" "Startup Co")

# Assign test user to acme-corp and startup-co (NOT partner-inc)
add_member "$ACME_ID" "$USER_ID"
add_member "$STARTUP_ID" "$USER_ID"

echo ""
echo "🎉 Seed complete!"
echo "   User 'test' is a member of: acme-corp, startup-co"
echo "   User 'test' is NOT a member of: partner-inc"
echo ""
echo "Run ./assets/get-token.sh to get a bearer token"
