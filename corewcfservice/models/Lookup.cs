namespace corewcfservice.models
{
    [DataContract]
    public class Lookup
    {
        [DataMember]
        public int Lookup_id { get; set; }

        [DataMember]
        public string? Lookup_type { get; set; }

        [DataMember]
        public string? Lookup_desc { get; set; }

        [DataMember]
        public string? Is_active { get; set; }

        [DataMember]
        public string? Createdby { get; set; }

        [DataMember]
        public DateTime CreatedDATE { get; set; }

        [DataMember]
        public string? Updatedby { get; set; }

        [DataMember]
        public DateTime? UpdatedDATE { get; set; }
    }
}