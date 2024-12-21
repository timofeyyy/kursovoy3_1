using app.app.Session;
using app.db.Context;
using app.db.Entities;
using app.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using app.app.Diag.ViewModel;
using app.app.Admin.Navigation;
using Microsoft.EntityFrameworkCore;
using app.app.Client.Messanger.ViewModel;
using app.app.Admin.Users.View;

namespace app.app.Admin.Messangers.ViewModel
{
    public class MessangersVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly Session.Session _session;
        private readonly DiagVM _diagVM;

        private string _newMessangersHighlight;
        private string _existedMessangersHighlight;

        private ObservableCollection<Chat> _chats;
        public ObservableCollection<Chat> Chats
        {
            get => _chats;
            set
            {
                _chats = value;
                OnPropertyChanged(nameof(Chats));
            }
        }

        public string NewMessangersHighlight
        {
            get { return _newMessangersHighlight; }
            set
            {
                _newMessangersHighlight = value;
                OnPropertyChanged(nameof(NewMessangersHighlight));
            }
        }
        public string ExistedMessangersHighlight
        {
            get { return _existedMessangersHighlight; }
            set
            {
                _existedMessangersHighlight = value;
                OnPropertyChanged(nameof(ExistedMessangersHighlight));
            }
        }
        public ICommand OpenChatCommand { get; }
        public ICommand SelectNewMessangersCommand { get; }
        public ICommand SetectExistedMessangersCommand { get; }
        public MessangersVM(Session.Session session, ContentNavigation contentNavigation, DiagVM diagVM)
        {
            _context = new Context();
            _diagVM = diagVM;
            _session = session;


            OpenChatCommand = new RelayCommand(Open);
            SelectNewMessangersCommand = new RelayCommand(SelectNewMessangers);
            SetectExistedMessangersCommand = new RelayCommand(SetectExistedMessangers);
            _contentNavigation = contentNavigation;

            SelectNewMessangers(null);
        }

        private void Open(object value)
        {
            if (value is Chat chat)
            {
                MessageBox.Show(chat.User.Login);
                _contentNavigation.CurrentView = new Admin.Messanger.ViewModel.MessangerVM(_session, _diagVM, chat.User);
            }


        }
        private void SelectNewMessangers(object value)
        {
            var list = _context.Chat.Include(c => c.User).Where(c => c.AdminId.Equals(null)).ToList();
            Chats = new ObservableCollection<Chat>(list);
            ExistedMessangersHighlight = "0,0,0,0";
            NewMessangersHighlight = "0,0,0,2";
        }

        private void SetectExistedMessangers(object value)
        {
            var list = _context.Chat.Include(c => c.User).Where(c => c.AdminId.Equals(_session.User.Id)).ToList();
            Chats = new ObservableCollection<Chat>(list);

            NewMessangersHighlight = "0,0,0,0";
            ExistedMessangersHighlight = "0,0,0,2";
        }

       
    }
}
