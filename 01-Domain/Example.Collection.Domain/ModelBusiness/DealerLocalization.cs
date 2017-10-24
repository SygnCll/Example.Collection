namespace Example.Collection.Domain
{
    public partial class DealerLocalization
    {
        public DealerLocalization(string description) : this()
        {
            this.Description = description;
        }

        public virtual void Update(string description)
        {
            this.Description = description;
        }
    }
}
