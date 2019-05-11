using System.Data.Entity;
using THH.Model.ReportModel.DBModel;

namespace THH.DAL.ReportContext
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext() : base("name=ReportContext")
        {
            //延迟加载
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BaseDbContext>());
        }
        public ReportDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public DbSet<Order> User { get; set; }
        public DbSet<OrderReport> OrderReport { get; set; }
        public DbSet<StoreReport> StoreReport { get; set; }
        public DbSet<ReportLog> ReportLog { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<PointTxnDetail> PointTxnDetail { get; set; }
        public DbSet<PosTxnDetail> PosTxnDetail { get; set; }
        public DbSet<PosTxnSettle> PosTxnSettle { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<OrderReport>();
            modelBuilder.Entity<StoreReport>();
            modelBuilder.Entity<ReportLog>();
            modelBuilder.Entity<Company>();
            modelBuilder.Entity<Merchant>();
            modelBuilder.Entity<Store>();
            modelBuilder.Entity<Machine>();
            modelBuilder.Entity<PointTxnDetail>();
            modelBuilder.Entity<PosTxnDetail>();
            modelBuilder.Entity<PosTxnSettle>();
        }
    }
}
