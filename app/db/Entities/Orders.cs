using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using app.db.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Count { get; set; }
        [StringLength(30)]
        public string Status { get; set; }
        [StringLength(100)]
        public string Adress { get; set; }
        public int? LaptopId { get; set; }
        public Laptop.Laptop Laptop { get; set; }

        public int? HeadphonesId { get; set; }
        public Headphones.Headphones Headphones { get; set; }

        public int? PhonesId { get; set; }
        public Phone.Phone Phone { get; set; }

        public int? SmartWatchesId { get; set; }
        public SmartWatches.SmartWatch SmartWatch { get; set; }
    }
}
