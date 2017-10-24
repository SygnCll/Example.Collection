namespace Example.Collection.Domain.Contract
{
    public interface ILocalizable
    {
        string Description { get; set; }

        string LanguageCode { get; set; }
    }
}
