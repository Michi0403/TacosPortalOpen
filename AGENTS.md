# AGENTS.md

## Purpose

This repository contains **TacosPortal**, a reusable .NET 8 prototype framework built around:

- DevExpress XAF WebAPI
- Blazor Server Interactive
- Blazor WebAssembly Interactive
- Telegram bot ingestion and processing
- claims-based security integrated with the DevExpress Security System
- SQL-backed persistence for normalized Telegram update data

This file is intended for AI coding agents and contributors so they can work with the repository without fighting its architecture.

---

## High-level structure

### `TacosPortal`
Main ASP.NET Core host application. This project contains:

- Blazor Server Interactive frontend
- backend services
- Telegram integration
- worker services
- DevExpress/XAF-related application wiring
- API and UI entry points

### `Core`
Shared business/domain layer. This project contains:

- business classes
- shared methods
- common model logic
- reusable core abstractions

### `TacosPortalWebassemblyClient`
WebAssembly interactive client used for the installable/browser frontend.

---

## Architectural intent

This system should be understood as a **WebAssembly-based monolithic system** with strong internal separation of responsibilities.

It behaves like a monolith from a development perspective, but it was built so it can still be:

- hosted multiple times
- deployed on Windows or Linux
- containerized
- connected to multiple Telegram bots
- connected to one or more shared databases or event flows

A useful mental model is:

**Telegram is treated as an event ingestion stream, not just a request-response bot.**

Incoming Telegram updates are intentionally captured, normalized, stored, and then processed by other layers.

---

## Telegram processing model

Preferred update flow:

```text
Telegram
  -> UpdateHandler
  -> internal service/API layer
  -> normalized database model
  -> worker/background services
  -> frontend updates / automation / analytics
```

Important principle:

- do **not** move complex business logic into the Telegram update handler
- keep the handler thin
- forward updates into the internal processing pipeline
- prefer persistence + later processing over brittle inline handling

This repository intentionally stores and maps Telegram structures into relational entities so they can be:

- queried through OData / APIs
- displayed in DevExpress grids
- processed asynchronously
- reused by background services
- audited and analyzed later

---

## Security model

The security system is intentionally closer to a **claims-based Microsoft Identity style** model, but integrated into the **DevExpress Security System** and XAF WebAPI world.

AI contributors should preserve these goals:

- security should work consistently across REST, OData, background services, and UI
- do not introduce shortcuts that bypass the DevExpress/XAF security model
- do not assume the project is a generic ASP.NET Core identity sample
- prefer compatibility with existing claims / permission / role flows

---

## DevExpress guidance

The project is intentionally optimized for **DevExpress frontend components**.

Important implications:

- prefer solutions that work cleanly with DevExpress grids, editors, and security integration
- do not replace DevExpress usage with unrelated OSS UI stacks unless explicitly requested
- preserve bindings, object lifecycles, and patterns that support DevExpress/XAF usage
- if a feature looks unusual, check whether it exists to support DevExpress interoperability

Special note:

- `DXChat` may no longer be documented publicly, but it still exists and is intentionally used in this repository
- do not "clean it up" by removing it just because current docs are sparse

---

## Configuration expectations

Assume the application needs `appsettings.json`-based configuration.

Common important sections include:

- `ConnectionStringsCore`
- `BotConfigurationCore`
- `ServiceConfigurationCore`
- `Vapid`
- `LoggingCore`

Rules for changes:

- do not silently rename config sections
- do not remove legacy-looking config without verifying it is truly unused
- prefer additive changes over breaking changes
- preserve compatibility with `appsettings.Development.json`

---

## Database and persistence guidance

The project is primarily expected to work with **Microsoft SQL Server**.

Additional EF Core providers may be technically possible, but are not the primary target.

Agent guidance:

- do not casually introduce provider-specific behavior for a different database
- preserve SQL Server compatibility unless asked otherwise
- when changing the domain model, note that EF migrations may be required

---

## Worker / async processing guidance

Background services are a first-class part of the architecture.

Suitable responsibilities for workers include:

- chat synchronization
- Telegram follow-up processing
- notification fan-out
- hub/frontend event propagation
- deferred processing of already persisted updates

Unsuitable responsibilities:

- UI-only logic
- highly coupled inline handler logic
- duplicating parsing logic already performed elsewhere

---

## How AI should modify code here

When implementing or editing features:

1. Keep the existing architecture unless explicitly asked to redesign it.
2. Prefer extending the current service boundaries over bypassing them.
3. Keep Telegram update ingestion thin and durable.
4. Preserve DevExpress/XAF compatibility.
5. Avoid framework churn for cosmetic reasons.
6. Do not replace custom infrastructure just because a generic library exists.
7. Be conservative with renames, folder moves, and mass refactors.

---

## Good contribution patterns

Good changes include:

- documenting hidden architecture decisions
- improving naming while preserving intent
- making config clearer without breaking compatibility
- tightening null handling and async handling
- improving worker/service separation
- clarifying how Telegram entities map into persistence
- improving comments around non-obvious DevExpress behavior

Risky changes include:

- flattening the architecture into a "simple bot"
- replacing DevExpress patterns with generic alternatives
- moving business logic into controllers or handlers just because it looks shorter
- deleting code that appears unused without understanding background/conditional flows
- changing auth/security assumptions casually

---

## Recommended first-read areas for an AI agent

Start here to understand the project:

- Telegram update handler
- internal Telegram API/service layer
- worker services
- database updater / seed logic
- chat/message UI pages
- security-related code paths
- main app startup / service registration

If a behavior seems unusual, assume it may be intentional and related to:

- DevExpress/XAF interoperability
- hosting portability
- Telegram normalization
- multi-frontend compatibility
- custom security flow

---

## Tone and expectations

This repository is a serious prototype and reference system, not a toy sample.

It may contain rough edges, but those rough edges often come from solving real cross-cutting integration problems.

AI agents should optimize for:

- preserving intent
- reducing accidental breakage
- documenting architecture
- making the system easier to understand without diluting what it is
