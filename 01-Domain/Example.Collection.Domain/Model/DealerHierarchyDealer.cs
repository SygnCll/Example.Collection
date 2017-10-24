using System.Collections.Generic;
using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class DealerHierarchyDealer : Entity<DealerHierarchyDealer, int>
    {
        protected DealerHierarchyDealer() { }

        public virtual short Level { get; protected set; }
        public virtual short ReverseLevel { get; protected set; }
        public virtual object DealerHierarchyId { get; protected set; }
        public virtual DealerHierarchy DealerHierarchy { get; protected set; }
        public virtual object DealerId { get; protected set; }
        public virtual Dealer Dealer { get; protected set; }
    }
}
