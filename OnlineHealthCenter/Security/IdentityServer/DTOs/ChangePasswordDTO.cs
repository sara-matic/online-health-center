using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Old password is required")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; } = null!;
    }
}
