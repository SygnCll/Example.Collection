namespace Example.Collection.Infrastructure
{
    public enum LockMode
    {
        None,
        Read,
        Upgrade,
        UpgradeNoWait,
        Write
    }
}
