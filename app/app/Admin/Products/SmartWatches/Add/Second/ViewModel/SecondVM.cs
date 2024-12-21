using app.app.Admin.Navigation;
using app.app.Admin.Products.SmartWatches.Add.First.ViewModel;
using app.app.Admin.Products.SmartWatches.Add.Third.ViewModel;
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

namespace app.app.Admin.Products.SmartWatches.Add.Second.ViewModel
{
    public class SecondVM :ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly db.Entities.SmartWatches.SmartWatch _smartWatch;
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
                _smartWatch.Producer = _context.Producer.Where(p => p.Name.Equals(value)).FirstOrDefault();
                OnPropertyChanged(nameof(Producer)); 
            }    
        }
       
        public string Model { 
           get { return _smartWatch.Model; }
           set {
                _smartWatch.Model = value;
               OnPropertyChanged(nameof(Model));
           }    
        }
        private string _os;
        public string OS
        {
            get { return _os; }
            set
            {
                _os = value;
                var list = _context.OS.Where(o => o.Brand.Name.Equals(value) && !o.Brand.IsLaptop).ToList();
                if (list.Count > 0)
                {
                    _smartWatch.OS.Brand = _context.OSBrand.Where(o => o.Name.Equals(value) && !o.IsLaptop).FirstOrDefault();
                    _smartWatch.OS = list.FirstOrDefault();
                    OSListVersion.Clear();
                    foreach (var osversion in list)
                    {
                        OSListVersion.Add(osversion.Version);
                    }
                    OSVersion = OSListVersion.FirstOrDefault();
                }
                OnPropertyChanged(nameof(OS));
            }
        }

        private string _osVersion;
        public string OSVersion
        {
            get { return _osVersion; }
            set
            {
                _osVersion = value;
                _smartWatch.OS.Version = value;

                OnPropertyChanged(nameof(OSVersion));
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
     
        public SecondVM(ContentNavigation navigation, DiagVM diag, db.Entities.SmartWatches.SmartWatch smartWatch)
        {
            _context = new Context();
            _smartWatch = smartWatch;
            _diag = diag;
            _navigation = navigation;
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);

            Producers = new ObservableCollection<string>(_context.Producer.Select(producer => producer.Name).ToList());
            OSList = new ObservableCollection<string>(_context.OSBrand.Where(os => !os.IsLaptop).Select(os => os.Name).ToList());
            OSListVersion = new ObservableCollection<string>();
          
            if (_smartWatch.Producer == null)
            {
                _smartWatch.Producer = new Producer();
                Producer = Producers.FirstOrDefault();
            }
            else
            {
                Producer = _smartWatch.Producer.Name;
            }
            if (_smartWatch.OS == null)
            {
                _smartWatch.OS = new db.Entities.OS.OS();
                _smartWatch.OS.Brand = new OSBrand();
                OS = OSList.First();
            }
            else
            {
                string val = _smartWatch.OS.Version;
                OS = _smartWatch.OS.Brand.Name;
                OSVersion = val;
            }
          

        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _smartWatch.OS.Brand.Name,
                _smartWatch.OS.Version,
                _smartWatch.Producer.Name,
                _smartWatch.Model
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
                _navigation.CurrentView = new ThirdVM(_navigation, _diag, _smartWatch);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new FirstVM(_navigation, _diag, _smartWatch);
        }
    }
}
