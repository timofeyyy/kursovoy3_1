using app.db.Entities.Laptop;
using app.db.Entities.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace app.db.Entities.Phone
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        public int ProcessorId { get; set; }
        public ProcessorModel Processor { get; set; }
        [StringLength(100)]
        public string Model { get; set; }
        public float Price { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [StringLength(30)]
        public float Width { get; set; }
        public float Height { get; set; }
        public float Wheight { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int OSId { get; set; }
        public OS.OS OS { get; set; }
        public int RAM { get; set; }
        public int InternalMemorySize { get; set; }
        public int Camera { get; set; }
        public int Battery { get; set; }
        public bool WaterProtection { get; set; }
        public ICollection<PhoneImages> ProductImages { get; set; } = new List<PhoneImages>();
        public int Stock { get; set; }
        public ICollection<Orders> Order { get; set; } = new List<Orders>();
        public ICollection<Cart> Cart { get; set; } = new List<Cart>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();


        public byte[] FirstImage => ProductImages.FirstOrDefault()?.Img;
        public string IsVisible => Stock == 0 ? "Visible" : "Hidden";
        public string Name => this.GetType().Name;


    }
}
