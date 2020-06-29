using Microsoft.EntityFrameworkCore;
using DebtsWebApi.DAL.Models;

namespace DebtsWebApi.DAL
{
    public class DebtStatisticContext : DbContext
    {
        public DebtStatisticContext(DbContextOptions<DebtStatisticContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DebtType> DebtTypes { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<DebtId> DebtIds { get; set; }
        public DbSet<DefaultCost> DefaultCosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>()
                .HasKey(d => new { d.Id, d.BeginDate });
            modelBuilder.Entity<DefaultCost>()
                .HasKey(dc => new { dc.Id, dc.BeginDate });
        }
    }
}
