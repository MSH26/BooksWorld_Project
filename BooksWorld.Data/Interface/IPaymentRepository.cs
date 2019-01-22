using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    interface IPaymentRepository: IRepository<Payment>
    {
       
        Payment GetById(int id);
        Payment GetByStatus(bool status);
        Payment GetByPaymentDate(DateTime date);
        Payment GetBySource(string source);
        Payment GetByType(string type);
        
       
        bool Delete(int id);
    }
}
