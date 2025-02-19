using app.app.Admin.Navigation;
using app.app.Admin.Products.Laptop.Add.First.ViewModel;
using app.app.Admin.Products.Laptop.Add.Third.ViewModel;
using app.app.Client.Products.SmartWatches.View;
using app.app.Session;
using app.db.Context;
using app.db.Entities;
using app.db.Entities.SmartWatches;
using app.utils;
using application.app.Client.Products.SmartWatches.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app.app.Client.Products.Phone.ViewModel
{
    public class PhoneVM : ViewModelBase
    {
        private readonly Context _context;
        private readonly Diag.ViewModel.DiagVM _diag;
        private readonly List<byte[]> _images;
        private readonly List<Star> _stars_;
        private readonly Session.Session _session;

        private db.Entities.Phone.Phone _phone;
        private int index = -1;
        private byte[] _currentImage;
        private string _desccription;
        private Reviews _review;
        private Star _star;
        private string _isVisibleButton;
        private string _visible;

        private ObservableCollection<Star> _stars;
        private ObservableCollection<Reviews> _reviews;


        public byte[] CurrentImage
        {
            get { return _currentImage; }
            set { _currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }

        public string Description
        {
            get { return _desccription; }
            set { _desccription = value; OnPropertyChanged(nameof(Description)); }
        }

        public db.Entities.Phone.Phone phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(nameof(Laptop)); }
        }
        public string IsVisible
        {
            get { return _visible; }
            set { _visible = value; OnPropertyChanged(nameof(IsVisible)); }

        }
        public ObservableCollection<Reviews> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; OnPropertyChanged(nameof(Reviews)); }

        }




        public string Message
        {
            get { return _review.Message; }
            set { _review.Message = value; OnPropertyChanged(nameof(Message)); }
        }

        public string IsVisibleButton
        {
            get { return _isVisibleButton; }
            set { _isVisibleButton = value; OnPropertyChanged(nameof(IsVisibleButton)); }
        }


   
        public Star Star
        {
            get { return _star; }
            set { _star = value; OnPropertyChanged(nameof(Star)); }

        }
        public ObservableCollection<Star> Stars
        {
            get { return _stars; }
            set { _stars = value; OnPropertyChanged(nameof(Stars)); }

        }


        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PrevImageCommand { get; }
        public ICommand SelectStarCommand { get; }
        public ICommand WriteReviewCommand { get; }

        public ICommand RemoveReviewCommand { get; }
        public PhoneVM(Session.Session session, Diag.ViewModel.DiagVM diag, db.Entities.Phone.Phone phone)
        {
            _context = new Context();
            _review = new Reviews();
            _session = session;
            _phone = phone;
            _diag = diag;
            _images = new List<byte[]>();
            foreach (var img in phone.ProductImages)
            {
                _images.Add(img.Img);
            }
            MessageBox.Show(_images.Count.ToString());

            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
            SelectStarCommand = new RelayCommand(SelectStar);
            RemoveReviewCommand = new RelayCommand(RemoveReview);

            NextImage(null);


            Reviews = new ObservableCollection<Reviews>(_context.Reviews
                           .Include(r => r.User)
                           .Where(r => r.PhonesId.Equals(phone.Id)).ToList());
            if (Reviews.Count > 0)
            {
                IsVisible = "Visible";
            }
            else
            {
                IsVisible = "Hidden";
            }

            Description = $"{phone.Producer.Name}" +
                $" {phone.Model}, " +
                $"color {phone.Color.Value}, " +
                $"OS {phone.OS.Brand.Name}" +
                $" {phone.OS.Version}, " +
                $"{phone.Price} BYN," +
                $" {phone.Width}x{phone.Height}," +
                $" RAM {phone.RAM}, " +
                $"Internal Memory {phone.InternalMemorySize}, " +
                $"wheight {phone.Wheight}," +
                $" processor {phone.Processor.Brand}" +
                $" {phone.Processor.Name}," +
                $" {phone.Camera} MP," +
                $" {phone.Battery} hours," +
                $"Water Protection  {phone.WaterProtection} " +
                $"{phone.Processor.BaseFrequency}-{phone.Processor.BaseFrequency}GHZ," +
                $" in stock {phone.Stock}";


            _stars_ = new List<Star>()
            {
                 new Star()
                {
                    Path = "pack://application:,,,/res/icons/unselected-star.svg",
                    Value = 1
                },
                new Star()
                {
                    Path = "pack://application:,,,/res/icons/unselected-star.svg",
                    Value = 2
                },
                new Star()
                {
                    Path = "pack://application:,,,/res/icons/unselected-star.svg",
                    Value = 3
                },
                new Star()
                {
                    Path = "pack://application:,,,/res/icons/unselected-star.svg",
                    Value = 4
                },
                new Star()
                {
                    Path = "pack://application:,,,/res/icons/unselected-star.svg",
                    Value = 5
                }
            };
            Stars = new ObservableCollection<Star>(_stars_);

            var existringReview = _context.Reviews.Include(r => r.User).Where(r => r.User.Id.Equals(_session.User.Id) && r.PhonesId.Equals(_phone.Id)).ToList();
            WriteReviewCommand = new RelayCommand(WriteReview);

            if (existringReview.Count == 0)
            {
                IsVisibleButton = "Hidden";
            }
            else
            {
                var review = existringReview.First();
                _review = review;
                Message = review.Message;
                Star = new Star()
                {
                    Value = review.Stars
                };
                SelectStar(null);
                IsVisibleButton = "Visible";
            }
        }


        private void SelectStar(object value)
        {


            Stars.Clear();
            foreach (var star in _stars_)
            {
                if (star.Value <= Star.Value)
                {
                    Stars.Add(new Star()
                    {
                        Path = "pack://application:,,,/res/icons/selected-star.svg",
                        Value = star.Value
                    });
                }
                else
                {
                    Stars.Add(new Star()
                    {
                        Path = "pack://application:,,,/res/icons/unselected-star.svg",
                        Value = star.Value
                    });
                }
            }
            _review.Stars = Star.Value;
        }

        private void WriteReview(object value)
        {
            var reviews = _context.Reviews.Where(r => r.UserId.Equals(_session.User.Id) && r.PhonesId.Equals(_phone.Id)).ToList();

            if (reviews.Count != 0)
            {
                UpdateReview(null);
                return;
            }
            else
            {
                var existingUser = _context.Users.Find(_session.User.Id);
                var existingItem = _context.Phone.Find(_phone.Id);

                _review.Phone = existingItem;
                _review.User = existingUser;


                _context.Reviews.Add(new db.Entities.Reviews()
                {
                    Message = _review.Message,
                    Stars = _review.Stars,
                    User = _review.User,
                    Phone = _review.Phone
                });
                _context.SaveChanges();
                _diag.Message = "Review was sucessesfully add";


                Reviews.Add(new db.Entities.Reviews()
                {
                    Stars = _review.Stars,
                    User = _review.User,
                    Message = _review.Message,
                });
                IsVisible = "Visible";
                IsVisibleButton = "Visible";
            }
            _diag.IsOpen = true;
        }

        private void UpdateReview(object value)
        {
            var reviews = _context.Reviews.Where(r => r.UserId.Equals(_session.User.Id) && r.PhonesId.Equals(_phone.Id)).ToList();
            if (_review.Stars == 0 || _review.Message.IsNullOrEmpty())
            {
                _diag.Message = "Fill all fields";
            }
            else if (reviews.Count > 0)
            {
 
                var newReview = reviews.First();
                newReview.Message = Message;
                newReview.Stars = _review.Stars;
                _context.Reviews.Update(newReview);
                _diag.Message = "Review was sucessesfully updated";
                _context.SaveChanges();


                Reviews = new ObservableCollection<Reviews>(_context.Reviews
                    .Include(r => r.User)
                    .Where(r => r.PhonesId.Equals(_phone.Id)).ToList());
            }
            _diag.IsOpen = true;
        }

        private void RemoveReview(object value)
        {
            var existringReview = _context.Reviews.Include(r => r.User).Where(r => r.User.Id.Equals(_session.User.Id) && r.PhonesId.Equals(_phone.Id)).First();

            if (existringReview != null)
            {
                _context.Reviews.Remove(existringReview);
                _context.SaveChanges();
                _diag.Message = "Review was sucessesfully removed";
                _diag.IsOpen = true;
                var reviews = _context.Reviews
                 .Include(r => r.User)
                 .Where(r => r.PhonesId.Equals(_phone.Id)).ToList();
                Reviews = new ObservableCollection<Reviews>(reviews);

                if (Reviews.Count > 0)
                {
                    IsVisible = "Visible";
                }
                else
                {
                    IsVisible = "Hidden";
                }
                IsVisibleButton = "Hidden";
                _review.Stars = 0;
                Message = "";
                Stars.Clear();
                foreach (var star in _stars_)
                {
                    Stars.Add(new Star()
                    {
                        Path = "pack://application:,,,/res/icons/unselected-star.svg",
                        Value = star.Value
                    });
                }
            }



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
