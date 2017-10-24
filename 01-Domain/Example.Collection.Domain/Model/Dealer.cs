using System.Collections.Generic;
using Example.Collection.Domain.Contract;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Domain
{
    public partial class Dealer : Entity<Dealer, long>
    {
        protected Dealer()
        {
            this.SubDealers = new HashSet<Dealer>();
            this.Localization = new HashSet<DealerLocalization>();
            this.DealerLocalization = new Dictionary<string, DealerLocalization>();
            this.DealerEmailAddresses = new HashSet<DealerEmailAddress>();
            this.DealerPhoneNumbers = new HashSet<DealerPhoneNumber>();
            this.DealerDistributors = new HashSet<DealerDistributor>();
            this.ProxyOwnerDealers = new HashSet<Dealer>();
        }

        public virtual string DealerCode { get; protected set; }
        public virtual object ParentDealerId { get; protected set; }
        public virtual string ParentDealerCode { get; protected set; }
        public virtual Dealer ParentDealer { get; protected set; }
        public virtual object DealerTypeId { get; protected set; }
        public virtual DealerType DealerType { get; protected set; }
        public virtual StatusTypeEnum Status { get; protected set; }
        public virtual object InstitutionId { get; protected set; }
        public virtual Institution Institution { get; protected set; }
        public virtual object DealerHierarchyId { get; protected set; }
        public virtual DealerHierarchy DealerHierarchy { get; protected set; }
        public virtual object RegionId { get; protected set; }
        public virtual Region Region { get; protected set; }
        public virtual object CityId { get; protected set; }
        public virtual City City { get; protected set; }
        public virtual object TownId { get; protected set; }
        public virtual Town Town { get; protected set; }
        public virtual string Address { get; protected set; }
        public virtual string PostalCode { get; protected set; }
        public virtual decimal Longitude { get; protected set; }
        public virtual decimal Latitude { get; protected set; }
        public virtual object ProxyOwnerDealerId { get; protected set; }
        public virtual Dealer ProxyOwnerDealer { get; protected set; }
        public virtual WorkTimeEnum WorkStartTime { get; protected set; }
        public virtual WorkTimeEnum WorkEndTime { get; protected set; }



        public virtual ICollection<Dealer> SubDealers { get; protected set; }
        public virtual ICollection<DealerDistributor> DealerDistributors { get; protected set; }
        public virtual ICollection<DealerLocalization> Localization { get; protected set; }
        public virtual IDictionary<string, DealerLocalization> DealerLocalization { get; protected set; }
        public virtual ICollection<DealerEmailAddress> DealerEmailAddresses { get; protected set; }
        public virtual ICollection<DealerPhoneNumber> DealerPhoneNumbers { get; protected set; }
        public virtual ICollection<Dealer> ProxyOwnerDealers { get; protected set; }

    }
}
