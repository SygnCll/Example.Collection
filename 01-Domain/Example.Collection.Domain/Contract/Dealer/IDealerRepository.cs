using System.Collections.Generic;

namespace Example.Collection.Domain.Contract
{
    public interface IDealerRepository
    {
        Dealer Add(Dealer dealer);
        Dealer AddOrUpdate(Dealer dealer);
        void Update(Dealer dealer);


        Dealer GetSingle(long id);
        Dealer GetSingle(Institution institution, string code);


        Dealer GetSingleWithLocalization(long id);
        Dealer GetSingleWithParentDealer(long id);
        Dealer GetSingleWithSubDealers(long id);

        ICollection<Dealer> GetSubDealersList(Dealer dealer); 

    }
}
