using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaveTheCave.Models
{
    public class Cart
    {
        public int Quantita { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public decimal CostoGrotta { get; set; }
        public decimal Importo { get; set; }
      public int IdOrari { get; set; }
        public string OrariGrotte { get; set; }
        
            // ... Altre proprietà ...

            // Proprietà per l'orario selezionato dall'utente
            public int OrarioSelezionato { get; set; }

            // Lista degli orari disponibili
            public List<SelectListItem> OrariDisponibili { get; set; }

            public Cart()
            {
                // Inizializza la lista degli orari disponibili
                OrariDisponibili = new List<SelectListItem>();
            }
        

        public int IdUser { get; set; }
        
        public int IdGrotte { get; set; }
    
       
       
        public Cart(int quantita, string nome, decimal costoGrotta, int idGrotte, int idOrari, string orariGrotte)
        {
            Quantita = quantita;
            Nome = nome;
            CostoGrotta = costoGrotta;
            IdGrotte = idGrotte;
            IdOrari = idOrari;
            OrariGrotte = orariGrotte;
            Quantita = quantita;
      
         

        }

        public static decimal CalcoloCostoTotale(List<Cart> cart)
        {
            decimal tot = 0;
            foreach (Cart item in cart)
            {
                tot += item.Quantita * item.CostoGrotta;
            }
            return tot;
        }
    }
}