using System;

namespace BooksWorld.Models
{
    public class RegistrationModel
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        public string Gender { set; get; }
        public DateTime Dob { set; get; }
        public string Address { set; get; }
        public string MobileNo { set; get; }

    }
}