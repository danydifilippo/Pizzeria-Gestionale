namespace Pizzeria_Gestionale.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [Key]
        public int IdOrdine { get; set; }

        [Column(TypeName = "money")]
        public decimal Totale { get; set; }

        [Required]
        [StringLength(20)]
        public string StatoOrdine { get; set; }

        public int IdDettaglio { get; set; }

        public int IdUtente { get; set; }

        public virtual Articoli_Ordine Articoli_Ordine { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
