using System;

namespace BooksWorld.Models.UserProfile
{
    public class EditProfileModel
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Gender { set; get; }
        public DateTime Dob { set; get; }
    }
}