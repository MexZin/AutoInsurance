using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels
{
    public class PackageDTO
    {

        [Required]
        public string Pack_Name { get; set; }

        
        public string Description { get; set; }
        [Required]

        public float Price { get; set; }
    }
}
