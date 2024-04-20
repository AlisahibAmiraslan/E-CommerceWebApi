using System.ComponentModel.DataAnnotations;

namespace NextEcommerceWebApi.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Current password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmNewPassword { get; set;}
    }
}
