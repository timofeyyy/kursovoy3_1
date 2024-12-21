using app.app.Admin.Messangers.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.db.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Login { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] UserImage { get; set; }
        public ICollection<Orders> Order { get; set; } = new List<Orders>();
        public ICollection<Cart> Cart { get; set; } = new List<Cart>();
        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
        public ICollection<Chat> ChatsAsUser { get; set; } = new List<Chat>();
        public ICollection<Chat> ChatsAsAdmin { get; set; } = new List<Chat>();

        public string Position => IsAdmin ? "Left" : "Right";


    }
}
