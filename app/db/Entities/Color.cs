using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Value { get; set; }
        public ICollection<Laptop.Laptop> Laptops { get; set; } = new List<Laptop.Laptop>();
        public ICollection<Phone.Phone> Phones{ get; set; } = new List<Phone.Phone>();
        public ICollection<SmartWatches.SmartWatch> SmartWatches { get; set; } = new List<SmartWatches.SmartWatch>();
        public ICollection<Headphones.Headphones> Headphones{ get; set; } = new List<Headphones.Headphones>();
    }
}
