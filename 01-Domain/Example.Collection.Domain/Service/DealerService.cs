using System;
using System.Linq;
using System.Collections.Generic;
using Example.Collection.Domain.Contract;
using Example.Collection.Domain.ServiceContext;


namespace Example.Collection.Domain.Service
{
    public class DealerService : DomainServiceBase
    {
        public DealerService(DealerServiceContext dealerServiceContext)
        {
            dealerRepository = dealerServiceContext.DealerRepository;
            domainConfig = dealerServiceContext.DomainConfig;
            institutionRepository = dealerServiceContext.InstitutionRepository;
        }

        protected readonly IDealerRepository dealerRepository;
        private readonly DomainConfig domainConfig;
        private readonly IInstitutionRepository institutionRepository;


        public void UpdateDealer(Dealer dealer)
        {
            this.dealerRepository.Update(dealer);
        }


        public virtual Dealer GetDealer(long dealerId)
        {
            return this.dealerRepository.GetSingle(dealerId);
        }

        public virtual Dealer GetDealer(Institution institution, string code)
        {
            return this.dealerRepository.GetSingle(institution, code);
        }

        public virtual Dealer GetDealerWithLocalization(long dealerId)
        {
            return this.dealerRepository.GetSingleWithLocalization(dealerId);
        }

        public virtual ICollection<Dealer> GetSubDealers(Dealer dealer)
        {
            return this.dealerRepository.GetSubDealersList(dealer);
        }
    }
}
