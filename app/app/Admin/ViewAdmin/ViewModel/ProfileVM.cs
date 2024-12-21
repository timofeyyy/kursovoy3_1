using app.db.Entities;
using app.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using app.db.Context;
using app.app.Diag.ViewModel;
using app.app.Admin.Navigation;

namespace app.app.Admin.ViewAdmin.ViewModel
{
    public class ProfileAdminVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly User _user;
        private readonly DiagVM _diagVM;

        private string _diagMessage;
        private string _userName;
        private string _password;
        private string _email;
        private byte[] _userImage;
       
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public byte[] UserImage
        {
            get { return _userImage; }
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        public ICommand BackCommand { get; }

        public ProfileAdminVM(ContentNavigation contentNavigation, User user, DiagVM diagVM) {

            _context = new Context();
            _diagVM = diagVM;
            _user = user;
            _contentNavigation = contentNavigation;

            UserName = user.Login;
            Password = user.Password;
            Email = user.Email;
            UserImage = user.UserImage;

            BackCommand = new RelayCommand(Back);
        }



        private void Back(object value)
        {
            _contentNavigation.CurrentView = new Users.ViewModel.UsersVM(_contentNavigation, _diagVM);
        }
    }
}

