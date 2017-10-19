using System.Collections.Generic;
using Example.Collection.Repository.Model;

namespace Example.Collection.Repository.Contract
{
    public interface IDealerDistributorRepository
    {
        void Update(DealerDistributor dealerDistributor);
        DealerDistributor GetSingle(long id);
        ICollection<Dealer> GetDealersByOutSourceDealerCode(string outSourceDealerCode);
        bool SearchByOutSourceDealerCode(long id, string outSourceDealerCode, long distributorId);
    }
}
