using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Profile
{
    public class TopUpDTO
    {
        [Required(ErrorMessage = "Value Cannot be blank")]
        [Range( 5000, 2147483647, ErrorMessage = "Minimum 5000")]
        public decimal TopUp { get; set; }
    }
}
