namespace Pizzeria_Gestionale.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articoli_Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articoli_Ordine()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int IdDettaglio { get; set; }

        public int IdProdotto { get; set; }

        [Required]
        public int Quantita { get; set; }

        public decimal Prezzo_Tot { get; set; }

        public string Note { get; set; }

        public List<Articoli_Ordine> cart = new List<Articoli_Ordine>();
        public virtual Prodotti Prodotti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
