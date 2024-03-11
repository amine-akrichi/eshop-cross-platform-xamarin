using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Eshop.Models
{
    class BoutiqueDataBase
    {
        private readonly SQLiteConnection _baseDeDonnees;

        public BoutiqueDataBase(string cheminBaseDeDonnees)
        {
            try
            {
                _baseDeDonnees = new SQLiteConnection(cheminBaseDeDonnees);
                _baseDeDonnees.CreateTable<Categorie>();
                _baseDeDonnees.CreateTable<Produit>();
                _baseDeDonnees.CreateTable<LigneCommande>();
                _baseDeDonnees.CreateTable<Commande>();
                _baseDeDonnees.CreateTable<ArticlePanier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating database tables: {ex.Message}");
                // Handle the exception, possibly show a message to the user
            }
        }


        // Opérations sur les catégories
        public List<Categorie> ObtenirCategories()
        {
            return _baseDeDonnees.Table<Categorie>().ToList();
        }

        public void AjouterCategorie(Categorie categorie)
        {
            _baseDeDonnees.Insert(categorie);
        }

        public void ModifierCategorie(Categorie categorie)
        {
            _baseDeDonnees.Update(categorie);
        }

        public void SupprimerCategorie(int idCategorie)
        {
            _baseDeDonnees.Delete<Categorie>(idCategorie);
        }

        // Opérations sur les produits
        public List<Produit> ObtenirProduits(int idCategorie)
        {
            return _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == idCategorie).ToList();
        }

        public List<Produit> ObtenirToutProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToList();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.Insert(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            _baseDeDonnees.Update(produit);
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.Delete<Produit>(idProduit);
        }

        // Opérations sur les lignes de commande
        public int AjouterLigneCommandeWithId(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Insert(ligneCommande);
            return ligneCommande.Id;

        }

      

        // Opérations sur les commandes
        public void AjouterCommande(Commande commande)
        {
            _baseDeDonnees.Insert(commande);
        }

        public List<LigneCommande> ObtenirLignesCommande(int idCommande)
        {
            return _baseDeDonnees.Table<LigneCommande>().Where(l => l.Id == idCommande).ToList();
        }

        public void SauvegarderPanier(List<ArticlePanier> articlesPanier)
        {
            _baseDeDonnees.CreateTable<ArticlePanier>();
            _baseDeDonnees.DeleteAll<ArticlePanier>(); // Clear existing entries

            foreach (var article in articlesPanier)
            {
                _baseDeDonnees.Insert(article);
            }
        }

        public List<Commande> ObtenirToutesCommandes()
        {
            return _baseDeDonnees.Table<Commande>().ToList();
        }


        public List<ArticlePanier> ObtenirPanier()
        {
            return _baseDeDonnees.Table<ArticlePanier>().ToList();
        }

    }
}
