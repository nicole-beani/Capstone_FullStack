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
     

        [Key]
        public int IdPrenotazione { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column(TypeName = "money")]
        public decimal? Importo { get; set; }
        [Column(TypeName = "int")]
        public int? Quantita { get; set; }
        public int? IdGrotte { get; set; }

        public int? IdOrari { get; set; }

        public int? IdUser { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        
        public virtual Orari Orari { get; set; }

        public virtual User User { get; set; }

        public virtual Grotte Grotte { get; set; }
        public Prenotazione( DateTime? data, decimal? importo, int? idOrari, int? idUser)
        {
            
            Data = data;
            Importo = importo;
            IdOrari = idOrari;
            IdUser = idUser;
           
           
        }

        public Prenotazione(DateTime data, decimal importo, int idUser)
        {
            Data = data;
            Importo = importo;
            IdUser = idUser;
        }
    }
}
