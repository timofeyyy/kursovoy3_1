using app.app.Admin.Navigation;
using app.app.Admin.Products.Headphones.Add.Fourth.ViewModel;
using app.app.Admin.Products.Headphones.Add.Second.ViewModel;
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

namespace app.app.Admin.Products.Headphones.Add.Third.ViewModel
{
    public class ThirdVM : ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly DiagVM _diag;
        private readonly Context _context;
        private db.Entities.Headphones.Headphones _headphones;

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
                _headphones.Color = _context.Color.Where(c => c.Value.Equals(value)).First();
                OnPropertyChanged(nameof(Color));
            }
        }
        public float Wheight
        {
            get { return _headphones.Wheight; }
            set
            {
                _headphones.Wheight = value;
                OnPropertyChanged(nameof(Wheight));
            }
        }

       
        public int Stock
        {
            get { return _headphones.Stock; }
            set
            {
                _headphones.Stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public float Price
        {
            get { return _headphones.Price; }
            set
            {
                _headphones.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public bool Wireless
        {
            get { return _headphones.Wireless; }
            set
            {
                _headphones.Wireless = value;
                OnPropertyChanged(nameof(Wireless));
            }
        }
      
        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }

        public ThirdVM(ContentNavigation navigation, DiagVM diag, db.Entities.Headphones.Headphones headphones)
        {
            _context = new Context();
            _headphones = headphones;
            _diag = diag;
            _navigation = navigation;
            Colors = new ObservableCollection<string>(_context.Color.Select(color => color.Value).ToList());
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            if (_headphones.Color == null)
            {
                _headphones.Color = new Color();
                Color = Colors.First();
            }
            else
            {
                Color = _headphones.Color.Value;
            }


        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _headphones.Color.Value,
                _headphones.Price.ToString(),
                _headphones.Wheight.ToString(),
                _headphones.Stock.ToString()
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
                _navigation.CurrentView = new FourthVM(_navigation, _diag, _headphones);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new SecondVM(_navigation, _diag, _headphones);
        }
    }
}
