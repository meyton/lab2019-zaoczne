using App10.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentsPage : ContentPage
    {
        private Teacher _teacher;
        public StudentsPage(Teacher teacher)
        {
            _teacher = teacher;
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var student = (Student)e.Item;

            await Navigation.PushAsync(new StudentDetailsPage(student, _teacher));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshList();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var firstName = entryFirstName.Text;
            var lastName = entryLastName.Text;
            var grade = int.Parse(entryGrade.Text);
            var birthday = dpBirthday.Date;

            var student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                Grade = grade,
                Birthday = birthday,
                TeacherId = _teacher.Id
            };

            await App.LocalDB.SaveItemAsync(student);
            await RefreshList();
        }

        private async Task RefreshList()
        {
            var students = await App.LocalDB.GetStudentsByTeacherId(_teacher.Id);
            lvStudents.ItemsSource = students;
        }
    }
}
