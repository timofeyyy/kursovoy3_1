using app.db.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int? AdminId { get; set; }
        public User Admin { get; set; }

        public ICollection<Messanger> Messangers { get; set; } = new List<Messanger>();
    }
}
