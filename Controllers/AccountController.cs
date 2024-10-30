using System; // Importing basic system functionality
using System.Security.Cryptography; // Importing cryptography tools for secure data handling
using System.Text; // Importing text encoding utilities
using API.Data; // Importing the DataContext class for database operations
using API.DTOs; // Importing the RegisterDTO and LoginDTO classes for registration and login data transfer
using API.Entities; // Importing the AppUser entity class
using API.Interfaces; // Importing the interface for token service operations
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionality
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core for database querying

namespace API.Controllers; // Declaring the namespace for the AccountController class

// Defines the AccountController class that handles account-related operations, inheriting from BaseAPIController
public class AccountController(DataContext context, ITokenService tokenService) : BaseAPIController
{
    // HTTP POST method for user registration
    [HttpPost("register")] // Specifies that the route for this method will be "account/register"
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO) 
    {
        // Creates an instance of HMACSHA512 for hashing the password. This ensures password security by converting the password into a cryptographic hash
        using var hmac = new HMACSHA512(); 

        // Check if the username already exists in the database
        if (await UserExists(registerDTO.Username)) 
            return BadRequest("Username is taken"); // Returns a 400 BadRequest if the username is already in use

        // Creating a new AppUser object with the provided username, password hash, and password salt
        var user = new AppUser
        {
            // The username is converted to lowercase to avoid case sensitivity issues in the database
            UserName = registerDTO.Username.ToLower(), 

            // The password provided by the user is hashed using the HMACSHA512 instance
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),

            // The cryptographic key (salt) used by HMACSHA512 is stored to ensure security when checking the password later
            PasswordSalt = hmac.Key
        };

        // Adds the new user to the database context but does not save the change yet
        context.Users.Add(user); 

        // Asynchronously saves changes to the database
        await context.SaveChangesAsync(); 

        // Returns the newly created user object as the response, wrapped in a UserDTO
        return new UserDTO
        {
            Username = user.UserName, // Sets the Username in the response DTO
            Token = tokenService.CreateToken(user) // Generates and includes a token in the response DTO
        };
    }

    // HTTP POST method for user login
    [HttpPost("login")] // Specifies that the route for this method will be "account/login"
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        // Find the user by their username (ignoring case) in the database
        var user = await context.Users.FirstOrDefaultAsync(x => 
            x.UserName.ToLower() == loginDTO.Username.ToLower());

        // If the user is not found, return an Unauthorized (401) response
        if (user == null) 
            return Unauthorized("Invalid username");

        // Create a new HMACSHA512 instance using the user's stored password salt
        using var hmac = new HMACSHA512(user.PasswordSalt);

        // Compute the hash of the provided password using the stored salt
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        // Compare the computed hash with the stored password hash
        for (int i = 0; i < computedHash.Length; i++)
        {
            // If any byte of the hash does not match, return an Unauthorized (401) response
            if (computedHash[i] != user.PasswordHash[i]) 
                return Unauthorized("Invalid password");
        }

        // If the password matches, return the user object as the response, wrapped in a UserDTO
        return new UserDTO
        {
            Username = user.UserName, // Sets the Username in the response DTO
            Token = tokenService.CreateToken(user), // Generates and includes a token in the response DTO
        };
    }

    // Helper method to check if a user with the provided username already exists in the database
    private async Task<bool> UserExists(string username)
    {
        // Asynchronously checks if any user in the database has the same username (ignoring case sensitivity)
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
