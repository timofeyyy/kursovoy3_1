using app.app.Admin.Navigation;
using app.app.Admin.Reviews.View;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.utils;
using application.app.Client.Products.SmartWatches.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace app.app.Admin.ViewReview.ViewModel
{
    public class ReviewVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly User _user;
        private readonly DiagVM _diagVM;

        private string _diagMessage;
        private db.Entities.Reviews _reviews;
        private ObservableCollection<Star> _stars;

        public ICommand BackCommand { get; }
        public ICommand RemoveCommand { get; }

        public db.Entities.Reviews Review { 
            get { return _reviews; } 
            set { _reviews = value; OnPropertyChanged(nameof(Review)); } 
        }
        public ObservableCollection<Star> Stars
        {
            get { return _stars; }
            set { _stars = value; OnPropertyChanged(nameof(Stars)); }

        }

        public ReviewVM(ContentNavigation contentNavigation, db.Entities.Reviews reviews, DiagVM diagVM)
        {
            _reviews = reviews;
            _context = new Context();
            _diagVM = diagVM;
            _contentNavigation = contentNavigation;
            Stars = new ObservableCollection<Star>();
            for (int i = 0; i < reviews.Stars; i++)
            {
                Stars.Add(new Star()
                {
                    Path = "pack://application:,,,/res/icons/selected-star.svg",
                });
            }
           

            BackCommand = new RelayCommand(Back);
            RemoveCommand = new RelayCommand(Remove);
        }

        private void Back(object value)
        {
            _contentNavigation.CurrentView = new Admin.Reviews.ViewModel.ReviewsVM(_contentNavigation, _diagVM);
        }

        private void Remove(object value)
        {
            _context.Reviews.Remove(_reviews);
            _context.SaveChanges();
            _diagVM.Message = "Was sucessesfullu deleted";
            _diagVM.IsOpen = true;
            Back(null);
        }
    }
}
