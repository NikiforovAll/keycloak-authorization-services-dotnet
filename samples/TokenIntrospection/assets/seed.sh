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

# Role mappers live on the 'roles' client scope
ROLES_SCOPE_ID=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/client-scopes" | jq -r '.[] | select(.name=="roles") | .id')
echo "📋 'roles' client scope ID: $ROLES_SCOPE_ID"

MAPPERS=$(curl -sf -H "$AUTH" "$KC_URL/admin/realms/$REALM/client-scopes/$ROLES_SCOPE_ID/protocol-mappers/models")

# For each role mapper: keep introspection.token.claim=true but set access.token.claim=false
# This simulates lightweight access tokens — roles are stripped from the JWT but available via introspection.
disable_access_token_claim() {
  local mapper_name=$1
  local mapper=$(echo "$MAPPERS" | jq --arg name "$mapper_name" '.[] | select(.name==$name)')
  local mapper_id=$(echo "$mapper" | jq -r '.id')

  if [ -z "$mapper_id" ] || [ "$mapper_id" = "null" ]; then
    echo "   ⚠️  Mapper '$mapper_name' not found"
    return
  fi

  # Update: access.token.claim=false, introspection.token.claim=true
  local updated=$(echo "$mapper" | jq '.config["access.token.claim"] = "false" | .config["introspection.token.claim"] = "true"')

  echo "   🔒 '$mapper_name': access.token.claim=false, introspection.token.claim=true"
  curl -sf -X PUT "$KC_URL/admin/realms/$REALM/client-scopes/$ROLES_SCOPE_ID/protocol-mappers/models/$mapper_id" \
    -H "$AUTH" \
    -H "Content-Type: application/json" \
    -d "$updated"
}

echo "🔧 Configuring lightweight access tokens (roles excluded from JWT, available via introspection)..."
disable_access_token_claim "realm roles"
disable_access_token_claim "client roles"

echo ""
echo "🎉 Seed complete!"
echo "   Tokens now lack realm_access/resource_access claims (lightweight)."
echo "   Token introspection will resolve the full claim set."
echo ""
echo "Run ./assets/get-token.sh to get a bearer token"
