namespace Reports.Common.DTOs
{
    public class ReportBaseDTO
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Comment { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
    }
}
