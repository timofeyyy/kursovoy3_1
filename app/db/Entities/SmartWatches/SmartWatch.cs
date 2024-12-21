using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.SmartWatches
{
    public class SmartWatch
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        public float Price { get; set; }
        public float Wheight { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [StringLength(20)]
        public float Width { get; set; }
        public float Height { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int OSId { get; set; }
        public db.Entities.OS.OS OS { get; set; }
        public bool Wifi { get; set; }
        public bool Bleatouth { get; set; }
        public bool Gps { get; set; }
        public bool Calls { get; set; }
        public ICollection<SmartWatchImages> ProductImages { get; set; } = new List<SmartWatchImages>();
        public int Stock { get; set; }


        public ICollection<Cart> Cart { get; set; } = new List<Cart>();
        public ICollection<Orders> Order { get; set; } = new List<Orders>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

        public byte[] FirstImage => ProductImages.FirstOrDefault()?.Img;
        public string IsVisible => Stock == 0 ? "Visible" : "Hidden";
        public string Name => this.GetType().Name;

    }
}
