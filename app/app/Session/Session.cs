using app.db.Entities;
using app.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.app.Session
{
    public class Session : ViewModelBase
    {
        private User _user;
        public User User { get { return _user; } set { _user = value; OnPropertyChanged(nameof(User)); } }
    }
}