﻿using Example.Collection.Infrastructure.Enum;
using System.Collections.Generic;

namespace Example.Collection.Repository.Model
{
    public partial class Distributor : Entity<Distributor, long>
    {
        protected Distributor()
        {
            DealerDistributors = new HashSet<DealerDistributor>();
            Localization = new HashSet<DistributorLocalization>();
            DistributorLocalization = new Dictionary<string, DistributorLocalization>();
        }

        public virtual object DealerId { get; protected set; }
        public virtual Dealer Dealer { get; protected set; }
        public virtual StatusTypeEnum Status { get; protected set; }


        public virtual ICollection<DealerDistributor> DealerDistributors { get; protected set; }
        public virtual ICollection<DistributorLocalization> Localization { get; protected set; }
        public virtual IDictionary<string, DistributorLocalization> DistributorLocalization { get; protected set; }
    }
}
