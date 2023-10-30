using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaveTheCave.Models
{
    public class Cart
    {
        public int Quantita { get; set; }

        public string Nome { get; set; }
        public decimal CostoGrotta { get; set; }

        
        public int IdGrotte { get; set; }


        public Cart() { }
        public Cart(int quantita, string nome, decimal costoGrotta, int idGrotte)
        {
            Quantita = quantita;
            Nome = nome;
            CostoGrotta = costoGrotta;
            IdGrotte = idGrotte;

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