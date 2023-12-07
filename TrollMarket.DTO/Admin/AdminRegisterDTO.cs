using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Admin
{
    public class AdminRegisterDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Password COnfirmation is required")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        [Compare("Password", ErrorMessage = "Password not match!")]
        public string ConfirmPassword { get; set; }

    }
}
