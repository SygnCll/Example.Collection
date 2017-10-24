using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class DistributorLocalization : Entity<DistributorLocalization, long>, ILocalizable
    {
        protected DistributorLocalization() { }

        public virtual Distributor Distributor { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
