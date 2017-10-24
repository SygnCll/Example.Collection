using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class InstitutionLocalization : Entity<InstitutionLocalization, long>, ILocalizable
    {
        protected InstitutionLocalization() { }


        public virtual Institution Institution { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
