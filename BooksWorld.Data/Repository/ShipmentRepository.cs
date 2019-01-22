using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public bool DeleteShipment(int id)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public Shipment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByShipmentCharge(double shipmentCharge)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByShipmentDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByStatus(bool status)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByType(bool type)
        {
            throw new NotImplementedException();
        }

        public Shipment GetByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
