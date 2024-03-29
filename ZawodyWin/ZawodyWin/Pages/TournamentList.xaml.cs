﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for TournamentList.xaml
    /// </summary>
    public partial class TournamentList : Page
    {
        private TournamentRepository _tournamentRepository;

        public TournamentList()
        {
            InitializeComponent();
            _tournamentRepository = new TournamentRepository();
            var model = new TournamentListViewModel();
            model.Tournaments = _tournamentRepository.GetAll().OrderByDescending(x => x.Date ?? DateTime.MaxValue);
            DataContext = model;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Get the selected item and navigate to the edit page
                var selectedItem = e.AddedItems[0] as Tournament;
                if (selectedItem != null)
                {
                    NavigationService.Navigate(new TournamentEdit(selectedItem));
                }
            }
        }

    }
}
