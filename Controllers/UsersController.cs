using System; // Importing the System namespace for basic functionality
using API.Data; // Importing the DataContext class from the API.Data namespace to interact with the database
using API.Entities; // Importing the AppUser entity from the API.Entities namespace to use the user data model
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionality for building web APIs
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core for asynchronous database operations

namespace API.Controllers; // Declaring the namespace for the UsersController class

// Defines the UsersController class to handle user-related API requests and inherits from BaseAPIController
public class UsersController(DataContext context) : BaseAPIController 
{
    // HTTP GET method to retrieve all users asynchronously
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // Returns a list of AppUser entities wrapped in an ActionResult
    {
        var users = await context.Users.ToListAsync(); // Asynchronously fetches all users from the database and stores them in a list
        return users; // Returns the list of users as the response
    }

    [Authorize]
    // HTTP GET method to retrieve a specific user by their ID asynchronously
    [HttpGet("{id:int}")] // Specifies that the route expects an integer ID parameter (e.g., api/users/3)
    public async Task<ActionResult<AppUser>> GetUser(int id) // Returns a single AppUser entity or a 404 NotFound response
    {
        var user = await context.Users.FindAsync(id); // Asynchronously searches for a user in the database using the given ID
        if (user == null) return NotFound(); // If the user is not found, return a 404 NotFound response
        return user; // If the user is found, return the user object as the response
    }
}
