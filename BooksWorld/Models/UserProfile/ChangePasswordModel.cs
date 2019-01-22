
namespace BooksWorld.Models.UserProfile
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { set; get; }
        public string NewPassword { set; get; }
        public string RetypeNewPassword { set; get; }
    }
}