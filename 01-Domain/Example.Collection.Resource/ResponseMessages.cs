using System.Resources;
using System.Reflection;
using System.Configuration;
using System.Globalization;

namespace Example.Collection.Resource
{
    public static class ResponseMessages
    {
        private static readonly ResourceManager resourceManager = new ResourceManager("Example.Collection.Resource.ResponseMessage", Assembly.GetExecutingAssembly());
        private static readonly string resourceKey = ConfigurationManager.AppSettings["ResourceKey"];

        public static string Get(string name, CultureInfo culture) => resourceManager.GetString(resourceKey + "_" + name, culture) ?? resourceManager.GetString(name, culture);

        public static string Get(string name, string languageCode) => Get(name, CultureInfo.CreateSpecificCulture(languageCode));

    }
}
