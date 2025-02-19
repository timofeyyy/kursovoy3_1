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
using app.app.Session;
using app.app.Diag.ViewModel;
using Microsoft.Win32;
using System.IO;

namespace app.app.Profile.ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly DiagVM _diagVM;
        private readonly Session.Session _session;

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
        public ICommand EditCommand { get; }
        public ICommand ChangeImageCommand { get; }

        public ProfileVM(Session.Session session, DiagVM diagVM)
        {

            _context = new Context();
            _diagVM = diagVM;
            _session = session;

            UserName = _session.User.Login;
            Password = _session.User.Password;
            Email = _session.User.Email;
            UserImage = _session.User.UserImage;

            EditCommand = new RelayCommand(Edit);
            ChangeImageCommand = new RelayCommand(ChangeImage);

        }
        private void ChangeImage(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.webp;*.bmp|Все файлы|*.*";
            if (openFileDialog.ShowDialog() == true)
                UserImage = File.ReadAllBytes(openFileDialog.FileName);
        }
        private void Edit(object value)
        {
            User user = _context.Users.Where(user => user.Login.Equals(_session.User.Login)).FirstOrDefault();

            user.Email = Email;
            user.Password = Password;
            user.Login = UserName;
            user.UserImage = UserImage;

            _context.SaveChanges();
            _session.User = user;
            _diagVM.Message = "Was sucessesfully chnaged!";

            DiagOpen(null);
        }

        private void DiagOpen(object value)
        {
            _diagVM.IsOpen = true;
        }

        private void DiagClose(object value)
        {
            _diagVM.IsOpen = false;
        }
    }
}

