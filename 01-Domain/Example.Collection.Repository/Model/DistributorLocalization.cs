

using Example.Collection.Repository.Contract;

namespace Example.Collection.Repository.Model
{
    public partial class DistributorLocalization : Entity<DistributorLocalization, long>, ILocalizable
    {
        protected DistributorLocalization() { }

        public virtual Distributor Distributor { get; protected set; }

        public virtual string LanguageCode { get; set; }

        public virtual string Description { get; set; }
    }
}
