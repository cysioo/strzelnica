using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.Pdf;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for TournamentEdit.xaml
    /// </summary>
    public partial class TournamentEdit : Page
    {
        private Tournament _tournament;
        private TournamentRepository _tournamentRepository;
        private ShootingClubRepository _shootingClubRepository;
        private CompetitionRepository _competitionRepository;

        public TournamentEdit(Tournament tournament)
        {
            InitializeComponent();
            _tournamentRepository = new TournamentRepository();
            _shootingClubRepository = new ShootingClubRepository();
            _competitionRepository = new CompetitionRepository();

            _tournament = tournament;
            tournamentEditor.Tournament = new TournamentViewModel();
            tournamentEditor.Tournament.SetFromDbModel(tournament);
            var allOrganizers = _shootingClubRepository.GetAll();
            tournamentEditor.Tournament.PopulateOrganizers(allOrganizers);
            var competitions = _competitionRepository.GetByTournamentId(tournament.Id);
            tournamentEditor.Tournament.PopulateCompetitions(competitions);

            refereesControl.Tournament = tournament;
        }

        private bool IsViewModelValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(tournamentEditor.Tournament.Name))
                {
                    MessageBox.Show("Wpisz nazwę turnieju");
                    return false;
                }
                return true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsViewModelValid)
            {
                var tournament = tournamentEditor.Tournament.ToDbModel();
                tournament.Id = _tournament.Id;
                var updateSucceeded = _tournamentRepository.Update(tournament);

                _competitionRepository.SetTournamentCompetitions(_tournament.Id, tournamentEditor.Tournament.Competitions);

                if (updateSucceeded) { MessageBox.Show("Turniej zapisany."); }
            }
        }

        private void btnContestantsRedirect_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TournamentContestantList(_tournament));
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            var pdfFactory = new PdfFactory();
            var pdfPath = pdfFactory.CreateTournamentPdf(_tournament);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = Path.GetExtension(pdfPath);
            saveFileDialog.Title = "Wyniki zawodów";
            saveFileDialog.FileName = Path.GetFileName(pdfPath);
            if (saveFileDialog.ShowDialog() == true)
                File.Copy(pdfPath, saveFileDialog.FileName);
        }
    }
}
