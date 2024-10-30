using System; // Importing the System namespace for basic functionality

namespace API.Entities; // Declaring the namespace for the AppUser class

// Represents a database table in Entity Framework called "AppUser"
public class AppUser
{
    // Represents a column in the database table

    // Primary key for the table, automatically set by Entity Framework
    // 'int' is a primitive type, and its default value is 0
    public int Id { get; set; } // Entity Framework requires properties to be public to map them to the database

    // Username of the user, represented as a string (reference type)
    // The 'required' keyword ensures that the UserName must be provided when creating an AppUser object
    public required string UserName { get; set; }

    // Password hash used for securely storing the user's password
    // The 'required' keyword indicates that this property must have a value when creating a new AppUser
    public required byte[] PasswordHash { get; set; }

    // Password salt used in combination with the password to create the hash, adding an extra layer of security
    // The 'required' keyword ensures that this property is set when creating an AppUser object
    public required byte[] PasswordSalt { get; set; }
}
