﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impressions.Common.DTOs
{
    public class CreateImpressionDto : BaseImpressionDto
    {
        public string PatientID { get; set; }
    }
}
