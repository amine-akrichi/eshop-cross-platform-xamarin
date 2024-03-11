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
    public partial class PageProduits : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        public ObservableCollection<Produit> Produits { get; set; }
       

        public PageProduits()
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            Produits  = new ObservableCollection<Produit>();
            BindingContext = this;
            RefreshProducts();
            
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshProducts();
            productListView.ItemsSource = Produits;
        }

        private void RefreshProducts()
        {
            Produits.Clear();
            var products = _boutiqueDatabase.ObtenirToutProduits();
            foreach (var product in products)
            {
                Produits.Add(product);
            }
            
           Console.WriteLine($"Number of products: {Produits.Count}");
        }

        // Handle item selection to navigate to details or update page
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                /*var selectedProduct = (Produit)e.SelectedItem;
                // Navigate to detail or update page, passing selectedProduct.Id
                await Navigation.PushAsync(new ProductDetailPage(selectedProduct.Id));
                productListView.SelectedItem = null; // Clear selection*/
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjoutProduit());
        }


        private void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            // Handle the "Edit" menu item click here
            var selectedProduct = (Produit)((MenuItem)sender).CommandParameter;
            // Navigate to the edit page with the selected product
            Navigation.PushAsync(new ModifierProduit(selectedProduct));
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.CommandParameter is Produit productToDelete)
            {
                var result = await DisplayAlert("Confirmation", $"Supprimer {productToDelete.Nom}?", "Oui", "Non");

                if (result)
                {
                    // Implement logic to delete the selected product from the database
                    _boutiqueDatabase.SupprimerProduit(productToDelete.Id);

                    // Refresh the products in the ListView
                    RefreshProducts();
                }
            }
        }
    }
}