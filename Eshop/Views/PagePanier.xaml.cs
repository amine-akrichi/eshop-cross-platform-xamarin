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
    public partial class PagePanier : ContentPage
    {
        private Panier Panier { get; set; }
        private BoutiqueDataBase _boutiqueDatabase { get; set; }

        public PagePanier()
        {
            InitializeComponent();
            Panier = new Panier();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));

            // Load shopping cart from the database
            LoadPanierFromDatabase();

            // Set the binding context and source for the ListView
            BindingContext = this;
            listPanier.ItemsSource = Panier.Articles;
           
        }

        private void IncreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var selectedArticle = (ArticlePanier)((Button)sender).CommandParameter;
            Panier.AugmenterQuantite(selectedArticle.IdProduit);
        }

        private void DecreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var selectedArticle = (ArticlePanier)((Button)sender).CommandParameter;
            Panier.DiminuerQuantite(selectedArticle.IdProduit);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh the content or perform other actions when the page appears
            LoadPanierFromDatabase();
        }


        private void LoadPanierFromDatabase()
        {
            // Retrieve the shopping cart data from the database
            var articlesFromDatabase = _boutiqueDatabase.ObtenirPanier();

            // Add the retrieved items to the shopping cart
            foreach (var article in articlesFromDatabase)
            {
                Panier.AjouterArticle(article.IdProduit, article.NomProduit, article.PrixUnitaire, article.Quantite);
            }
        }

        private void ViderPanier_Clicked(object sender, EventArgs e)
        {
            // Clear the shopping cart
            _boutiqueDatabase.SauvegarderPanier(Panier.Articles);

            // Clear the shopping cart
            Panier.ViderPanier();

            // Optionally, load the shopping cart from the database after clearing
            LoadPanierFromDatabase();
        }

        private async void ValiderCommande_Clicked(object sender, EventArgs e)
        {
            // Prompt the user for their name using an entry alert
            string clientName = await DisplayPromptAsync("Enter votre nom", "Entre votre nom pour valider la commande:", "OK", "Annuler");

            if (clientName != null)
            {
                // Continue with order validation
                if (Panier.Articles.Any())
                {
                    string lignesCommandeString = string.Join(", ", Panier.Articles.Select(article =>
                 $"\nIdProduit: {article.IdProduit}, \nQuantite: {article.Quantite}, \nNomProduit: {article.NomProduit}"));

                    // Create a new Commande object
                    Commande nouvelleCommande = new Commande
                    {
                        NomClient = clientName,
                        LignesCommande = lignesCommandeString
                    };

                    // Save the new order to the database
                    _boutiqueDatabase.AjouterCommande(nouvelleCommande);

                    // Display a success message with order details
                    string orderDetails = $"Votre Cammande a éte passé en success\n\nDetails Commande:\nClient: {clientName}\n\nPrduits:{lignesCommandeString}";


                    await DisplayAlert("Cammande Validé", orderDetails, "OK");

                    Panier.ViderPanier();

                    // Save the updated shopping cart to the database
                    _boutiqueDatabase.SauvegarderPanier(Panier.Articles);

                    LoadPanierFromDatabase();
                }
                else
                {
                    // Display an alert if the cart is empty
                    await DisplayAlert("Panier vide", "Vorte panier est vide , ajouter des produits pour passe la commande", "OK");
                }
            }
        }


    }
}