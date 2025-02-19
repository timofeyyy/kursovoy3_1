using app.db.Entities.Laptop.VideoCard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Processor
{
    public class ProcessorBrand
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<ProcessorModel> ProcessorModels { get; set; } = new List<ProcessorModel>();

    }
}
