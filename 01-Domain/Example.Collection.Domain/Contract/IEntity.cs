using System;

namespace Example.Collection.Domain.Contract
{
    public interface IEntity<TEntity, TId>
    {
        TId Id { get; set; }

        DateTime CreatedOn { get; set; }

        long CreatedBy { get; set; }

        long? ModifiedBy { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
