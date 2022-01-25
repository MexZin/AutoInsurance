using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels
{
    public class OrderDetailDTO
    {
        
        [Required]
        public string Pack_name { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
