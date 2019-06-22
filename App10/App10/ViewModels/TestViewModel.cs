using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App10.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    RaisePropertyChanged(nameof(Message));
                    RaisePropertyChanged(nameof(TextColor));
                }
            }
        }

        public Color TextColor
        {
            get
            {
                if (Message == "Success")
                    return Color.Green;
                else
                    return Color.Black;
            }
        }

        private string _firstName;

        public Command<string> NavigateCommand { get; set; }

        public TestViewModel()
        {
            FirstName = "Jan";
            NavigateCommand = new Command<string>((x) => Abort(x));
            Message = "1234";
            Items = new ObservableCollection<string>();
            Init();
        }

        private void Abort(string x)
        {
            Message = x;
            var a = FirstName;
        }

        public ObservableCollection<string> Items { get; set; }
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

        private async void Init()
        {
            Items.Add("Item 1");
            await Task.Delay(3000);
            Message = "Connecting...";
            Items.Add("Item 2");
            Items.Add("Item 3");
            await Task.Delay(2000);
            Items.RemoveAt(1);
            Items.Add("Item 4");
            Message = "Almost done...";
            await Task.Delay(2000);
            Message = "Success";
            Items.Add("Item 5");
            Items.Add("Item 6");
        }
    }
}
