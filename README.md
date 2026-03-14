# TacosPortal

# Setup
I mention that part in appsettings but, the appsettings and subservice settings are not optional.

There are many general things and I rather refer to the official documentation, there is a full appsettings.json example just anonymized.
Most sub settings are actually unused so it's messy.

1. Botfather in Telegram, you need a bot-token for the TelegramBot.Net (rest is kinda optional) https://github.com/TelegramBots/Telegram.Bot

this example of my framework is totally around the telegram implementation.
In mapping over many monthes I tried to translate any bot result and message content to it's referential structure and embedded it, in dx xaf webapi style, to it.

https://core.telegram.org/bots/tutorial

to make it short, create a bot in telegram and make it admin in a room of you, never share your secret but with your appsettings.json.
Better make two for development and production reason (one for the regular config, one for the appsettings.development.json).
Add it here (atleast token and atleast name for yourself).

The other fields you can ignore:

   "BotConfigurationCore": {
    "BotToken": "REPLACE_WITH_BOT_TOKEN",
    "BotName": "Example Bot",
    "HostAddress": "https://your-app.example.com/",
    "Route": "/bot",
    "SecretToken": "REPLACE_WITH_SECRET_TOKEN",
    "ChatId": "REPLACE_WITH_CHAT_ID"
  },
  
see here in startup.cs
    services.AddHttpClient("telegram_bot_client")
        .AddTypedClient<ITelegramBotClient>(
            (httpClient, sp) =>
            {
                ArgumentNullException.ThrowIfNull(configRoot);
                var botConfig = configRoot.BotConfigurationCore;
                ArgumentNullException.ThrowIfNull(botConfig);
                var options = new TelegramBotClientOptions(botConfig.BotToken);
                return new TelegramBotClient(options, httpClient);
            });
			
2. Many dotnet service tools require you a port, ignore that I needed to manage all over the intended way that all finds each other in all debug and hosting situation. (took years)
(there might be rests).

3. Really important is this part here:
  "ServiceConfigurationCore": {
    "ApiUser": "YourApiUser",
    "ApiPassword": "REPLACE_WITH_API_PASSWORD"
  }
It's for the backend service users to authenticate with the api.

To make it easy use a user you added in the databaseupdater.cs

4. I tested it with any kind of Microsoft SQL Databases I host under windows and linux, anything else possible -> ef but untested so you can change that if you want (but bear the consequences).
 "ConnectionStringsCore": {
    "ConnectionString": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;",
    "EasyTestConnectionString": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;",
    "DefaultConnection": "Server=your-server.example.com;Database=YourDatabase;User Id=YourDbUser;Password=YourStrongPasswordHere;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=True;"
  },
These are not randomized but not exist anymore so, these were working connection strings.

5. Vapid
You need to generate that and it needs to fit to your hosting scenario and "public" identity of your monolith. Now there is just a api method to send notifications as test to all, which works but yeah.
  "Vapid": {
    "PublicKey": "REPLACE_WITH_VAPID_PUBLIC_KEY",
    "PrivateKey": "REPLACE_WITH_VAPID_PRIVATE_KEY",
    "Subject": "mailto:admin@example.com"
  },

6. Logging
My logging is probably garbage so you might want to use your own BUT it's written from scratch
E-Mail is untested and I can say from production it doesn't work on any platform (atleast for the clients).

I separated these level from regular system logging to get a more "fine grained control" for my purpose,
but because Logging LogLevel in Config have a secret auto mechanic (unreuseable), there is LoggingCore on top of that.

"LoggingCore": {
    "CoreLogLevel": 4,
    "FileCore": {
      "CoreLogLevel": 4,
      "FilePath": ""
    },
    "EmailCore": {
      "CoreLogLevel": 6,
      "SenderEmail": "noreply@example.com",
      "SmtpServer": "smtp.example.com",
      "SmtpPort": 587,
      "Username": "smtp-user",
      "Password": "REPLACE_WITH_SMTP_PASSWORD",
      "EnableSsl": true,
      "EmailRecipients": [
        "recipient@example.com"
      ]
    }
  },



# to build
Dotnet 8 SDK 8.0.24
VStudio 2022 or 2026
DX WebApi & Blazor 24.12.14 (Trial or licensed)
# to build

# to build and run 
Dotnet 8 SDK 8.0.24
VStudio 2022 or 2026
DX WebApi & Blazor 24.12.14 (Trial or licensed)
add appsettings.json or/and appsettings.Development.json based on the example and provide any values 

I use Microsoft SQL Server on Linux, SQLite should be possible but is untested

add that also in connection string.

dotnet ef database update but I shouldn't need to say that.
If you change the datamodel than add migrations, update the database.
Users you can add via odata and so on

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "User") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "User",
                    "124308548zSHJAFaOFSUHI()!",
                    (user) => user.Roles.Add(defaultRole));
            }

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "Admin") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "Admin",
                    "a789jsbd34abnsdb=22§§(j:M",
                    (user) => user.Roles.Add(adminRole));
            }

or in the Database Updater (Typical DevExpress defaults so read the DevExpress Documentation it's nice).

I built all that it finds it self because ports made a lot of problems in hosting, this was a lot of hustle till it worked in any direction and any operation system.

The WASM Frontends work anywhere.
The WinUI Host just on windows.

I publish always self contained that I provide dotnet and all.

You might also to change the launchSettings.json and add like mentioned earlier appsettings.json + appsettings.Development.json . It should run in debug, published and so on.

Hosted wasm with that and lets encrypt on linux server and you don't need an appstore for your environment (built on top)
# to build and run this

# Why I share this in this state
It took me thousands of hours, for the framework (it's totally reuseable) and it breaks my heart to see so much winforms and other disgusting stuff of the past while we can do all without stores,
with Wasm, on any Operation System (I hosted it on Windows and Linux Server).
I basically stopped using TGram so I don't need to manage groups.
The WinUI Example OnTOp shows how to replace any Winforms app with Blazor and full data and Auth Model so!
Hope you learn a lot, have fun, do whatever you like.

Tacos Portal


it literally breaks my heart but I never have the life time to finish that.

Reverse engineering the data model of telegram and writing the parser sucked all life out of my guts,

although the frontend is absolutely fitting any DevExpress Component, evolving it now is nothing I can't take anymore.



Much thanks to the guy who wrote the telegram dotnet bot and to the DevExpress Team

(I work with your components for years, with basically any, had dozens of support tickets so I see myself as part of the team)



Because you want money for the "Audit Log" I needed to write it myself from scratch, I hate it and want to replace it so hard but yeah that's your fault.



My ultimate goal was to write a all ready ERP Platform, Web3 no external cookie, working Public hosted in the Web with Installable WASM Frontend.



This here is part of my life work and I put really thousands of hours to replace WinForms and give C# and Blazor the love it deserves.



You can replace any ERP and Forms app with this, any smartphone app, embed any WASM Stuff WITH Security and Auth Codebase.



What you need is a DX Blazor and WebApi License!



Their Api is pretty default but I like to use it.



I hope this makes you understand Blazor and all connections each side fundamentally.



It is basically a monolith system and can even run as Winforms Replacement (It's even better to debug it this way... test it...)



My Icons are "Self" AI Generated so whatever.



Anyway this is the best Blazor DevExpress Example on the world.

This is the best WASM Example on the World and

it is the most complete useable Telegram Bot Implementation in existence (All bots have just subfeatures, here you have EVERYTHING).



There is also a lot unused stuff because that is how I built new features, shady in the background. (The generic stuff, decided midway against half way completed).



I also assume there is no more intact datamodel for the Devexpress XAF System on the market but I could be wrong.



https://github.com/TelegramBots/Telegram.Bot



Dotnet 8

Dx 24.2.14



A fully working Microsoft Identity Replacement till any microservice or frontend, compatible with Rest and OData even with short commands.



You can provide any infrastructure without playstore, I host these things on Windows, Linux and so on.



I hope it brings you to new Blazor and ASP.NET Core Levels, sounds arrogant but I know how much that is and mostly written myself (you can't pay enough tokens to generate that datamodel lol)



Sharing this is deeply personal, it's my last years prototype framework for dotnet8 basically part of my lifeworks and for certain health reasons I'd like to "commit" and "push" just to be sure. 

Incomplete, as is but ALL WAYS work partly so totally powerful demo and tutorial (or more).

Here some other of my free prototype projects



https://github.com/Michi0403/LocalGPT

https://github.com/Michi0403/OpenMorph.NET



Older Stuff

https://bitbucket.org/da-legendary-Michi/legendary\_repository/

