using System.Collections.Generic;
using Example.Collection.Repository.Model;


namespace Example.Collection.Repository.Contract
{
    public interface IDealerRepository
    {
        Dealer Add(Dealer dealer);
        Dealer AddOrUpdate(Dealer dealer);
        

        Dealer GetSingle(long id);
        Dealer GetSingle(Institution institution, string code);


        Dealer GetSingleWithLocalization(long id);
        Dealer GetSingleWithParentDealer(long id);
        Dealer GetSingleWithSubDealers(long id);

        ICollection<Dealer> GetSubDealersList(Dealer dealer); 

    }
}
