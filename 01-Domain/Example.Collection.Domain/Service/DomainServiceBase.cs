using System.Configuration;
using Example.Collection.Domain.Contract;


namespace Example.Collection.Domain.Service
{
    public class DomainServiceBase : IDomainService
    {
        public short CurrencyCode => short.Parse(ConfigurationManager.AppSettings.Get("CurrencyCode"));

        public string LanguageCode => ConfigurationManager.AppSettings.Get("LanguageCode");

        public string InstitutionCode => ConfigurationManager.AppSettings.Get("InstitutionCode");
        
    }
}
