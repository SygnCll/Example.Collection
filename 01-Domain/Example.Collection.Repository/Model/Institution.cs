using System.Collections.Generic;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Repository.Model
{
    public partial class Institution : Entity<Institution, long>
    {
        protected Institution()
        {
            this.Localization = new HashSet<InstitutionLocalization>();
            this.InstitutionLocalization = new Dictionary<string, InstitutionLocalization>();
        }

        public virtual string Code { get; protected set; }
        public virtual StatusTypeEnum Status { get; protected set; }
        public virtual int CurrenyCode { get; protected set; }


        public virtual ICollection<InstitutionLocalization> Localization { get; protected set; }
        public virtual IDictionary<string, InstitutionLocalization> InstitutionLocalization { get; protected set; }
    }
}
