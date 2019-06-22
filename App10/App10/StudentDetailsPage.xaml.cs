using App10.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentDetailsPage : ContentPage
	{
        private Teacher _teacher;
        private Student _student;

		public StudentDetailsPage(Student student, Teacher teacher)
		{
            _student = student;
            _teacher = teacher;
			InitializeComponent ();

            lblId.Text = _student.Id.ToString();
            lblTeacher.Text = $"Teacher: {_teacher.FirstName} {_teacher.LastName}";
            entryFirstName.Text = _student.FirstName;
            entryLastName.Text = _student.LastName;
            entryGrade.Text = _student.Grade.ToString();
            dpBirthday.Date = _student.Birthday;
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            _student.FirstName = entryFirstName.Text;
            _student.LastName = entryLastName.Text;
            _student.Grade = int.Parse(entryGrade.Text);
            _student.Birthday = dpBirthday.Date;

            await App.LocalDB.SaveItemAsync(_student);
            await Navigation.PopAsync();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Pytanie", "Czy na pewno usunąć studenta?", "TAK", "NIE"))
            {
                await App.LocalDB.DeleteItemAsync(_student);
                await Navigation.PopAsync();
            }
        }
    }
}