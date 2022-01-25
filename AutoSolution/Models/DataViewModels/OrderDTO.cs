using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels
{
    public class OrderDTO
    {
        public int Id_order { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        public DateTime Order_date { get; set; }
        [Required]
        public float Total { get; set; }
    }
}
