using BooksWorld.Data;
using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using BooksWorld.Models.UserProfile;
using BooksWorld.Models.AdminUserList;
using BooksWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BooksWorld.Controllers
{
    
    public class AdminController : Controller
    {
        IRepository<User> userRepo;
        IRepository<Order> orderRepo;
        IRepository<Book> book;
        IRepository<Invoice> invoiceRepo;
      
        [HttpGet]
        public ActionResult Index()
        {
           
            return View();
        }

        #region Profile

        public ActionResult InValidAccess()
        {
            return View("../Shared/InValidAccess");
        }
        [HttpGet]
        public ActionResult ViewProfile()
        {
            userRepo = new RepositoryFactory().Create<User>();

            User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
            if (user != null)
            {
                if (user.Password.Equals(Request.Cookies["User"]["userPassword"]))
                {
                    ViewBag.User = user;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Profiles/Profile",ViewBag);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View("Profiles/Dashboard");
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
                return View("Profiles/EditProfile", edp);
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
                }
                else
                {
                    return RedirectToAction("InValidAccess", "User");
                }
                return View("Profiles/EditProfile", edp);
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }

        }




        [HttpGet]
        public ActionResult EditPhoto()
        {
            return View("Profiles/EditPhoto");
        }


        [HttpGet]
        public ActionResult EditPassword()
        {
            if (Request.Cookies["User"] != null)
            {
                userRepo = new RepositoryFactory().Create<User>();
                User user = ((IUserRepository)userRepo).GetByUserName(Request.Cookies["User"]["userName"]);
                if (user == null)
                {
                    return RedirectToAction("InValidAccess", "Admin");
                }
                return View("Profiles/EditPassword");
            }
            else
            {
                return RedirectToAction("InValidAccess", "Admin");
            }

        }


        [HttpPost]
        public ActionResult EditPassword(ChangePasswordModel cps)
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
                            if (!cps.CurrentPassword.Equals(cps.NewPassword) && cps.NewPassword.Equals(cps.RetypeNewPassword))
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
                return View("Profiles/EditPassword");
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }
        #endregion

        #region Order

        [HttpGet]
        public ActionResult PendingOrder()
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            IEnumerable<Invoice> invoice = ((IInvoiceRepository)invoiceRepo).GetAllPending();


            if (invoice != null)
            {

                ViewBag.Invoice = invoice;

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            return View("Orders/PendingOrder", ViewBag);

        }





        [HttpPost]
        public ActionResult PendingOrder(DateTime pendingOrderDate)
        {
            
            if(pendingOrderDate == null)
            {
                return RedirectToAction("PendingOrder", "Admin");
            }
            else
            {
                invoiceRepo = new RepositoryFactory().Create<Invoice>();
                IEnumerable<Invoice> invoice = ((IInvoiceRepository)invoiceRepo).GetAllPending(pendingOrderDate);
                if (invoice != null)
                {

                    ViewBag.Invoice = invoice;

                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View("Orders/PendingOrder", ViewBag);

            }

        }



        [HttpGet]
        public ActionResult OrderHistory()
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            IEnumerable<Invoice> invoice = ((IInvoiceRepository)invoiceRepo).GetAllApprovedInvoices();
            
            if (invoice != null)
            {
               
                    ViewBag.Invoice = invoice;
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Orders/OrderHistory", ViewBag);
            
        }

        [HttpPost]
        public ActionResult OrderHistory(DateTime date)
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            IEnumerable<Invoice> invoice = ((IInvoiceRepository)invoiceRepo).GetAllPending(date);

            if (invoice != null)
            {

                ViewBag.Invoice = invoice;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Orders/PendingOrder", ViewBag);

        }

        #endregion Order

        #region Account

        [HttpGet]
        public ActionResult Balance()
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            IEnumerable<Invoice> invoices = ((IInvoiceRepository)invoiceRepo).GetAllInvoices();
            if (invoices != null)
            {
                double total = 0, daily = 0;
                ViewBag.Invoices = invoices;
                foreach(var invo in invoices)
                {
                    
                    total = total + invo.GrandPrice;
                    if(invo.InvoiceDate == DateTime.Today.Date)
                    {
                        daily += invo.GrandPrice;
                    }
                
                }
                ViewBag.Total = total;
                ViewBag.Daily = daily;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Accounts/Balance",ViewBag);
        }

        [HttpPost]
        public ActionResult Balance(DateTime date)
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            IEnumerable<Invoice> invoice = ((IInvoiceRepository)invoiceRepo).GetAllPending(date);

            if (invoice != null)
            {

                ViewBag.Invoice = invoice;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Orders/PendingOrder", ViewBag);

        }

        [HttpGet]
        public ActionResult Offer()
        {
            return View("Accounts/Offer");
        }

        #endregion Account

        #region Book
        [HttpGet]
        public ActionResult BookList()
        {
            book = new RepositoryFactory().Create<Book>();
            IEnumerable<Book> book1 = ((IBookRepository)book).GetAllByCategory();
            if (book1 != null)
            {
                ViewBag.BookList = book1;
                return View("Books/BookList", ViewBag);
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult BookDetails(int id = 0)
        {
            book = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)book).GetById(id);
            if (book1 != null)
            {
                ViewBag.Book = book1;
                return View("Books/BookDetails", ViewBag);
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult EditBookDetails(int id = 0)
        {
            book = new RepositoryFactory().Create<Book>();
            Book book1 = ((IBookRepository)book).GetById(id);
            if (book1 != null)
            {
                ViewBag.Book = book1;
                return View("Books/EditBookDetails", ViewBag);
            }
            return View("Index","Admin");
        }


        [HttpGet]
        public ActionResult AddNewBook()
        {
            return View("Books/AddNewBook");
        }

        [HttpPost]
        public ActionResult AddNewBook(AddBookModel addBookModel, HttpPostedFileBase file)
        {
            Book book = new Book() {
                Name = addBookModel.Name,
                Price = addBookModel.Price,
                AvailableQuantity = addBookModel.Quantity,
                Picture = null,
                Description = addBookModel.Description,
                AddDate = DateTime.Today.Date
            };
            if (((IBookRepository)new RepositoryFactory().Create<Book>()).InsertWithAuthorPublicationCategory(book, addBookModel.AuthorId, addBookModel.PublicationId, addBookModel.CategoryId))
            {
                TempData["msg"] = "Success";
            }
            TempData["msg"] = "Not successed";
            return View("Books/AddNewBook");
        }

        [HttpGet]
        public ActionResult ShowAuthors()
        {
            IEnumerable<Author> authors = new List<Author>(((IAuthorRepository)new RepositoryFactory().Create<Author>()).GetAll());
            ViewBag.Authors = authors;
            return PartialView("Books/Authors/ShowAuthors", ViewBag);
        }

        [HttpPost]
        public JsonResult AddAuthor(int authorId)
        {
            Author author = ((IAuthorRepository)new RepositoryFactory().Create<Author>()).GetById(authorId);
            return Json(author);
        }

        [HttpGet]
        public ActionResult ShowPublications()
        {
            IEnumerable<Publication> publications = new List<Publication>(((IPublicationRepository)new RepositoryFactory().Create<Publication>()).GetAll());
            ViewBag.Publications = publications;
            return PartialView("Books/Publications/ShowPublications", ViewBag);
        }

        [HttpPost]
        public JsonResult AddPublication(int publicationId)
        {
            Publication publication = ((IPublicationRepository)new RepositoryFactory().Create<Publication>()).GetById(publicationId);
            return Json(publication);
        }

        [HttpGet]
        public ActionResult ShowBookCategories()
        {
            IEnumerable<BookCategory> bookCategories = new List<BookCategory>(((IBookCategoryRepository)new RepositoryFactory().Create<BookCategory>()).GetAll());
            ViewBag.BookCategories = bookCategories;
            return PartialView("Books/ShowBookCategories", ViewBag);
        }

        [HttpPost]
        public JsonResult AddBookCategory(int bookCategoryId)
        {
            BookCategory bookCategory = ((IBookCategoryRepository)new RepositoryFactory().Create<BookCategory>()).GetById(bookCategoryId);
            return Json(bookCategory);
        }

        [HttpGet]
        public ActionResult ShowOffers()
        {
            IEnumerable<Offer> offers = new List<Offer>(((IOfferRepository)new RepositoryFactory().Create<Offer>()).GetAll());
            ViewBag.Offers = offers;
            return PartialView("Books/ShowOffers", ViewBag);
        }

        [HttpGet]
        public ActionResult AddOffer()
        {
            return PartialView("Books/AddOffer");
        }


        #endregion Book

        #region User

        [HttpGet]
        public ActionResult UsersList()
        {
            userRepo = new RepositoryFactory().Create<User>();
            IEnumerable<User> user = ((IUserRepository)userRepo).GetAllUser();

            if (user != null)
            {

                ViewBag.Users = user;

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            return View("Users/UsersList", ViewBag);
        }


        [HttpGet]
        public ActionResult AddUser()
        {
            return View("Users/AddUser");
        }

        [HttpPost]
        public ActionResult AddUser(RegistrationModel rgmdl)
        {
            
            if (this.ModelState.IsValid)
            {
                if ((rgmdl.Name.Length > 3) && IsEmailValid(rgmdl.Email) && IsNameValid(rgmdl.UserName) && IsPasswordValid(rgmdl.Password) && rgmdl.Password.Equals(rgmdl.ConfirmPassword) && IsDobValid(rgmdl.Dob))
                {
                    userRepo = new RepositoryFactory().Create<User>();
                    User user = ((IUserRepository)userRepo).GetByUserName(rgmdl.UserName.Trim());

                    if (user == null)
                    {
                        User newUser = new User()
                        {
                            Name = rgmdl.Name,
                            Email = rgmdl.Email,
                            UserName = rgmdl.UserName,
                            Password = rgmdl.Password,
                            Gender = rgmdl.Gender,
                            DateOfBirth = rgmdl.Dob.Date,
                            Role = Convert.ToString(Request["role"]),
                            Status = Request["status"].Equals(1) ? true : false,
                            Address = rgmdl.Address,
                            MobileNo = rgmdl.MobileNo,
                            RewardPoint = 0,
                            Membership = ((IMembershipRepository)new RepositoryFactory().Create<Membership>()).GetByName("none"),
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
                TempData["msg"] = "<fieldset>Registration not successful</fieldset><br/><br/>";
            }
            return View("Users/AddUser");
        }

        
        [HttpGet]
        public ActionResult DetailUser(int id)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetById(id);
            if (user != null)
            {
                ViewBag.User = user;
                return View("Users/DetailUser", ViewBag);
            }
            return View("Users/DetailUser");
        }


        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetById(id);
           
            if (user != null)
            {
                ViewBag.User = user;
                return View("Users/DeleteUser", ViewBag);
            }
            

            return View("Users/DeleteUser");
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DoDelete(int id)
        {
            userRepo = new RepositoryFactory().Create<User>();

            if(((IUserRepository)userRepo).Delete(id))
            {
                TempData["msg"] = "<fieldset>User Deleted Successfully</fieldset>";
                return RedirectToAction("UsersList");
            }
            TempData["Message"] = "Not Successful";
            
            return View("Users/DeleteUser");
            
        }



        [HttpGet]
        public ActionResult EditUser(int id)
        {
            userRepo = new RepositoryFactory().Create<User>();
            User user = ((IUserRepository)userRepo).GetById(id);
            if (user != null)
            {
                AdminUserListEditUserModel adminUserListEditUserModel = new AdminUserListEditUserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Name = user.Name,
                    Gender = user.Gender,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    Address = user.Address,
                    Picture = user.Picture,
                    Role = user.Role,
                    Status = user.Status,
                    RewardPoint = user.RewardPoint,
                    Membership = user.Membership,
                    RegistrationDate = user.RegistrationDate,
                    LastLoginDate = user.LastLoginDate,
                    MobileNo = user.MobileNo
                };
                return View("Users/EditUser", adminUserListEditUserModel);
            }
            return View("Users/EditUser");
        }

        [HttpPost]
        public ActionResult EditUser(AdminUserListEditUserModel adminUserListEditUserModel)
        {
            User user = ((IUserRepository)new RepositoryFactory().Create<User>()).GetById(adminUserListEditUserModel.Id);
            if (user != null)
            {
                user.Role = adminUserListEditUserModel.Role;
                user.Status = adminUserListEditUserModel.Status;
                user.MobileNo = adminUserListEditUserModel.MobileNo;
                if (((IUserRepository)new RepositoryFactory().Create<User>()).Update(user))
                {
                    user = ((IUserRepository)new RepositoryFactory().Create<User>()).GetById(user.Id);
                    adminUserListEditUserModel.Id = user.Id;
                    adminUserListEditUserModel.UserName = user.UserName;
                    adminUserListEditUserModel.Password = user.Password;
                    adminUserListEditUserModel.Name = user.Name;
                    adminUserListEditUserModel.Gender = user.Gender;
                    adminUserListEditUserModel.DateOfBirth = user.DateOfBirth;
                    adminUserListEditUserModel.Email = user.Email;
                    adminUserListEditUserModel.Address = user.Address;
                    adminUserListEditUserModel.Picture = user.Picture;
                    adminUserListEditUserModel.Role = user.Role;
                    adminUserListEditUserModel.Status = user.Status;
                    adminUserListEditUserModel.RewardPoint = user.RewardPoint;
                    adminUserListEditUserModel.Membership = user.Membership;
                    adminUserListEditUserModel.RegistrationDate = user.RegistrationDate;
                    adminUserListEditUserModel.LastLoginDate = user.LastLoginDate;
                    adminUserListEditUserModel.MobileNo = user.MobileNo;
                    
                    TempData["msg"] = "<fieldset>User edited</fieldset>";

                    return View("Users/EditUser", adminUserListEditUserModel);
                }
                TempData["msg"] = "<fieldset>Failed to edit user</fieldset>";
                return RedirectToAction("UsersList");
            }
            TempData["msg"] = "<fieldset>Failed to edit user</fieldset>";
            return RedirectToAction("UsersList");
        }


        [HttpGet]
        public ActionResult RatedUser()
        {
            return View("Reports/RatedUser");
        }

        #endregion User


        #region Report
        [HttpGet]
        public ActionResult StatisticReport()
        {
            return View("Reports/StatisticReport");
        }


        [HttpGet]
        public ActionResult ActivityReport()
        {
            return View("Reports/ActivityReport");
        }

        [HttpGet]
        public ActionResult LibraryStatisticReport()
        {
            return View("Reports/LibraryStatisticReport");
        }

        #endregion Report


        #region ValidationMethods

        public ActionResult CancelById(int id)
        {
            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            
            if (((IInvoiceRepository)invoiceRepo).Delete(id))
            {
                IEnumerable<Invoice> invoices = ((IInvoiceRepository)invoiceRepo).GetAllPending();

                if (invoices != null)
                {
                    ViewBag.Invoice = invoices;
                    TempData["msg"] = "<fielset>Successfully Deleted</fielset>";
                }

            }
            else
            {
                TempData["msg"] = "<fielset>Not Successful</fielset>";
                return View("Orders/PendingOrder", ViewBag);
            }
            return View("Orders/PendingOrder", ViewBag);
        }


        public ActionResult ApprovedById(int id)
        {

            invoiceRepo = new RepositoryFactory().Create<Invoice>();
            Invoice invoice = ((IInvoiceRepository)invoiceRepo).ApprovedById(id);
            invoice.Status = true;
            if (((IInvoiceRepository)invoiceRepo).Update(invoice))
            {
                IEnumerable<Invoice>  invoices = ((IInvoiceRepository)invoiceRepo).GetAllPending();
                
                if (invoices != null)
                {
                    ViewBag.Invoice = invoices;
                    TempData["msg"] = "<fielset>Successful</fielset>";
                }
                
            }
            else
            {
                TempData["msg"] = "<fielset>Not Successful</fielset>";
                return View("Orders/PendingOrder", ViewBag);
            }
            return View("Orders/PendingOrder", ViewBag);   
        }

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