# CLAUDE.md

## Repository guidance for Claude or similar coding assistants

This repository is not a generic CRUD sample.

It is a .NET 8 DevExpress/XAF + Blazor system with Telegram ingestion, normalized persistence, worker services, and a custom security approach.

## Core rules

- Preserve the existing architecture unless a redesign is explicitly requested.
- Keep Telegram handlers thin; prefer forwarding updates into services and persistence.
- Treat Telegram as an ingestion/event stream, not only as inline bot commands.
- Preserve compatibility with DevExpress frontend components and XAF security patterns.
- Do not remove DXChat usage just because current public documentation is limited.
- Avoid replacing custom infrastructure with generic alternatives unless the tradeoff is clearly better and explicitly requested.
- Be conservative with sweeping refactors.

## Important projects

- `TacosPortal`: host app, services, Telegram, UI, API
- `Core`: business/domain layer
- `TacosPortalWebassemblyClient`: WASM frontend

## Practical editing guidance

Prefer:
- small targeted changes
- architecture-aware improvements
- comments around non-obvious choices
- preserving config compatibility
- keeping SQL Server compatibility

Avoid:
- introducing a second competing architecture
- bypassing service boundaries
- moving business logic into UI or transport handlers
- deleting "unused" code without checking worker/background scenarios

## Mental model

Telegram -> handler -> service/API layer -> normalized DB model -> workers -> UI/automation

## Documentation to improve first

If asked to improve documentation, prioritize:
- Telegram processing pipeline
- project structure
- required configuration sections
- security model intent
- hosting expectations
