using app.app.Navigation;
using app.utils;
using app.app.Client.Home.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using app.app.Diag.ViewModel;
using System.Windows;
using app.app.Profile.ViewModel;
using app.app.Client.Cart.ViewModel;
using app.app.Client.Orders.ViewModel;
using app.db.Entities;
using app.app.Admin.Navigation;

namespace app.app.Client.Main.ViewModel
{
    public class ClientVM : ViewModelBase
    {
        private readonly PageNavigation _pageNavigation;
        private readonly ContentNavigation _contentNavigation;
        private readonly DiagVM _diagVM;
        //private readonly User _user;
        private readonly Session.Session _session;

        private object _currentVew;
        private string _diagHeight;
        private string _bgBlur;
        private string _diagMessage;

        public ICommand LogOutCommand { get; }
        public ICommand HomeCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand CartCommand { get; }
        public ICommand OrdersCommand { get; }
        public ICommand DiagOpenCommand { get; }
        public ICommand DiagCloseCommand { get; }
        public ICommand HelpCommand { get; }

        public object CurrentView
        {
            get { return _contentNavigation.CurrentView; }
            set { _contentNavigation.CurrentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }
        public string DiagHeight
        {
            get => _diagHeight;
            set { _diagHeight = value; OnPropertyChanged(nameof(DiagHeight)); }
        } 
        public string BgBlur
        {
            get => _bgBlur;
            set { _bgBlur = value; OnPropertyChanged(nameof(BgBlur)); }
        }
        public string DiagMessage
        {
            get { return _diagMessage; }
            set
            {
                _diagMessage = value;
                OnPropertyChanged(nameof(DiagMessage));
            }
        }
        public ClientVM(PageNavigation pageNavigation, Session.Session session)
        {
            //_user = user;
            _session = session;
            _diagVM = new DiagVM();
            _diagVM.PropertyChanged += (s, e) => DisplayDiag();
            _pageNavigation = pageNavigation;
            _contentNavigation = new ContentNavigation();
            _contentNavigation.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CurrentView));
            LogOutCommand = new RelayCommand(LogOut);
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            DiagOpenCommand = new RelayCommand(DiagOpen);
            DiagCloseCommand = new RelayCommand(DiagClose);
            CartCommand = new RelayCommand(Cart);
            OrdersCommand = new RelayCommand(Orders);
            HelpCommand = new RelayCommand(Help);
            Home(null);
            DiagClose(null);
            
        }

        private void DisplayDiag()
        {
            DiagMessage = _diagVM.Message;
            DiagHeight = _diagVM.IsOpen ? "Auto" : "0";
            BgBlur = _diagVM.IsOpen ? "10" : "0";
        }

        private void LogOut(object value)
        {
            _pageNavigation.CurrentView = new Login.ViewModel.LogInVM(_pageNavigation);
        }

        private void Home(object value)
        {
            CurrentView = new HomeVM(_contentNavigation, _diagVM, _session);
        }
        private void Cart(object value)
        {
            CurrentView = new CartVM(_session, _diagVM);
        }
        private void Profile(object value)
        {
            CurrentView = new ProfileVM(_session, _diagVM);
        }
        private void Orders(object value)
        {
            CurrentView = new OrdersVM(_session, _diagVM);
        }
        private void Help(object value)
        {
            CurrentView = new Messanger.ViewModel.MessangerVM(_session, _diagVM);
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
