using System;

namespace BooksWorld.Models.UserProfile
{
    public class ViewProfileModel
    {
        public string UserName { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Gender { set; get; }
        public string Picture { set; get; }
        public DateTime Dob { set; get; }
        public string Role { set; get; }
        public bool Status { set; get; }
        public DateTime RegistrationDate { set; get; }
    }
}