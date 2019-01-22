using BooksWorld.Data;
using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using BooksWorld.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksWorld.Controllers
{
    public class DeliveryController : Controller
    {
        IRepository<User> userRepo;
        #region Profile

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoggedinDashboard()
        {
            return View("Profile/LoggedinDashboard");
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
                        vp.UserName = user.UserName;
                        vp.Name = user.Name;
                        vp.Email = user.Email;
                        vp.Gender = user.Gender;
                        vp.Dob = user.DateOfBirth;
                        vp.Role = user.Role;
                        vp.Status = user.Status;
                        vp.RegistrationDate = user.RegistrationDate;
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
                var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                    };
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + "_" + user.Id + ext; //appending the name with id  
                                                                // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/Resource/Picture/AllUserPictures"), myfile);
                    user.Picture = path;
                    file.SaveAs(path);
                    if (((IUserRepository)userRepo).Update(user))
                    {

                        ViewBag.message = "<fieldset>Image uploaded successfully</fieldset><br/>";
                    }
                    else
                    {
                        ViewBag.message = "<fieldset>Sorry some exception occurs! please try again.</fieldset><br/>";
                    }
                }
                else
                {
                    ViewBag.message = "<fieldset>Please choose only Image file</fieldset><br/>";
                }
            }
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
            return View("Profile/ProfilePicture");
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

                ChangePasswordModel changePasswordModel = new ChangePasswordModel()
                {
                    CurrentPassword = user.Password
                };


                return View("Profile/ChangePassword",changePasswordModel);
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
            else
            {
                return RedirectToAction("InValidAccess", "User");
            }
        }
        #endregion


        #region Shipment

        [HttpGet]
        public ActionResult PendingShipment()
        {
            return View("Shipment/PendingShipment");
        }

        [HttpGet]
        public ActionResult PendingHistory()
        {
            return View("Shipment/PendingHistory");
        }

        [HttpGet]
        public ActionResult BookDetails()
        {
            return View("Book/BookDetails");
        }

        [HttpGet]
        public ActionResult CategoryBook()
        {
            return View("Book/CategoryBook");
        }

        #endregion

        #region Report

        [HttpGet]
        public ActionResult ShipmentReport()
        {
            return View("Report/ShipmentReport");
        }

        #endregion
    }
}