# Azure Application Gateway

## What is Azure Application Gateway?

**Azure Application Gateway** is a **Layer 7 (Application Layer) load balancer** that manages **HTTP and HTTPS** traffic for web applications.

Instead of users connecting directly to your web servers or applications, they connect to the **Application Gateway** first. The gateway then analyzes each request and forwards it to the appropriate backend service.

Think of it as a **smart traffic controller** for web applications.

---

## Why Do We Need an Application Gateway?

Without an Application Gateway:

```text
                 Internet
        ┌──────────┼──────────┐
        ▼          ▼          ▼
     Web App 1  Web App 2  Web App 3
```

Problems:

- Every application is exposed directly to the internet.
- Each application must manage its own SSL certificates.
- No centralized security.
- No intelligent routing.
- Difficult to scale and manage.

---

With an Application Gateway:

```text
                  Internet
                      │
                      ▼
        Azure Application Gateway
      (SSL + WAF + Routing + Load Balancing)
                      │
      ┌───────────────┼───────────────┐
      ▼               ▼               ▼
   Web App 1       Web App 2       Web App 3
```

Benefits:

- Only the Application Gateway is exposed to the internet.
- Backend applications remain protected.
- Centralized SSL certificate management.
- Intelligent request routing.
- Built-in Web Application Firewall (WAF).
- Improved scalability and availability.

---

# How It Works

When a user sends a request:

```text
User
 │
 ▼
https://example.com/products
 │
 ▼
Azure Application Gateway
 │
 ├── Decrypt HTTPS
 ├── Apply WAF Rules
 ├── Check Backend Health
 ├── Determine Routing Rule
 ▼
Products Web Application
 │
 ▼
Response
 │
 ▼
User
```

The Application Gateway performs several tasks before forwarding the request.

---

# Key Features

## 1. Load Balancing

Distributes incoming requests across multiple backend servers.

Example:

```text
                 Application Gateway
                  /      |      \
                 /       |       \
             Server1  Server2  Server3
```

If one server becomes unavailable, traffic is automatically directed to the remaining healthy servers.

---

## 2. SSL/TLS Termination

Normally, every backend server must decrypt HTTPS requests.

Without SSL termination:

```text
Client
   │ HTTPS
   ▼
Server 1

Client
   │ HTTPS
   ▼
Server 2
```

Each server performs SSL decryption.

With Application Gateway:

```text
Client
   │ HTTPS
   ▼
Application Gateway
   │ HTTP or HTTPS
   ▼
Backend Servers
```

The gateway handles SSL/TLS decryption, reducing processing on backend servers and simplifying certificate management.

---

## 3. URL Path-Based Routing

Routes requests based on the URL path.

Example:

```text
Request:
https://shop.contoso.com/products

Application Gateway
        │
        ▼
Products Service
```

Routing rules:

| URL | Backend |
|------|---------|
| `/products/*` | Products Service |
| `/orders/*` | Orders Service |
| `/users/*` | User Service |

One public endpoint can serve multiple applications.

---

## 4. Host-Based Routing

Routes traffic based on the hostname.

Example:

| Hostname | Backend |
|----------|---------|
| `shop.contoso.com` | Shopping Application |
| `admin.contoso.com` | Admin Portal |
| `api.contoso.com` | API Service |

All applications can share the same Application Gateway while using different domains or subdomains.

---

## 5. Web Application Firewall (WAF)

The WAF inspects incoming web requests before they reach your application.

It helps protect against common web attacks, including:

- SQL Injection (SQLi)
- Cross-Site Scripting (XSS)
- Command Injection
- OWASP Top 10 vulnerabilities

Example:

```text
Attacker
    │
Malicious Request
    │
    ▼
Application Gateway (WAF)
    │
 Request Blocked
```

The malicious request never reaches the backend application.

---

## 6. Health Probes

The gateway continuously checks whether backend servers are healthy.

Example:

```text
Application Gateway
      │
      ├── Server A ✓ Healthy
      ├── Server B ✗ Unhealthy
      └── Server C ✓ Healthy
```

Traffic is sent only to healthy servers.

---

## 7. Session Affinity

Some applications require a user's requests to always reach the same backend server.

Application Gateway can maintain this behavior using session affinity.

Example:

```text
User A
   │
   ▼
Server 1

Next Request
   │
   ▼
Server 1
```

This is useful for applications that store temporary session data locally.

---