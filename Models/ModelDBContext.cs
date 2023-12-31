using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WaveTheCave.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }


        public virtual DbSet<Grotte> Grotte { get; set; }
        public virtual DbSet<Orari> Orari { get; set; }
        public virtual DbSet<Prenotazione> Prenotazione { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grotte>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prenotazione>()
                .Property(e => e.Importo)
                 .HasColumnName("Importo")  
                 .HasColumnType("money");


            base.OnModelCreating(modelBuilder);
        }
    }
}
