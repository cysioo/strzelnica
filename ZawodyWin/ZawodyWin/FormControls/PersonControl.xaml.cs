using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for PersonControl.xaml
    /// </summary>
    public partial class PersonControl : UserControl
    {
        private PersonRepository _personRepository;

        public PersonControl()
        {
            InitializeComponent();
            _personRepository = new PersonRepository();
            if (ViewModel == null)
            {
                ViewModel = new PersonViewModel();
            }

            DataContext = ViewModel;
        }

        public PersonViewModel ViewModel
        {
            get { return (PersonViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
            }
        }

        private void AddPersonIfNew_Click(object sender, RoutedEventArgs e)
        {
            var person = _personRepository.Get(ViewModel.Name, ViewModel.Surname);
            if (person == null)
            {
                person = ViewModel.ToDbModel();
                _personRepository.Add(person);
            }

            var eventArgs = new PersonAddClickedEventArgs(person);
            PersonAddClicked?.Invoke(this, eventArgs);
        }

        public event EventHandler<PersonAddClickedEventArgs> PersonAddClicked;
    }

    public class PersonAddClickedEventArgs : EventArgs
    {
        public Person Person { get; }

        public PersonAddClickedEventArgs(Person person)
        {
            Person = person;
        }
    }
}
