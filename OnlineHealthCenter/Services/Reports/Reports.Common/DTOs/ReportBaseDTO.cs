namespace Reports.Common.DTOs
{
    public class ReportBaseDTO
    {
        public string PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string Comment { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
    }
}
