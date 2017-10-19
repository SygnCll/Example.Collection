using System.Runtime.Serialization;

namespace Example.Collection.Infrastructure.Enum
{
    [DataContract]
    public enum ResponseCodeEnum
    {
        /// <summary>
        /// Transaction completed successfully.
        /// </summary>
        [EnumMember]
        RC0000,
        /// <summary>
        /// Technical problem. Please try again.
        /// </summary>
        [EnumMember]
        RC0001,



        /// <summary>
        /// Record not found.
        /// </summary>
        [EnumMember]
        RC0010,

        /// <summary>
        /// Operation timeout.
        /// </summary>
        [EnumMember]
        RC0031
    }
}
