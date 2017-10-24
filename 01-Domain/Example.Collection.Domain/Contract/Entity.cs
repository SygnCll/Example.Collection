using System;

namespace Example.Collection.Domain.Contract
{
    public abstract class Entity<TEntity, TId> : EntityBase, IEntity<TEntity, TId>
    {
        private int? oldHashCode;
        
        public virtual TId Id { get; set; }

        public virtual void DeleteMark(params string[] fields)
        {
            foreach (var fieldName in fields)
            {
                if (GetType().GetProperty(fieldName) == null)
                {
                    throw new ApplicationException(string.Format("Property ({0}) not found.", fieldName));
                }

                var currentValue = GetType().GetProperty(fieldName).GetValue(this, null).ToString();
                var newValue = string.Format("D_{0}_{1}", currentValue, DateTime.Now.ToString("yyyyMMddHHmmss"));

                GetType().GetProperty(fieldName).SetValue(this, newValue, null);
            }
        }

        public override bool Equals(object obj)
        {
            return GetEquality(obj);
        }

        public override int GetHashCode()
        {
            if (oldHashCode.HasValue)
                return oldHashCode.Value;

            var thisIsTransient = Equals(Id, default(TId));

            if (thisIsTransient)
            {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        private bool GetEquality(object obj)
        {
            var other = obj as IEntity<TEntity, TId>;
            if (other == null)
                return false;

            var otherIsTransient = Equals(other.Id, default(TId));
            var thisIsTransient = Equals(Id, default(TId));
            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(other, this);

            return other.Id.Equals(Id);
        }
    }
}
