using System.Linq;
using Example.Collection.Domain;
using Example.Collection.Infrastructure;
using Example.Collection.Domain.Contract;

namespace Example.Collection.Repository
{
    public class InstitutionRepository : BaseEntityRepository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(IRepository<Institution> repository) : base(repository) { }

        public virtual Institution Get(string code) => Repository.Get(q => q.Code == code).ToList().FirstOrDefault();
    }
}
