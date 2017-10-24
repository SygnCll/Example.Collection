using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class DealerLocalization : Entity<DealerLocalization, long>, ILocalizable
    {
        protected DealerLocalization() { }

        public virtual Dealer Dealer { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
