using FluentNHibernate.Mapping;
using Example.Collection.Domain;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Repository.NHibernateMapping
{
    public partial class DealerMap : ClassMap<Dealer>
    {
        public DealerMap()
        {
            Table("Dealer");
            LazyLoad();
            Id(x => x.Id);
            Map(x => x.DealerCode).Length(25).UniqueKey("Unique_DealerInstitution");
            Map(x => x.ParentDealerId).CustomType<long>().ReadOnly();
            Map(x => x.ParentDealerCode).Length(25);
            Map(x => x.DealerTypeId).CustomType<long>().ReadOnly();
            Map(x => x.Status).CustomType(typeof(StatusTypeEnum)).Not.Nullable();
            Map(x => x.InstitutionId).CustomType<long>().ReadOnly();
            Map(x => x.RegionId).CustomType<long>().ReadOnly();
            Map(x => x.CityId).CustomType<long>().ReadOnly();
            Map(x => x.TownId).CustomType<long>().ReadOnly();
            Map(x => x.Address).Length(1000);
            Map(x => x.PostalCode).Length(15);
            Map(x => x.Longitude).Precision(10).Scale(7).Nullable();
            Map(x => x.Latitude).Precision(10).Scale(7).Nullable();
            Map(x => x.ProxyOwnerDealerId).CustomType<long>().ReadOnly();
            Map(x => x.WorkStartTime).CustomType(typeof(WorkTimeEnum)).Not.Nullable();
            Map(x => x.WorkEndTime).CustomType(typeof(WorkTimeEnum)).Not.Nullable();
            Map(x => x.CreatedOn).Not.Nullable();
            Map(x => x.CreatedBy).Not.Nullable();
            Map(x => x.ModifiedBy);
            Map(x => x.ModifiedOn);


            HasMany(x => x.SubDealers).AsSet().LazyLoad().Inverse().Generic().KeyColumns.Add("ParentDealerId", mapping => mapping.Name("ParentDealerId").Nullable());
            HasMany(x => x.ProxyOwnerDealers).AsSet().LazyLoad().Inverse().Generic().KeyColumns.Add("ProxyOwnerDealerId", mapping => mapping.Name("ProxyOwnerDealerId").Nullable());
            HasMany(x => x.Localization).AsSet().ReadOnly().LazyLoad().Generic().KeyColumns.Add("DealerId", mapping => mapping.Name("DealerId").Not.Nullable());
            HasMany(x => x.DealerLocalization).AsMap<string>("LanguageCode").LazyLoad().Generic().KeyColumns.Add("DealerId", mapping => mapping.Name("DealerId").Nullable());
            HasMany(x => x.DealerEmailAddresses).AsSet().Cascade.AllDeleteOrphan().LazyLoad().Inverse().Generic().KeyColumns.Add("DealerId", mapping => mapping.Name("DealerId").Not.Nullable());
            HasMany(x => x.DealerPhoneNumbers).AsSet().Cascade.AllDeleteOrphan().LazyLoad().Inverse().Generic().KeyColumns.Add("DealerId", mapping => mapping.Name("DealerId").Not.Nullable());


            References(x => x.ParentDealer).LazyLoad().Columns("ParentDealerId");
            References(x => x.DealerType).LazyLoad().Columns("DealerTypeId");
            References(x => x.Institution).LazyLoad().Columns("InstitutionId");
            References(x => x.Region).LazyLoad().Columns("RegionId");
            References(x => x.City).LazyLoad().Columns("CityId");
            References(x => x.Town).LazyLoad().Columns("TownId");
            References(x => x.ProxyOwnerDealer).LazyLoad().Columns("ProxyOwnerDealerId");
        }
    }
}
