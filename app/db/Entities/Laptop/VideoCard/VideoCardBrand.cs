using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.Laptop.VideoCard
{
    public class VideoCardBrand
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<VideoCardModel> VideoCardModels { get; set; } = new List<VideoCardModel>();

    }
}
