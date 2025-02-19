using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Laptop
{
    public class LaptopImages
    {
        [ForeignKey("Laptop")]
        public int LaptopId { get; set; }
        public Laptop Laptop { get; set; }
        [Key]
        public int Id { get; set; }
        public byte[] Img { get; set; }
    }
}
