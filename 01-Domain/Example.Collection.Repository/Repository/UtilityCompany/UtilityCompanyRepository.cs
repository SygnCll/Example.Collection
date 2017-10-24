using System.Linq;
using NHibernate.Linq;
using Example.Collection.Domain;
using System.Collections.Generic;
using Example.Collection.Infrastructure;
using Example.Collection.Domain.Contract;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Repository
{
    public class UtilityCompanyRepository : BaseEntityRepository<UtilityCompany>, IUtilityCompanyRepository
    {
        public UtilityCompanyRepository(IRepository<UtilityCompany> repository) : base(repository) { }


        public ICollection<UtilityCompany> Get(Institution institution)
        {
            var items = Repository.Get(utilityCompany => utilityCompany.Status == StatusTypeEnum.Available &&
                                                         utilityCompany.Institutions.Any(x => x == institution))
                                  .Fetch(f => f.UtilityCompanyLocalization)
                                  .ToList();

            items.ForEach(item => item.UtilityCompanyLocalization.ProxyInitialize());

            return items;
        }

        public UtilityCompany GetSignleWithDetails(string code, bool statusRestriction)
        {
            throw new System.NotImplementedException();
        }

        public UtilityCompany GetSignleWithOperationCode(string code, bool statusRestriction = true)
        {
            var items = Repository.Get(utilityCompany => (utilityCompany.Status == StatusTypeEnum.Available || !statusRestriction) && utilityCompany.Code == code)
                //.Fetch(f => f.UtilityCompanyOperationCodes)
                .Fetch(f => f.UtilityCompanyLocalization)
                .ToList();

            items.ForEach(item => item.UtilityCompanyLocalization.ProxyInitialize());
            return items.FirstOrDefault();
        }

        public UtilityCompany GetSingle(string code, bool statusRestriction = true)
        {
            throw new System.NotImplementedException();
        }

        public UtilityCompany GetSingle(int id, bool statusRestriction = true)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<UtilityCompany> GetUtilityCompanies(Institution institution, string searchTerm, string languageCode, bool isOnlyAvailable = true)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<UtilityCompany> GetWithDetails(string keyword, bool statusRestriction)
        {
            throw new System.NotImplementedException();
        }


    }
}
