using Example.Collection.Domain.Contract;

namespace Example.Collection.Domain.ServiceContext
{
    public class DealerServiceContext
    {
        public DomainConfig DomainConfig { get; set; }

        public IDealerRepository DealerRepository { get; set; }

        public IInstitutionRepository InstitutionRepository { get; set; }
    }
}
