﻿using System.Numerics;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class CompetitionViewModel
    {
        public string Name { get; set; }
        public long NumberOfRounds { get; set; }
        public string RoundsText => NumberOfRounds == 1 ? "1 seria" : $"{NumberOfRounds} serie";
        public bool DeleteAllowed { get; set; } = true;

        public Competition ToDbModel()
        {
            var result = new Competition();
            result.Name = Name;
            result.NumberOfRounds = NumberOfRounds;
            return result;
        }

        public void SetFromDbModel(Competition competition)
        {
            Name = competition.Name;
            NumberOfRounds = competition.NumberOfRounds;
        }
    }
}
