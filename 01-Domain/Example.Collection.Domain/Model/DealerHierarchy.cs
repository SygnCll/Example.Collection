using System.Collections.Generic;
using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class DealerHierarchy : Entity<DealerHierarchy, int>
    {
        protected DealerHierarchy()
        {
            this.DealerHierarchyDealers = new HashSet<DealerHierarchyDealer>();
        }

        public virtual string HashCode { get; protected set; }
        public virtual string DealerCodes { get; protected set; }
        public virtual ICollection<DealerHierarchyDealer> DealerHierarchyDealers { get; protected set; }
        //public virtual DealerHierarchyView DealerHierarchyView { get; protected set; }
    }
}
