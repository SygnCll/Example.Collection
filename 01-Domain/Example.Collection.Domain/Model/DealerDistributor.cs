using Example.Collection.Domain.Contract;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Domain
{
    public partial class DealerDistributor : Entity<DealerDistributor, long>
    {
        protected DealerDistributor() { }

        public virtual object DealerId { get; protected set; }
        public virtual Dealer Dealer { get; protected set; }
        public virtual object DistributorId { get; protected set; }
        public virtual Distributor Distributor { get; protected set; }
        public virtual string OutSourceDealerCode { get; protected set; }
        public virtual StatusTypeEnum Status { get; protected set; }
        public virtual bool HasParentDealerDepositUsage { get; protected set; }
    }
}
