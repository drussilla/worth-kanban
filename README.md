# Requirements

## Goal
Build a Kanban board web application. The webapplication should communicate
with the backend through an API layer.

## Assignment

* Four default sections - Backlogs, Planned, In Progress and Completed.

* One default board and the ability to create multiple boards.

* The option to add new tasks to the board, update existing tasks and delete tasks.

* The option to drag and drop tasks across the board to change their statuses.

* The ability to add, update and delete custom sections apart from the default
four.

## Additional requirements

* Use proper coding standards and add whatever you would consider necessary
for this app to be included in a production environment

* Use github or equivalent to share your code with us at least 48 hours before your
next interview

* Retain your commit history and use proper documentation standards

* Be prepared to give a demo as well as technical walkthrough of your work
during a second interview 

# TODO
- [x] Initial project setup
- [x] Setup test projects
- [x] Task add/edit/delete/move
- [] Stage management
- [] Board management
- [] Add Docker support
- [] Polishing UI and Backend

# Assumptions, Design and Architecture

* Since this is a test assignemnt, I have made a few compromises between development speed, production-readines and features of the app.
* To reduce project complexity, both Backend and Frontend are written in C#
* I have decided to implement a simple authentication mechanism (both UI and API) to demonstrate cross-user collaboration. Since it was not in the requirements list I didn't want to spend much time on it so I went with a default identity server from the solution template (e.g. Duende IdentityServer).
* This application follows standard 3-tier model (DB, Buisness logic, UI), where DB and Business logic is in the Backend, and UI is in the Frontend.
* Logging is implemented using Microsoft.Logging classes and can be configured in the `appsettings.json`. By default the app output to console.
* Configuration is done via `appsettings.json` file.
* To simplify development, Backend is also responsible for serving Frontend files (wasm DLLs, index.htm and related js and css files), but they can be easily decoupled and shipped as a separate apps.
* Any frontend app can be used with provided Backend since they are loosely coupled.

## Backend

* Backend is an ASP.NET 7.0 application with a set of HTTP APIs to facilitate Frontend app.
* For data storage on the backend side I am using SQL Server Express LocalDB as a convinient way to reduce local dependencies and speedup development. To access data I am using Entity Framework 7.0 so data store could be repalced with MSSQL in the configuration (or with other RDBMS with a bit of coding).
* I am using EF Core code-first approach to track and apply change to the database schema (e.g. `dotnet ef database update`).
* Backned is a standard API application with a set of endpoints and internal services to facilitate them. For a more complex app I would use DDD approach with Event Souring and CQRS, but in this case it seems a an overkill.

## Frontend

* Frontend is a Blazor WebAssembly app that is communicating with the backend via HTTP API.
* Frontend app is using Redux pattern to manage internal state.
* Communication with the backend and internal data models are separated from the rest of the UI.

# Dependencies and third-party libraries

- [Duende IdentityServer](https://duendesoftware.com/products/identityserver) - This is to siplify registration and authentication process. This could be replaced with [Azure Active Directory](https://github.com/dotnet/aspnetcore/blob/main/src/Identity/README.md)
- [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) - This is a simpel local-file database, should be replaced with a production-ready alternatives (MSSQL, PostgreSQL, MySQL, etc.)
- Bootstrap
- Fluxor.Blazor.Web - Flux/Redux library for Blazor - https://github.com/mrpmorris/Fluxor/
- xUnit - Unit test framework for Backend and Frontend
- bUnit - a testing library for Blazor components
- Moq - Mocking framework for .NET
- FluentAssertions - Write unit test assertions in more fluent way

# Local Development

## Prerequisites

*Please note:* In most cases all this is cross-platform, but in some commands I use Windows-specific syntaxis.

- Download and install the lastest .NET 7.0 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
- Download and install SQL Server Express https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Install git: https://git-scm.com/download
- Clone this repo `git clone https://github.com/drussilla/worth-kanban.git`
- Install dotnet-ef `dotnet tool install --global dotnet-ef` to create and database apply migrations

## Build and Run

Build by executing:
```
dotnet build
```

You can just open Visual Studio or Rider and start `KanbanBoard.Server` project.

or just

```
dotnet run --project .\Server\KanbanBoard.Server.csproj
```

and open `https://localhost:7185` in the browser

**Note** You have to Register first!

## Tests

All tests are written in C# and using Moq framework to mock dependencies and test subjects in isolation.

Since I had a limited time implmeneting this assignment, I didn't want to achive 100% coverage with test, main goal is to validate critical pathes and show general approach to testing in this solution. Test should be expanded further more for production scenarios.

**Test projects**
* KanbanBoard.Server.UnitTests - Backend unit tests 
* KanbanBoard.Client.UnitTests - Client unit tests 
* KanbanBoard.Server.ComponentTests - Client component tests

To run tests simply execute in the root forlder:
```
dotnet test
```

_Note_: I didn't include any end-to-end, integration or performance tests since I had a limited time for this assignment.

# Production

# Future Impovements

* Archive tasks/stages/boards
* Use SignarR to add interactivity to the board
* Add support for images, rich text, markdown
* Add support for task reordering inside singe Stage
* Add support for Stage reordering
* Extend error handling logic
* Extend logging

