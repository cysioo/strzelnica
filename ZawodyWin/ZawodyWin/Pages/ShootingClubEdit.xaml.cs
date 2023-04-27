using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZawodyWin.DataModels;
using ZawodyWin.FormControls;
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
            var updateSucceeded = _shootingClubRepository.Update(shootingClub);
            if (updateSucceeded) { MessageBox.Show("Klub zapisany."); }
        }
    }
}
