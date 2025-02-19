using app.app.Admin.Navigation;
using app.app.Profile.ViewModel;
using app.app.Admin.Users.ViewModel;
using app.app.Client.Cart.View;
using app.app.Client.Orders.View;
using app.app.Diag.ViewModel;
using app.app.Navigation;
using app.db.Entities;
using app.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using app.db.Context;
using System.Windows;
using app.app.Admin.Reviews.ViewModel;
using app.app.Admin.Messangers.ViewModel;

namespace app.app.Admin.Main.ViewModel
{
    public class AdminVM : ViewModelBase
    {
        private readonly PageNavigation _pageNavigation;
        private readonly ContentNavigation _contentNavigation;
        private readonly DiagVM _diagVM;
        private readonly Session.Session _session;

        private string _diagHeight;
        private string _bgBlur;
        private string _diagMessage;

        public ICommand LogOutCommand { get; }
        public ICommand ViewUsersCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand DiagOpenCommand { get; }
        public ICommand DiagCloseCommand { get; }
        public ICommand ProductsCommand { get; } 
        public ICommand AddLaptopCommand { get; }
        public ICommand ViewLaptopTableCommand { get; }
        public ICommand AddPhoneCommand { get; }
        public ICommand ViewPhoneTableCommand { get; }
        public ICommand AddSmartWatchesCommand { get; }
        public ICommand ViewSmartWatchesTableCommand { get; }
        public ICommand AddHeadphonesCommand { get; }
        public ICommand ViewHeadphonesTableCommand { get; }
        public ICommand ViewReviewsCommand { get; }
        public ICommand ViewOrdersCommand { get; }
        public ICommand ViewMessangerCommand { get; }

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
        public AdminVM (PageNavigation pageNavigation, Session.Session session)
        {
            _contentNavigation = new ContentNavigation();
            _session = session;
            _pageNavigation = pageNavigation;
            _diagVM = new DiagVM();

            _diagVM.PropertyChanged += (s, e) => DisplayDiag();
            _contentNavigation.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CurrentView));
            LogOutCommand = new RelayCommand(LogOut);
            ViewUsersCommand = new RelayCommand(ViewUsers);
            ProfileCommand = new RelayCommand(Profile);
            DiagOpenCommand = new RelayCommand(DiagOpen);
            DiagCloseCommand = new RelayCommand(DiagClose);

            AddLaptopCommand = new RelayCommand(AddLaptop);
            ViewLaptopTableCommand = new RelayCommand(ViewLaptopTable);
            AddPhoneCommand = new RelayCommand(AddPhone);
            ViewPhoneTableCommand = new RelayCommand(ViewPhoneTable);
            AddSmartWatchesCommand = new RelayCommand(AddSmartWatches);
            ViewSmartWatchesTableCommand = new RelayCommand(ViewSmartWatchesTable);
            AddHeadphonesCommand = new RelayCommand(AddHeadphones);
            ViewHeadphonesTableCommand = new RelayCommand(ViewHeadphonesTable);
            ViewReviewsCommand = new RelayCommand(ViewReviews);
            ViewOrdersCommand = new RelayCommand(ViewOrders);
            ViewMessangerCommand = new RelayCommand(ViewMessanger);
           
            DiagClose(null);
        }
        private void AddLaptop(object value)
        {
            CurrentView = new Products.Laptop.Add.First.ViewModel.FirstVM(_contentNavigation, _diagVM, null);
        }

        private void ViewLaptopTable(object value)
        {
            CurrentView = new Products.Laptop.Table.ViewModel.TableVM(_contentNavigation, _diagVM);

        }
        private void AddPhone(object value)
        {
            CurrentView = new Products.Phone.Add.First.ViewModel.FirstVM(_contentNavigation, _diagVM, null);
        }
        private void ViewPhoneTable(object value)
        {
            CurrentView = new Products.Phone.Table.ViewModel.TableVM(_contentNavigation, _diagVM);

        }
        private void AddSmartWatches(object value)
        {
            CurrentView = new Products.SmartWatches.Add.First.ViewModel.FirstVM(_contentNavigation, _diagVM, null);
        }
        private void ViewSmartWatchesTable(object value)
        {
            CurrentView = new Products.SmartWatches.Table.ViewModel.TableVM(_contentNavigation, _diagVM);

        } 
        private void AddHeadphones(object value)
        {
            CurrentView = new Products.Headphones.Add.First.ViewModel.FirstVM(_contentNavigation, _diagVM, null);
        }
        private void ViewHeadphonesTable(object value)
        {
            CurrentView = new Products.Headphones.Table.ViewModel.TableVM(_contentNavigation, _diagVM);

        }
        private void LogOut(object value)
        {
            _pageNavigation.CurrentView = new Login.ViewModel.LogInVM(_pageNavigation);
        }

        private void ViewUsers(object value)
        {
            CurrentView = new UsersVM(_contentNavigation, _diagVM);
        }
        private void ViewReviews(object value)
        {
            CurrentView = new ReviewsVM(_contentNavigation, _diagVM);
        }
        private void ViewOrders(object value)
        {
            CurrentView = new Orders.ViewModel.OrdersVM(_contentNavigation, _diagVM);
        }
        private void ViewMessanger(object value)
        {
            CurrentView = new MessangersVM(_session, _contentNavigation, _diagVM);
        }
        private void DisplayDiag()
        {
            DiagMessage = _diagVM.Message;
            DiagHeight = _diagVM.IsOpen ? "Auto" : "0";
            BgBlur = _diagVM.IsOpen ? "10" : "0";
        }
        private void DiagOpen(object value)
        {
            _diagVM.IsOpen = true;
        }

        private void DiagClose(object value)
        {
            _diagVM.IsOpen = false;
        }
        private void Profile(object value)
        {
            CurrentView = new ProfileVM(_session, _diagVM);
        }

    }
}
