namespace Reports.API.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Comment { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
