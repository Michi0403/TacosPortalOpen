# Architecture for AI Contributors

## Short version

TacosPortal is a DevExpress/XAF-oriented .NET 8 system where Telegram updates are ingested, normalized, stored, and then processed by internal services and workers.

## Key idea

The important architectural distinction is this:

**Telegram is an input stream into the application, not only a bot command surface.**

That means the project is designed to:
- persist updates
- preserve relational structure
- expose information through DevExpress/XAF-friendly patterns
- process work asynchronously
- support frontends, dashboards, and automation

## Why the model looks heavier than a typical bot

A simple Telegram bot often handles updates inline and sends a direct response.

This project intentionally goes further:

- updates are forwarded into an internal service layer
- Telegram data is mapped into a relational model
- worker services perform later processing
- frontend layers can consume the resulting state

This supports a more ERP-like or platform-style system.

## What to avoid misunderstanding

Avoid assuming that unusual code is accidental.

Several patterns exist because the project tries to combine:
- DevExpress/XAF
- Blazor
- Telegram
- reusable security
- multi-host deployment
- browser/WASM-first delivery

## If you are changing code

Ask yourself:
- Does this preserve the ingestion pipeline?
- Does this keep DevExpress/XAF compatibility?
- Does this move logic into the wrong layer?
- Does this break future worker/background processing?
- Does this reduce the ability to query or reuse persisted Telegram data?

If yes, rethink the change.
