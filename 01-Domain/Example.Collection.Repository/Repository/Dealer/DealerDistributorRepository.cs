using System.Linq;
using System.Collections.Generic;
using Example.Collection.Infrastructure;
using Example.Collection.Repository.Model;
using Example.Collection.Repository.Contract;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Repository
{
    public class DealerDistributorRepository : BaseEntityRepository<DealerDistributor>, IDealerDistributorRepository
    {
        public DealerDistributorRepository(IRepository<DealerDistributor> repository) : base(repository)
        {
        }

        public DealerDistributor GetSingle(long id)
        {
            return base.Repository
                       .Get(q => q.Id == id && q.Status != StatusTypeEnum.Deleted)
                       .ToList()
                       .FirstOrDefault();
        }

        public new void Update(DealerDistributor dealerDistributor) => Repository.Update(dealerDistributor);

        public ICollection<Dealer> GetDealersByOutSourceDealerCode(string outSourceDealerCode)
        {
            return Repository.Get(q => q.Status != StatusTypeEnum.Deleted &&
                                       q.OutSourceDealerCode == outSourceDealerCode)
                             .Select(x => x.Dealer)
                             .ToList();
        }

        public bool SearchByOutSourceDealerCode(long id, string outSourceDealerCode, long distributorId)
        {
            return Repository.Get(q => q.Id != id &&
                                       q.Status != StatusTypeEnum.Deleted &&
                                       (q.OutSourceDealerCode == outSourceDealerCode && q.Distributor.Id == distributorId))
                             .Count() > 0;
        }
    }
}
