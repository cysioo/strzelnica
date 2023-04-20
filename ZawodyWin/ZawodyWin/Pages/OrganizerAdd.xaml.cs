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
using ZawodyWin.FormControls;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for VenueAdd.xaml
    /// </summary>
    public partial class OrganizerAdd : Page
    {
        public OrganizerAdd()
        {
            InitializeComponent();

            var organizer = new OrganizerViewModel();
            organizerEditor.Organizer = organizer;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var organizer = organizerEditor.Organizer.ToDbModel();
            var repo = new OrganizerRepository();
            var id = repo.Add(organizer);
            organizer.Id = id;
            NavigationService.Navigate(new OrganizerEdit(organizer));
        }
    }
}
