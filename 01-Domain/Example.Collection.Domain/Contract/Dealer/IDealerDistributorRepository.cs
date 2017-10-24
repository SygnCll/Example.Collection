using System.Collections.Generic;

namespace Example.Collection.Domain.Contract
{
    public interface IDealerDistributorRepository
    {
        void Update(DealerDistributor dealerDistributor);

        DealerDistributor GetSingle(long id);

        ICollection<Dealer> GetDealersByOutSourceDealerCode(string outSourceDealerCode);

        bool SearchByOutSourceDealerCode(long id, string outSourceDealerCode, long distributorId);
    }
}
