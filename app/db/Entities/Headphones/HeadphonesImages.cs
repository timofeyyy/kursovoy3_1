using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Headphones
{
    public class HeadphonesImages
    {
        [ForeignKey("Headphones")]
        public int HeadphonesId { get; set; }
        public Headphones Headphones { get; set; }
        [Key]
        public int Id { get; set; }
        public byte[] Img { get; set; }
    }
}
