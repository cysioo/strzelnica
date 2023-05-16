using System;
using System.Collections.Generic;
using System.Linq;
using ZawodyWin.DataModels;
using ZawodyWin.DB;

namespace ZawodyWin.Repositories
{
    public class ScoreRepository
    {
        public ScoreRepository() { 
        }

        public IEnumerable<Score> GetScoresForCompetitions(IEnumerable<long> competitionIds)
        {
            using (var context = new DataContext())
            {
                var result = context.Scores.Where(x => competitionIds.Contains(x.CompetitionId));
                return result.ToList();
            }
        }

        public IEnumerable<Score> AddContestantToCompetition(long contestantId, Competition competition)
        {
            using (var context = new DataContext())
            {
                for (var i = 0; i < competition.NumberOfRounds; i++)
                {
                    var score = new Score
                    {
                        CompetitionId = competition.Id,
                        ContestantId = contestantId,
                        Round = i + 1
                    };
                    context.Scores.Add(score);
                    yield return score;
                }
                context.SaveChanges();
            }
        }

        internal bool UpdatePoints(long id, long points)
        {
            using (var context = new DataContext())
            {
                var score = new Score { Id = id, Points = points };
                context.Scores.Attach(score).Property(x => x.Points).IsModified = true;
                var numberOfUpdatedRows = context.SaveChanges();
                if (numberOfUpdatedRows == 0)
                {
                    throw new InvalidOperationException($"No score updated (expected to update points for {id}!.");
                }
                if (numberOfUpdatedRows > 1)
                {
                    throw new InvalidOperationException($"No score updated (expected to update points for score {id}!.");
                }
                return numberOfUpdatedRows == 1;
            }
        }
    }
}
