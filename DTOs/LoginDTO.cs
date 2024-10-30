using System; // Importing basic system functionality

namespace API.DTOs; // Declaring the namespace for the DTO (Data Transfer Object)

// Defines the LoginDTO class used to receive login credentials from the user
public class LoginDTO
{
    // 'Username' property is required, used to store the username provided by the user
    public required string Username { get; set; }

    // 'Password' property is required, used to store the password provided by the user
    public required string Password { get; set; }
}
