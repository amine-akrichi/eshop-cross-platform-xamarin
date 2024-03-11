using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;

namespace Eshop.Models
{
    class Panier
    {
        public List<ArticlePanier> Articles { get; set; }
        private BoutiqueDataBase _boutiqueDatabase { get; set; }
        public Panier()
        {
            Articles = new List<ArticlePanier>();
            _boutiqueDatabase = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boutique.db"));
            ChargerPanierDepuisDatabase();
        }

        public void AjouterArticle(int idProduit, string nomProduit, decimal prixUnitaire, int quantite)
        {
            var articleExist = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (articleExist != null)
            {
                articleExist.Quantite += 1;
            }
            else
            {
                Articles.Add(new ArticlePanier
                {
                    IdProduit = idProduit,
                    NomProduit = nomProduit,
                    PrixUnitaire = prixUnitaire,
                    Quantite = 1
                });
            }

            SauvegarderPanierEnDatabase();
        }

        public void RetirerArticle(int idProduit)
        {
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (article != null)
            {
                Articles.Remove(article);
                SauvegarderPanierEnDatabase();
            }
        }

        public decimal CalculerTotal()
        {
            return Articles.Sum(article => article.PrixUnitaire * article.Quantite);
        }

        public void ViderPanier()
        {
            Articles.Clear();
            SauvegarderPanierEnDatabase();
            
        }

        private void SauvegarderPanierEnDatabase()
        {
            var cartForDatabase = new List<ArticlePanier>();

            foreach (var article in Articles)
            {
                cartForDatabase.Add(new ArticlePanier
                {
                    IdProduit = article.IdProduit,
                    NomProduit = article.NomProduit,
                    PrixUnitaire = article.PrixUnitaire,
                    Quantite = article.Quantite
                });
            }

            _boutiqueDatabase.SauvegarderPanier(cartForDatabase);
        }

        private void ChargerPanierDepuisDatabase()
        {
            var articlesFromDatabase = _boutiqueDatabase.ObtenirPanier();

            Articles.Clear();

            foreach (var article in articlesFromDatabase)
            {
                Articles.Add(new ArticlePanier
                {
                    IdProduit = article.IdProduit,
                    NomProduit = article.NomProduit,
                    PrixUnitaire = article.PrixUnitaire,
                    Quantite = article.Quantite
                    
                });
            }
        }

        public void AugmenterQuantite(int idProduit)
        {
            
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (article != null)
            {
                article.Quantite++;
            }
        }

        public void DiminuerQuantite(int idProduit)
        {
            
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (article != null && article.Quantite > 1)
            {
                article.Quantite--;
            }
            else if (article != null && article.Quantite == 1)
            {
                
                RetirerArticle(idProduit);
            }
        }

    }
}
