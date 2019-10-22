using System;

namespace UsersAppMVVM.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Uri ImageLarge { get; set; }
        public Uri ImageMedium { get; set; }
    }
}
