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

# Assumptions, Design and Architecture

* I have decided to implement a simple authentication mechanism (both UI and API) to demonstrate cross-user collaboration. Since it was not in the requirements list I didn't want to spend much time on it so I went with a default identity server from the solution template (e.g. Duende IdentityServer).
* For data storage on the backend side I am using SQL Server Express LocalDB as a convinient way to reduce local dependencies and speedup development. To access data I am using Entity Framework 7.0 so data store could be repalced with MSSQL in the configuration (or with other RDBMS with a bit of coding).
* I am using EF Core code-first approach to track and apply change to the database schema (e.g. `dotnet ef database update`).

# Dependencies and third-party libraries

- [Duende IdentityServer](https://duendesoftware.com/products/identityserver) - This is to siplify registration and authentication process. This could be replaced with [Azure Active Directory](https://github.com/dotnet/aspnetcore/blob/main/src/Identity/README.md)
- [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) - This is a simpel local-file database, should be replaced with a production-ready alternatives (MSSQL, PostgreSQL, MySQL, etc.)
- 

# Local Development

## Prerequisites

*Please note:* In most cases all this is cross-platform, but in some commands I use Windows-specific syntaxis.

- Download and install the lastest .NET 7.0 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/7.0
- Install git: https://git-scm.com/download
- Clone this repo `git clone https://github.com/drussilla/worth-kanban.git`
- Install dotnet-ef `dotnet tool install --global dotnet-ef` to create and database apply migrations
- Apply migrations: `cd worth-kanban\KanbanBoard\Server && dotnet ef database update`

## Tests

# Production

# Logging

