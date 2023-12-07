using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.Utility;

namespace TrollMarket.DTO.Account
{
    public class RegisterDTO
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
        public string PasswordConfirmation { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; }
    }
}
