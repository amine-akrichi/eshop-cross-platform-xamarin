
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {


           
            if (username.Text == "admin2024" && password.Text == "admin2024")
            {
             
                Navigation.PushAsync(new Administration());
            }
            else
            {
                
                DisplayAlert("Error", "Username ou password invalid", "OK");
            }
        }
    }
}