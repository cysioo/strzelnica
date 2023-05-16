using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;

namespace ZawodyWin.ViewModels
{
    public class ScoreViewModel : INotifyPropertyChanged
    {
        private long _points { get; set; }
        private ScoreRepository _scoreRepository;

        public ScoreViewModel(ScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public long Id {get; set;}
        public long Round { get; set; }
        public long Points
        {
            get { return _points; }
            set
            {
                _points = value;
                _scoreRepository.UpdatePoints(Id, value);
                OnPropertyChanged(nameof(Points));
            }
        }

        public void SetFromDbModel(Score score)
        {
            Id = score.Id;
            Round = score.Round;
            _points = score.Points;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
