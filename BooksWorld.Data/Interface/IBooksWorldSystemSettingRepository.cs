using BooksWorld.Entity;

namespace BooksWorld.Data.Interface
{
    interface IBooksWorldSystemSettingRepository: IRepository<BooksWorldSystemSetting>
    {
        
        int GetRentPercentage();
        string GetLogo();
        string GetHomePageDescription();
        string GetAboutUs();
        string GetHomeHeader();
        double GetServiceRating();

        double GetShipmentCharge();


    }
}