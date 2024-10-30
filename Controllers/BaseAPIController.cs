using System; // Importing the System namespace for basic functionality
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC functionality for building web APIs

namespace API.Controllers; // Declaring the namespace for the BaseAPIController class

[ApiController] // Indicates that this class will handle HTTP API requests and automatically handle model validation errors
[Route("api/[controller]")] // Sets the base route for any derived controllers to "api/[controller]" (e.g., "api/users" if the derived controller is UsersController)
public class BaseAPIController : ControllerBase // Defines the BaseAPIController class that serves as a base class for other API controllers
{
    // This class doesn't have any specific logic or methods but serves as a base class to provide common functionality to other API controllers.
    // Inheriting from ControllerBase gives this class basic controller features like returning HTTP responses.
}
