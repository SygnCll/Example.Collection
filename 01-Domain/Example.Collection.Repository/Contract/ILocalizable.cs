namespace Example.Collection.Repository.Contract
{
    public interface ILocalizable
    {
        string Description { get; set; }

        string LanguageCode { get; set; }
    }
}
