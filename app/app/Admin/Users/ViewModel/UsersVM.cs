using app.app.Admin.Navigation;
using app.app.Admin.ViewAdmin.ViewModel;
using app.app.Admin.ViewUser.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app.app.Admin.Users.ViewModel
{
    public class UsersVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly DiagVM _diagVM;

        private ObservableCollection<User> _users;

        private bool _isAdmin;
        private string _clientHighlight;
        private string _adminHighlight;
        private string _searchValue;
        public string ClientHighlight
        {
            get { return _clientHighlight; }
            set
            {
                _clientHighlight = value;
                OnPropertyChanged(nameof(ClientHighlight));
            }
        }
        public string AdminHighlight
        {
            get { return _adminHighlight; }
            set
            {
                _adminHighlight = value;
                OnPropertyChanged(nameof(AdminHighlight));
            }
        }
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
            }
        }
      
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand SetectAdminUsersCommand { get; }
        public ICommand SelectClientUsersCommand { get; }
        public ICommand FindCommand { get; }
        public ICommand OpenProfileCommand { get; }

        public UsersVM(ContentNavigation contentNavigation, DiagVM diag) {
            _diagVM = diag;
            _contentNavigation = contentNavigation;
            //_contentNavigation.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CurrentView));
            _context = new Context();
            Users = new ObservableCollection<User>(_context.Users.ToList());
            SetectAdminUsersCommand = new RelayCommand(SelectAdminUsers);
            SelectClientUsersCommand = new RelayCommand(SelectClientUsers);
            OpenProfileCommand = new RelayCommand(SelectClientUsers);
            FindCommand = new RelayCommand(Find);
            SelectClientUsers(null);
        }

        private void SelectAdminUsers(object value)
        {
            Users.Clear();
            foreach (var item in _context.Users.Where(user => user.IsAdmin).ToList())
                Users.Add(item);
            ClientHighlight = "0,0,0,0";
            AdminHighlight = "0,0,0,2";
            _isAdmin = true;
            SearchValue = "";
        }
        private void SelectClientUsers(object value)
        {
            Users.Clear();
            foreach (var item in _context.Users.Where(user => !user.IsAdmin).ToList())
                Users.Add(item);
            ClientHighlight = "0,0,0,2";
            AdminHighlight = "0,0,0,0";
            _isAdmin = false;
            SearchValue = "";
        }

        private void Find(object value)
        {
            Users.Clear();

            if(SearchValue.IsNullOrEmpty())
                foreach (var item in _context.Users.Where(user => (_isAdmin ? user.IsAdmin : !user.IsAdmin)).ToList())
                    Users.Add(item);
            else
                foreach (var item in _context.Users.Where(user => (_isAdmin ? user.IsAdmin : !user.IsAdmin) && user.Login.Contains(SearchValue)).ToList())
                    Users.Add(item);

        }

        public void OpenProfile(User user)
        {

            _contentNavigation.CurrentView = user.IsAdmin ? new ProfileAdminVM(_contentNavigation, user, _diagVM) : new ProfileClientVM(_contentNavigation, user, _diagVM);
        }

    }
}
