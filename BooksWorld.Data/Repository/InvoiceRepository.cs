using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace BooksWorld.Data.Repository
{
   public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
       
        public bool Delete(int id)
        {
            Invoice invoice = context.Invoices.Include(a => a.Orders).SingleOrDefault(a => a.Id == id);
            foreach (var order in invoice.Orders.ToList())
            {
                context.Orders.Remove(order);
            }
            context.Invoices.Remove(invoice);
            return context.SaveChanges() > 0;
        }

        public Invoice GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Invoice GetByGreaterAmount(double amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable <Invoice> GetAllApprovedInvoices()
        {
            return context.Invoices.Include(a => a.User).Include(a => a.Shipment).Include(a => a.Payment).Where(a=>a.Status==true).ToList();
        }

       public IEnumerable<Invoice> GetAllInvoices()
        {
            return context.Invoices.Include(a => a.User).Include(a => a.Shipment).Include(a => a.Payment).ToList();
        }

        public Invoice GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice ApprovedById(int id)
        {
            return context.Invoices.Include(i => i.User).Include(i => i.Payment).Include(i => i.Shipment).Where(i => i.Id==id).SingleOrDefault();
        }

        public Invoice GetByLowerAmount(double amount)
        {
            throw new NotImplementedException();
        }

        public Invoice GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetAllPending()
        {
            return context.Invoices.Include(i => i.User).Include(i => i.Payment).Include(i => i.Shipment).Where(i => i.Status == false).ToList();
            

        }


        public double GetGrandPrice(int totalDay)
        {

            return context.Invoices.Where(s =>(DateTime.Today.Date.Subtract(s.InvoiceDate)).Days <= totalDay).ToList().Sum(s=> s.GrandPrice);
        }


        public IEnumerable<Invoice> GetAllPending(DateTime date) {
            return context.Invoices.Include(i => i.User).Include(i => i.Payment).Include(i => i.Shipment).Where(i => i.Status == false && i.InvoiceDate == date).ToList();
        }
    }
}
