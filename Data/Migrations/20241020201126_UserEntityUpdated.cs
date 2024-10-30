using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <summary>
    /// Represents a database migration that updates the User entity.
    /// This migration adds new columns to store password hash and salt for security purposes.
    /// </summary>
    public partial class UserEntityUpdated : Migration
    {
        /// <summary>
        /// Applies changes to the database schema when the migration is executed.
        /// </summary>
        /// <param name="migrationBuilder">The object that builds operations to modify the database schema.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adds a new column called 'PasswordHash' of type 'BLOB' to the 'Users' table
            // 'BLOB' is used to store binary data, and here it stores the hashed password
            // The column is marked as 'not nullable' and has a default value of an empty byte array
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            // Adds a new column called 'PasswordSalt' of type 'BLOB' to the 'Users' table
            // This column stores the salt value used in hashing the password
            // It is also marked as 'not nullable' with a default value of an empty byte array
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <summary>
        /// Reverts the changes applied by the Up method when the migration is rolled back.
        /// </summary>
        /// <param name="migrationBuilder">The object that builds operations to modify the database schema.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Removes the 'PasswordHash' column from the 'Users' table if the migration is rolled back
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            // Removes the 'PasswordSalt' column from the 'Users' table if the migration is rolled back
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
        }
    }
}
