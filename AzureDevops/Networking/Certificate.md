# SSL/TLS Certificates for Webpages

## What is a Certificate?

A **digital certificate** (commonly called as **SSL/TLS certificate**) is a security credential that verifies the identity of a website and enables encrypted communication between a user's browser and the web server.

Example:

- `https://example.com` → Secure (uses an SSL/TLS certificate)
- `http://example.com` → Not secure (no encryption)

---

# Purpose of a Certificate

A certificate serves several important process:

## 1. Authentication (Identity Verification)

The certificate proves that the website actually belongs to the organization or individual who owns the domain.

Example:

When users visit:

```
https://www.example.com
```

the browser verifies that the certificate was issued for **example.com**.

Without this verification, attackers could impersonate the website.

---

## 2. Encryption

Cerificates enable HTTPS, which encrypts all communication between:

- User's Browser
- Web Server

Encrypted data cannot be easily read by hackers.

Example of protected data:

- Passwords
- Credit card information
- Personal details
- Login tokens

---

## 3. Data Integrity 

Certificates ensure that the transmitted data is not modified during transmission.

If someone tries to alter the data, the browser detects the tampering.

---

## 4. Trust

Browsers display:

- 🔒 Lock icon
- HTTPS

These indicate that the connection is secure.

Without a valid certificate, browsers display warnings such as:

```
Your connection is not private
```

or

```
Certificate is not trusted
```

---

# Why Certificates Are Needed

Certificates protect websites from:

- Man-in-the-Middle (MITM) attacks
- Data theft
- Password interception
- Identity spoofing
- Session hijacking

Without HTTPS, attackers on the same network can read transmitted information.

---

# Certificate Validation

When a browser connects to a website, it performs several validation checks.

## 1. Domain Validation

The browser checks whether:

```
Certificate Domain
        ==
Requested Domain
```

Example:

Certificate:

```
example.com
```

User visits:

```
example.com
```

✅ Valid

User visits:

```
myexample.com
```

❌ Invalid

---

## 2. Expiration Check

Every certificate has:

- Issue Date
- Expiration Date

Example:

```
Valid From:
01 Jan 2025

Valid Until:
01 Jan 2026
```

If expired:

```
Certificate Expired
```

The browser blocks or warns the user.

---

## 3. Certificate Authority (CA) Verification

The browser verifies whether the certificate was issued by a trusted Certificate Authority (CA).

Common Certificate Authorities:

- Let's Encrypt
- DigiCert
- GlobalSign
- Sectigo

If the issuer is unknown:

```
Certificate Not Trusted
```

---

## 4. Digital Signature Verification

Certificates are digitally signed by the issuing Certificate Authority.

The browser verifies this signature to ensure the certificate has not been altered.

---

## 5. Revocation Check

The browser checks whether the certificate has been revoked due to:

- Private key compromise
- Fraud
- Security breach

If revoked:

```
Certificate Revoked
```

The browser rejects the connection.

---