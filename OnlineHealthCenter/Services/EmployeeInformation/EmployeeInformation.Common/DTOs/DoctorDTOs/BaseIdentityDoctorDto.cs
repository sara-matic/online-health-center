using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInformation.Common.DTOs.DoctorDTOs
{
    public class BaseIdentityDoctorDto : BaseDoctorDto
    {
        public Guid Id { get; set; }
    }
}
