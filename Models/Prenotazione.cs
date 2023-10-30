namespace WaveTheCave.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prenotazione")]
    public partial class Prenotazione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prenotazione()
        {
            DetPrenotazione = new HashSet<DetPrenotazione>();
        }

        [Key]
        public int IdPrenotazione { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column(TypeName = "money")]
        public decimal? Importo { get; set; }

        public int? IdOrari { get; set; }

        public int? IdUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetPrenotazione> DetPrenotazione { get; set; }

        public virtual Orari Orari { get; set; }

        public virtual User User { get; set; }
        public Prenotazione( DateTime? data, decimal? importo, int? idOrari, int? idUser)
        {
            
            Data = data;
            Importo = importo;
            IdOrari = idOrari;
            IdUser = idUser;
           
           
        }

    }
}
