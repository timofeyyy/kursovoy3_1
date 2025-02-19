using app.app.Admin.Navigation;
using app.app.Admin.Products.Laptop.Add.Fourth.ViewModel;
using app.app.Admin.Products.Laptop.Add.Second.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.db.Entities.Laptop.VideoCard;
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

namespace app.app.Admin.Products.Laptop.Add.Third.ViewModel
{
    public class ThirdVM : ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private db.Entities.Laptop.Laptop _laptop;

        private ObservableCollection<string> _osList;
        private ObservableCollection<string> _osListVersion;
        private ObservableCollection<string> _videoCardBrands;
        private ObservableCollection<string> _videoCardModels;
        private ObservableCollection<string> _colors;

        public ObservableCollection<string> OSList {
            get { return _osList; }
            set { _osList = value; OnPropertyChanged(nameof(OSList)); }
        
        }
        public ObservableCollection<string> OSListVersion
        {
            get { return _osListVersion; }
            set { _osListVersion = value; OnPropertyChanged(nameof(OSListVersion)); }

        }
        public ObservableCollection<string> VideoCardBrands
        {
            get { return _videoCardBrands; }
            set { _videoCardBrands = value; OnPropertyChanged(nameof(VideoCardBrands)); }

        }
        public ObservableCollection<string> VideoCardModels
        {
            get { return _videoCardModels; }
            set { _videoCardModels = value; OnPropertyChanged(nameof(VideoCardModels)); }

        }
        public ObservableCollection<string> Colors
        {
            get { return _colors; }
            set { _colors = value; OnPropertyChanged(nameof(Colors)); }

        }
        private string _os;
        public string OS
        {
            get { return _os; }
            set
            {
                _os = value;
                var list = _context.OS.Where(o => o.Brand.Name.Equals(value) && o.Brand.IsLaptop).ToList();
                _laptop.OS = list.First();
                OSListVersion.Clear();
                foreach (var osversion in list)
                {
                    OSListVersion.Add(osversion.Version);
                }
                OSVersion = OSListVersion.FirstOrDefault();
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
                _laptop.OS.Version = value;
                _laptop.OS.Brand = _context.OSBrand.Where(o => o.Name.Equals(_os) && o.IsLaptop).FirstOrDefault();

                OnPropertyChanged(nameof(OSVersion));
            }
        }
        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _laptop.Color = _context.Color.Where(c => c.Value.Equals(value)).First();
                OnPropertyChanged(nameof(Color));
            }
        }
        public float Wheight
        {
            get { return _laptop.Wheight; }
            set
            {
                _laptop.Wheight = value;
                OnPropertyChanged(nameof(Wheight));
            }
        }

        public float Width
        {
            get { return _laptop.Width; }
            set
            {
                _laptop.Width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public float Height
        {
            get { return _laptop.Height; }
            set
            {
                _laptop.Height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public int RAM
        {
            get { return _laptop.RAMMemorySize; }
            set
            {
                _laptop.RAMMemorySize = value;
                OnPropertyChanged(nameof(RAM));
            }
        }
        public int SSD
        {
            get { return _laptop.SSDMemorySize; }
            set
            {
                _laptop.SSDMemorySize = value;
                OnPropertyChanged(nameof(SSD));
            }
        }
        public int Stock
        {
            get { return _laptop.Stock; }
            set
            {
                _laptop.Stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public float Price
        {
            get { return _laptop.Price; }
            set
            {
                _laptop.Price = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        public ThirdVM(ContentNavigation navigation, DiagVM diag, db.Entities.Laptop.Laptop laptop)
        {
            _context = new Context();
            _laptop = laptop;
            _diag = diag;
            _navigation = navigation;
            OSList = new ObservableCollection<string>(_context.OSBrand.Where(os => os.IsLaptop).Select(os => os.Name).ToList());
            OSListVersion = new ObservableCollection<string>();
            Colors = new ObservableCollection<string>(_context.Color.Select(color => color.Value).ToList());
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            if (_laptop.OS == null)
            {
                _laptop.OS = new OS();
                _laptop.OS.Brand = new OSBrand();
                OS = OSList.First();
            }
            else
            {
                string val = _laptop.OS.Version;
                OS = _laptop.OS.Brand.Name;
                OSVersion = val;
            }
            if (_laptop.Color == null)
            {
                _laptop.Color = new Color();
                Color = Colors.First();
            }
            else
            {
                Color = _laptop.Color.Value;
            }
        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _laptop.OS.Brand.Name,
                _laptop.OS.Version,
                _laptop.Color.Value,
                _laptop.Height.ToString(),
                _laptop.Price.ToString(),
                _laptop.Width.ToString(),
                _laptop.Wheight.ToString(),
                _laptop.RAMMemorySize.ToString(),
                _laptop.SSDMemorySize.ToString(),
                _laptop.Stock.ToString()
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
                _navigation.CurrentView = new FourthVM(_navigation, _diag, _laptop);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new SecondVM(_navigation, _diag, _laptop);
        }
    }
}
