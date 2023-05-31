using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZawodyWin.Common;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for VenueEdit.xaml
    /// </summary>
    public partial class ShootingClubEdit : Page
    {
        private long _shootingClubId;
        private ShootingClubRepository _shootingClubRepository;

        public ShootingClubEdit(ShootingClub shootingClub)
        {
            InitializeComponent();
            _shootingClubRepository = new ShootingClubRepository();
            _shootingClubId = shootingClub.Id;
            shootingClubEditor.ShootingClub = new ShootingClubViewModel();
            shootingClubEditor.ShootingClub.SetFromDbModel(shootingClub);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var shootingClub = shootingClubEditor.ShootingClub.ToDbModel();
            shootingClub.Id = _shootingClubId;
            if (shootingClubEditor.ShootingClub.LogoPathToUpload != null)
            {
                var fileName = $"logo-club-{_shootingClubId}";
                var extension = System.IO.Path.GetExtension(shootingClubEditor.ShootingClub.LogoPathToUpload);
                var path = FileOperations.PrepareFilePath(Settings.ImagesFolder, fileName, extension);
                System.IO.File.Copy(shootingClubEditor.ShootingClub.LogoPathToUpload, path);
                shootingClub.LogoPath = path;
            }
            var updateSucceeded = _shootingClubRepository.Update(shootingClub);
            if (updateSucceeded) { MessageBox.Show("Klub zapisany."); }
        }
    }
}
