using app.app.Admin.Navigation;
using app.app.Admin.Products.Phone.Add.First.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace app.app.Admin.Products.Phone.Table.ViewModel
{
    public class TableVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private object _products;
        private db.Entities.Phone.Phone _phone;

        public db.Entities.Phone.Phone Phone { 
            get { return _phone; }
            set { _phone = value; }
        } 
        public object Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }
        public TableVM(ContentNavigation contentNavigation, DiagVM diag) {
            _diag = diag;
            _contentNavigation = contentNavigation;
            _context = new Context();
            Products = new ObservableCollection<db.Entities.Phone.Phone>(_context.Phone.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList());
            EditCommand = new RelayCommand(Edit);
            RemoveCommand = new RelayCommand(Remove);

        }
        private void Edit(object value)
        {
            //MessageBox.Show(_laptop.Id.ToString());
            _contentNavigation.CurrentView = new FirstVM(_contentNavigation, _diag, _phone);

        }
        private void Remove(object value)
        {
            _context.Phone.Remove(_phone);
            _context.SaveChanges();
            Products = new ObservableCollection<db.Entities.Phone.Phone>(_context.Phone.Include(l => l.Color).Include(l => l.Producer).ToList());
        }
    }
}
