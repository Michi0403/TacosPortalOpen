# TacosPortal
I let chatgpt overwork my readme and overworked it again, this is the result.
## Overview

TacosPortal is a prototype framework built with **.NET 8**, **DevExpress
XAF WebAPI**, and **Blazor (WASM + Server Interactive)**.

The project demonstrates how to build a secure, reusable system that
combines:

-   DevExpress XAF WebAPI backend
-   Blazor frontend (WASM + Server Interactive)
-   Telegram bot integration
-   claims-based security compatible with the DevExpress Security System
-   installable WebAssembly frontend
-   Windows and Linux hosting
-   optional WinUI host

The repository represents **many thousands of hours of work** and is
shared mainly as a **learning and reference implementation**.

It is incomplete in some areas but fully functional in others and
demonstrates how the different parts of the system connect.

------------------------------------------------------------------------

# Project Structure

The repository is organized into several main projects.

**TacosPortal**\
ASP.NET Core application that hosts:

-   Blazor Server Interactive frontend
-   backend services
-   Telegram bot integration
-   DevExpress WebAPI

**Core**\
Contains:

-   business classes
-   shared core methods
-   domain model

**TacosPortalWebassemblyClient**\
The WebAssembly Interactive client used for the installable frontend.

Together these components form what could be described as a
**Blazor WASM and ASP.NET-Core-based monolithic system including a Winforms like Wrappersystem**.

In germany we would say "Zhis iz Zhe EgglayingWhoolMilkPig" for dotnet in prototyped and ugly.

It behaves like a monolith from a development perspective, but it can
still be:

-   hosted multiple times
-   containerized
-   connected to multiple Telegram bots
-   connected to multiple message streams

For example multiple bots could feed updates into the same database.

------------------------------------------------------------------------

# Requirements

-   .NET SDK **8.0**
-   Visual Studio **2022 or later**
-   DevExpress **Blazor + WebAPI 24.2** (trial or licensed)

DevExpress components are required for the frontend.

You can try them using the trial packages or through NuGet.

Demo examples: https://demos.devexpress.com/blazor/

------------------------------------------------------------------------

# DevExpress Components

The frontend relies heavily on DevExpress components.

Especially:

-   Grid
-   nested grids
-   detail views
-   security integration

Example nested grid:
https://demos.devexpress.com/blazor/Grid/MasterDetail/NestedGrid

Example pages from this repository:

Chat administrators:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Components/Pages/Telegram/ChatAdministrators.razor

Permission administration:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Components/Pages/Admin/PermissionPolicyTypePemissiobObjectAdministration.razor

------------------------------------------------------------------------

# DXChat

DevExpress removed **DXChat** from their documentation even though the
component still exists and still works.

This project still uses it.

Example implementation:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Components/Pages/AllChatsMessages.razor

DevExpress now refers developers to the **DevExtreme Chat component**:

https://js.devexpress.com/DevExtreme/Guide/UI_Components/Chat/Overview/

Conceptually it is compatible.

------------------------------------------------------------------------

# Security System

The project implements a **claims-based security model** similar to
Microsoft Identity but integrated with the **DevExpress Security
System**.

Relevant documentation:

XAF WebAPI:
https://docs.devexpress.com/eXpressAppFramework/403394/backend-web-api-service

Security system:
https://docs.devexpress.com/eXpressAppFramework/113366/data-security-and-safety/security-system

The goal was to create a security system that works across:

-   REST APIs
-   OData
-   background services
-   frontends

------------------------------------------------------------------------

# Configuration

The application requires an **appsettings.json** configuration file.

A typical setup is:

appsettings.json\
appsettings.Development.json

The repository contains anonymized examples.

Many configuration sections exist because the system evolved over time,
so some settings are currently unused.

------------------------------------------------------------------------

# Telegram Bot Integration

The project integrates a full Telegram bot system using:

https://github.com/TelegramBots/Telegram.Bot

Telegram documentation: https://core.telegram.org/bots/tutorial

## Creating a Bot

1.  Use **BotFather** in Telegram
2.  create a bot
3.  obtain the bot token
4.  add the bot to a chat or group
5.  make the bot administrator

Never share your token publicly.

------------------------------------------------------------------------

# Telegram Configuration

Example configuration:

``` json
"BotConfigurationCore": {
  "BotToken": "REPLACE_WITH_BOT_TOKEN",
  "BotName": "Example Bot",
  "HostAddress": "https://your-app.example.com/",
  "Route": "/bot",
  "SecretToken": "REPLACE_WITH_SECRET_TOKEN",
  "ChatId": "REPLACE_WITH_CHAT_ID"
}
```

Only **BotToken** and **BotName** are required, rest obsolet garbage.

------------------------------------------------------------------------

# Telegram Processing Architecture

Most Telegram bot implementations process updates immediately in the
update handler.

This system intentionally separates **receiving updates** from
**processing updates**.

The goal was to create a system that is:

-   scalable
-   queryable
-   compatible with DevExpress data systems
-   usable by background services and dashboards

## Update Pipeline

Incoming Telegram updates follow this flow:

    Telegram
        ↓
    UpdateHandler
        ↓
    TacosPortalApiService
        ↓
    Database (normalized update model)
        ↓
    Background Services / Workers
        ↓
    Frontend notifications / automation

### 1. UpdateHandler

The bot receives updates through Telegram and forwards them into the
system:

``` csharp
_ = tacosApi.NewUpdate(update).ConfigureAwait(false);
```

Handler implementation:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Services/Telegram/UpdateHandler.cs

The handler does **not** perform heavy logic.

Its job is only to forward updates.

------------------------------------------------------------------------

### 2. Internal API Service

The update is processed through an internal service layer:

https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Services/Telegram/TacosPortalApiService.cs

This service:

-   parses the update
-   maps the update into database entities
-   normalizes nested Telegram structures

------------------------------------------------------------------------

### 3. Database Normalization

Telegram updates contain many nested objects such as:

-   messages
-   chats
-   users
-   attachments
-   reactions
-   commands

The project maps these structures into relational entities.

This required reverse engineering the effective data structure of
Telegram updates.

The advantage is that updates become:

-   queryable via OData
-   accessible through DevExpress grids
-   usable for analytics or automation

------------------------------------------------------------------------

### 4. Worker Services

Background services process updates asynchronously.

Example:

https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Services/Telegram/TelegramWorkerService.cs

Workers can:

-   update chat metadata
-   ignore unreachable chats
-   trigger automation logic
-   send updates to frontends
-   integrate with SignalR hubs

This design allows the bot to behave more like an **event ingestion
system** rather than a simple request-response bot.

------------------------------------------------------------------------

# Example Telegram UI

Example frontend for sending Telegram messages:

Component:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Components/Pages/AllChatsMessages.razor

Styling:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Components/Pages/AllChatsMessages.razor.css

------------------------------------------------------------------------

# Database

Tested with:

-   Microsoft SQL Server on Windows (2019-2022)
-   Microsoft SQL Server on Linux (2019-2022)

Example connection configuration:

``` json
  "ConnectionStringsCore": {
    "ConnectionString": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;",
    "EasyTestConnectionString": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;",
    "DefaultConnection": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;"
  },
```

Other EF providers may work but are untested.
(To help is a bit sqlite untested implemented)

------------------------------------------------------------------------

# Database Initialization

Run:

    dotnet ef database update

If the data model changes:

    dotnet ef migrations add MigrationName
    dotnet ef database update

Example users created by the database updater include:

(Add there your own before compiling all and increase the version number always.)

-   User
-   Admin

Updater example:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/DatabaseUpdate/Updater.cs

------------------------------------------------------------------------

# VAPID (Push Notifications)

Push notifications require VAPID keys.

Example configuration:

``` json
"Vapid": {
  "PublicKey": "REPLACE_WITH_VAPID_PUBLIC_KEY",
  "PrivateKey": "REPLACE_WITH_VAPID_PRIVATE_KEY",
  "Subject": "mailto:admin@example.com"
}
```

Keys must match the hosting domain.

------------------------------------------------------------------------

# Logging

The project contains a custom logging system and most not frontend relevant services are written relatively from scratch (I hate to use libraries).

The reason is that ASP.NET logging configuration has limitations when
trying to control log levels dynamically 
(in the appsettings.json because microsoft has a word in it using the default LogLevel class).

Therefore a separate configuration section called **LoggingCore**
exists.

Email logging is included but not fully tested, I can also say from production systems of mine that it doesn't work on smartphones.
I usually totally rely on debugging debug and production + filelogging on my servers. I feel kinda sorry for that.

It's totally reasonable to kick all logging out and replace it with your own solution or logging.net.

------------------------------------------------------------------------

# Hosting

The system can be hosted on:

-   Windows Server
-   Linux Server
-   (Mac us untested, but all general dotnet so it could work)

Typical setup:

-   ASP.NET backend
-   hosted WASM frontend
-   HTTPS with Let's Encrypt

The WASM frontend can run on any modern browser.

The general monolithic startup is done in the program.cs and startup.cs.
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Program.cs
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortal/Startup.cs

Under WinUI Host:
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortalWindows/App.xaml.cs
https://github.com/Michi0403/TacosPortalOpen/blob/main/TacosPortalWindows/MainWindow.xaml.cs
------------------------------------------------------------------------

# WinUI Host

The repository also contains a **WinUI host example**.

The idea is to demonstrate how a Blazor application can replace
traditional desktop applications such as WinForms while still using the
same backend and authentication system.

This host runs only on Windows.

The package project is also included but you can also debug the WinUI host directly.

------------------------------------------------------------------------

# Why This Project Exists

This repository represents a personal prototype framework developed over
several years and I seek always for usecases to continue developing my mainframe.

I started this with a certain telegram group project but at end,
 would have needed to manage + code all my own and lost all fun to use telegram beside of that.

I started on Dotnet-Framework and threw all over board many times.

The goal was to demonstrate that modern .NET technologies can replace
traditional desktop and mobile architectures.

Instead of:

-   WinForms
-   native mobile apps
-   platform specific stores

applications can run as:

-   installable WebAssembly frontends
-   browser-based applications
-   secure backend services

This system attempts to show how such an architecture could look in
practice.
It's not good nor perfect but it shows most parts Web3 BlazorWASM Ecosystems need to provide 
and can (also how they can work DIRECTLY CONNECTED together in a practical example)

Also I wanted to use NO MODELS and DTO's it drove me insane and reuse the BusinessObjects really anywhere, to exchange data, to display, whatever.
------------------------------------------------------------------------

# Unrelated Projects of mine

LocalGPT\
https://github.com/Michi0403/LocalGPT

OpenMorph.NET\
https://github.com/Michi0403/OpenMorph.NET

Older projects\
https://bitbucket.org/da-legendary-Michi/legendary_repository/
