using System.Windows.Controls;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for OrganizerEditor.xaml
    /// </summary>
    public partial class OrganizerEditor : UserControl
    {
        public OrganizerEditor()
        {
            InitializeComponent();
            if (Organizer == null)
            {
                Organizer = new OrganizerViewModel();
            }

            DataContext = Organizer;
        }

        public OrganizerViewModel Organizer
        {
            get { return (OrganizerViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
