using app.app.Admin.Navigation;
using app.app.Admin.Products.SmartWatches.Add.First.ViewModel;
using app.app.Admin.Products.SmartWatches.Add.Third.ViewModel;
using app.db.Context;
using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace app.app.Admin.Products.SmartWatches.Add.Fourth.ViewModel
{
    public class FourthVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly ContentNavigation _navigation;
        private readonly Diag.ViewModel.DiagVM _diag;
        private readonly db.Entities.SmartWatches.SmartWatch _smartWatches;
        private readonly List<byte[]> _images;

        private int index = -1;
        private byte[] _currentImage;

        public byte[] CurrentImage
        {
            get { return _currentImage; }
            set { _currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }

        private string _desccription;
        public string Description
        {
            get { return _desccription; }
            set { _desccription = value; OnPropertyChanged(nameof(Description)); }
        }

        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PrevImageCommand { get; }

        public FourthVM(ContentNavigation navigation, Diag.ViewModel.DiagVM diag, db.Entities.SmartWatches.SmartWatch smartWatches)
        {
            _context = new Context();
            _smartWatches = smartWatches;
            _diag = diag;
            _navigation = navigation;
            _images = new List<byte[]>();
            foreach (var img in smartWatches.ProductImages)
            {
                _images.Add(img.Img);
            }
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
            NextImage(null);
            smartWatches.ColorId = _context.Color.FirstOrDefault(c => c.Value.Equals(smartWatches.Color.Value)).Id;
            smartWatches.ProducerId = _context.Producer.FirstOrDefault(p => p.Name.Equals(smartWatches.Producer.Name)).Id;
            smartWatches.OSId = _context.OSBrand.FirstOrDefault(p => p.Name.Equals(smartWatches.OS.Brand.Name)).Id;


            Description = $"{_smartWatches.Producer.Name} {_smartWatches.Model}, color {_smartWatches.Color.Value}, OS {_smartWatches.OS.Brand.Name} {_smartWatches.OS.Version}, {_smartWatches.Price} BYN, {_smartWatches.Width}x{_smartWatches.Height}, wheight {_smartWatches.Wheight}, in stock {_smartWatches.Stock}, Wifi {_smartWatches.Wifi}, Bluetooth {_smartWatches.Bleatouth}, Calls {_smartWatches.Calls}, GPS {_smartWatches.Gps}";
        }

        private void Next(object value)
        {

            _smartWatches.Producer = null;
            _smartWatches.Color = null;
            _smartWatches.OS = null;

            if (_smartWatches.Id != 0)
            {
                var images = _context.SmartWatchesImages.Where(i => i.SmartWatchesId.Equals(_smartWatches.Id)).ToList();
                foreach (var img in images)
                {
                    _context.SmartWatchesImages.Remove(img);
                }
                _context.SmartWatches.Update(_smartWatches);
                _diag.Message = "Sucessesfuly updated";

            }
            else
            {
                _context.SmartWatches.Add(_smartWatches);
                _diag.Message = "Sucessesfuly add";

            }
            _context.SaveChanges();
            _diag.IsOpen = true;
            _navigation.CurrentView = new FirstVM(_navigation, _diag, null);
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new ThirdVM(_navigation, _diag, _smartWatches);

        }

        private void NextImage(object value)
        {
            if (index < _images.Count && _images.Count != 0 && index < _images.Count - 1)
            {
                index++;
                CurrentImage = _images[index];
            }
        }

        private void PrevImage(object value)
        {
            if (index > 0)
            {
                index--;
                CurrentImage = _images[index];
            }
        }
    }
}
