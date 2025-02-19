using app.app.Admin.Navigation;
using app.app.Admin.Products.Laptop.Add.First.ViewModel;
using app.app.Admin.Products.Laptop.Add.Third.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.Processor;
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

namespace app.app.Admin.Products.Laptop.Add.Second.ViewModel
{
    public class SecondVM :ViewModelBase
    {
        private readonly ContentNavigation _navigation;
        private readonly db.Entities.Laptop.Laptop _laptop;
        private readonly DiagVM _diag;
        private readonly Context _context;

 

        private ObservableCollection<string> _producers;
        private ObservableCollection<string> _processorBrands;
        private ObservableCollection<string> _processorModels;
        private ObservableCollection<string> _videoCardBrands;
        private ObservableCollection<string> _videoCardModels;

        private string _processorBrand;
        public string ProcessorBrand { 
            get { return _processorBrand; }
            set {
                _processorBrand = value;
                _laptop.Processor.Brand = _context.ProcessorBrand.Where(p => p.Name.Equals(value)).FirstOrDefault();
                _processorModels.Clear();
                foreach (var item in _context.ProcessorModel.Where(model => model.Brand.Name.Equals(value) && model.IsLaptop).ToList())
                {
                    ProcessorModels.Add(item.Name);
                }
                ProcessorModel = ProcessorModels.FirstOrDefault();
                OnPropertyChanged(nameof(ProcessorBrand));
            }    
        }
        private string _processorModel;
        public string ProcessorModel { 
            get { return _processorModel; }
            set {
                _processorModel = value;
                _laptop.Processor = _context.ProcessorModel.Where(p => p.Name.Equals(value) && p.IsLaptop).FirstOrDefault(); 
                OnPropertyChanged(nameof(ProcessorModel)); 
            }    
        }
        private string _producer;
        public string Producer { 
           get { return _producer; }
           set {
                _producer = value;
                _laptop.Producer = _context.Producer.Where(p => p.Name.Equals(value)).FirstOrDefault();
                OnPropertyChanged(nameof(Producer)); 
            }    
        }
        private string _videoCardBrand;
        public string VideoCardBrand { 
           get { return _videoCardBrand; }
           set {

                _videoCardBrand = value;
                _laptop.VideoCardModel.Brand = _context.VideoCardBrand.Where(v => v.Name.Equals(value)).FirstOrDefault();
                _videoCardModels.Clear();
                foreach (var item in _context.VideoCardModel.Where(model => model.Brand.Name.Equals(value)).ToList())
                {
                    VideoCardModels.Add(item.Name);
                }
                VideoCardModel = VideoCardModels.FirstOrDefault();
                OnPropertyChanged(nameof(VideoCardBrand));
            }    
        }
        private string _videoCardModel;
        public string VideoCardModel { 
           get { return _videoCardModel; }
           set {
                _videoCardModel = value;
                _laptop.VideoCardModel = _context.VideoCardModel.Where(v => v.Name.Equals(value)).FirstOrDefault();
                OnPropertyChanged(nameof(VideoCardModel));
           }    
        }
        public string Model { 
           get { return _laptop.Model; }
           set {      
               _laptop.Model = value;
               OnPropertyChanged(nameof(VideoCardModel));
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
        public ObservableCollection<string> VideoCardBrands
        {
            get => _videoCardBrands;
            set
            {
                _videoCardBrands = value;
                OnPropertyChanged(nameof(VideoCardBrands));
            }
        }
        public ObservableCollection<string> VideoCardModels
        {
            get => _videoCardModels;
            set
            {
                _videoCardModels = value;
                OnPropertyChanged(nameof(VideoCardModels));
            }
        }
        public SecondVM(ContentNavigation navigation, DiagVM diag, db.Entities.Laptop.Laptop laptop)
        {
            _context = new Context();
            _laptop = laptop;
            _diag = diag;
            _navigation = navigation;
            NextCommand = new RelayCommand(Next);
            PrevCommand = new RelayCommand(Prev);

            Producers = new ObservableCollection<string>(_context.Producer.Select(producer => producer.Name).ToList());
            ProcessorBrands = new ObservableCollection<string>(_context.ProcessorBrand.Select(brand => brand.Name).ToList());
            ProcessorModels = new ObservableCollection<string>();
            VideoCardBrands = new ObservableCollection<string>(_context.VideoCardBrand.Select(brand => brand.Name).ToList());
            VideoCardModels = new ObservableCollection<string>();


            if (_laptop.Producer == null)
            {
                _laptop.Producer = new Producer();
                Producer = Producers.FirstOrDefault();
            }
            else
            {
                Producer = _laptop.Producer.Name;
            }

            if (_laptop.Processor == null)
            {
                _laptop.Processor = new ProcessorModel();
                _laptop.Processor.Brand = new ProcessorBrand();
                ProcessorBrand = ProcessorBrands.First();
            }
            else
            {
                string val = _laptop.Processor.Name;
                ProcessorBrand = _laptop.Processor.Brand.Name;
                ProcessorModel = val;
            }

            if (_laptop.VideoCardModel == null)
            {
                _laptop.VideoCardModel = new db.Entities.Laptop.VideoCard.VideoCardModel();
                _laptop.VideoCardModel.Brand = new db.Entities.Laptop.VideoCard.VideoCardBrand();
                VideoCardBrand = VideoCardBrands.First();
            }
            else
            {
                string val = _laptop.VideoCardModel.Name;
                VideoCardBrand = _laptop.VideoCardModel.Brand.Name;
                VideoCardModel = val;
            }
        }

        private bool CheckValues()
        {
            List<string> values = new List<string>()
            {
                _laptop.VideoCardModel.Brand.Name,
                _laptop.VideoCardModel.Name,
                _laptop.Processor.Brand.Name,
                _laptop.Processor.Name,
                _laptop.Producer.Name,
                _laptop.Model
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
                _navigation.CurrentView = new ThirdVM(_navigation, _diag, _laptop);
            }
            else
            {
                _diag.Message = "Fill all properties";
                _diag.IsOpen = true;
            }
        }
        private void Prev(object value)
        {
            _navigation.CurrentView = new FirstVM(_navigation, _diag, _laptop);
        }
    }
}
