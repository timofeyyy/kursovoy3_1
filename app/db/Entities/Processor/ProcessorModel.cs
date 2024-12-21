using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Processor
{
    public class ProcessorModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public ProcessorBrand Brand { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsLaptop { get; set; }
        public float BaseFrequency { get; set; }
        public float BoostFrequency { get; set; }
        public ICollection<Laptop.Laptop> Laptops { get; set; } = new List<Laptop.Laptop>();
        public ICollection<Phone.Phone> Phones { get; set; } = new List<Phone.Phone>();

    }
}
