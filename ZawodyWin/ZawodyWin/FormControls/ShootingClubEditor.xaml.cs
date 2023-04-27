using System.Windows.Controls;
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
            set { DataContext = value; }
        }
    }
}
