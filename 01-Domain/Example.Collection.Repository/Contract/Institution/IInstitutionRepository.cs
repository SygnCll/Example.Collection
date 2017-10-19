using Example.Collection.Repository.Model;

namespace Example.Collection.Repository.Contract
{
    public interface IInstitutionRepository
    {
        Institution Get(string code);
    }
}
