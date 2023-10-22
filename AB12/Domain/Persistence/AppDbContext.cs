using AB12.Domain.Base.Common;
using AB12.Domain.Base.Schema;
using AB12.Domain.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AB12.Domain.Persistence
{
    public class AppDbContext : DbContext
    {
        #region Props
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderProducts { get; set; }
        #endregion

        #region Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        #endregion

        #region Private Methods       
        private void Trail()  // Audit Trailing
        {
            ChangeTracker.DetectChanges();

            #region Adding
            var adding = ChangeTracker.Entries()
                                               .Where(track => track.State == EntityState.Added)
                                               .Select(track => track.Entity)
                                               .ToArray();

            foreach (var entity in adding.OfType<AuditableEntity>())
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            #endregion

            #region Updating
            var updating = ChangeTracker.Entries()
                                                 .Where(track => track.State == EntityState.Modified)
                                                 .Select(track => track.Entity)
                                                 .ToArray();

            foreach (var entity in updating.OfType<AuditableEntity>())
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
            #endregion

            #region Deleting
            //will do later
            #endregion

        }
        #endregion

        #region Override Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProductConfig)));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(OrderConfig)));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(OrderItemConfig)));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Trail();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
