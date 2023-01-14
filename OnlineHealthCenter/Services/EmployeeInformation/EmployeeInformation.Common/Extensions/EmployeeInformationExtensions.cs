using EmployeeInformation.Common.Data;
using EmployeeInformation.Common.DTOs.DoctorDTOs;
using EmployeeInformation.Common.DTOs.NurseDTOs;
using EmployeeInformation.Common.Entities;
using EmployeeInformation.Common.Repositories;
using EmployeeInformation.Common.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInformation.Common.Extensions
{
    public static class EmployeeInformationExtensions
    {
        public static void AddEmployeeInformationExtensions(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeInformationContext, EmployeeInformationContext>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<INurseRepository, NurseRepository>();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
        }
    }
}
