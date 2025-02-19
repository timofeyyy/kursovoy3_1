using app.app.Admin.Navigation;
using app.app.Admin.Products.Headphones.Add.First.ViewModel;
using app.app.Admin.Products.Headphones.Add.Third.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.db.Entities.Processor;
using app.db.Entities.OS;
using app.utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using app.db.Entities.Phone;

namespace app.app.Admin.Products.Headphones.Add.Second.ViewModel
{
    public class SecondVM :ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly db.Entities.Headphones.Headphones _headphones;
        private readonly DiagVM _diag;
        private readonly Context _context;

 

        private ObservableCollection<string> _producers;
        private ObservableCollection<string> _osList;
        private ObservableCollection<string> _osListVersion;


        
        private string _producer;
        public string Producer { 
           get { return _producer; }
           set {
                _producer = value;
                _headphones.Producer = _context.Producer.Where(p => p.Name.Equals(value)).FirstOrDefault();
                OnPropertyChanged(nameof(Producer)); 
            }    
        }
       
        public string Model { 
           get { return _headphones.Model; }
           set {
                _headphones.Model = value;
               OnPropertyChanged(nameof(Model));
           }    
        }
      

      
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
      
        public ObservableCollection<string> Producers
        {
            get => _producers;
            set
            {
                _producers = value;
                OnPropertyChanged(nameof(Producers));
            }
        }

        public ObservableCollection<string> OSList
        {
            get { return _osList; }
            set { _osList = value; OnPropertyChanged(nameof(OSList)); }

        }
        public ObservableCollection<string> OSListVersion
        {
            get { return _osListVersion; }
            set { _osListVersion = value; OnPropertyChanged(nameof(OSListVersion)); }

        }
     
        public SecondVM(ContentNavigation navigation, DiagVM diag, db.Entities.Headphones.Headphones headphones)
        {
            _context = new Context();
            _headphones = headphones;
            _diag = diag;
            _navigation = navigation;
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);

            Producers = new ObservableCollection<string>(_context.Producer.Select(producer => producer.Name).ToList());
            OSList = new ObservableCollection<string>(_context.OSBrand.Where(os => !os.IsLaptop).Select(os => os.Name).ToList());
            OSListVersion = new ObservableCollection<string>();
          
            if (_headphones.Producer == null)
            {
                _headphones.Producer = new Producer();
                Producer = Producers.FirstOrDefault();
            }
            else
            {
                Producer = _headphones.Producer.Name;
            }
          
          

        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _headphones.Producer.Name,
                _headphones.Model
            };

            foreach (var item in values)
                if (item.IsNullOrEmpty())
                    return false;

            return true;
        }
        private void Next(object value)
        {
            if (CheckValues())
            {
                _navigation.CurrentView = new ThirdVM(_navigation, _diag, _headphones);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new FirstVM(_navigation, _diag, _headphones);
        }
    }
}
