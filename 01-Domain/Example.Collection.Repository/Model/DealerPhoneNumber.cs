namespace Example.Collection.Repository.Model
{
    public partial class DealerPhoneNumber : Entity<DealerPhoneNumber, long>
    {
        protected DealerPhoneNumber() { }

        public virtual object DealerId { get; protected set; }
        public virtual Dealer Dealer { get; protected set; }
        public virtual string PhoneNumber { get; protected set; }
        public virtual bool IsDefault { get; protected set; }
    }
}
