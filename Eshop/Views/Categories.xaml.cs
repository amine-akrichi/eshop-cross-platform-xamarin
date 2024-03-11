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
    public partial class Categories : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;
        public ObservableCollection<Categorie> CategorieList;
        public Categories()
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));

            // Initialize Categories as an ObservableCollection
            CategorieList = new ObservableCollection<Categorie>();

            // Set the BindingContext to this instance
            BindingContext = this;

            // Load categories initially
            RefreshCategories();
            categoryListView.ItemsSource = CategorieList;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCategories();
        }

        private void RefreshCategories()
        {
            CategorieList.Clear();
            var categories = _boutiqueDatabase.ObtenirCategories();
            foreach (var category in categories)
            {
                CategorieList.Add(category);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Categorie selectedCategory)
            {
                // Navigate to the ProductsPage, passing the selected category
                Navigation.PushAsync(new ProduitsParCategorie (selectedCategory));
            }

        }
    }
}