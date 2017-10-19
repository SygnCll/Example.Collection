using Example.Collection.Repository.Contract;

namespace Example.Collection.Repository.Model
{
    public partial class DealerLocalization : Entity<DealerLocalization, long>, ILocalizable
    {
        protected DealerLocalization() { }

        public virtual Dealer Dealer { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
