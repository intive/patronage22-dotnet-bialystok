# Patronage22-Dotnet-Bialystok-BackEnd

To build the project from the source you need .NET 6.0, C# 10. The prefered IDE is Visual Studio 2022, although it is not required.

Project currently uses Entity Framework Core 6.0 nuget packages.

# Database connection

In the file `appsettings.json` in project `Patronage.Api` there is a list of all current connection strings. By default the `Default` connection string is used, although that can be changed in the Program.cs file (`builder.Configuration.GetConnectionString("Default")` passes the name of connection string to be used).

By default, connection string should point to your local MSSQL installation with folder set to `patronageDB`.
To generate your own connection string open SQL Server Objective Explorer, select the connection you want to use and right click on Properties. Use the connection string shown to you in General.

# Migrations
To apply migrations use 

```
Update-Database
```

with optional argument of `-Migration` to apply specific migration instead of newest.

To revert a migration use the same command with `-Migration` set to previous migration or run 

```
Update-Database -Migration 0
```

to revert all migrations.

To generate migration SQL script run
```
Script-Migration
```
with optional argument of `-Migration` to target specific migration.

To create a migration run
```
Add-Migration [name] -Project Patronage.Migrations
```

To remove a migration run
```
Remove-Migration
```
with optional argument of `-Migration` pointing to the specific migration.

To list migrations run
```
Get-Migration
```

All the commands work in Visual Studio's Packet Manager.