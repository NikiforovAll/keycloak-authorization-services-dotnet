# High Availability & Scalability

## Clustering

### Requirements

- Shared database (PostgreSQL recommended)
- Distributed cache (Infinispan, built-in)
- Load balancer (HAProxy, NGINX, AWS ALB)
- Consistent configuration across all nodes

### Load Balancer Configuration

- **Sticky sessions**: recommended for performance
- **Health checks**: `/health/ready` endpoint
- **SSL termination**: at load balancer or Keycloak
- **Connection timeouts**: 60+ seconds for admin operations

## Database Performance

### Connection Pool

```properties
KC_DB_POOL_INITIAL_SIZE=10
KC_DB_POOL_MIN_SIZE=10
KC_DB_POOL_MAX_SIZE=50
```

### Optimization

- Index on `username`, `email` columns
- Regular vacuum (PostgreSQL)
- Monitor slow queries
- Database replication for read scaling

## Caching

| Cache | Content |
|-------|---------|
| Realm | Realm configuration |
| User | User data from federation sources |
| Keys | Signing and encryption keys |
| Authorization | Permissions and policies |

Configure: max entries, lifespan, eviction policy (LRU). In clustered mode, caches use distributed invalidation.

## Monitoring

### Health Endpoints

```
GET /health/live     — liveness probe (is Keycloak running)
GET /health/ready    — readiness probe (ready to serve)
GET /metrics         — Prometheus metrics
```

### Key Metrics

- Active sessions count
- Token issuance rate
- Login success/failure rate
- Database connection pool usage
- Cache hit/miss ratio
- JVM memory and GC pressure

### Alerting Triggers

- High failed login rate
- Database connection pool exhaustion
- High JVM memory usage
- Increased response times
- SSL certificate expiration

## Backup & Disaster Recovery

- **Database**: daily automated backups, point-in-time recovery, test restores regularly
- **Configuration**: export realm configs (`bin/kc.sh export`), store in version control
- **Keys**: back up realm signing keys separately (needed for token validation continuity)
- **Recovery plan**: document restore steps, target RTO/RPO, test failover procedures
