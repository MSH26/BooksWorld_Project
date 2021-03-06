﻿using BooksWorld.Entity;
using System;

namespace BooksWorld.Models.AdminUserList
{
    public class AdminUserListEditUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public int RewardPoint { get; set; }
        public Membership Membership { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string MobileNo { get; set; }
    }
}