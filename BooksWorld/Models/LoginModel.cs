using System;

namespace BooksWorld.Models
{
    public class LoginModel
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string RememberMe { set; get; }
    }
}