using app.db.Context;
using app.db.Entities.SmartWatches;
using app.utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace app.app.Client.Orders.ViewModel
{
    public class OrdersVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly Session.Session _session;
        private readonly Diag.ViewModel.DiagVM _diagVM;

        private db.Entities.Orders _orders;
        private object _obj;
        private int _count;
        private int _productId;
        private int _stock;

        private ObservableCollection<object> _products;
        public ObservableCollection<object> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

       
        public object Obj
        {
            get => _obj;
            set
            {
                _obj = value;
                OnPropertyChanged(nameof(Obj));
            }
        }
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public int Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }

        public db.Entities.Orders Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }


        public OrdersVM(Session.Session session, Diag.ViewModel.DiagVM diagVM)
        {
            _context = new Context();
            _session = session;
            _diagVM = diagVM;
            Orders = new db.Entities.Orders();


            UpdateCart();

        }
        private void UpdateCart()
        {
            var list = _context.Orders
               .Include(c => c.Laptop)
               .Include(c => c.Phone)
               .Include(c => c.Headphones)
               .Include(c => c.SmartWatch)
               .Include(c => c.Laptop.Color)
               .Include(c => c.Laptop.Producer)
               .Include(c => c.Laptop.ProductImages)
               .Include(c => c.Phone.Color)
               .Include(c => c.Phone.Producer)
               .Include(c => c.Phone.ProductImages)
               .Include(c => c.Headphones.Color)
               .Include(c => c.Headphones.Producer)
               .Include(c => c.Headphones.ProductImages)
               .Include(c => c.SmartWatch.Color)
               .Include(c => c.SmartWatch.Producer)
               .Include(c => c.SmartWatch.ProductImages)

               .Where(c => c.UserId.Equals(_session.User.Id)).ToList();

            Products = new ObservableCollection<object>(list);
            Count = list.Count;
        }
    }
}
