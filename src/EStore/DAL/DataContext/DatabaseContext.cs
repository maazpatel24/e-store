using DAL.Entities.Login;
using DAL.Entities.Store;
using DAL.Entities.Store.Features;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public static OptionsBuild Options = new OptionsBuild();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            OptionsBuild.OptionsConfigure(options);
        }

        #region DbSets

        #region Login

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        #endregion Login

        #region Store

        #region Features

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Size> Size { get; set; }

        #endregion Features

        public virtual DbSet<Marka> Markas { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductFeature> ProductFeature { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }

        #endregion Store

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Turkish_CI_AS");

            // ===

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}