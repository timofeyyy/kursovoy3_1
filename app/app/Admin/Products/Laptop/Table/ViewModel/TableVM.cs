using app.app.Admin.Navigation;
using app.app.Admin.Products.Laptop.Add.First.ViewModel;
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

namespace app.app.Admin.Products.Laptop.Table.ViewModel
{
    public class TableVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private object _products;
        private db.Entities.Laptop.Laptop _laptop;

        public db.Entities.Laptop.Laptop Laptop { 
            get { return _laptop; }
            set { _laptop = value; }
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
            Products = new ObservableCollection<db.Entities.Laptop.Laptop>(_context.Laptop.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList());
            EditCommand = new RelayCommand(Edit);
            RemoveCommand = new RelayCommand(Remove);

        }
        private void Edit(object value)
        {
            //MessageBox.Show(_laptop.Id.ToString());
            _contentNavigation.CurrentView = new FirstVM(_contentNavigation, _diag, _laptop);

        }
        private void Remove(object value)
        {
            _context.Laptop.Remove(Laptop);
            _context.SaveChanges();
            Products = new ObservableCollection<db.Entities.Laptop.Laptop>(_context.Laptop.Include(l => l.Color).Include(l => l.Producer).ToList());
        }
    }
}
