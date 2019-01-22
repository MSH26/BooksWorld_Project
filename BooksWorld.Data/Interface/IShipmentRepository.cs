using System;
using System.Collections.Generic;
using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    interface IShipmentRepository : IRepository<Shipment>
    {
        Shipment GetById(int id);
        bool DeleteShipment(int id);
        Shipment GetByStatus(bool status);
        Shipment GetByShipmentDate(DateTime date);
        Shipment GetByUserId(int id);
        Shipment GetByShipmentCharge(double shipmentCharge);
        Shipment GetByAddress(string address); 
        Shipment GetByType(bool type);
    }
}
