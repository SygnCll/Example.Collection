
using System.Linq;
using Example.Collection.Domain;
using System.Collections.Generic;
using Example.Collection.Infrastructure;
using Example.Collection.Domain.Contract;
using Example.Collection.Infrastructure.Enum;
using NHibernate.Linq;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Util;


namespace Example.Collection.Repository
{
    public class DealerRepository : BaseEntityRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(IRepository<Dealer> repository) : base(repository)
        {
        }

        public Dealer GetSingle(long id)
        {
            var items = base.Repository.Get(q => q.Id == id && q.Status != StatusTypeEnum.Deleted).ToList();
            return items.FirstOrDefault();
        }

        public Dealer GetSingle(Institution institution, string code)
        {
            return base.Repository.Get(q => q.Institution == institution && q.DealerCode == code && q.Status != StatusTypeEnum.Deleted)
                       .ToList()
                       .FirstOrDefault();
        }

        public Dealer GetSingleWithLocalization(long id)
        {
            var items = base.Repository
                            .Get(dealer => dealer.Id == id && dealer.Status != StatusTypeEnum.Deleted)
                            .Fetch(item => item.DealerLocalization)
                            .ToList();

            items.ForEach(item =>
            {
                item.DealerLocalization.ProxyInitialize();
            });

            return items.FirstOrDefault();
        }

        public Dealer GetSingleWithParentDealer(long id)
        {
            var items = base.Repository
                            .Get(q => q.Id == id && q.Status != StatusTypeEnum.Deleted)
                            .Fetch(f => f.ParentDealer)
                            .Fetch(f => f.DealerLocalization)
                            .ToList();

            items.ForEach(item =>
            {
                item.ParentDealer.ProxyInitialize();
                item.DealerLocalization.ProxyInitialize();
            });

            return items.FirstOrDefault();
        }

        public Dealer GetSingleWithSubDealers(long id)
        {
            var session = base.Repository.CurrentSession() as ISession;

            Dealer subDealer = null;
            DealerLocalization dealerLocalization = null;

            var items = session.QueryOver<Dealer>()
                               .Where(dealer => dealer.Id == id && dealer.Status != StatusTypeEnum.Deleted)
                               .JoinAlias(x => x.SubDealers, () => subDealer, JoinType.LeftOuterJoin)
                               .JoinAlias(() => subDealer.DealerLocalization, () => dealerLocalization, JoinType.LeftOuterJoin)
                               .Where(() => subDealer.Status == StatusTypeEnum.Available)
                               .List();

            items.ForEach(item =>
            {
                item.SubDealers.ProxyInitialize();
                item.SubDealers.ForEach(dealer =>
                {
                    dealer.DealerLocalization.ProxyInitialize();
                });
            });

            return items.FirstOrDefault();
        }

        public ICollection<Dealer> GetSubDealersList(Dealer dealer)
        {
            var session = base.Repository.CurrentSession() as ISession;

            //DealerHierarchy dealerHierarchyAlias = null;
            //DealerHierarchyDealer dealerHierarchyDealerAlias = null;
            DealerDistributor dealerDistributorAlias = null;
            Dictionary<string, DealerLocalization> dealerLocalizationAlias = null;

            var query = session.QueryOver<Dealer>()
                    //.JoinAlias(d => d.DealerHierarchy, () => dealerHierarchyAlias, JoinType.LeftOuterJoin)
                    //.JoinAlias(() => dealerHierarchyAlias.DealerHierarchyDealers, () => dealerHierarchyDealerAlias, JoinType.LeftOuterJoin)
                    .JoinAlias(d => d.DealerDistributors, () => dealerDistributorAlias, JoinType.LeftOuterJoin)
                    .JoinAlias(d => d.DealerLocalization, () => dealerLocalizationAlias, JoinType.LeftOuterJoin)
                    //.Where(() => dealerHierarchyDealerAlias.Dealer == dealer);
                    ;
            return query.Future().ToList();
        }
    }
}
