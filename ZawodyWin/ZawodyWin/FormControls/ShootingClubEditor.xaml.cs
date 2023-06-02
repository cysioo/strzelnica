using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for ShootingClubEditor.xaml
    /// </summary>
    public partial class ShootingClubEditor : UserControl
    {
        public ShootingClubEditor()
        {
            InitializeComponent();
            if (ShootingClub == null)
            {
                ShootingClub = new ShootingClubViewModel();
            }

            DataContext = ShootingClub;
        }

        public ShootingClubViewModel ShootingClub
        {
            get { return (ShootingClubViewModel)DataContext; }
            set { DataContext = value; value.PropertyChanged += ViewModel_PropertyChanged; }
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShootingClubViewModel.LogoPathExisting))
            {
                var viewModel = sender as ShootingClubViewModel;
                if (!string.IsNullOrEmpty(viewModel.LogoPathExisting))
                {
                    var absoluteUri = new Uri(new Uri(System.AppDomain.CurrentDomain.BaseDirectory, UriKind.Absolute), new Uri(viewModel.LogoPathExisting, UriKind.Relative));
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = absoluteUri;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imageLogo.Source = bitmap;
                }
                else
                {
                    imageLogo.Source = null;
                }
            }
        }

        private void btnPickLogoFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf|All |*";
            if (openFileDialog.ShowDialog() == true)
            {
                ShootingClub.LogoPathToUpload = openFileDialog.FileName;
            }
        }
    }
}
