using app.app.Admin.Navigation;
using app.app.Admin.Products.Headphones.Add.Second.ViewModel;
using app.app.Diag.ViewModel;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.db.Entities.Phone;
using app.db.Entities.Headphones;
using app.utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace app.app.Admin.Products.Headphones.Add.First.ViewModel
{
    public class FirstVM : ViewModelBase
    {

        private readonly ContentNavigation _navigation;
        private readonly db.Entities.Headphones.Headphones _headphones;
        private readonly DiagVM _diag;
        private readonly List<byte[]> _images;

        private int index = -1;
        private byte[] _currentImage;

        public byte[] CurrentImage
        {
            get { return _currentImage; }
            set { _currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }
        public ICommand AddImageCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PrevImageCommand { get; }
        public ICommand NextCommand { get; }

        public FirstVM(ContentNavigation navigation, DiagVM diag, db.Entities.Headphones.Headphones headphones)
        {
            _navigation = navigation;
            if(headphones != null)
            {
                _headphones = headphones;
                _images = headphones.ProductImages.Select(img => img.Img).ToList();
                CurrentImage = _images.FirstOrDefault();
                NextImage(null);
            }
            else
            {
                _images = new List<byte[]>();
                _headphones = new db.Entities.Headphones.Headphones();
                _headphones.ProductImages = new List<db.Entities.Headphones.HeadphonesImages>();
            }

           
            _diag = diag;
            _navigation = navigation;
            NextCommand = new RelayCommand(Next);
            AddImageCommand = new RelayCommand(AddImage);
            RemoveImageCommand = new RelayCommand(RemoveImage);
            NextImageCommand = new RelayCommand(NextImage);
            PrevImageCommand = new RelayCommand(PrevImage);
        }
        private void AddImage(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.webp;*.bmp|Все файлы|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentImage = File.ReadAllBytes(openFileDialog.FileName);
                _images.Add(CurrentImage);
                index = _images.Count - 1;
            }
        }
        private void Next(object value)
        {
            _headphones.ProductImages.Clear();
            foreach (var img in _images)
            {
                _headphones.ProductImages.Add(new HeadphonesImages() { Img = img });

            }
            if(_headphones.ProductImages.Count != 0)
            {
                _navigation.CurrentView = new SecondVM(_navigation, _diag, _headphones);
            }
            else
            {
                _diag.Message = "add images";
                _diag.IsOpen = true;
            }
        }

        private void RemoveImage(object value)
        {
            if (_images.Count != 0)
            {
                _images.RemoveAt(index);
                if (_images.Count != 0)
                {
                    if (index != 0) index--;
                    CurrentImage = _images[index];
                }
                else
                {
                    CurrentImage = null;
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
