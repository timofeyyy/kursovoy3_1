using app.app.Admin.Navigation;
using app.app.Admin.Products.Headphones.Add.First.ViewModel;
using app.app.Admin.Products.Headphones.Add.Third.ViewModel;
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

namespace app.app.Admin.Products.Headphones.Add.Fourth.ViewModel
{
    public class FourthVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly ContentNavigation _navigation;
        private readonly Diag.ViewModel.DiagVM _diag;
        private readonly db.Entities.Headphones.Headphones _headphones;
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

        public FourthVM(ContentNavigation navigation, Diag.ViewModel.DiagVM diag, db.Entities.Headphones.Headphones headphones)
        {
            _context = new Context();
            _headphones = headphones;
            _diag = diag;
            _navigation = navigation;
            _images = new List<byte[]>();
            foreach (var img in headphones.ProductImages)
            {
                _images.Add(img.Img);
            }
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
            NextImage(null);
            headphones.ColorId = _context.Color.FirstOrDefault(c => c.Value.Equals(headphones.Color.Value)).Id;
            headphones.ProducerId = _context.Producer.FirstOrDefault(p => p.Name.Equals(headphones.Producer.Name)).Id;


            Description = $"{_headphones.Producer.Name} {_headphones.Model}, color {_headphones.Color.Value}, {_headphones.Price} BYN, wheight {_headphones.Wheight}, in stock {_headphones.Stock}, Wireless {_headphones.Wireless}";
        }

        private void Next(object value)
        {

            _headphones.Producer = null;
            _headphones.Color = null;

            if (_headphones.Id != 0)
            {
                var images = _context.HeadphonesImages.Where(i => i.HeadphonesId.Equals(_headphones.Id)).ToList();
                foreach (var img in images)
                {
                    _context.HeadphonesImages.Remove(img);
                }
                _context.Headphones.Update(_headphones);
                _diag.Message = "Sucessesfuly updated";

            }
            else
            {
                _context.Headphones.Add(_headphones);
                _diag.Message = "Sucessesfuly add";

            }
            _context.SaveChanges();
            _diag.IsOpen = true;
            _navigation.CurrentView = new FirstVM(_navigation, _diag, null);
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new ThirdVM(_navigation, _diag, _headphones);

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
