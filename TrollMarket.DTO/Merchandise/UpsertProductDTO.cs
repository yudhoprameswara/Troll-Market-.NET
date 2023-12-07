using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollMarket.DTO.Merchandise
{
    public class UpsertProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name cannot be blank")]
        public string Name { get; set; }

        public int sellerId { get; set; }

        [Required(ErrorMessage = "Category cannot be blank")]
        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Discontinue { get; set; }


    }
}
