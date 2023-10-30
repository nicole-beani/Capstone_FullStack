using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public int IdUser { get; set; }
        
        public int IdGrotte { get; set; }
        public string OrariGrotte { get; set; }
       
        public Cart() { }
        public Cart(int quantita, string nome, decimal costoGrotta, int idGrotte, int idOrario,string orariGrotte)
        {
            Quantita = quantita;
            Nome = nome;
            CostoGrotta = costoGrotta;
            IdGrotte = idGrotte;
            IdOrari = idOrario;
            OrariGrotte = orariGrotte;

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