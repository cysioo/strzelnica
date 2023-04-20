using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
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
    public partial class OrganizerList : Page
    {
        private OrganizerRepository _organizerRepository;

        public OrganizerList()
        {
            InitializeComponent();
            _organizerRepository = new OrganizerRepository();
            var model = new OrganizerListViewModel();
            model.Organizers = _organizerRepository.GetAll().OrderBy(x => x.Name);
            DataContext = model;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Get the selected item and navigate to the edit page
                var selectedItem = e.AddedItems[0] as Organizer;
                if (selectedItem != null)
                {
                    NavigationService.Navigate(new OrganizerEdit(selectedItem));
                }
            }
        }
    }
}
