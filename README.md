# Sample Refactor API

## Introduction
This API is designed to be a RESTful API to manage User, Roles and a link between a two (UserRoles).
There are some purposeful errors in this code base for you to identify. Your main areas for analysis & improvement are the Repositories and Controllers.

The models themselves are fine. However, you may wish to add or remove some if you find they don't fit the solution.

## Running
This solution was built in Visual Studio 2019 (v16.10) and hasn't been tested in older versions.
It targets .NET 5. Upon **each** startup an in-memory database (SQLite) will be dropped & created with the relevant tables for you to get up-and-running with straight away. The name of this database is called `identity.db`