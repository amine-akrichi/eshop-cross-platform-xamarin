using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Models
{
    public class LigneCommande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey("Fk_IDProduit")]
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public int Quantite { get; set; }
        public string Nom;
      

    }
}
