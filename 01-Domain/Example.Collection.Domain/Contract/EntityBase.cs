using System;

namespace Example.Collection.Domain.Contract
{
    public abstract class EntityBase
    {
        public virtual DateTime CreatedOn { get; set; }

        public virtual long CreatedBy { get; set; }

        public virtual DateTime? ModifiedOn { get; set; }

        public virtual long? ModifiedBy { get; set; }
    }
}
