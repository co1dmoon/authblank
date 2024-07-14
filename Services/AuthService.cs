using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Controllers;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _database;
        private readonly PasswordHasherService _hasher;
        private readonly SessionService _sessionService;

        public AuthService (ApplicationDbContext database, PasswordHasherService hasher, SessionService sessionService) 
        {
            this._hasher = hasher; 
            this._database = database;
            this._sessionService = sessionService;
        }

        async public Task<string> SignIn(UserCredentials userCredentials)
        {
            var isUserExists = await this.IsUserExists(userCredentials.Username);

            if (!isUserExists) {
                throw new InvalidOperationException("User doesn't exists.");
            }

            var isPasswordValid = await this.ValidateUserPassword(userCredentials.Username, userCredentials.Password);

            if(!isPasswordValid)
            {
                throw new InvalidOperationException("Wrong user password.");
            }

            return await this._sessionService.CreateForUsername(userCredentials.Username);
        }

        async public Task<string> SignUp(UserCredentials userCredentials)
        {
            var userExists = await this.IsUserExists(userCredentials.Username);

            if(userExists) {

                throw new InvalidOperationException("Username already exists.");
            }

            var saltedPassword = this._hasher.HashPassword(userCredentials.Password);

            var user = new User { Username = userCredentials.Username, Password = saltedPassword };

            await this._database.AddAsync(user);
            await this._database.SaveChangesAsync();

            return await this._sessionService.CreateForUsername(userCredentials.Username);
        }

        async private Task<bool> IsUserExists(string username) 
        {
            return await this._database.Users.AnyAsync(u => String.Equals(u.Username, username)); 
        }

        async private Task<bool> ValidateUserPassword(string username, string password)
        {
            User user = await this._database.Users.FirstAsync(u => u.Username == username);

            return this._hasher.VerifyPassword(password, user.Password);
        }
    }
}
