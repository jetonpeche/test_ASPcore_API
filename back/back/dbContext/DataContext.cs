using back.table;
using Microsoft.EntityFrameworkCore;

namespace back.dbContext
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> _options): base(_options) { }

       protected override void OnModelCreating(ModelBuilder _modelBuilder)
       {
            // clé primaire composé
            _modelBuilder.Entity<PainCommande>().HasKey(table => new { table.idCommande, table.idPain });

            // clé primaire et étrangere

            // clé etrangere idCommande
            _modelBuilder.Entity<PainCommande>().HasOne(c => c.commande).WithMany(pc => pc.listePainCommande).HasForeignKey(ci => ci.idCommande);

            // clé etrangere idPain
            _modelBuilder.Entity<PainCommande>().HasOne(p => p.pain).WithMany(pc => pc.listePainCommande).HasForeignKey(pi => pi.idPain);
       }

        // Add-Migration ""
        // update-Database

        public DbSet<Departement> departement { get; set; }
        public DbSet<Pain> pain { get; set; }
        public DbSet<Utilisateur> utilisateur { get; set; }
        public DbSet<Commande> commande { get; set; }
        public DbSet<PainCommande> painCommande { get; set; }
    }
}
