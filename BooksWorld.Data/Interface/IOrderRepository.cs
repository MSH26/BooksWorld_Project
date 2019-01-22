using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        Order GetByBookId(int bookId);
        Order GetByInvoiceId(int invoiceId);
        Order GetByDate(DateTime date);
        Order GetByType(string type);
        IEnumerable<Order> GetAllOrder();
        IEnumerable<Order> GetAllByUserId(int userId);
        IEnumerable<Order> GetAllPendingByUserId(int userId);
        bool DeleteInvoice(int id);
        int GetPendingOrderCountByUserId(int id);
        int GetApprovedOrderCountByUserId(int userId);
        int GetDeliveredOrderCountByUserId(int userId);
        int GetOnDeliveredOrderCountByUserId(int userId);
        IEnumerable<Order> GetAllPendingWithBookNameByUserId(int userId, bool type, string name);
        IEnumerable<Order> GetAllOrderWithTypeByUserId(int userId, bool type);
        IEnumerable<Order> GetAllOrderWithBookNameByUserId(int userId, string name);
        IEnumerable<Order> GetAllOrderWithPendingStatusByUserId(int userId, bool type);
        IEnumerable<Order> GetAllOrderWithStatusByUserId(int userId, bool shipmentType, bool paymentType);
    }
}
