using app.app.Admin.Navigation;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using app.db.Entities;
using app.app.Admin.Users.View;

namespace app.app.Admin.ViewOrder.ViewModel
{
    public class OrderStatusVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly db.Entities.Orders _order;
        private readonly Context _context;
        private readonly DiagVM _diagVM;
        private ObservableCollection<string> _statusList;
        private string _status;
        public ObservableCollection<string> StatusList
        {
            get => _statusList;
            set
            {
                _statusList = value;
                OnPropertyChanged(nameof(StatusList));
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ICommand EditCommand { get; }
        public ICommand BackCommand { get; }
        public OrderStatusVM(ContentNavigation contentNavigation, db.Entities.Orders order, DiagVM diag)
        {
            _context = new Context();
            _contentNavigation = contentNavigation;
            _diagVM = diag;
            _order = order; 
            EditCommand = new RelayCommand(Edit);
            BackCommand = new RelayCommand(Back);

            StatusList = new ObservableCollection<string>() {
                "in stock",
                "on the way",
                "finished"
            };
        }
        private void Back(object value)
        {
            _contentNavigation.CurrentView = new Orders.ViewModel.OrdersVM(_contentNavigation, _diagVM);
        }
        private void Edit(object value)
        {
                MessageBox.Show(Status);
            var existingOrder = _context.Orders.Find(_order.Id);
            //MessageBox.Show($"{existingOrder == null}");
            existingOrder.Status = Status;
            _context.Orders.Update(existingOrder);
            _context.SaveChanges();
            _diagVM.Message = "Changed";
            _diagVM.IsOpen = true;
            Back(null);
            if(Status == "finished")
            {
                var existingUser = _context.Users.Find(existingOrder.UserId);
                NotifyClient(existingUser, existingOrder);
            }

        }

        private void NotifyClient(User user, db.Entities.Orders order)
        {
            try
            {
                MailAddress from = new MailAddress("t.p.se@mailServ.com", "Digital Products Shop");
                MailAddress to = new MailAddress(user.Email);

                MailMessage m = new MailMessage(from, to);
                m.Subject = "Заказ доставлен";
                m.Body = $"Ваш заказ доставлен был доставлен по адресу {order.Adress}";


                SmtpClient smtp = new SmtpClient("192.168.43.20", 25)
                {
                    Credentials = new NetworkCredential("t.p.se@mailServ.com", "tp28032004")
                };

                smtp.Send(m);

            }
            catch (Exception ex)
            {
                _diagVM.Message = ex.Message;
                _diagVM.IsOpen = true;
            }
        }
    }
}
