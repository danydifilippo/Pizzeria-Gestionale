namespace Pizzeria_Gestionale.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            Articoli_Ordine = new HashSet<Articoli_Ordine>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Articolo")]
        public string NomeProdotto { get; set; }


        [StringLength(20)]
        public string Foto { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Prezzo { get; set; }

        [NotMapped()]
        public int Qta { get; set; }

        [StringLength(10)]
        [Display(Name ="Pronta in ")]
        public string TempiConsegna { get; set; }


        public string Ingredienti { get; set; }

        public bool Update(Prodotti p)
        {
            ModelDbContext db1 = new ModelDbContext();
            db1.Entry(p).State = EntityState.Modified;
            db1.SaveChanges();
            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articoli_Ordine> Articoli_Ordine { get; set; }
    }
}
