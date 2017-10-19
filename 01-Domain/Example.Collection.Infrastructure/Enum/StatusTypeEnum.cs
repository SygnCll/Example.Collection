using System.Runtime.Serialization;

namespace Example.Collection.Infrastructure.Enum
{
    [DataContract]
    public enum StatusTypeEnum : short
    {
        [EnumMember]
        Available = 1,

        [EnumMember]
        NotAvailable = 0,

        [EnumMember]
        Deleted = -1
    }

}
