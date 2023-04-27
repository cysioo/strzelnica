using System.Windows.Controls;
using System.Windows.Navigation;
using ZawodyWin.FormControls;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for VenueAdd.xaml
    /// </summary>
    public partial class ShootingClubAdd : Page
    {
        public ShootingClubAdd()
        {
            InitializeComponent();

            var shootingClub = new ShootingClubViewModel();
            shootingClubEditor.ShootingClub = shootingClub;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var shootingClub = shootingClubEditor.ShootingClub.ToDbModel();
            var repo = new ShootingClubRepository();
            var id = repo.Add(shootingClub);
            shootingClub.Id = id;
            NavigationService.Navigate(new ShootingClubEdit(shootingClub));
        }
    }
}
