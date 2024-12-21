using app.app.Admin.Navigation;
using app.app.Admin.Products.SmartWatches.Add.Fourth.ViewModel;
using app.app.Admin.Products.SmartWatches.Add.Second.ViewModel;
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

namespace app.app.Admin.Products.SmartWatches.Add.Third.ViewModel
{
    public class ThirdVM : ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private db.Entities.SmartWatches.SmartWatch _smartWatch;

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
                _smartWatch.Color = _context.Color.Where(c => c.Value.Equals(value)).First();
                OnPropertyChanged(nameof(Color));
            }
        }
        public float Wheight
        {
            get { return _smartWatch.Wheight; }
            set
            {
                _smartWatch.Wheight = value;
                OnPropertyChanged(nameof(Wheight));
            }
        }

        public float Width
        {
            get { return _smartWatch.Width; }
            set
            {
                _smartWatch.Width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public float Height
        {
            get { return _smartWatch.Height; }
            set
            {
                _smartWatch.Height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public int Stock
        {
            get { return _smartWatch.Stock; }
            set
            {
                _smartWatch.Stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public float Price
        {
            get { return _smartWatch.Price; }
            set
            {
                _smartWatch.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public bool Wifi
        {
            get { return _smartWatch.Wifi; }
            set
            {
                _smartWatch.Wifi = value;
                OnPropertyChanged(nameof(Wifi));
            }
        }
        public bool Bleatouth
        {
            get { return _smartWatch.Bleatouth; }
            set
            {
                _smartWatch.Bleatouth = value;
                OnPropertyChanged(nameof(Bleatouth));
            }
        }
        public bool Gps
        {
            get { return _smartWatch.Gps; }
            set
            {
                _smartWatch.Gps = value;
                OnPropertyChanged(nameof(Gps));
            }
        }
        public bool Calls
        {
            get { return _smartWatch.Calls; }
            set
            {
                _smartWatch.Calls = value;
                OnPropertyChanged(nameof(Calls));
            }
        }
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        public ThirdVM(ContentNavigation navigation, DiagVM diag, db.Entities.SmartWatches.SmartWatch smartWatches)
        {
            _context = new Context();
            _smartWatch = smartWatches;
            _diag = diag;
            _navigation = navigation;
            Colors = new ObservableCollection<string>(_context.Color.Select(color => color.Value).ToList());
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            if (_smartWatch.Color == null)
            {
                _smartWatch.Color = new Color();
                Color = Colors.First();
            }
            else
            {
                Color = _smartWatch.Color.Value;
            }


        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _smartWatch.Color.Value,
                _smartWatch.Height.ToString(),
                _smartWatch.Price.ToString(),
                _smartWatch.Width.ToString(),
                _smartWatch.Wheight.ToString(),
                _smartWatch.Stock.ToString()
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
                _navigation.CurrentView = new FourthVM(_navigation, _diag, _smartWatch);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new SecondVM(_navigation, _diag, _smartWatch);
        }
    }
}
