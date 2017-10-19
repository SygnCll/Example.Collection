using System.Collections.Generic;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Repository.Model
{
    public partial class UtilityCompany : Entity<UtilityCompany, int>
    {
        protected UtilityCompany()
        { 
            Institutions = new HashSet<Institution>();
            Localization = new HashSet<UtilityCompanyLocalization>();
            UtilityCompanyLocalization = new Dictionary<string, UtilityCompanyLocalization>();
        }

        public virtual string Code { get; protected set; }
        public virtual StatusTypeEnum Status { get; protected set; }
        public virtual object UtilityCompanyGroupId { get; protected set; }

        public virtual ICollection<Institution> Institutions { get; protected set; }
        public virtual ICollection<UtilityCompanyLocalization> Localization { get; protected set; }
        public virtual IDictionary<string, UtilityCompanyLocalization> UtilityCompanyLocalization { get; protected set; }
    }
}
