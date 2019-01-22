using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
     public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string userName);
        User GetById(int id);
        bool Delete(int id);
        User GetByName(string name);
        User GetByGender(string gender);
        User GetByEmail(string email);
        User GetByDateOfBirth(DateTime dateOfBirth);
        User GetByAddress(string address);
        User GetByRole(int role);
        User GetByStatus(string status);
        User GetByRewardPoint(int rewardPoint);
        User GetByMembership(Membership membership);
        IEnumerable<User> GetAllUser();
        Book GetAllBooks(int id);
        BankAccount GetAllBankAccounts(int id);
        Invoice GetAllInvoices(int id);
        bool ChangePassword(int id);
        bool ChangeProfilePicture(int id);
        double GetTotalBuyAmount(int id);
        double GetTotalRentAmount(int id);
        bool InsertWithMembership(User newUser, int MembershipId);
    }
}
