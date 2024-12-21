using app.app.Admin.Main.ViewModel;
using app.app.Admin.Users.View;
using app.app.Client.Main.ViewModel;
using app.app.Diag.ViewModel;
using app.app.Navigation;
using app.db.Context;
using app.db.Entities;
using app.utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app.app.Login.ViewModel
{
    class LogInVM : ViewModelBase
    {

        private readonly UserControl _instance;
        private readonly PageNavigation _navigation;
        private readonly Context _context;
        private readonly DiagVM _diagVM;

        public ICommand SetAdminRoleCommand { get; }
        public ICommand SetClientRoleCommand { get; }
        public ICommand LogInCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand DiagOpenCommand { get; }
        public ICommand DiagCloseCommand { get; }

        private string _diagMessage;

        private bool _entry;
        private bool _isAdmin;
        private string _entryLabel;

        private string _userName;
        private string _password;
        private string _email;

        private string _clientHighlight;
        private string _adminHighlight;
        private string _emailParamHeight;
        private string _emailOrUserName;
        private string _diagHeight;
        private string _bgBlur;
        public string DiagMessage
        {
            get { return _diagMessage; }
            set
            {
                _diagMessage = value;
                OnPropertyChanged(nameof(DiagMessage));
            }
        }
        public string EntryLabel
        {
            get { return _entryLabel; }
            set
            {
                _entryLabel = value;
                OnPropertyChanged(nameof(EntryLabel));
            }
        }
        public string ClientHighlight
        {
            get { return _clientHighlight; }
            set
            {
                _clientHighlight = value;
                OnPropertyChanged(nameof(ClientHighlight));
            }
        }
        public string AdminHighlight
        {
            get { return _adminHighlight; }
            set
            {
                _adminHighlight = value;
                OnPropertyChanged(nameof(AdminHighlight));
            }
        }
        public string EmailParamHeight
        {
            get { return _emailParamHeight; }
            set
            {
                _emailParamHeight = value;
                OnPropertyChanged(nameof(EmailParamHeight));
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string EmailOrUserName
        {
            get { return _emailOrUserName; }
            set
            {
                _emailOrUserName = value;
                OnPropertyChanged(nameof(EmailOrUserName));
            }
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
        public LogInVM (PageNavigation navigation)
        {
            _context = new Context();
            _diagVM = new DiagVM();
            _diagVM.PropertyChanged += (s, e) => DisplayDiag();
            _navigation = navigation;
            SetAdminRoleCommand = new RelayCommand(EntryAsAdmin);
            SetClientRoleCommand = new RelayCommand(EntryAsClient);
            LogInCommand = new RelayCommand(LogIn);
            SignUpCommand = new RelayCommand(SignUp);
            DiagCloseCommand = new RelayCommand(DiagClose);
            EntryAsClient();
            LogIn(null);
            DiagClose(null);
        }

        private void EntryAsAdmin(object value)
        {
            ClientHighlight = "0,0,0,0";
            AdminHighlight = "0,0,0,2";
            _isAdmin = true;
            EntryLabel = EntryLabel?.Replace("User", "Admin");
        }
        private void EntryAsClient(object value = null) {
            ClientHighlight = "0,0,0,2";
            AdminHighlight = "0,0,0,0";
            _isAdmin = false;
            EntryLabel = EntryLabel?.Replace("Admin", "User");
        }

        private void LogIn(object value)
        {

            if (EntryLabel == null || !EntryLabel.Contains("Log in"))
            {
                EmailOrUserName = "username or email";
                string role = _isAdmin ? "Admin" : "User";
                EntryLabel = $"Log in for {role}";
                EmailParamHeight = "0";
            }
            else
            {
                DiagMessage = "";

                User user = GetUser();
                if (user == null || !ComparePasswords())
                    DiagMessage = "Such user does not exist or wrong password";
                else if (!_isAdmin.Equals(user.IsAdmin))
                    DiagMessage = "Wrong role";

                if (!DiagMessage.IsNullOrEmpty())
                {
                    DiagOpen(null);
                    return;
                }

                _navigation.CurrentView = _isAdmin ? new AdminVM(_navigation, new Session.Session() { User = user }) : new ClientVM(_navigation, new Session.Session() { User = user });
            }

        }
        private void NotifySmtpServer()
        {
            try
            {
                MailAddress from = new MailAddress("t.p.se@mailServ.com", "viewer");
                MailAddress to = new MailAddress("t.p.se@mailServ.com");

                MailMessage m = new MailMessage(from, to);
                m.Subject = "Новый пользователь";
                m.Body = $"Пользователь {UserName} зарегестрировался.\nПочта {Email}";


                SmtpClient smtp = new SmtpClient("192.168.43.20", 25)
                {
                    Credentials = new NetworkCredential("t.p.se@mailServ.com", "tp28032004")
                };

                smtp.Send(m);

            }
            catch (Exception ex)
            {
                DiagMessage = ex.Message;
                DiagOpen(null);
            }
        }
        private bool CheckSymbols(string value)
        {
            var cyrillic = Enumerable.Range(1024, 256).Select(ch => (char)ch);
            var res = value.Any(cyrillic.Contains);
            return !res;
        }

        private void SignUp(object value)
        {
            string role = _isAdmin ? "Admin" : "User";

            if (EntryLabel == null || !EntryLabel.Contains("Sign up"))
            {
                EmailOrUserName = "username";
                EntryLabel = $"Sign up for {role}";
                EmailParamHeight = "Auto";
            }
            else
            {
                DiagMessage = "";

                if (!ValidateUserName())
                    DiagMessage += "Username must not to contain white spaces or symbols except(-_)" + Environment.NewLine + Environment.NewLine;
                if (!ValidateEmail())
                    DiagMessage += "Email domen does not exist" + Environment.NewLine + Environment.NewLine;
                if (!ValidatePassword())
                    DiagMessage += "Password must be more than 8 characters and not containe symbols at all" + Environment.NewLine + Environment.NewLine;
                if (GetUser() != null)
                    DiagMessage = "Such user already exists" + Environment.NewLine + Environment.NewLine;

                if (!DiagMessage.IsNullOrEmpty())
                {
                    DiagOpen(null);
                    return;
                }

                User user = new User()
                {
                    Login = UserName,
                    Email = Email,
                    Password = Password,
                    IsAdmin = _isAdmin,
                    UserImage = File.ReadAllBytes("../../../res/images/default-icon.jpg")
                };

                _context.Add(user);
                _context.SaveChanges();
                _navigation.CurrentView = _isAdmin ? new AdminVM(_navigation, new Session.Session() { User = user }) : new ClientVM(_navigation, new Session.Session() { User = user });
                NotifySmtpServer();
            }
        }

        private User GetUser() => _context.Users.Where(user => user.Login.Equals(UserName)).FirstOrDefault();
        private bool ComparePasswords() => _context.Users.Where(user => user.Login.Equals(UserName) && user.Password.Equals(Password)).FirstOrDefault() != null;
        private bool ValidatePassword() {
            Regex regex = new Regex(@"^[^\(\)\*&^%$#@!№;:+= ]+$");
            return !Password.IsNullOrEmpty() && regex.IsMatch(Password) && Password.Length > 7;
        }
        private bool ValidateUserName() {
            Regex regex = new Regex(@"^[^\(\)\*&^%$#@!№;:+= ]+$");            
            return !UserName.IsNullOrEmpty() && regex.IsMatch(UserName);
        }
        private bool ValidateEmail()
        {
            //return true;
            if (Email == null) return false;
            try
            {
                var domain = Email.Split('@')[1];
                var hostEntry = Dns.GetHostEntry(domain);
                return hostEntry != null;
            }
            catch
            {
                return false;
            }
            //Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            //return !Email.IsNullOrEmpty() && regex.IsMatch(Email);
        }

        private void DisplayDiag()
        {
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
    }
}
