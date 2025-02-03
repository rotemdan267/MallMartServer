using MallMartAPI.Models;
using MallMartAPI.Repos;
using MallMartDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDBManager DbManager;

        public LoginController(IConfiguration config, IDBManager manager)
        {
            _config = config;
            this.DbManager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);
            if (user == null)
                return NotFound("username or password is incorrect");

            Customer customer = null;
            Employee employee = null;

            if (user.Authorization == "Customer")
                customer = await DbManager.GetCustomerByUser(user);
            else if (user.Authorization == "Manager")
                employee = await DbManager.GetEmployeeByUser(user);

            if (customer != null)
            {
                var token = GenerateToken(customer.User);
                return Ok(new
                {
                    customer = customer,
                    employee = -1,
                    jwt = token
                });
            }

            if (employee != null)
            {
                var token = GenerateToken(employee.User);
                return Ok(new
                {
                    customer = -1,
                    employee = employee,
                    jwt = token
                });
            }
            // if the user exists but he's not a customer or manager - then he's a worker
            // and I havn't built the pages for him yet so "under construction"
            return Unauthorized("Site is under construction");
        }

        // To generate token
        private string GenerateToken(User user)
        {
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey( // Convert the loaded key from base64 to bytes.
                source: Convert.FromBase64String(_config["Jwt:Asymmetric:PrivateKey"]), // Use the private key to sign tokens
                bytesRead: out int _); // Discard the out variable 

            var signingCredentials = new SigningCredentials(
                new RsaSecurityKey(rsa),
                 SecurityAlgorithms.RsaSha256) // Important to use RSA version of the SHA algo
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            DateTime jwtDate = DateTime.Now;

            var jwt = new JwtSecurityToken(
                audience: _config["Jwt:Audience"],
                issuer: _config["Jwt:Issuer"],
                claims: new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Role, user.Authorization)
                    },
                notBefore: jwtDate,
                expires: jwtDate.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        //To authenticate user
        private async Task<User> Authenticate(UserLogin userLogin)
        {
            var user = await DbManager.GetUserByUsername(userLogin.Username);
            if (user is null)
                return null;

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.HashedPassword, userLogin.Password) == PasswordVerificationResult.Success)
                return user;

            return null;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
        [Authorize(Roles = "Manager")]
        public IActionResult ValidateTokenAsymmetric()
        {
            var currentUser = GetCurrentUser();
            if (currentUser is not null)
                return Ok($"Hi, {currentUser.Username} you are a {currentUser.Password}");
            else
                return Unauthorized("Unauthorized user");
        }

        private UserLogin GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserLogin
                {
                    Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Password = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
