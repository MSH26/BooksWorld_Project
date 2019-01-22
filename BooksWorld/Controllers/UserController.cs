using BooksWorld.Data;
using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using BooksWorld.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BooksWorld.Controllers
{
    public class UserController : Controller
    {

        IRepository<BookCategory> repo;
        IRepository<User> userRepo;
        IRepository<Book> bookRepo;
        IRepository<Order> orderRepo;
        IRepository<Cart> cartRepo;
        IRepository<Offer> offerRepo;

        #region user_controller

        [HttpGet]
        public ActionResult Index()
        {
            repo = new RepositoryFactory().Create<BookCategory>();

            ViewBag.List = ((IBookCategoryRepository)repo).GetAllWithBooks();
            return View();
        }

        #endregion

        #region Profile
        
        public ActionResult InValidAccess()
        {
            return View("../Shared/InValidAccess");
        }

        [HttpGet]
        public ActionResult LoggedinDashboard()
        {
            if (Request.Cookies["User"] != null)
            {
                LoggedinDashboardModel lgdsbrd = new LoggedinDashboardModel();
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user != null)
                {
                    if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                    {

                        string[] registrationDateParts = Convert.ToString(user.RegistrationDate).Split('/');
                        string[] registrationDateYearParts = registrationDateParts[0].Split(' ');
                        string[] LastLoginDateParts = Convert.ToString(user.LastLoginDate).Split('/');
                        string[] LastLoginDateYearParts = LastLoginDateParts[2].Split(' ');
                        string[] currentDateParts = DateTime.Today.ToString("dd-MM-yyyy").Split('-');

                        int userSince = Convert.ToInt32(currentDateParts[2].Trim()) - Convert.ToInt32(registrationDateYearParts[0].Trim());
                        int lastLogDay = (30 - Convert.ToInt32(currentDateParts[0].Trim())) + Convert.ToInt32(LastLoginDateParts[0].Trim());
                        int lastLogMonth = (12 - Convert.ToInt32(currentDateParts[1].Trim())) + Convert.ToInt32(LastLoginDateParts[1].Trim());
                        int lastLogYear = Convert.ToInt32(currentDateParts[2].Trim()) - Convert.ToInt32(LastLoginDateYearParts[0].Trim());

                        ViewBag.UserSince = userSince + " years back";
                        ViewBag.LastLogin = (lastLogDay - 30) + " days " + (lastLogMonth%12) + " month " + (lastLogMonth / 12) + " years";

                        /** offer showing code */
                        offerRepo = new RepositoryFactory().Create<Offer>();
                        List<Offer> offers = ((IOfferRepository)offerRepo).GetAllCurrentOffers();

                        List<string> offerText = new List<string>();
                        if (offers.Count != 0)
                        {
                            foreach (var offer in offers)
                            {
                                if (offer.OfferCategory.Category.Equals("All"))
                                {
                                    if (offer.Percentage != 0)
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Percentage + " discount</a>");
                                    }
                                    else
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Amount + " discount</a>");
                                    }
                                }
                                else if (offer.OfferCategory.Category.Equals("Book") || offer.OfferCategory.Category.Equals("Author") || offer.OfferCategory.Category.Equals("Publication"))
                                {

                                }
                                else if (offer.OfferCategory.Category.Equals("Book Category"))
                                {
                                    if (offer.Percentage != 0)
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Percentage + " discount</a>");
                                    }
                                    else
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Amount + " discount</a>");
                                    }
                                }
                                else
                                {
                                    if (offer.Percentage != 0)
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Percentage + " discount for " + offer.OfferCategory + " </a>");
                                    }
                                    else
                                    {
                                        offerText.Add("<a>Buy any book  at " + offer.Amount + " discount for " + offer.OfferCategory + " </a>");
                                    }
                                }
                            }
                        }
                        else
                        {
                            offerText.Add("<a>At this moment there is no offer</a>");
                        }
                        ViewBag.Offers = offerText;

                        /** Suggested book code */

                        orderRepo = new RepositoryFactory().Create<Order>();
                        IEnumerable<Order> orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);

                        List<int> bookCategoryId = new List<int>();
                        foreach (var order in orders)
                        {
                            if (bookCategoryId.Count == 0) {
                                bookCategoryId.Add(order.Book.BookCategory.Id);
                            }
                            else {
                                bool tmp = false;
                                foreach (var item in bookCategoryId)
                                {
                                    if (item == order.Book.BookCategory.Id) {
                                        tmp = true;
                                        break;
                                    }
                                }
                                if (tmp == false) {
                                    bookCategoryId.Add(order.Book.BookCategory.Id);
                                }
                            }
                        }


                        repo = new RepositoryFactory().Create<BookCategory>();
                        List<Book> bookList = new List<Book>();
                        int flag1 = 0;
                        foreach (var item in bookCategoryId)
                        {
                            int flag2 = 0;
                            BookCategory bookCategory = ((IBookCategoryRepository)repo).GetAllBooksWithId(item);
                            foreach (var book in bookCategory.Books)
                            {
                                if (flag2 == 2 || flag1 == 5) {
                                    break;
                                }
                                bookList.Add(book);
                                ++flag1;
                                ++flag2;
                            }
                            if (flag1 == 5) {
                                break;
                            }
                        }
                        
                        ViewBag.BookList = bookList;

                        /** Pie chart */

                        orderRepo = new RepositoryFactory().Create<Order>();
                        ViewBag.PendingOrderCount = ((IOrderRepository)orderRepo).GetPendingOrderCountByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
                        ViewBag.ApprovedOrderCount = ((IOrderRepository)orderRepo).GetApprovedOrderCountByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
                        ViewBag.OnDeliveredOrderCount = ((IOrderRepository)orderRepo).GetOnDeliveredOrderCountByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
                        ViewBag.DeliveredOrderCount = ((IOrderRepository)orderRepo).GetDeliveredOrderCountByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);

                        ViewBag.User = user;
                    }
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/LoggedinDashboard", ViewBag);
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpGet]
        public ActionResult ViewProfile()
        {
            if (Request.Cookies["User"] != null)
            {
                ViewProfileModel vp = new ViewProfileModel();
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user != null)
                {
                    if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                    {

                        string[] profilePicture = user.Picture.Split('_');
                        string tempPicture = "";
                        for (int i = 0; i < profilePicture.Length; i++)
                        {
                            if (i == 0)
                            {
                                tempPicture += "..";
                            }
                            else if (profilePicture[i].Equals("/") && i > 0)
                            {
                                tempPicture += profilePicture[i] + "..";
                            }
                            else if (!profilePicture[i].Equals("/") && i > 0)
                            {
                                tempPicture += profilePicture[i];
                            }
                        }

                        vp.Name = user.Name;
                        vp.Email = user.Email;
                        vp.Gender = user.Gender;
                        vp.Dob = user.DateOfBirth.Date;
                        vp.Picture = tempPicture;
                        vp.Role = user.Role;
                        vp.Status = user.Status;
                        vp.RegistrationDate = user.RegistrationDate.Date;
                    }
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/ViewProfile", vp);
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Request.Cookies["User"] != null)
            {
                EditProfileModel edp = new EditProfileModel();
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user != null)
                {
                    if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                    {
                        edp.Name = user.Name;
                        edp.Email = user.Email;
                        edp.Gender = user.Gender;
                        edp.Dob = user.DateOfBirth;
                    }
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/EditProfile", edp);
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileModel edp)
        {
            if (Request.Cookies["User"] != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user != null)
                {
                    if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                    {
                        if (IsEmailValid(edp.Email) && IsDobValid(edp.Dob))
                        {
                            user.Name = edp.Name;
                            user.Email = edp.Email;
                            user.Gender = edp.Gender;
                            user.DateOfBirth = edp.Dob;
                            if (((IUserRepository)userRepo).Update(user))
                            {
                                TempData["msg"] = "<fielset>Successful</fielset>";
                            }
                            else
                            {
                                TempData["msg"] = "<fielset>Not successful</fielset>";
                            }
                        }
                        else {
                            TempData["msg"] = "<fielset>Not successful!! Email or date of birth error!</fielset>";
                        }
                    }
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/EditProfile", edp);
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpGet]
        public ActionResult ProfilePicture()
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
            if (user != null)
            {
                ViewBag.User = user;
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
            return View("Profile/ProfilePicture");
        }

        [HttpPost]
        [ActionName("ProfilePicture")]
        public ActionResult ProfilePicturePost(FormCollection fc, HttpPostedFileBase file)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
            if (user != null)
            {
                if (file.ContentLength < 5000000)
                {
                    var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                    };
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = "-" + user.UserName + "-" + user.Id + ext; //appending the name with id  
                                                                                   // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Resource/Picture/AllUserPictures"), myfile);
                        user.Picture = "_/_/_/Resource/Picture/AllUserPictures/" + myfile;

                        file.SaveAs(path);

                        string OldPicture = user.Picture;

                        if (((IUserRepository)userRepo).Update(user))
                        {
                            if (System.IO.File.Exists(OldPicture))
                            {
                                System.IO.File.Delete(OldPicture);
                            }
                            string[] profilePicture = user.Picture.Split('_');
                            string tempPicture = "";
                            for (int i = 0; i < profilePicture.Length; i++)
                            {
                                if (i == 0)
                                {
                                    tempPicture += "..";
                                }
                                else if (profilePicture[i].Equals("/") && i > 0)
                                {
                                    tempPicture += profilePicture[i] + "..";
                                }
                                else if (!profilePicture[i].Equals("/") && i > 0)
                                {
                                    tempPicture += profilePicture[i];
                                }
                            }

                            ViewBag.Picture = tempPicture;
                            TempData["Msg"] = "<fieldset>Image uploaded successfully</fieldset><br/>";
                        }
                        else
                        {
                            ViewBag.Picture = "#";
                            TempData["Msg"] = "<fieldset>Sorry some exception occurs! please try again.</fieldset><br/>";
                        }
                    }
                    else
                    {
                        ViewBag.Picture = "#";
                        TempData["Msg"] = "<fieldset>Please choose only Image file</fieldset><br/>";
                    }
                }
                else {
                    ViewBag.Picture = "#";
                    TempData["Msg"] = "<fieldset>Exceed The Image File Limit</fieldset><br/>";
                }
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
            return View("Profile/ProfilePicture",ViewBag);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Request.Cookies["User"] != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user == null)
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/ChangePassword");
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel cps)
        {
            if (Request.Cookies["User"] != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user != null)
                {
                    if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                    {
                        if (cps.CurrentPassword.Equals(user.Password.Trim()))
                        {
                            if (!cps.CurrentPassword.Equals(cps.NewPassword) && IsPasswordValid(cps.NewPassword) == true && cps.NewPassword.Equals(cps.RetypeNewPassword))
                            {
                                user.Password = cps.NewPassword;
                                if (((IUserRepository)userRepo).Update(user))
                                {
                                    TempData["Flag"] = "<fielset>Successful</fielset>";
                                }
                                else
                                {
                                    TempData["Flag"] = "<fielset>Exception occurs! please change password again.</fielset>";
                                }

                            }
                            else
                            {
                                TempData["Flag"] = "<fieldset>New password and retype new password mismatched!!</fieldset>";
                            }
                        }
                        else
                        {
                            TempData["Flag"] = "<fieldset>Please insert the correct current password!</fieldset>";
                        }
                    }
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profile/ChangePassword");
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user == null)
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                Request.Cookies["User"].Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(Request.Cookies["User"]);
                return RedirectToAction("Index", "Home");
            }
            else {
                return RedirectToAction("InValidAccess", "User");
            }
        }

        #endregion

        #region Book

        [HttpGet]
        public ActionResult SearchBook()
        {
            bookRepo = new RepositoryFactory().Create<Book>();
            IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllByCategory();
            if (book1 != null)
            {
                ViewBag.BookList = book1;
                return View("Book/SearchBook", ViewBag);
            }
            return View("Book/SearchBook");
        }

        [HttpPost]
        [ActionName("SearchBook")]
        public ActionResult SearchBookPost()
        {
            if (Request["filter"].Trim().Equals("Name")) {
                bookRepo = new RepositoryFactory().Create<Book>();
                IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllByName(Request["SearchByFilter"].Trim());
                if (book1 != null)
                {
                    ViewBag.BookList = book1;
                    return View("Book/SearchBook", ViewBag);
                }
                else {
                    ViewBag.Msg = "<fieldset>Sorry no book found</fieldset>";
                    return View("Books/SearchBook", ViewBag);
                }
            }
            else if (Request["filter"].Trim().Equals("Author"))
            {
                bookRepo = new RepositoryFactory().Create<Book>();
                IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllBookByAuthorName(Request["SearchByFilter"].Trim());
                if (book1 != null)
                {
                    ViewBag.BookList = book1;
                    return View("Books/SearchBook", ViewBag);
                }
                else
                {
                    ViewBag.Msg = "<fieldset>Sorry no book found</fieldset>";
                    return View("Books/SearchBook", ViewBag);
                }
            }
            else if (Request["filter"].Trim().Equals("Publisher"))
            {
                bookRepo = new RepositoryFactory().Create<Book>();
                IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllBookByPublicationName(Request["SearchByFilter"].Trim());
                if (book1 != null)
                {
                    ViewBag.BookList = book1;
                    return View("Books/SearchBook", ViewBag);
                }
                else
                {
                    ViewBag.Msg = "<fieldset>Sorry no book found</fieldset>";
                    return View("Books/SearchBook", ViewBag);
                }
            }
            else if (Request["filter"].Trim().Equals("Category"))
            {
                bookRepo = new RepositoryFactory().Create<Book>();
                IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllBookByCategoryName(Request["SearchByFilter"].Trim());
                if (book1 != null)
                {
                    ViewBag.BookList = book1;
                    return View("Books/SearchBook", ViewBag);
                }
                else
                {
                    ViewBag.Msg = "<fieldset>Sorry no book found</fieldset>";
                    return View("Books/SearchBook", ViewBag);
                }
            }
            return View("Book/SearchBook");
        }

        [HttpGet]
        public ActionResult BookDetails(int id=0)
        {
            bookRepo = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)bookRepo).GetById(id);
            if (book1 != null)
            {
                ViewBag.Book = book1;
                return View("Book/BookDetails", ViewBag);
            }
            ViewBag.Msg = "<fieldset>Sorry the book has been removed</fieldset>";
            return View("Book/BookDetails", ViewBag);
        }

        [HttpGet]
        public ActionResult BookDetail(int id = 0)
        {
            bookRepo = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)bookRepo).GetById(id);
            if (book1 != null)
            {
                ViewBag.Book = book1;
                return View("Book/BookDetail", ViewBag);
            }
            ViewBag.Msg = "<fieldset>Sorry the book has been removed</fieldset>";
            return View("Book/BookDetail", ViewBag);
        }

        [HttpGet]
        public ActionResult CategoryBook(int id = 0)
        {
            bookRepo = new RepositoryFactory().Create<Book>();
            IEnumerable<Book> book1 = ((IBookRepository)bookRepo).GetAllByCategoryId(id);
            if (book1 != null)
            {
                ViewBag.Cat = book1;
                return View("Book/CategoryBook", ViewBag);
            }
            ViewBag.Msg = "<fieldset>Sorry this category has has no book right now</fieldset>";
            return View("Book/CategoryBook", ViewBag);
        }

        [HttpPost]
        public ActionResult CategoryBook(string str = null)
        {
            return View("Book/CategoryBook");
        }

        #endregion

        #region Offer

        [HttpGet]
        public ActionResult CurrentOffer()
        {
            return View("Book/CurrentOffer");
        }

        [HttpPost]
        public ActionResult CurrentOffer(string str = null)
        {
            return View("Book/CurrentOffer");
        }

        #endregion

        #region Order

        [HttpGet]
        public ActionResult PendingOrder()
        {
            orderRepo = new RepositoryFactory().Create<Order>();
            IEnumerable<Order> pendingOrders = ((IOrderRepository)orderRepo).GetAllPendingByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
            ViewBag.PendingOrders = pendingOrders;
            return View("Order/PendingOrder", ViewBag);
        }

        [HttpPost]
        public ActionResult PendingOrder(string str = null)
        {
            return View("Order/PendingOrder");
        }
        
        [HttpPost]
        public ActionResult SearchPendingOrderByFilter()
        {
            orderRepo = new RepositoryFactory().Create<Order>();
            IEnumerable<Order> pendingOrders;
            if (Request["filter"].Trim().Equals("Buy")) {
                pendingOrders = ((IOrderRepository)orderRepo).GetAllPendingWithBookNameByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, true, Request["SearchByBookName"]);
            }
            else {
                pendingOrders = ((IOrderRepository)orderRepo).GetAllPendingWithBookNameByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, false, Request["SearchByBookName"]);
            }
            ViewBag.PendingOrders = pendingOrders;
            return View("Order/PendingOrder", ViewBag);
        }
        [HttpPost]
        public ActionResult SearchOrderHistoryByFilter()
        {
            orderRepo = new RepositoryFactory().Create<Order>();
            IEnumerable<Order> orders;
            if (Request["filter"].Trim().Equals("NAME"))
            {
                orders = ((IOrderRepository)orderRepo).GetAllOrderWithBookNameByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, Request["SearchByBookName"]);
                if (orders == null)
                {
                    ViewBag.Msg = "<fieldset>Sorry search doesn't match</fieldset><br />";
                    orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
                }
            }
            else if (Request["filter"].Trim().Equals("ORDER TYPE"))
            {
                if (Request["SearchByBookName"].Trim().Equals("Buy") || Request["SearchByBookName"].Trim().Equals("buy"))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithTypeByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, true);
                }
                else if (Request["SearchByBookName"].Trim().Equals("Rent") || Request["SearchByBookName"].Trim().Equals("rent"))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithTypeByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, false);
                }
                else {
                    ViewBag.Msg = "<fieldset>Sorry search doesn't match</fieldset><br />";
                    orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
                }
            }
            else if (Request["filter"].Trim().Equals("STATUS"))
            {
                if (Request["SearchByBookName"].Trim().ToLower().Equals("Pending".ToLower()))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithPendingStatusByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, false);
                }
                else if (Request["SearchByBookName"].Trim().ToLower().Equals("Approved".ToLower()))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithStatusByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, false, false);
                }
                else if (Request["SearchByBookName"].Trim().ToLower().Equals("On Delivery".ToLower()))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithStatusByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, false, true);
                }
                else if (Request["SearchByBookName"].Trim().ToLower().Equals("Delivered".ToLower()))
                {
                    orders = ((IOrderRepository)orderRepo).GetAllOrderWithStatusByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id, true, true);
                }
                else
                {
                    ViewBag.Msg = "<fieldset>Sorry search doesn't match</fieldset><br />";
                    orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);

                }
            }
            else
            {
                ViewBag.Msg = "<fieldset>Sorry search doesn't match</fieldset><br />";
                orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
            }

            ViewBag.Orders = orders;
            return View("Order/OrderHistory", ViewBag);
        }
        
        [HttpGet]
        public ActionResult OrderHistory()
        {
            orderRepo = new RepositoryFactory().Create<Order>();
            IEnumerable<Order> orders = ((IOrderRepository)orderRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
            ViewBag.Orders = orders;
            return View("Order/OrderHistory", ViewBag);
        }
        

        [HttpPost]
        public ActionResult OrderHistory(string str = null)
        {
            return View("Order/OrderHistory");
        }

        [HttpGet]
        public ActionResult ClearOrderHistory()
        {
            return View("Order/OrderHistory");
        }

        [HttpGet]
        public ActionResult OrderBookDetail()
        {
            return View("Order/OrderBookDetail");
        }

        [HttpGet]
        public ActionResult CancelOrder()
        {
            return View("Order/PendingOrder");
        }

        [HttpGet]
        public ActionResult DeleteOrderHistory()
        {
            return View("Order/OrderHistory");
        }

        #endregion

        #region Cart

        [HttpGet]
        public ActionResult CartHistory()
        {
            cartRepo = new RepositoryFactory().Create<Cart>();
            IEnumerable<Cart> cartHistory = ((ICartRepository)cartRepo).GetAllByUserId(((IUserRepository)(new RepositoryFactory().Create<User>())).GetByUserName(Request.Cookies["User"]["userName"]).Id);
            ViewBag.CartHistory = cartHistory;
            return View("Order/Cart/CartHistory", ViewBag);
        }

        [HttpGet]
        public ActionResult BuyCart(int id = 0)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
            if (user != null)
            {
                cartRepo = new RepositoryFactory().Create<Cart>();
                Cart cart = new Cart();
                cart.Book = ((IBookRepository)new RepositoryFactory().Create<Book>()).GetById(id);
                cart.User = user;
                cart.Type = true;
                if (((ICartRepository)cartRepo).Insert(cart))
                {
                    return RedirectToAction("CartHistory", "User");
                }
                else {
                    ViewBag.BookCart = cart;
                }
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
            return View("Order/Cart/CartHistory", ViewBag);
        }

        [HttpGet]
        public ActionResult RentCart(int id = 0)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
            if (user != null)
            {
                cartRepo = new RepositoryFactory().Create<Cart>();
                Cart cart = new Cart();
                cart.Book = ((IBookRepository)new RepositoryFactory().Create<Book>()).GetById(id);
                cart.User = user;
                cart.Type = false;
                if (((ICartRepository)cartRepo).Insert(cart))
                {
                    return RedirectToAction("Order/Cart/CartHistory", "User");
                }
                else
                {
                    ViewBag.BookCart = cart;
                }
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
            return View("Order/Cart/CartHistory", ViewBag);
        }

        [HttpGet]
        public ActionResult AddToCart(int id = 0)
        {
            bookRepo = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)bookRepo).GetById(id);
            if (book1 != null)
            {
                ViewBag.BookCart = book1;
                return View("Order/Cart/AddToCart", ViewBag);
            }
            ViewBag.Msg = "<fieldset>Sorry This book doesn't exist to the shop right now</fieldset>";
            return View("Order/Cart/AddToCart", ViewBag);
        }

        [HttpPost]
        public ActionResult AddToCart()
        {
            return RedirectToAction("Cart", "User");
        }

       // [HttpGet]
        //public ActionResult Cart()
        //{
        //    return View("Order/Cart/Cart");
        //}

        [HttpGet]
        public ActionResult CartConfirmation()
        {
            return View("Order/Cart/CartConfirmation");
        }

        #endregion

        #region Payment

        [HttpGet]
        public ActionResult Payment()
        {
            return View("Payment/Payment");
        }

        [HttpPost]
        public ActionResult Payment(string str = null)
        {
            return RedirectToAction("CartConfirmation", "User");
        }

        #endregion
        
        #region Report

        [HttpGet]
        public ActionResult TopLikedBooks()
        {
            return View("Report/TopLikedBooks");
        }

        [HttpGet]
        public ActionResult BooksAdd()
        {
            return View("Report/BooksAdd");
        }

        [HttpGet]
        public ActionResult Sale()
        {
            return View("Report/Sale");
        }

        [HttpGet]
        public ActionResult Rent()
        {
            return View("Report/Rent");
        }

        [HttpGet]
        public ActionResult Rating()
        {
            return View("Report/Rating");
        }
        #endregion

        #region Validation

        public bool IsPasswordValid(string password)
        {
            if (Convert.ToInt32(password.Length) >= 8 && Regex.Matches(password, @"[@#$%]").Count >= 1)
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