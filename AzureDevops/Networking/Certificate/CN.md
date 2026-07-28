# Common Name (CN) - Brief Overview

## What is Common Name (CN)?

The **Common Name (CN)** is the primary domain or hostname that a digital certificate is issued for. It determines the website or server that the certificate protects.

**Examples:**
- `www.example.com`
- `api.example.com`
- `portal.company.com`

> **Note:** The CN should contain only the hostname, **not** `https://`, ports, or paths.

---

## Why is CN needed?

The CN is used to:

- Identify the website or server the certificate belongs to.
- Allow browsers and clients to verify they are connecting to the correct website.
- Help establish a secure HTTPS connection.
- Prevent certificate name mismatch errors.

---

## How Does It Work?

```text
User visits: https://www.example.com
                 │
                 ▼
Server sends SSL/TLS Certificate
                 │
                 ▼
Certificate Common Name (CN):
www.example.com
                 │
                 ▼
Browser compares:
Requested URL == Certificate CN ?
        │
   Yes ─┴─► Secure Connection ✅
   No  ───► Certificate Name Mismatch ❌
```

---

## Valid CN Examples

| Valid | Reason |
|--------|--------|
| `www.example.com` | Valid hostname |
| `example.com` | Valid root domain |
| `api.example.com` | Valid subdomain |

---

## Invalid CN Examples

| Invalid CN | Reason |
|------------|--------|
| `https://www.example.com` | Contains protocol |
| `www.example.com/login` | Contains path |
| `www.example.com:443` | Contains port |
| `example.com?user=1` | Contains query string |

---

## CN vs SAN

| Common Name (CN) | Subject Alternative Name (SAN) |
|------------------|--------------------------------|
| Primary domain name | Additional domain names |
| One hostname | Multiple hostnames supported |
| Modern browsers primarily validate SAN | Recommended for all new certificates |

**Example:**

- **CN:** `www.example.com`
- **SANs:**
  - `www.example.com`
  - `example.com`
  - `api.example.com`

---

## Key Points 

- **CN (Common Name)** is the primary domain or hostname for the certificate.
- It must be a **valid hostname**, not a full URL.
- It helps browsers verify they are connecting to the intended website.
- If the CN (or SAN) doesn't match the website's hostname, users may see certificate warnings.
- Modern SSL/TLS certificates rely mainly on **Subject Alternative Names (SANs)**, while the CN is kept for compatibility.