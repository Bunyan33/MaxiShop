using MaxiShop.Domine.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Domine.Models
{
    public class Category : BaseModel
    {

        [Required]
        public string? Name { get; set; }
    }
}
