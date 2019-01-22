using BooksWorld.Entity;
using System.Web.Mvc;
using BooksWorld.Data.Interface;
using BooksWorld.Data.Repository;
using BooksWorld.Data;
using BooksWorld.Models;
using System.Web;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BooksWorld.Controllers
{
    public class HomeController : Controller
    {
        IRepository<BookCategory> repo;
        IRepository<User> userRepo;
        IRepository<Book> book;



        [HttpGet]
        public ActionResult Index()
        {
            repo = new RepositoryFactory().Create<BookCategory>();
            
            ViewBag.List = ((IBookCategoryRepository)repo).GetAllWithBooks();
            return View(ViewBag);
        }

        

        #region Book

        [HttpGet]
        public ActionResult BookDetails(int id = 0)
        {
            book = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)book).GetById(id);
            if (book1 != null)
            {
                ViewBag.Book = book1;
                return View("Book/BookDetails",ViewBag);
            }
            return View("Index");
        }


        [HttpGet]
        public ActionResult Book()
        {
            return View("Cart/Cart");
        }
        

        [HttpGet]
        public ActionResult CategoryBook(int id = 0)
        {
            book = new RepositoryFactory().Create<Book>();
            IEnumerable<Book> book1 = ((IBookRepository)book).GetAllByCategoryId(id); 
            if (book1 != null)
            {
                ViewBag.Cat = book1;
                return View("Book/CategoryBook", ViewBag);
            }
            return View("Index");
        }

        #endregion

        #region Cart

        [HttpGet]
        public ActionResult BookAddToCart(int id = 0)
        {
            book = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)book).GetById(id);
            if (book1 != null)
            {
                ViewBag.BookCart = book1;
                return View("Cart/AddToCart", ViewBag);
            }
            return View("Index");
            
        }

        [HttpPost]
        public ActionResult AddToCart(int id = 0)
        {
            return RedirectToAction("Cart", "Home", id);
        }

        [HttpGet]
        public ActionResult Cart()
        {
            return View("Cart/Cart");
        }

        [HttpPost]
        public ActionResult Cart(int id = 0)
        {
            return View("Cart/Cart");
        }

        [HttpGet]
        public ActionResult CartConfirmation()
        {
            return View("Cart/CartConfirmation");
        }

        #endregion

        #region Payment

        [HttpGet]
        public ActionResult Payment()
        {
            return View("Payment/Payment");
        }

        [HttpPost]
        public ActionResult Payment(int id = 0)
        {
            return RedirectToAction("CartConfirmation", "User");
        }




        #endregion
        
        #region Others

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (loginModel.UserName != null && loginModel.Password != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(loginModel.UserName.Trim());

                if (user != null)
                {
                    if (user.Password.Equals(loginModel.Password.Trim()))
                    {
                        if (user.Role.Equals("Admin"))
                        {
                            Session["User"] = user;     //Storing user object to session
                            

                            HttpCookie UserCookie = new HttpCookie("User");     //Creating Cookie
                            UserCookie.Values["userName"] = user.UserName;
                            UserCookie.Values["userPassword"] = user.Password;
                            UserCookie.Expires = DateTime.Now.AddDays(0.25);
                            Response.Cookies.Add(UserCookie);

                            return RedirectToAction("Index", "Admin");
                        }
                        else if (user.Role.Equals("User"))
                        {
                            Session["User"] = user;     //Storing user object to session

                            HttpCookie UserCookie = new HttpCookie("User");     //Creating Cookie
                            UserCookie.Values["userName"] = user.UserName;
                            UserCookie.Values["userPassword"] = user.Password;
                            UserCookie.Expires = DateTime.Now.AddDays(0.25);
                            Response.Cookies.Add(UserCookie);

                            return RedirectToAction("LoggedinDashboard", "User");
                        }
                        else if (user.Role.Equals("DeliveryMan"))
                        {
                            Session["User"] = user;     //Storing user object to session

                            HttpCookie UserCookie = new HttpCookie("User");     //Creating Cookie
                            UserCookie.Values["userName"] = user.UserName;
                            UserCookie.Values["userPassword"] = user.Password;
                            UserCookie.Expires = DateTime.Now.AddDays(0.25);
                            Response.Cookies.Add(UserCookie);

                            return RedirectToAction("Index", "Delivery");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "<fieldset>Invalid UserName or Password</fieldset><br/>";
                    }
                }
                else
                {
                    TempData["msg"] = "<fieldset>Please register</fieldset><br/>";
                }
            }
            else
            {
                TempData["msg"] = "<fieldset>Please fill all boxes</fieldset><br/><br/>";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel rgmdl) {
            if (this.ModelState.IsValid)
            {
                if ( (rgmdl.Name.Length > 3) && IsEmailValid(rgmdl.Email) && IsNameValid(rgmdl.UserName) && IsPasswordValid(rgmdl.Password) && rgmdl.Password.Equals(rgmdl.ConfirmPassword) && IsDobValid(rgmdl.Dob))
                { 
                    userRepo = new RepositoryFactory().Create<User>();
                    User user = ((IUserRepository)userRepo).GetByUserName(rgmdl.UserName.Trim());

                    if (user == null)
                    {
                        //UserDesignation userDesignation = new UserDesignation();
                        User newUser = new User()
                        {
                            Name = rgmdl.Name,
                            Email = rgmdl.Email,
                            UserName = rgmdl.UserName,
                            Password = rgmdl.Password,
                            Gender = rgmdl.Gender,
                            DateOfBirth = rgmdl.Dob.Date,
                            Role = "User",
                            Status = true,
                            Address = rgmdl.Address,
                            MobileNo=rgmdl.MobileNo,
                            RewardPoint = 0,
                            Membership = ((IMembershipRepository)(new RepositoryFactory().Create<Membership>())).GetByName("none"),
                            RegistrationDate = DateTime.Today.Date,
                            LastLoginDate = DateTime.Today.Date
                        };
                        if (((IUserRepository)userRepo).InsertWithMembership(newUser, newUser.Membership.Id))
                        {
                            TempData["msg"] = "<fieldset>Registration successful</fieldset><br/><br/>";
                        }
                    }
                    else
                    {
                        TempData["msg"] = "<fieldset>Registration Failled. User name allready exists!</fieldset><br/><br/>";
                    }
                }
                else
                {
                    TempData["msg"] = "<fieldset>Registration not successful</fieldset><br/><br/>";
                }
            }
            else
            {
                TempData["msg"] = "<fieldset>Registration not successful!</fieldset><br/><br/>";
            }
            return View("Registration");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        #endregion

        [HttpGet]
        public ActionResult ShowwAll()
        {
            return View();
        }

        #region ValidationMethods

        public bool IsNameValid(string name)
        {
            if (Regex.Matches(name, @"[a-zA-Z0-9.\-\@&*_ ]").Count == Convert.ToInt32(name.Length) && Convert.ToInt32(name.Length) >= 2)
            {
                return true;
            }
            return false;
        }

        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            if (Convert.ToInt32(password.Length) >= 8 && Regex.Matches(password, @"[@#$%]").Count >= 1)
            {
                return true;
            }
            return false;
        }

        public bool IsDobValid(DateTime date)
        {
            string[] dateParts = Convert.ToString(date.Date).Split('/');
            string[] temp = dateParts[2].Split(' ');
            dateParts[2] = temp[0];

            if (Convert.ToInt32(dateParts[1]) < 32 && Convert.ToInt32(dateParts[1]) > 0 && Convert.ToInt32(dateParts[0]) < 13 && Convert.ToInt32(dateParts[0]) > 0)
            {
                string[] currentDateParts = DateTime.Today.ToString("dd-MM-yyyy").Split('-');
                temp = currentDateParts[2].Split(' ');
                currentDateParts[2] = temp[0];
                if (Convert.ToInt32(currentDateParts[2]) > (Convert.ToInt32(dateParts[2]) + 17))
                {
                    if (Convert.ToInt32(dateParts[0]) != 2)
                    {
                        return true;
                    }
                    else
                    {
                        if (Convert.ToInt32(dateParts[2]) % 4 != 0)
                        {
                            if (Convert.ToInt32(dateParts[1]) > 28)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(dateParts[1]) > 29)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        #endregion


    }
}