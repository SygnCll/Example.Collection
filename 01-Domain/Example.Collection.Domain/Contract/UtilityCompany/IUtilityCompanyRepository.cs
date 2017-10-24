using System.Collections.Generic;

namespace Example.Collection.Domain.Contract
{
    public interface IUtilityCompanyRepository
    {
        UtilityCompany AddOrUpdate(UtilityCompany instance);

        UtilityCompany GetSingle(string code, bool statusRestriction = true);
        UtilityCompany GetSingle(int id, bool statusRestriction = true);
        UtilityCompany GetSignleWithDetails(string code, bool statusRestriction);
        UtilityCompany GetSignleWithOperationCode(string code, bool statusRestriction = true);


        ICollection<UtilityCompany> Get(Institution institution);
        ICollection<UtilityCompany> GetWithDetails(string keyword, bool statusRestriction);
        ICollection<UtilityCompany> GetUtilityCompanies(Institution institution, string searchTerm, string languageCode, bool isOnlyAvailable = true);
    }
}
