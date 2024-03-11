using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Models
{
    class ArticlePanier
    {
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
    }

}
