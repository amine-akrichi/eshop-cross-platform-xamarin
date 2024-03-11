using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using SQLiteNetExtensions.Attributes;

namespace Eshop.Models
{
    public class Commande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string NomClient { get; set; }

        public string LignesCommande { get; set; }

    }
}
