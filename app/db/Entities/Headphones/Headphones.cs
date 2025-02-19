using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Headphones
{
    public class Headphones
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        public float Price { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public bool Wireless { get; set; }
        public ICollection<HeadphonesImages> ProductImages { get; set; } = new List<HeadphonesImages>();
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public float Wheight{ get; set; }
        public int Stock { get; set; }
        public ICollection<Orders> Order { get; set; } = new List<Orders>();
        public ICollection<Cart> Cart { get; set; } = new List<Cart>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

        public byte[] FirstImage => ProductImages.FirstOrDefault()?.Img;
        public string IsVisible => Stock == 0 ? "Visible" : "Hidden";
        public string Name => this.GetType().Name;

    }
}
