using app.app.Admin.Navigation;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace app.app.Admin.Orders.ViewModel
{
    public class OrdersVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly DiagVM _diagVM;

        private ObservableCollection<db.Entities.Orders> _orders;

    

        public ObservableCollection<db.Entities.Orders> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public db.Entities.Orders Order { get; set; }


        public ICommand OpenOrderStatusCommand { get; }

        public OrdersVM(ContentNavigation contentNavigation, DiagVM diag)
        {
            _diagVM = diag;
            _contentNavigation = contentNavigation;
            _context = new Context();
            Orders = new ObservableCollection<db.Entities.Orders>(
               _context.Orders
                 .Include(r => r.User)
                 .Include(r => r.Laptop)
                 .Include(r => r.Phone)
                 .Include(r => r.SmartWatch)
                 .Include(r => r.Headphones)
                 .ToList()
                );

            OpenOrderStatusCommand = new RelayCommand(OpenOrderStatus);

        }


        public void OpenOrderStatus(object value)
        {
            _contentNavigation.CurrentView = new Admin.ViewOrder.ViewModel.OrderStatusVM(_contentNavigation, Order, _diagVM);
        }
    }
}
