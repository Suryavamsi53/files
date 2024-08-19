using System.ServiceModel;

    namespace corewcfservice.models{
    [DataContract]
    public class Account
    {
        [DataMember]
        public int AccId { get; set; }

        [DataMember]
        public string ?AccountNumber { get; set; }

        [DataMember]
        public int AccountStatus_id { get; set; }

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