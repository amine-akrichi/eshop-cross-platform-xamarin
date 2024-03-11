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
    public partial class PageCommandes : ContentPage
    {
        private BoutiqueDataBase _boutiqueDatabase;

        public ObservableCollection<Commande> Commandes { get; set; }

        public PageCommandes()
        {
            InitializeComponent();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            Commandes = new ObservableCollection<Commande>(_boutiqueDatabase.ObtenirToutesCommandes());
            BindingContext = this;
        }
        private void OnCommandSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            // Retrieve the selected command
            var selectedCommand = (Commande)e.SelectedItem;

            // Display an alert with the details of the selected command
            DisplayAlert("Details de Commande", $"Client: {selectedCommand.NomClient}\n LigneCommande: {selectedCommand.LignesCommande}", "OK");

            // Deselect the item
            ((ListView)sender).SelectedItem = null;
        }
    }
}