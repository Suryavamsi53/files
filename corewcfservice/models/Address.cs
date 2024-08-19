using System;
using System.Runtime.Serialization;

namespace corewcfservice.models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        public int AddressTypeID { get; set; }

        [DataMember]
        public string ?Addres { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string? CreatedBy { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public string? UpdatedBy { get; set; }
    }
}