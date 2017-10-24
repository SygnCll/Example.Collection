
namespace Example.Collection.Domain
{
    public partial class DealerPhoneNumber
    {
        public override bool Equals(object obj)
        {
            DealerPhoneNumber toCompare = obj as DealerPhoneNumber;

            if (toCompare == null)
                return false;

            if (Equals(this.Dealer, toCompare.Dealer) &&
                Equals(this.PhoneNumber, toCompare.PhoneNumber))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + this.Dealer.GetHashCode();
            hashCode = (hashCode * 7) + this.PhoneNumber.GetHashCode();
            return hashCode;
        }

        public DealerPhoneNumber(Dealer dealer, bool IsDefault, string phoneNumber) : this()
        {
            this.Dealer = dealer;
            this.PhoneNumber = phoneNumber;
            this.IsDefault = IsDefault;
        }

        public virtual void MakeDefault()
        {
            this.IsDefault = true;
        }
    }
}
