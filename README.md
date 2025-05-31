
# Agent Portal Backend

## General organization of this project

The project is divided into the following Source Folders.

 1. **API**
 This folder contains project for backend api service.
 
 2. **Domain**
 Database context and model classes resides in this project.
 
 3. **Infrastructure**
 Project in this folder will be used to manage audit trail.

 4. **Shared**
This section contains the library projects which will be shared by all other projects, if required.

 5. **Solution Items**
This section contains versioned sql script files which can be run sequentially to build database using sql server management studio.

## Instructions for Running

To create database run sql scripts which are in Solution Items/Migration folder.

To scaffold models when changes are made in database, use following command:

scaffold-dbcontext "Data Source=.\SQLEXPRESS;Initial Catalog=AgentPortal;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -ContextDir Database -Project Intelli.AgentPortal.Domain -NoOnConfiguring -Force

After running above mentioned command you have to do following steps to remove errors:

1. Remove all model classes which begins with AspNet*.

2. Remove DbContext base class from which generated AgentPortalContext is being inherited by default.
(Its partial implementation is already inherited from IdentityDbContext<AspNetUser>)

3. Remove model class references from generated AgentPortalcontext class which begins with AspNet* except AspNetUsers.

### 4. Repository Pattern

In order to use repository pattern built into domain, each model class should implement IEntity interface.

This implementation can be done in partial classes of models, which are in custom folder.

While creating a table in database its primary key must be of type int with name "Id" along with bit "IsActive", bigint "CreatedAt" and "UpdatedAt".

Doing this will automatically implement IEntity interface and model can be managed using repository pattern.


## Audit database

Audit logs in th Api are created asynchronously using RabbitMQ event bus.

Api stores audit logs in a separate database.

### To Set up RabbitMQ

In order to set up RabbitMQ please follow instruction in following link:

<a href="https://www.rabbitmq.com/download.html" target="_blank">Downloading & Installing RabbitMQ</a>

### To Create Audit Database

Run script file Migrations\Audit\AuditDbSetup.sql to create audit database.
