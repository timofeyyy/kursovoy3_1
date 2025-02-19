using app.app.Admin.Navigation;
using app.app.Admin.Products.Phone.Add.First.ViewModel;
using app.app.Admin.Products.Phone.Add.Third.ViewModel;
using app.app.Client.Products.Laptop.View;
using app.db.Context;
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

namespace app.app.Admin.Products.Phone.Add.Fourth.ViewModel
{
    public class FourthVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly ContentNavigation _navigation;
        private readonly Diag.ViewModel.DiagVM _diag;
        private readonly db.Entities.Phone.Phone _phone;
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

        public FourthVM(ContentNavigation navigation, Diag.ViewModel.DiagVM diag, db.Entities.Phone.Phone phone)
        {
            _context = new Context();
            _phone = phone;
            _diag = diag;
            _navigation = navigation;
            _images = new List<byte[]>();
            foreach (var img in phone.ProductImages)
            {
                _images.Add(img.Img);
            }
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
            NextImage(null);
            _phone.ColorId = _context.Color.FirstOrDefault(c => c.Value.Equals(_phone.Color.Value)).Id;
            _phone.ProducerId = _context.Producer.FirstOrDefault(p => p.Name.Equals(_phone.Producer.Name)).Id;
            _phone.OSId = _context.OSBrand.FirstOrDefault(p => p.Name.Equals(_phone.OS.Brand.Name)).Id;
            _phone.ProcessorId = _context.ProcessorModel.FirstOrDefault(p => p.Name.Equals(_phone.Processor.Name)).Id;


            Description = $"{_phone.Producer.Name} {_phone.Model}, color {_phone.Color.Value}, OS {_phone.OS.Brand.Name} {_phone.OS.Version}, {_phone.Price} BYN, {_phone.Width}x{_phone.Height}, RAM {_phone.RAM}, Internal Memory {_phone.InternalMemorySize}, wheight {_phone.Wheight}, water protection {_phone.InternalMemorySize}, Camera {_phone.Camera}, in stock {_phone.Stock}";
        }

        private void Next(object value)
        {
      
            _phone.Producer = null;
            _phone.Color = null;
            _phone.OS = null;
            _phone.Processor = null;

            if (_phone.Id != 0)
            {
                var images = _context.PhoneImages.Where(i => i.PhoneId.Equals(_phone.Id)).ToList();
                foreach (var img in images)
                {
                    _context.PhoneImages.Remove(img);
                }
                _context.Phone.Update(_phone);
                _diag.Message = "Sucessesfuly updated";

            }
            else
            {
                _context.Phone.Add(_phone);
                _diag.Message = "Sucessesfuly add";

            }
            _context.SaveChanges();
            _diag.IsOpen = true;
            _navigation.CurrentView = new FirstVM(_navigation, _diag, null);
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new ThirdVM(_navigation, _diag, _phone);

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
