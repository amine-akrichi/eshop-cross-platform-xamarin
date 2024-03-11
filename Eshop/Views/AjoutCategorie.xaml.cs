using Eshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjoutCategorie : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        public AjoutCategorie()
        {
            InitializeComponent();

            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
        }

        private void Add_btn_Clicked(object sender, EventArgs e)
        {
            // Get values from the Entry fields
            string categoryName = categoryNameEntry.Text;

            if (categoryName != null)
            {
                Categorie newCategory = new Categorie
                {
                    Nom = categoryName
                };

                _boutiqueDatabase.AjouterCategorie(newCategory);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "Nom categorie invalid", "OK");
            }
        }
    }
}