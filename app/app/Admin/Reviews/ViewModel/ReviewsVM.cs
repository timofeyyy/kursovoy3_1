using app.app.Admin.Navigation;
using app.app.Admin.ViewAdmin.ViewModel;
using app.app.Admin.ViewUser.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.db.Entities;
using app.utils;
using Microsoft.EntityFrameworkCore;
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

namespace app.app.Admin.Reviews.ViewModel
{
    public class ReviewsVM : ViewModelBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly Context _context;
        private readonly DiagVM _diagVM;

        private ObservableCollection<db.Entities.Reviews> _reviews;

        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
            }
        }
      
        public ObservableCollection<db.Entities.Reviews> Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }
        public db.Entities.Reviews Review { get; set; }

 
        public ICommand OpenProfileCommand { get; }
        public ICommand OpenReviewCommand { get; }

        public ReviewsVM(ContentNavigation contentNavigation, DiagVM diag) {
            _diagVM = diag;
            _contentNavigation = contentNavigation;
            _context = new Context();
            Reviews = new ObservableCollection<db.Entities.Reviews>(
               _context.Reviews
                 .Include(r => r.User)
                 .Include(r => r.Laptop)
                 .Include(r => r.Phone)
                 .Include(r => r.SmartWatch)
                 .Include(r => r.Headphones)
                 .ToList()
                );

            OpenReviewCommand = new RelayCommand(OpernReview);

        }

  
        public void OpernReview(object value)
        {
            _contentNavigation.CurrentView = new Admin.ViewReview.ViewModel.ReviewVM(_contentNavigation, Review, _diagVM);
        }

    }
}
