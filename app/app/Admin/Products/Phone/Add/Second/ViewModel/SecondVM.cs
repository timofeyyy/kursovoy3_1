using app.app.Admin.Navigation;
using app.app.Admin.Products.Phone.Add.First.ViewModel;
using app.app.Admin.Products.Phone.Add.Third.ViewModel;
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

namespace app.app.Admin.Products.Phone.Add.Second.ViewModel
{
    public class SecondVM :ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly db.Entities.Phone.Phone _phone;
        private readonly DiagVM _diag;
        private readonly Context _context;

 

        private ObservableCollection<string> _producers;
        private ObservableCollection<string> _osList;
        private ObservableCollection<string> _osListVersion;
        private ObservableCollection<string> _processorBrands;
        private ObservableCollection<string> _processorModels;

        
        private string _producer;
        public string Producer { 
           get { return _producer; }
           set {
                _producer = value;
                _phone.Producer = _context.Producer.Where(p => p.Name.Equals(value)).FirstOrDefault();
                OnPropertyChanged(nameof(Producer)); 
            }    
        }
       
        public string Model { 
           get { return _phone.Model; }
           set {      
               _phone.Model = value;
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
                    _phone.OS.Brand = _context.OSBrand.Where(o => o.Name.Equals(value) && !o.IsLaptop).FirstOrDefault();
                    _phone.OS = list.FirstOrDefault();
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
                _phone.OS.Version = value;

                OnPropertyChanged(nameof(OSVersion));
            }
        }

        private string _processorBrand;
        public string ProcessorBrand
        {
            get { return _processorBrand; }
            set
            {
                _processorBrand = value;
                var list = _context.ProcessorBrand.Where(p => p.Name.Equals(value)).ToList();
                    _phone.Processor.Brand = list.FirstOrDefault();

                    _processorModels.Clear();
                    foreach (var item in _context.ProcessorModel.Where(model => model.Brand.Name.Equals(value) && !model.IsLaptop).ToList())
                    {
                        ProcessorModels.Add(item.Name);
                    }
                    if (ProcessorModels.FirstOrDefault() != null && list.FirstOrDefault() != null && ProcessorModels.Count> 0)
                        ProcessorModel = ProcessorModels.First();


                OnPropertyChanged(nameof(ProcessorBrand));
            }
        }
        private string _processorModel;
        public string ProcessorModel
        {
            get { return _processorModel; }
            set
            {
                _processorModel = value;
                _phone.Processor = _context.ProcessorModel.Where(p => p.Name.Equals(value) && !p.IsLaptop).FirstOrDefault();
                OnPropertyChanged(nameof(ProcessorModel));
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
        public ObservableCollection<string> ProcessorBrands
        {
            get => _processorBrands;
            set
            {
                _processorBrands = value;
                OnPropertyChanged(nameof(ProcessorBrands));
            }
        }
        public ObservableCollection<string> ProcessorModels
        {
            get => _processorModels;
            set
            {
                _processorModels = value;
                OnPropertyChanged(nameof(ProcessorModels));
            }
        }
        public SecondVM(ContentNavigation navigation, DiagVM diag, db.Entities.Phone.Phone phone)
        {
            _context = new Context();
            _phone = phone;
            _diag = diag;
            _navigation = navigation;
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);

            Producers = new ObservableCollection<string>(_context.Producer.Select(producer => producer.Name).ToList());
            OSList = new ObservableCollection<string>(_context.OSBrand.Where(os => !os.IsLaptop).Select(os => os.Name).ToList());
            OSListVersion = new ObservableCollection<string>();
            ProcessorBrands = new ObservableCollection<string>(_context.ProcessorBrand
    .Where(brand => brand.ProcessorModels.Any(model => !model.IsLaptop))
    .Select(brand => brand.Name)
    .ToList());
            ProcessorModels = new ObservableCollection<string>();
            if (_phone.Producer == null)
            {
                _phone.Producer = new Producer();
                Producer = Producers.FirstOrDefault();
            }
            else
            {
                Producer = _phone.Producer.Name;
            }
            if (_phone.OS == null)
            {
                _phone.OS = new db.Entities.OS.OS();
                _phone.OS.Brand = new OSBrand();
                OS = OSList.First();
            }
            else
            {
                string val = _phone.OS.Version;
                OS = _phone.OS.Brand.Name;
                OSVersion = val;
            }
            if (_phone.Processor == null)
            {
                _phone.Processor = new ProcessorModel();
                _phone.Processor.Brand = new ProcessorBrand();
                ProcessorBrand = ProcessorBrands.First();
            }
            else
            {
                string val = _phone.Processor.Name;
                ProcessorBrand = _phone.Processor.Brand.Name;
                ProcessorModel = val;
            }

        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _phone.OS.Brand.Name,
                _phone.OS.Version,
                _phone.Producer.Name,
                _phone.Processor.Brand.Name,
                _phone.Processor.Name,
                _phone.Model
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
                _navigation.CurrentView = new ThirdVM(_navigation, _diag, _phone);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new FirstVM(_navigation, _diag, _phone);
        }
    }
}
