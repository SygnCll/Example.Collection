﻿using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain
{
    public partial class DealerEmailAddress : Entity<DealerEmailAddress, long>
    {
        protected DealerEmailAddress() { }

        public virtual object DealerId { get; protected set; }
        public virtual Dealer Dealer { get; protected set; }
        public virtual string EmailAddress { get; protected set; }
        public virtual bool IsDefault { get; protected set; }
    }
}
