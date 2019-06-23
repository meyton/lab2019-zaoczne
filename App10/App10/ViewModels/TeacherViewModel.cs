using App10.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App10.ViewModels
{
    public class TeacherViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChanged(nameof(LastName));
                }
            }
        }

        public int ID { get => _teacher.Id; }

        private Teacher _teacher;

        public Command SaveCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public TeacherViewModel(Teacher teacher)
        {
            _teacher = teacher;
            FirstName = _teacher.FirstName;
            LastName = _teacher.LastName;
            SaveCommand = new Command(async () => await SaveChanges());
            DeleteCommand = new Command(async () => await Delete());
        }

        private async Task Delete()
        {
            await App.LocalDB.DeleteItemAsync(_teacher);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task SaveChanges()
        {
            _teacher.FirstName = FirstName;
            _teacher.LastName = LastName;
            await App.LocalDB.SaveItemAsync(_teacher);
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
