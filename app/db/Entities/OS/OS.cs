using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.db.Entities.Laptop;

namespace app.db.Entities.OS
{
    public class OS
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public OSBrand Brand { get; set; }
        [StringLength(30)]
        public string Version { get; set; }
        public ICollection<Laptop.Laptop> Laptops { get; set; } = new List<Laptop.Laptop>();
        public ICollection<Phone.Phone> Phones { get; set; } = new List<Phone.Phone>();
        public ICollection<SmartWatches.SmartWatch> SmartWatches { get; set; } = new List<SmartWatches.SmartWatch>();


    }
}
