using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;


namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for VenueList.xaml
    /// </summary>
    public partial class ShootingClubList : Page
    {
        private ShootingClubRepository _shootingClubRepository;

        public ShootingClubList()
        {
            InitializeComponent();
            _shootingClubRepository = new ShootingClubRepository();
            var model = new ShootingClubListViewModel();
            model.ShootingClubs = _shootingClubRepository.GetAll().OrderBy(x => x.Name);
            DataContext = model;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Get the selected item and navigate to the edit page
                var selectedItem = e.AddedItems[0] as ShootingClub;
                if (selectedItem != null)
                {
                    NavigationService.Navigate(new ShootingClubEdit(selectedItem));
                }
            }
        }
    }
}
