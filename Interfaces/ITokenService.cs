using System; // Importing basic system functionality
using API.Entities; // Importing the AppUser entity class

namespace API.Interfaces; // Declaring the namespace for the ITokenService interface

// Defines an interface for creating JWT tokens for a given user
public interface ITokenService
{
    // Method signature for creating a token, which takes an AppUser object as input
    // and returns a string (the generated JWT token).
    string CreateToken(AppUser user);
}
