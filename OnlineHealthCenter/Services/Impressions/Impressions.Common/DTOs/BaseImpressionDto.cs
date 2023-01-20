using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impressions.Common.DTOs
{
    public class BaseImpressionDto
    {
        public string DoctorID { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public decimal Mark { get; set; }
    }
}
