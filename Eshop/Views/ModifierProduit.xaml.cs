using Eshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eshop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModifierProduit : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        private Produit _selectedProduit;

        public ModifierProduit(Produit selectedProduit)
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            _selectedProduit = selectedProduit;

            // Populate existing data for editing
            productNameEntry.Text = selectedProduit.Nom;
            descriptionEntry.Text = selectedProduit.Description;
            priceEntry.Text = selectedProduit.Prix.ToString();
            var categories = _boutiqueDatabase.ObtenirCategories();
            foreach (var category in categories)
            {
                categoryPicker.Items.Add(category.Nom);
            }

            // Set the selected category based on the existing product data
            var selectedCategory = categories.FirstOrDefault(c => c.Id == selectedProduit.IdCategorie);
            if (selectedCategory != null)
            {
                categoryPicker.SelectedItem = selectedCategory.Nom;
            }
        }

        private async void SelectImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    productImage.Source = ImageSource.FromFile(result.FullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting image: {ex.Message}");
            }
        }

        private void Update_btn_Clicked(object sender, EventArgs e)
        {
            // Get values from the Entry fields
            string productName = productNameEntry.Text;
            string description = descriptionEntry.Text;

            // Validate and parse the price
            if (decimal.TryParse(priceEntry.Text, out decimal price))
            {
                // Get the selected category
                string selectedCategoryName = categoryPicker.SelectedItem as string;

                if (!string.IsNullOrEmpty(selectedCategoryName))
                {
                    // Find the corresponding category by name
                    var selectedCategory = _boutiqueDatabase.ObtenirCategories().FirstOrDefault(c => c.Nom == selectedCategoryName);

                    if (selectedCategory != null)
                    {
                        // Update the selected product with the new data
                        _selectedProduit.Nom = productName;
                        _selectedProduit.Description = description;
                        _selectedProduit.Prix = price;
                        _selectedProduit.IdCategorie = selectedCategory.Id;

                        // Update the product in the database
                        _boutiqueDatabase.ModifierProduit(_selectedProduit);

                        // Optionally, navigate back to the previous page or perform other actions
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error", "Invalid category selected", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Please select a category", "OK");
                }
            }
            else
            {
                // Handle invalid price input
                DisplayAlert("Error", "Invalid price input", "OK");
            }
        }
    }
}