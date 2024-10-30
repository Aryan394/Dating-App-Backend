using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/*
public class DataContext: This defines the DataContext class, which inherits from DbContext provided by Entity Framework. DbContext is the class responsible for interacting with the database.
(DbContextOptions options): This constructor takes DbContextOptions as a parameter, which holds the configuration information (like the database connection string, provider, etc.) necessary to connect to the database.
: DbContext(options): The DataContext class passes the options parameter to the base DbContext class constructor. This ensures that the database connection and other settings are initialized correctly.
*/
public class DataContext(DbContextOptions options) : DbContext(options)
{
    /*
    DbSet<AppUser>: This defines a property named Users of type DbSet<AppUser>. In Entity Framework, a DbSet<T> represents a table in the database, where T is the entity type.
    AppUser: This entity type (from the API.Entities namespace) defines the schema of the table.
    Users: The name of this property will be used to create a table called Users in the database. Each row in this table will represent an instance of AppUser.
    { get; set; }: This is the getter and setter for the property, allowing you to query or modify the Users table.
    */
    public DbSet<AppUser> Users { get; set; } // This will use the AppUser.cs file and create a table called Users
}
