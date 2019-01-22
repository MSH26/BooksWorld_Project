using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BooksWorld.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public bool DeleteInvoice(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return context.Orders.Include(o => o.Invoice).Include(o => o.Invoice.User).Include(o => o.Invoice.Shipment).Include(o => o.Invoice.Shipment).ToList();
        }

        public IEnumerable<Order> GetAllByUserId(int userId)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Book.BookCategory).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId).ToList();
        }

        public IEnumerable<Order> GetAllPendingByUserId(int userId)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == false).ToList();
        }
        
        public IEnumerable<Order> GetAllPendingWithBookNameByUserId(int userId, bool type, string name)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == false && s.Type == type && s.Book.Name == name).ToList();
        }

        public IEnumerable<Order> GetAllOrderWithBookNameByUserId(int userId, string name)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true  && s.Book.Name == name).ToList();
        }
        
        public IEnumerable<Order> GetAllOrderWithTypeByUserId(int userId, bool type)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true && s.Type == type).ToList();
        }

        public IEnumerable<Order> GetAllOrderWithPendingStatusByUserId(int userId, bool type)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == false ).ToList();
        }

        public IEnumerable<Order> GetAllOrderWithStatusByUserId(int userId, bool shipmentType, bool paymentType)
        {
            return context.Orders.Include(s => s.Book).Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true && s.Invoice.Shipment.Status == shipmentType && s.Invoice.Payment.Status == paymentType).ToList();
        }

        public Order GetByBookId(int bookId)
        {
            throw new NotImplementedException();
        }

        public Order GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetByInvoiceId(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public Order GetByType(string type)
        {
            throw new NotImplementedException();
        }

        public int GetPendingOrderCountByUserId(int userId)
        {
            return context.Orders.Include(s => s.Invoice).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == false).ToList().Count;
        }

        public int GetApprovedOrderCountByUserId(int userId)
        {
            return context.Orders.Include(s => s.Invoice).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true).ToList().Count;
        }

        public int GetOnDeliveredOrderCountByUserId(int userId)
        {
            return context.Orders.Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true && s.Invoice.Shipment.Status == false && s.Invoice.Payment.Status == true).ToList().Count;
        }

        public int GetDeliveredOrderCountByUserId(int userId)
        {
            return context.Orders.Include(s => s.Invoice).Include(s => s.Invoice.Shipment).Include(s => s.Invoice.Payment).Include(s => s.Invoice.User).Where(s => s.Invoice.User.Id == userId && s.Invoice.Status == true && s.Invoice.Shipment.Status == true && s.Invoice.Payment.Status == true).ToList().Count;
        }
    }
}