
namespace Example.Collection.Domain
{
    public partial class DealerEmailAddress
    {
        public override bool Equals(object obj)
        {
            DealerEmailAddress toCompare = obj as DealerEmailAddress;
            if (toCompare == null)
                return false;

            if (Equals(this.Dealer, toCompare.Dealer) &&
               Equals(this.EmailAddress, toCompare.EmailAddress))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + this.Dealer.GetHashCode();
            hashCode = (hashCode * 7) + this.EmailAddress.GetHashCode();
            return hashCode;
        }

        public DealerEmailAddress(Dealer dealer, bool isDefault, string emailAddress) : this()
        {
            this.Dealer = dealer;
            this.EmailAddress = emailAddress;
            this.IsDefault = isDefault;
        }

        public virtual void MakeDefault()
        {
            this.IsDefault = true;
        }
    }
}
