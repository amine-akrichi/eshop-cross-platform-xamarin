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
    public partial class ProduitsParCategorie : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        public ObservableCollection<Produit> Products { get; set; }
        private Panier Panier;
        public ProduitsParCategorie(Categorie selectedCategory)
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            Products = new ObservableCollection<Produit>(_boutiqueDatabase.ObtenirProduits(selectedCategory.Id));
            BindingContext = this;
            Title = selectedCategory.Nom ;
            Panier = new Panier();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            // Obtain the selected product from the button's BindingContext
            var selectedProduct = (Produit)((Button)sender).BindingContext;

            // Check if the product is not null
            if (selectedProduct != null)
            {
                // Add the selected product to the Panier
                Panier.AjouterArticle(selectedProduct.Id, selectedProduct.Nom, selectedProduct.Prix, 1);

                // Display a confirmation message
                DisplayAlert("Success", $"{selectedProduct.Nom} ajouté au panier!", "OK");
            }
        }
    }
}