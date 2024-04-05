using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Models
{
    [Table("cars")]
    public class Car : BaseEntity
    {
        
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        [MaxLength(12)]
        [Unique]
        public string Vin { get; set; } = string.Empty;

    }


}
