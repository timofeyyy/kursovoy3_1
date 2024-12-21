using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Laptop.VideoCard
{
    public class VideoCardModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public VideoCardBrand Brand { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public float BaseFrequency { get; set; }
        public float BoostFrequency { get; set; }
        public ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    }
}
