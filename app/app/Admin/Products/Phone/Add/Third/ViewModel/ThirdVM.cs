using app.app.Admin.Navigation;
using app.app.Admin.Products.Phone.Add.Fourth.ViewModel;
using app.app.Admin.Products.Phone.Add.Second.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.db.Entities.Laptop.VideoCard;
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

namespace app.app.Admin.Products.Phone.Add.Third.ViewModel
{
    public class ThirdVM : ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private db.Entities.Phone.Phone _phone;

        private ObservableCollection<string> _colors;

        public ObservableCollection<string> Colors
        {
            get { return _colors; }
            set { _colors = value; OnPropertyChanged(nameof(Colors)); }

        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _phone.Color = _context.Color.Where(c => c.Value.Equals(value)).First();
                OnPropertyChanged(nameof(Color));
            }
        }
        public float Wheight
        {
            get { return _phone.Wheight; }
            set
            {
                _phone.Wheight = value;
                OnPropertyChanged(nameof(Wheight));
            }
        }

        public float Width
        {
            get { return _phone.Width; }
            set
            {
                _phone.Width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public float Height
        {
            get { return _phone.Height; }
            set
            {
                _phone.Height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public int RAM
        {
            get { return _phone.RAM; }
            set
            {
                _phone.RAM = value;
                OnPropertyChanged(nameof(RAM));
            }
        }
        public int InternalMemorySize
        {
            get { return _phone.InternalMemorySize; }
            set
            {
                _phone.InternalMemorySize = value;
                OnPropertyChanged(nameof(InternalMemorySize));
            }
        }
        public int Stock
        {
            get { return _phone.Stock; }
            set
            {
                _phone.Stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public float Price
        {
            get { return _phone.Price; }
            set
            {
                _phone.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public int Camera
        {
            get { return _phone.Camera; }
            set
            {
                _phone.Camera = value;
                OnPropertyChanged(nameof(Camera));
            }
        }
        public int Battery
        {
            get { return _phone.Battery; }
            set
            {
                _phone.Battery = value;
                OnPropertyChanged(nameof(Battery));
            }
        }
        public bool WaterProtection
        {
            get { return _phone.WaterProtection; }
            set
            {
                _phone.WaterProtection = value;
                OnPropertyChanged(nameof(WaterProtection));
            }
        }
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        public ThirdVM(ContentNavigation navigation, DiagVM diag, db.Entities.Phone.Phone phone)
        {
            _context = new Context();
            _phone = phone;
            _diag = diag;
            _navigation = navigation;
            Colors = new ObservableCollection<string>(_context.Color.Select(color => color.Value).ToList());
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            if (_phone.Color == null)
            {
                _phone.Color = new Color();
                Color = Colors.First();
            }
            else
            {
                Color = _phone.Color.Value;
            }


        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _phone.Color.Value,
                _phone.Height.ToString(),
                _phone.Price.ToString(),
                _phone.Width.ToString(),
                _phone.Wheight.ToString(),
                _phone.RAM.ToString(),
                _phone.InternalMemorySize.ToString(),
                _phone.Stock.ToString()
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
                //MessageBox.Show(_phone.WaterProtection.ToString());
                _navigation.CurrentView = new FourthVM(_navigation, _diag, _phone);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new SecondVM(_navigation, _diag, _phone);
        }
    }
}
