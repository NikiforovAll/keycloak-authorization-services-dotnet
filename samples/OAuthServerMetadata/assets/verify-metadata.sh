#!/usr/bin/env bash
set -euo pipefail

KC_URL="${KC_URL:-http://localhost:8080}"
REALM="Test"

echo "--- OIDC Discovery ---"
echo "GET $KC_URL/realms/$REALM/.well-known/openid-configuration"
curl -sf "$KC_URL/realms/$REALM/.well-known/openid-configuration" | jq '{issuer, authorization_endpoint, token_endpoint, jwks_uri}'
echo ""

echo "--- RFC 8414 OAuth Authorization Server Metadata ---"
echo "GET $KC_URL/realms/$REALM/.well-known/oauth-authorization-server"
curl -sf "$KC_URL/realms/$REALM/.well-known/oauth-authorization-server" | jq '{issuer, authorization_endpoint, token_endpoint, jwks_uri}' \
  && echo "" \
  || echo "⚠️  RFC 8414 endpoint not available (requires Keycloak 26.4+)"
