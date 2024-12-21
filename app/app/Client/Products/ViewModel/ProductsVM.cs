using app.app.Admin.Navigation;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using app.utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace app.app.Client.Products.ViewModel
{
    public class ProductsVM : ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly Context _context;
        private readonly Session.Session _session;
        private readonly Diag.ViewModel.DiagVM _diagVM;
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

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private object _obj;

        public object Obj
        {
            get => _obj;
            set
            {
                _obj = value;
                OnPropertyChanged(nameof(Obj));
            }
        }
        public ICommand AddOrRemoveCommand { get; }
        public ICommand OpenItemCommand { get; }
        public ProductsVM(Session.Session session, Diag.ViewModel.DiagVM diagVM, ContentNavigation contentNavigation, IEnumerable<object> list, string name) {
            _navigation = contentNavigation;
            _context = new Context();
            _session = session;
            _diagVM = diagVM;
            Name = name;
            Products = new ObservableCollection<object>(list);

            OpenItemCommand = new RelayCommand(OpenItem);
            AddOrRemoveCommand = new RelayCommand(AddOrRemove);
        }
        private void AddOrRemove(object value)
        {
            if (Obj is db.Entities.Laptop.Laptop laptop)
            {
                var list = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.LaptopId.Equals(laptop.Id)).ToList();
                
                if(list.Count == 0)
                {
                    _context.Carts.Add(new db.Entities.Cart()
                    {
                        LaptopId = laptop.Id,
                        UserId = _session.User.Id
                    });
                    _diagVM.Message = $"Product with id {laptop.Id} was add to cart";
                }
                else
                {
                    _context.Carts.Remove(list.First());
                    _diagVM.Message = $"Product with id {laptop.Id} was removed from cart";
                }
                _diagVM.IsOpen = true;
            }
            if (Obj is db.Entities.Phone.Phone phone)
            {
                var list = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.PhonesId.Equals(phone.Id)).ToList();

                if (list.Count == 0)
                {
                    _context.Carts.Add(new db.Entities.Cart()
                    {
                        PhonesId = phone.Id,
                        UserId = _session.User.Id
                    });
                    _diagVM.Message = $"Product with id {phone.Id} was add to cart";
                }
                else
                {
                    _context.Carts.Remove(list.First());
                    _diagVM.Message = $"Product with id {phone.Id} was removed from cart";
                }
                _diagVM.IsOpen = true;
            }
            if (Obj is db.Entities.Headphones.Headphones headphones)
            {
                var list = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.HeadphonesId.Equals(headphones.Id)).ToList();

                if (list.Count == 0)
                {
                    _context.Carts.Add(new db.Entities.Cart()
                    {
                        HeadphonesId = headphones.Id,
                        UserId = _session.User.Id
                    });
                    _diagVM.Message = $"Product with id {headphones.Id} was add to cart";
                }
                else
                {
                    _context.Carts.Remove(list.First());
                    _diagVM.Message = $"Product with id {headphones.Id} was removed from cart";
                }
                _diagVM.IsOpen = true;
            }
            if (Obj is db.Entities.SmartWatches.SmartWatch smartWartch)
            {
                var list = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.SmartWatchesId.Equals(smartWartch.Id)).ToList();

                if (list.Count == 0)
                {
                    _context.Carts.Add(new db.Entities.Cart()
                    {
                        SmartWatchesId = smartWartch.Id,
                        UserId = _session.User.Id
                    });
                    _diagVM.Message = $"Product with id {smartWartch.Id} was add to cart";
                }
                else
                {
                    _context.Carts.Remove(list.First());
                    _diagVM.Message = $"Product with id {smartWartch.Id} was removed from cart";
                }
                _diagVM.IsOpen = true;
            }
            _context.SaveChanges();
        }
        private void OpenItem(object value)
        {
            if (Obj is db.Entities.Laptop.Laptop laptop)
            {
                var list = _context.Laptop
                    .Include(l => l.Color)
                    .Include(l => l.OS)
                    .Include(l => l.OS.Brand)
                    .Include(l => l.Producer)
                    .Include(l => l.ProductImages)
                    .Include(l => l.Processor)
                    .Include(l => l.Processor.Brand)
                    .Include(l => l.VideoCardModel)
                    .Include(l => l.VideoCardModel.Brand)
                    .Include(l => l.Reviews)
                    .Where(c => c.Id.Equals(laptop.Id)).ToList();
        
                if(list.Count > 0)
                    _navigation.CurrentView = new Laptop.ViewModel.LaptopVM(_session, _diagVM, list.First());


            }
            if (Obj is db.Entities.Phone.Phone phone)
            {
                var list = _context.Phone
                   .Include(l => l.Color)
                   .Include(l => l.OS)
                   .Include(l => l.OS.Brand)
                   .Include(l => l.Producer)
                   .Include(l => l.ProductImages)
                   .Include(l => l.Processor)
                   .Include(l => l.Processor.Brand)
                   .Include(l => l.Reviews)
                   .Where(c => c.Id.Equals(phone.Id)).ToList();

                if (list.Count > 0)
                    _navigation.CurrentView = new Phone.ViewModel.PhoneVM(_session, _diagVM, list.First());
            }
            if (Obj is db.Entities.Headphones.Headphones headphones)
            {
                var list = _context.Headphones
                     .Include(l => l.Color)
                     .Include(l => l.Producer)
                     .Include(l => l.ProductImages)
                     .Include(l => l.Reviews)
                     .Where(c => c.Id.Equals(headphones.Id)).ToList();

                if (list.Count > 0)
                    _navigation.CurrentView = new Headphones.ViewModel.HeadphonesVM(_session, _diagVM, list.First());
            }
            if (Obj is db.Entities.SmartWatches.SmartWatch smartWartch)
            {
                var list = _context.SmartWatches
                      .Include(l => l.Color)
                      .Include(l => l.OS)
                      .Include(l => l.OS.Brand)
                      .Include(l => l.Producer)
                      .Include(l => l.ProductImages)
                      .Include(l => l.Reviews)
                      .Where(c => c.Id.Equals(smartWartch.Id)).ToList();

                if (list.Count > 0)
                    _navigation.CurrentView = new SmartWatches.ViewModel.SmartWatchesVM(_session, _diagVM, list.First());
            }
        }
    }
}
