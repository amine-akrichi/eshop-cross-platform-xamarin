using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SQLite;
using SQLitePCL;

namespace Eshop.Models
{
    public class Produit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }
        public string Description { get; set; }

        [NotNull]
        public decimal Prix { get; set; }

        public string UrlImage { get; set; }

        [ForeignKey("Fk_IdCategorie")]
        public int IdCategorie { get; set; }

    }
}
