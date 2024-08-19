using System;
using System.Runtime.Serialization;

namespace corewcfservice.models
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public int TransID { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        public int TransTypeID { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string ?Transaction_type { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ?CreatedBy { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public string ?UpdatedBy { get; set; }
    }
}