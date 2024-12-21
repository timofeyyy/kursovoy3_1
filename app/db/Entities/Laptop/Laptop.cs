using app.db.Entities.Laptop.VideoCard;
using app.db.Entities.Processor;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Laptop
{
    public class Laptop
    {
       [Key]
       public int Id { get; set; }
       public int ProducerId { get; set; }
       public Producer Producer { get; set; }
       public float Price { get; set; }
       [StringLength(50)] 
       public string Model { get; set; }
       [StringLength(20)]
       public float Width { get; set; }
       public float Height { get; set; }
       public float Wheight { get; set; }
       public int ProcessorId { get; set; }
       public ProcessorModel Processor { get; set; }
       public int OSId { get; set; }
       public OS.OS OS { get; set; }
       public int RAMMemorySize { get; set; }
       public int SSDMemorySize { get; set; }
       public int Stock { get; set; }

       public int ColorId { get; set; }
       public Color Color { get; set; }

       public int VideoCardModelId { get; set; }
       public VideoCardModel VideoCardModel { get; set; }
       public ICollection<LaptopImages> ProductImages { get; set; } = new List<LaptopImages>();
        public ICollection<Orders> Order { get; set; } = new List<Orders>();
        public ICollection<Cart> Cart { get; set; } = new List<Cart>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

        public byte[] FirstImage => ProductImages.FirstOrDefault()?.Img;
       public string IsVisible => Stock == 0 ? "Visible" : "Hidden";
        public string Name => this.GetType().Name;


    }
}
