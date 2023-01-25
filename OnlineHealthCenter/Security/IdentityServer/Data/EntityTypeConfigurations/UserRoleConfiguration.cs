using IdentityServer.Data.EntityTypeConfigurations.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.EntityTypeConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = RoleConstants.PatientId,
                    UserId = UserConstants.PatientId
                },
                new IdentityUserRole<string>
                {
                    RoleId = RoleConstants.DoctorId,
                    UserId = UserConstants.DoctorId
                },
                new IdentityUserRole<string>
                {
                    RoleId = RoleConstants.NurseId,
                    UserId = UserConstants.NurseId
                }
            );
        }
    }
}
