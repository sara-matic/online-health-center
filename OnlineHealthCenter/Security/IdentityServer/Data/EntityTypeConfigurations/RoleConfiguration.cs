using IdentityServer.Data.EntityTypeConfigurations.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.EntityTypeConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = RoleConstants.PatientId,
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new IdentityRole
                {
                    Id = RoleConstants.DoctorId,
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                },
                new IdentityRole
                {
                    Id = RoleConstants.NurseId,
                    Name = "Nurse",
                    NormalizedName = "NURSE"
                }
            );
        }
    }
}
