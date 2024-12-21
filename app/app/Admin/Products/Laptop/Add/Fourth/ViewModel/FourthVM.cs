using app.app.Admin.Navigation;
using app.app.Admin.Products.Laptop.Add.First.ViewModel;
using app.app.Admin.Products.Laptop.Add.Third.ViewModel;
using app.db.Context;
using app.utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace app.app.Admin.Products.Laptop.Add.Fourth.ViewModel
{
    public class FourthVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly ContentNavigation _navigation;
        private readonly Diag.ViewModel.DiagVM _diag;
        private readonly db.Entities.Laptop.Laptop _laptop;
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

        public FourthVM(ContentNavigation navigation, Diag.ViewModel.DiagVM diag, db.Entities.Laptop.Laptop laptop)
        {
            _context = new Context();
            _laptop = laptop;
            _diag = diag;
            _navigation = navigation;
            _images = new List<byte[]>();
            foreach (var img in laptop.ProductImages)
            {
                _images.Add(img.Img);
            }
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);
            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
            NextImage(null);
            _laptop.ColorId = _context.Color.FirstOrDefault(c => c.Value.Equals(_laptop.Color.Value)).Id;
            _laptop.ProducerId = _context.Producer.FirstOrDefault(p => p.Name.Equals(_laptop.Producer.Name)).Id;
            _laptop.OSId = _context.OSBrand.FirstOrDefault(p => p.Name.Equals(_laptop.OS.Brand.Name)).Id;
            _laptop.VideoCardModelId = _context.VideoCardModel.FirstOrDefault(p => p.Name.Equals(_laptop.VideoCardModel.Name)).Id;
            _laptop.ProcessorId = _context.ProcessorModel.FirstOrDefault(p => p.Name.Equals(_laptop.Processor.Name)).Id;
            _context.SaveChanges();

           
            Description = $"{laptop.Producer.Name} {laptop.Model}, color {_laptop.Color.Value}, OS {laptop.OS.Brand.Name} {laptop.OS.Version}, {laptop.Price} BYN, {laptop.Width}x{laptop.Height}, RAM {laptop.RAMMemorySize}, SSD {laptop.SSDMemorySize}, wheight {laptop.Wheight}, processor {laptop.Processor.Brand} {laptop.Processor.Name}, videocard {laptop.VideoCardModel.Brand.Name} {laptop.VideoCardModel.Name}, in stock {laptop.Stock}";
        }

        private void Next(object value)
        {

            _laptop.Color = null;
            _laptop.Producer = null;
            _laptop.Color = null;
            _laptop.OS = null;
            _laptop.VideoCardModel = null;
            _laptop.Processor = null;

            if(_laptop.Id != 0)
            {
                var images = _context.LaptopImages.Where(i => i.LaptopId.Equals(_laptop.Id)).ToList();
                foreach (var img in images)
                {
                    _context.LaptopImages.Remove(img);
                }

                _context.Laptop.Update(_laptop);
                _diag.Message = "Sucessesfuly updated";

            }
            else
            {
                _context.Laptop.Add(_laptop);
                _diag.Message = "Sucessesfuly add";

            }
            _context.SaveChanges();
            _diag.IsOpen = true;
            _navigation.CurrentView = new FirstVM(_navigation, _diag, null);
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new ThirdVM(_navigation, _diag, _laptop);

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
