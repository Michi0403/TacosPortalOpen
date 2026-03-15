# GitHub Copilot Instructions for TacosPortal

## What this repository is

TacosPortal is a .NET 8 prototype framework built around DevExpress XAF WebAPI, Blazor, and Telegram integration.

It is designed as a reusable, security-aware system with:

- ASP.NET Core host application
- Blazor Server Interactive UI
- Blazor WebAssembly client
- Telegram update ingestion
- normalized database persistence
- worker/background processing
- DevExpress/XAF security integration

## Coding expectations

When suggesting code:

- preserve the existing architecture
- keep transport handlers thin
- prefer service-layer processing
- preserve DevExpress/XAF compatibility
- avoid broad refactors unless explicitly requested
- preserve `appsettings.json` compatibility
- assume SQL Server is the primary database target

## Telegram-specific expectations

This repository does not treat Telegram as a simple inline command bot.

Telegram updates are intended to be:

- received
- forwarded into the system
- parsed and normalized
- stored in relational form
- processed asynchronously by workers/services

Suggestions should support that model.

## UI/component expectations

The project intentionally uses DevExpress components.
Do not suggest replacing DevExpress UI with unrelated component libraries unless explicitly asked.

`DXChat` may still be used even if current public documentation is sparse.

## Security expectations

The security model is custom and aligned with DevExpress/XAF security patterns.
Do not assume a plain ASP.NET Identity sample architecture.

## Style expectations for suggestions

Prefer:
- explicit naming
- async-safe code
- minimal breakage
- comments for unusual behavior
- targeted improvements

Avoid:
- unnecessary abstraction
- cosmetic churn
- deleting apparently unused infrastructure without investigation
