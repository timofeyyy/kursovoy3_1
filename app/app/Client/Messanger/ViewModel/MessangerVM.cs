using app.db.Context;
using app.db.Entities;
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

namespace app.app.Client.Messanger.ViewModel
{
    public class MessangerVM :ViewModelBase
    {
        private readonly Context _context;
        private readonly Session.Session _session;
        private readonly Diag.ViewModel.DiagVM _diagVM;

        private db.Entities.Orders _orders;
        private string _message;

        private ObservableCollection<db.Entities.Messanger> _messages;
        public ObservableCollection<db.Entities.Messanger> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public ICommand SendCommand { get;}
        public MessangerVM(Session.Session session, Diag.ViewModel.DiagVM diagVM) {
            _context = new Context();
            _diagVM = diagVM;
            _session = session;


            SendCommand = new RelayCommand(Send);
            GetAllChatMessages();
        }

        private void Send(object value)
        {
            var chat = CreateOrOpenChat();
            //MessageBox.Show($"{chat == null}");
            //MessageBox.Show($"{chat.Id}");
            _context.Messanger.Add(new db.Entities.Messanger()
            {
                Message = this.Message,
                ChatId = chat.Id,
            });
            _context.SaveChanges();

            Message = "";
            GetAllChatMessages();
        }

        private void GetAllChatMessages()
        {
            var chat = CreateOrOpenChat();

            var list = _context.Messanger.Include(m => m.Chat).Include(m => m.Chat.User).Where(m => m.ChatId.Equals(chat.Id)).ToList();


            Messages = new ObservableCollection<db.Entities.Messanger>(list);
        }
        private Chat CreateOrOpenChat()
        {
            var existringChat = _context.Chat.Where(c => c.UserId.Equals(_session.User.Id)).ToList();

            if (existringChat.Count == 0) {
                Chat chat = new db.Entities.Chat()
                {
                    UserId = _session.User.Id
                };
                
                _context.Chat.Add(chat);
                _context.SaveChanges();
                
                return chat;
            }

            return existringChat.First();

        }
    }
}
