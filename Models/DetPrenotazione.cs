namespace WaveTheCave.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetPrenotazione")]
    public partial class DetPrenotazione
    {
        public DetPrenotazione(int quantita, int idGrotte, int idPrenotazione)
        {
            Quantita = quantita;
            IdGrotte = idGrotte;
            IdPrenotazione = idPrenotazione;
        }

        [Key]
        public int IdDetPrenotazione { get; set; }

        public int? Quantita { get; set; }

        public int? IdGrotte { get; set; }

        public int? IdPrenotazione { get; set; }

        public virtual Grotte Grotte { get; set; }

        public virtual Prenotazione Prenotazione { get; set; }
    }
}
