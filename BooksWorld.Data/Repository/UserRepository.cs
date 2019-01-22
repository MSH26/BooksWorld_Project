using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace BooksWorld.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public bool InsertWithMembership(User newUser, int MembershipId) {
            Membership membershipToAdd = context.Memberships.FirstOrDefault(s=> s.Id == MembershipId);
            newUser.Membership = membershipToAdd;
            context.Users.Add(newUser);
            return context.SaveChanges() > 0;
        }
        public bool ChangePassword(int id)
        {
            throw new NotImplementedException();
        }

        public bool ChangeProfilePicture(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            User user = context.Users.Include(u => u.BankAccounts).SingleOrDefault(u => u.Id == id);
            foreach (var bank in user.BankAccounts.ToList())
            {
               context.BankAccounts.Remove(bank);
            }
            context.Users.Remove(user);
            return context.SaveChanges() > 0;
        }

        public BankAccount GetAllBankAccounts(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetAllBooks(int id)
        {
            throw new NotImplementedException(); 
        }

        public Invoice GetAllInvoices(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public User GetByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetByGender(string gender)
        {
            throw new NotImplementedException();
        }

        public User Get(string userName)
        {
            return null;
        }

        public User GetById(int id)
        {
            return base.context.Users.Include(s => s.Membership).Include(u => u.Invoices).Include(u => u.Invoices.Select(s=> s.Shipment)).Include(u => u.Invoices.Select(s => s.Orders)).Where(u => u.Role == "user").FirstOrDefault(s => s.Id == id);
        }
        
        public User GetByMembership(Membership membership)
        {
            throw new NotImplementedException();
        }

       
        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public User GetByRewardPoint(int rewardPoint)
        {
            throw new NotImplementedException();
        }

        public User GetByRole(string role)
        {
            throw new NotImplementedException();
        }

        public User GetByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUser()   
        {
            return context.Users.Include(u => u.Invoices).Include(u => u.Invoices.Select(s => s.Orders)).Where(u=> u.Role =="user").ToList();
        }

        //public IEnumerable<User> GetAll()
        //{
        //    return context.Users.ToList();
        //}

        public User GetByUserName(string userName)
        {
            return base.context.Users.Include(s => s.Membership).FirstOrDefault(s => s.UserName.Equals(userName));
        }

        public double GetTotalBuyAmount(int id)
        {
            throw new NotImplementedException();
        }

        public double GetTotalRentAmount(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByRole(int role)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            User userToUpdate = context.Users.FirstOrDefault(s=> s.Id == user.Id);
            userToUpdate.Role = user.Role;
            userToUpdate.Status = user.Status;
            userToUpdate.MobileNo = user.MobileNo;
            context.Entry(userToUpdate).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }
    }
}
