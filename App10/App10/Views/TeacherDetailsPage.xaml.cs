using App10.Model;
using App10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeacherDetailsPage : ContentPage
	{
		public TeacherDetailsPage (Teacher teacher)
		{
			InitializeComponent ();
            BindingContext = new TeacherViewModel(teacher);
		}
	}
}