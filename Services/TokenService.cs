using System; // Importing basic system functionality
using System.IdentityModel.Tokens.Jwt; // Importing tools for handling JWT (JSON Web Tokens)
using System.Security.Claims; // Importing functionality for creating claims for a token
using System.Text; // Importing text encoding utilities
using API.Entities; // Importing the AppUser entity class
using API.Interfaces; // Importing the ITokenService interface
using Microsoft.IdentityModel.Tokens; // Importing security tools for token creation

namespace API.Services; // Declaring the namespace for the TokenService class

// TokenService class implements the ITokenService interface and is used for creating JWT tokens
public class TokenService(IConfiguration config) : ITokenService
{
    // Method to create a JWT token for a given user
    public string CreateToken(AppUser user)
    {
        // Retrieve the secret key for token creation from the configuration file (e.g., appsettings.json)
        // If the key is not available, throw an exception
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access token key from appsettings");

        // Ensure the token key length is sufficient for security purposes; throw an exception if it's too short
        if (tokenKey.Length < 64) throw new Exception("Your token Key needs to be longer");

        // Create a symmetric security key using the secret key, which will be used to sign the token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Create a list of claims (information about the user), such as the user's username
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName)
        };

        // Generate signing credentials using the security key and HMAC-SHA512 algorithm for signing the token
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Define the details of the token, including claims, expiration time, and signing credentials
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // Attach user claims to the token
            Expires = DateTime.UtcNow.AddDays(7), // Set token expiration date to 7 days from now
            SigningCredentials = creds // Include the signing credentials in the token
        };

        // Create a JWT token using the token descriptor
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Return the serialized token as a string
        return tokenHandler.WriteToken(token);
    }
}
