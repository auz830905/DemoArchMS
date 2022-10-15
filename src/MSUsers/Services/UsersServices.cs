using Auth.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MSUsers.Interfaces;
using MSUsers.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSUsers.Services
{
    public class UsersServices : IUsersRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersServices(UserManager<IdentityUser> userManager,
                        
                             SignInManager<IdentityUser> signInManager, 
                             IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<UserToken> CreateUser(UserCredentials userCredentials)
        {
            UserToken? userToken = null;

            var user = new IdentityUser()
            {
                UserName = userCredentials.Email,
                Email = userCredentials.Email,
            };

            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return userToken!;
            }
        }

        public async Task<UserToken> LoginUser(UserCredentials userCredentials)
        {
            UserToken? userToken = null;
           
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentials);
            }
            else
            {
                return userToken!;
            }
        }

        #region Methods privates for Aux
        private async Task<UserToken> BuildToken(UserCredentials userCredentials)
        {
            var IdentityUser = await _userManager.FindByEmailAsync(userCredentials.Email);
            var claimsUser = await _userManager.GetClaimsAsync(IdentityUser);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userCredentials.Email),
                new Claim(ClaimTypes.Email, userCredentials.Email),                
            };            

            claims.AddRange(claimsUser);

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secret"]));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new(
                issuer: null,
                audience: null,
                claims: claims,
                signingCredentials: credentials,
                expires: expiration
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
        #endregion
    }
}