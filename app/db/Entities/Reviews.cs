using app.db.Entities.Headphones;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using app.db.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.app.Client.Products.SmartWatches.Model;
using System.Collections.ObjectModel;

namespace app.db.Entities
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? LaptopId { get; set; }
        public Laptop.Laptop Laptop { get; set; }

        public int? HeadphonesId { get; set; }
        public Headphones.Headphones Headphones { get; set; }

        public int? PhonesId { get; set; }
        public Phone.Phone Phone { get; set; }

        public int? SmartWatchesId { get; set; }
        public SmartWatches.SmartWatch SmartWatch { get; set; }
        public int Stars { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        public ObservableCollection<Star> StarsOC
        {
            get
            {
                var oc = new ObservableCollection<Star>();
                for (int i = 0; i < Stars; i++)
                {
                    oc.Add(new Star()
                    {
                        Path = "pack://application:,,,/res/icons/selected-star.svg"
                    });
                }
                return oc;
            }
        }

    }
}
