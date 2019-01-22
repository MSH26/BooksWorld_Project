using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    public interface IInvoiceRepository: IRepository<Invoice>
    {
        
        Invoice GetById(int id);
        Invoice ApprovedById(int id);
        bool Delete(int id);
        Invoice GetByUserId(int userId);
        Invoice GetByDate(DateTime date);
        Invoice GetByGreaterAmount(double amount);
        Invoice GetByLowerAmount(double amount);
        IEnumerable<Invoice> GetAllApprovedInvoices();
        IEnumerable<Invoice> GetAllInvoices();
        IEnumerable<Invoice> GetAllPending();
        IEnumerable<Invoice> GetAllPending(DateTime date);

    }
}
