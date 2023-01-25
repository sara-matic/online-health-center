using IdentityServer.Data.EntityTypeConfigurations.Constants;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Data.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Navigation(u => u.RefreshTokens).AutoInclude();

            var patient = new User
            {
                Id = UserConstants.PatientId,
                FirstName = "James",
                LastName = "Brown",
                UserName = "jamesbrown",
                NormalizedUserName = "JAMESBROWN"
            };

            var doctor = new User
            {
                Id = UserConstants.DoctorId,
                FirstName = "Alan",
                LastName = "Stern",
                UserName = "alanstern",
                NormalizedUserName = "ALANSTERN"
            };

            var nurse = new User
            {
                Id = UserConstants.NurseId,
                FirstName = "Rachel",
                LastName = "Gray",
                UserName = "rachelgray",
                NormalizedUserName = "RACHELGRAY"
            };

            var passwordHasher = new PasswordHasher<User>();
            patient.PasswordHash = passwordHasher.HashPassword(patient, "aXc3o!9+U");
            doctor.PasswordHash = passwordHasher.HashPassword(doctor, "-5@HoNqr7L");
            nurse.PasswordHash = passwordHasher.HashPassword(nurse, "Fdj+94ks#");

            builder.HasData(patient, doctor, nurse);
        }
    }
}
