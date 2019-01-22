using BooksWorld.Data.Interface;
using BooksWorld.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWorld.Data.Repository
{
    class BooksWorldSystemSettingRepository : Repository<BooksWorldSystemSetting>, IBooksWorldSystemSettingRepository
    {
        public string GetAboutUs()
        {
            throw new NotImplementedException();
        }

        public string GetHomeHeader()
        {
            throw new NotImplementedException();
        }

        public string GetHomePageDescription()
        {
            throw new NotImplementedException();
        }

        public string GetLogo()
        {
            throw new NotImplementedException();
        }

        public int GetRentPercentage()
        {
            throw new NotImplementedException();
        }

        public double GetServiceRating()
        {
            throw new NotImplementedException();
        }

        public double GetShipmentCharge()
        {
            throw new NotImplementedException();
        }
    }
}
