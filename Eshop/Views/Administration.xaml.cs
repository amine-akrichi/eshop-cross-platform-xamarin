using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Eshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Administration : ContentPage
    {
        

        public Administration()
        {
            InitializeComponent();
        }

       
        public void Button_produits_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageProduits());
        }
        public void Button_categories_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageCategories());
        }

        private void Button_commande_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageCommandes());
        }
    }
}