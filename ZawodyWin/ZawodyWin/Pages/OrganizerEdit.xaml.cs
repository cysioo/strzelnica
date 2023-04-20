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
    public partial class OrganizerEdit : Page
    {
        private long _organizerId;
        private OrganizerRepository _organizerRepository;

        public OrganizerEdit(Organizer organizer)
        {
            InitializeComponent();
            _organizerRepository = new OrganizerRepository();
            _organizerId = organizer.Id;
            organizerEditor.Organizer = new OrganizerViewModel();
            organizerEditor.Organizer.SetFromDbModel(organizer);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var oganizer = organizerEditor.Organizer.ToDbModel();
            oganizer.Id = _organizerId;
            var updateSucceeded = _organizerRepository.Update(oganizer);
            if (updateSucceeded) { MessageBox.Show("Orgganizator zapisany."); }
        }
    }
}
