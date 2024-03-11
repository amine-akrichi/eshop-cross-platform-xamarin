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
    public partial class AjoutProduit : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        private string selectedImagePath;
        public AjoutProduit()
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            var categories = _boutiqueDatabase.ObtenirCategories();
            foreach (var category in categories)
            {
                categoryPicker.Items.Add(category.Nom);
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
                    selectedImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error selecting image: {ex.Message}");
            }
        }

        private void Add_btn_Clicked(object sender, EventArgs e)
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
                        // Create a new product with the selected category and add it to the database
                        Produit newProduct = new Produit
                        {
                            Nom = productName,
                            Description = description,
                            Prix = price,
                            IdCategorie = selectedCategory.Id,
                            UrlImage = selectedImagePath
                        };

                        // Add newProduct to the database using your BoutiqueDataBase instance
                         _boutiqueDatabase.AjouterProduit(newProduct);

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