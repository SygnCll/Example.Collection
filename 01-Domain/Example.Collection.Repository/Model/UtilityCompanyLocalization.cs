using Example.Collection.Repository.Contract;

namespace Example.Collection.Repository.Model
{
    public partial class UtilityCompanyLocalization : Entity<UtilityCompanyLocalization, long>, ILocalizable
    {
        protected UtilityCompanyLocalization() { }

        public virtual UtilityCompany UtilityCompany { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
