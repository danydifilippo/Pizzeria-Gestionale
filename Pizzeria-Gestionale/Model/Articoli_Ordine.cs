namespace Pizzeria_Gestionale.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articoli_Ordine
    {

        [Key]
        public int IdDettaglio { get; set; }

        public int IdProdotto { get; set; }

        [Required]
        public int Quantita { get; set; }

        public decimal Prezzo_Tot { get; set; }

        public string Note { get; set; }

        public int? IdOrdine { get; set; }

        public int? IdUtente { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
