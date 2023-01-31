using MallMartDB.Models;
using MallMartAPI.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IGenericRepository<User> repo;
        public UsersController(IGenericRepository<User> repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await repo.GetAll();
            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var item = await repo.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User item)
        {
            string errorMessage = await ValidateUser(item);
            if (errorMessage == "")
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                item.HashedPassword = hasher.HashPassword(item, item.HashedPassword);
                var newItem = await repo.Insert(item);
                return CreatedAtAction(nameof(GetById), new { id = newItem.Id, controller = "users" }, newItem);
            }
            else
            {
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] User item)
        {
            bool updated = await repo.Update(id, item);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool deleted = await repo.Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }


        #region Validations
        private async Task<string> ValidateUser(User user)
        {
            string message = "";

            if (!(ValidateUsername(user.Username)))
            {
                message += "Username must be at least 2 chars\n" +
                                "Can contain english letters and numbers only\n" +
                                "Must start with an english letter\n\n";
            }
            else
            {
                if (!(await DoesUsernameAvailable(user.Username)))
                {
                    message += "This username already exists in our system. Please pick another one\n\n";
                }
            }

            if (!(ValidatePassword(user.HashedPassword)))
            {
                message += "Password must contain at least 6 chars\n\n";
            }

            if (!(ValidateFirstName(user.FirstName)))
            {
                message += "First name must be at least 2 letters\n" +
                                "Can contain english letters only\n" +
                                "Must start with a capital letter\n\n";
            }

            if (!(ValidateLastName(user.LastName)))
            {
                message += "Last name must be at least 2 letters\n" +
                                "Can contain english letters only\n" +
                                "Must start with a capital letter\n\n";
            }

            if (!(ValidateEmail(user.Email)))
            {
                message += "Invalid email address\n\n";
            }

            if (!(ValidatePhone(user.Phone)))
            {
                message += "Invalid israeli phone number\n\n";
            }

            return message;
        }

        private async Task<bool> DoesUsernameAvailable(string username)
        {
            var users = await repo.GetAll();

            foreach (var user in users)
            {
                if (username == user.Username)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateUsername(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]+$");
            return regex.IsMatch(username);
            /*
             * Username must be at least 2 chars
             * Can contain english letters and numbers only
             * Must start with an english letter
             */

        }

        private bool ValidatePassword(string password)
        {
            Regex regex = new Regex(@".{6,}");
            return regex.IsMatch(password);
            // Password must contain at least 6 chars

        }

        private bool ValidateFirstName(string fName)
        {
            Regex regex = new Regex(@"^[A-Z][a-zA-Z ]*[a-zA-Z]$");
            return regex.IsMatch(fName);
        }   /*
             * First name must be at least 2 letters
             * Can contain english letters only
             * Must start with a capital letter
             */

        private bool ValidateLastName(string lName)
        {
            Regex regex = new Regex(@"^[A-Z][a-zA-Z ]*[a-zA-Z]$");
            return regex.IsMatch(lName);
        }   /*
             * Last name must be at least 2 letters
             * Can contain english letters only
             * Must start with a capital letter
             */

        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return regex.IsMatch(email);
        }

        private bool ValidatePhone(string phone)
        {
            Regex regex = new Regex(@"[0]{1}[0-9]{8,9}");
            return regex.IsMatch(phone);
        }

        #endregion
    }
}
