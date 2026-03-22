# User Federation

## LDAP / Active Directory

### Setup

1. User Federation → Add Provider → LDAP
2. Edit mode: `READ_ONLY`, `WRITABLE`, or `UNSYNCED`
3. Vendor: Active Directory, Red Hat Directory Server, or Other

### Connection

| Setting | Example |
|---------|---------|
| Connection URL | `ldaps://ldap.example.com:636` |
| Bind DN | `cn=admin,dc=example,dc=com` |
| Bind credential | LDAP admin password |
| Enable StartTLS | For secure connection on port 389 |

### Search Settings

| Setting | Generic LDAP | Active Directory |
|---------|-------------|-----------------|
| Users DN | `ou=users,dc=example,dc=com` | `ou=users,dc=example,dc=com` |
| Username attribute | `uid` | `sAMAccountName` |
| UUID attribute | `entryUUID` | `objectGUID` |
| User object classes | `inetOrgPerson` | `person, organizationalPerson, user` |

### Sync

- **Full sync**: scheduled periodic synchronization of all users
- **Changed users sync**: incremental sync of modified users
- **Import on login**: fetch user from LDAP on first authentication
- **Sync registrations**: allow Keycloak-created users to write back to LDAP

### LDAP Mappers

| Mapper | Purpose |
|--------|---------|
| User attribute | Map LDAP attributes to Keycloak attributes |
| Full name | Map `cn` to first/last name |
| Group | Import LDAP groups into Keycloak |
| Role | Map LDAP groups to Keycloak roles |

## Custom User Storage (SPI)

For integrating with custom user databases or APIs:

1. Implement `UserStorageProvider` interface (Java)
2. Package as JAR and deploy to Keycloak's `providers/` directory
3. Configure in User Federation

Use cases: legacy databases, external user APIs, custom authentication logic.

## Best Practices

- Use `READ_ONLY` mode unless password writeback is required
- Always use LDAPS or StartTLS — never plain LDAP in production
- Map only required attributes to minimize sync overhead
- Test connection before enabling in production
- Monitor sync errors in server logs
- Plan for LDAP downtime (cached users can still authenticate)
