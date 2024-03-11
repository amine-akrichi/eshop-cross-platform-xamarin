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
    public partial class ModifierCategorie : ContentPage
    {
        BoutiqueDataBase _boutiqueDatabase;
        Categorie _selectedCategorie;
        public ModifierCategorie(Categorie selectedCategorie)
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            Nom.Text = selectedCategorie.Nom;
            _selectedCategorie = selectedCategorie;
        }

      

        private void Update_btn_Clicked(object sender, EventArgs e)
        {

            _selectedCategorie.Nom = Nom.Text;

            // Save changes to the database
            _boutiqueDatabase.ModifierCategorie(_selectedCategorie);

            // Optionally, navigate back to the previous page or perform other actions
            Navigation.PopAsync();

        }
    }
}