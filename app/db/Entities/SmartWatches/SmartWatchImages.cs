using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.SmartWatches
{
    public class SmartWatchImages
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("SmartWatch")]
        public int SmartWatchesId { get; set; }
        public SmartWatch SmartWatch { get; set; }
        public byte[] Img { get; set; }
    }
}
