namespace Impressions.Common.Entities
{
    public class Impression
    {
        public Guid Id { get; set; }
        public string PatientID { get; set; }
        public string DoctorID { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public decimal Mark { get; set; }
        public DateTime ImpressionDateTime { get; set; }
    }
}
