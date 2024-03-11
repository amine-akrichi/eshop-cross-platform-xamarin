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
    public partial class PageCategories : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        public ObservableCollection<Categorie> Categories { get; set; }
        public PageCategories()
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));

            // Initialize Categories as an ObservableCollection
            Categories = new ObservableCollection<Categorie>();

            // Set the BindingContext to this instance
            BindingContext = this;

            // Load categories initially
            RefreshCategories();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCategories();
        }

        private void RefreshCategories()
        {
            Categories.Clear();
            var categories = _boutiqueDatabase.ObtenirCategories();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjoutCategorie());
        }

        private void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var selectedCategory = menuItem?.CommandParameter as Categorie;

            if (selectedCategory != null)
            {
                Navigation.PushAsync(new ModifierCategorie(selectedCategory));
            }
        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.CommandParameter is Categorie selectedCategory)
            {
                var result = await DisplayAlert("Confirmation", $"Are you sure you want to delete {selectedCategory.Nom}?", "Yes", "No");

                if (result)
                {
                    // Perform the deletion
                    _boutiqueDatabase.SupprimerCategorie(selectedCategory.Id);
                    RefreshCategories();
                }
            }
        }
    }
}