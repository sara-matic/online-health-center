using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInformation.Common.DTOs.DoctorDTOs
{
    public class BaseDoctorDto : BaseEmployeeDto
    {
        public string MedicalSpecialty { get; set; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public decimal Mark { get; set; }
    }
}
