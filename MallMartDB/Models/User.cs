using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMartDB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public int ImageId { get; set; }
        public UserImage? Image { get; set; }
        public string Authorization { get; set; }


        public override string ToString()
        {
            return $"{FirstName} + {LastName} - {Authorization}";
        }
    }
}
