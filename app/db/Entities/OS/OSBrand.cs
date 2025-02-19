using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities.OS
{
    public class OSBrand
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public bool IsLaptop { get; set; }
        public ICollection<OS> OS { get; set; } = new List<OS>();

    }
}
