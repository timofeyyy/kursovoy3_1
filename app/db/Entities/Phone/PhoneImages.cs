using app.db.Entities.SmartWatches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.db.Entities.Headphones;
using app.db.Entities.Laptop;

namespace app.db.Entities.Phone
{
    public class PhoneImages
    {
        [ForeignKey("Phone")]
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        [Key]
        public int Id { get; set; }
        public byte[] Img { get; set; }

    }
}
