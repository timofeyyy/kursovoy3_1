using app.app.Session;
using app.db.Context;
using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using app.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using app.db.Entities;
using app.app.Admin.Users.View;

namespace app.app.Client.Cart.ViewModel
{
    public class CartVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly Session.Session _session;
        private readonly Diag.ViewModel.DiagVM _diagVM;

        private db.Entities.Orders _orders;
        private object _obj;
        private string _formHeight;
        private string _bgBlur;
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

        public string FormHeight
        {
            get => _formHeight;
            set { _formHeight = value; OnPropertyChanged(nameof(FormHeight)); }
        }
        public string BgBlur
        {
            get => _bgBlur;
            set { _bgBlur = value; OnPropertyChanged(nameof(BgBlur)); }
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
        public ICommand MakeOrderCommand { get; }
        public ICommand OpenOrderFormCommand { get; }
        public ICommand CloseOrderFormCommand { get; }

        public CartVM(Session.Session session, Diag.ViewModel.DiagVM diagVM)
        {
            _context = new Context();
            _session = session;
            _diagVM = diagVM;
            Orders = new db.Entities.Orders();
           
            CloseOrderForm(null);
            OpenOrderFormCommand = new RelayCommand(OpenOrderForm);
            CloseOrderFormCommand = new RelayCommand(CloseOrderForm);
            MakeOrderCommand = new RelayCommand(MakeOrder);
            UpdateCart();

        }
        private void UpdateCart()
        {
            var list = _context.Carts
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
        private void OpenOrderForm(object value)
        {
            if(Obj is db.Entities.Cart cart)
            {
               if(cart.Phone != null)
               {
                    Stock = cart.Phone.Stock;
                    ProductId = cart.Phone.Id;
                    Obj = cart.Phone;
               }
                if (cart.Laptop != null)
                {
                    Stock = cart.Laptop.Stock;
                    ProductId = cart.Laptop.Id;
                    Obj = cart.Laptop;
                }
                if (cart.Headphones != null)
                {
                    Stock = cart.Headphones.Stock;
                    ProductId = cart.Headphones.Id;
                    Obj = cart.Headphones;
                }
                if (cart.SmartWatch != null)
                {
                    Stock = cart.SmartWatch.Stock;
                    ProductId = cart.SmartWatch.Id;
                    Obj = cart.SmartWatch;
                }
            }
            FormHeight = "Auto";
            BgBlur = "10";
        }
        private void MakeOrder(object value)
        {
            if (Orders.Count == 0 || Orders.Adress.IsNullOrEmpty())
            {
                _diagVM.Message = $"Fill all the fields";
            }
            else if (Orders.Count > Stock)
            {
                _diagVM.Message = $"Count must not be more than {Stock}";
            }
            else
            {
                var existringUser = _context.Users.Find(_session.User.Id);
                if (Obj is db.Entities.Phone.Phone phone)
                {
                    Orders.PhonesId = ProductId;
                    var existingPhone = _context.Phone.Find(ProductId);
                    existingPhone.Stock = Stock - Orders.Count;
                    _context.Phone.Update(existingPhone);
                    var existingCartRecord = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.PhonesId.Equals(existingPhone.Id)).First();
                    _context.Carts.Remove(existingCartRecord);
                }
                if (Obj is db.Entities.Laptop.Laptop laptop)
                {
                    Orders.LaptopId = ProductId;
                    var existingLaptop = _context.Laptop.Find(ProductId);
                    existingLaptop.Stock = Stock - Orders.Count;
                    _context.Laptop.Update(existingLaptop);
                    var existingCartRecord = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.LaptopId.Equals(existingLaptop.Id)).First();
                    _context.Carts.Remove(existingCartRecord);
                }
                if (Obj is db.Entities.Headphones.Headphones headphones)
                {
                    Orders.HeadphonesId = ProductId;
                    var existingHeadphones = _context.Headphones.Find(ProductId);
                    existingHeadphones.Stock = Stock - Orders.Count;
                    _context.Headphones.Update(existingHeadphones);
                    var existingCartRecord = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.HeadphonesId.Equals(existingHeadphones.Id)).First();
                    _context.Carts.Remove(existingCartRecord);
                }
                if (Obj is db.Entities.SmartWatches.SmartWatch smartwatch)
                {
                    Orders.SmartWatchesId = ProductId;
                    var existingSmartWatch = _context.SmartWatches.Find(ProductId);
                    existingSmartWatch.Stock = Stock - Orders.Count;
                    _context.SmartWatches.Update(existingSmartWatch);
                    var existingCartRecord = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.SmartWatchesId.Equals(existingSmartWatch.Id)).First();
                    _context.Carts.Remove(existingCartRecord);
                }
                Orders.User = existringUser;
                Orders.Status = "in stock";
                _context.Orders.Add(Orders);
                _context.SaveChanges();

                _diagVM.Message = $"The product is odered";
                CloseOrderForm(null);
                NotifySmtpServer(existringUser);
            }
            UpdateCart();
            _diagVM.IsOpen = true;

        }

        private void NotifySmtpServer(User user)
        {
            try
            {
                MailAddress from = new MailAddress("t.p.se@mailServ.com", "viewer");
                MailAddress to = new MailAddress("t.p.se@mailServ.com");

                MailMessage m = new MailMessage(from, to);
                m.Subject = "Пользователь соверши заказ";
                m.Body = $"Пользователь {user.Login} заказал товар id {ProductId} на адрес {Orders.Adress}";


                SmtpClient smtp = new SmtpClient("192.168.43.20", 25)
                {
                    Credentials = new NetworkCredential("t.p.se@mailServ.com", "tp28032004")
                };

                smtp.Send(m);

            }
            catch (Exception ex)
            {
                _diagVM.Message = ex.Message;
            }
        }
        private void CloseOrderForm(object value)
        {
           
            FormHeight = "0";
            BgBlur = "0";
        } 
        private void AddOrRemove(object value)
        {
            if (Obj is Laptop laptop)
            {
                var list = _context.Carts.Where(c => c.UserId.Equals(_session.User.Id) && c.LaptopId.Equals(laptop.Id)).ToList();

                if (list.Count == 0)
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
            if (Obj is Phone phone)
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
            if (Obj is Headphones headphones)
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
            if (Obj is SmartWatch smartWartch)
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
            MessageBox.Show("OpenItem");
        }
    }
}
