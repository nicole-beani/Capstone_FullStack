namespace WaveTheCave.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grotte")]
    public partial class Grotte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grotte()
        {
            Prenotazione = new HashSet<Prenotazione>();
        }

        [Key]
        public int IdGrotte { get; set; }

        public string Nome { get; set; }

        public string Descrizione { get; set; }

        public string Foto { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prenotazione> Prenotazione { get; set; }
    }
}
