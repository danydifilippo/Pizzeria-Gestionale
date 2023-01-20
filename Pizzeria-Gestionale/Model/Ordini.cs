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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordini()
        {
            Articoli_Ordine = new HashSet<Articoli_Ordine>();
        }

        [Key]
        public int IdOrdine { get; set; }

        [Column(TypeName = "money")]
        public decimal Totale { get; set; }

        [Required]
        [StringLength(20)]
        public string StatoOrdine { get; set; }

        public int IdUtente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articoli_Ordine> Articoli_Ordine { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
