using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THH.Model;
using THH.Model.BaseModule;
using THH.Model.TestModel;

namespace THH.DAL
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base("name=ERPContext")
        {
            //延迟加载
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;  //自动监测变化，默认值为 true  
                                                                 //删除数据库
                                                                 //Database.Initialize(true);
                                                                 // Database.SetInitializer<BaseDbContext>(null);
                                                                 // Database.SetInitializer(new CreateDatabaseIfNotExists<BaseDbContext>());//数据库不存在时，自动创建数据库
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BaseDbContext>());//数据库不存在时，自动创建数据库
                                                                                           // Database.SetInitializer(new DropCreateDatabaseAlways<BaseDbContext>());//先删除原数据库，后创建新的数据库
                                                                                           //Database.SetInitializer(new BaseDbContext<ERPContext>());//每次均先删除原数据库再创建新的数据库，不管数据模型是否发生改变
        }
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        public BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public DbSet<User> User { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SysButton> SysButton { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRights> UserRights { get; set; }
        public DbSet<TestTable> TestTable { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Goods> Goods { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<SysMenu>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<UserRole>();
            modelBuilder.Entity<SysButton>();
            modelBuilder.Entity<UserRights>();
            modelBuilder.Entity<TestTable>();
            modelBuilder.Entity<Student>();
            modelBuilder.Entity<StudentAddress>();
            modelBuilder.Entity<Log>();
            modelBuilder.Entity<Goods>();
            //  Database.SetInitializer<BaseDbContext>(new ManagerInitializer());
            // 禁用默认表名复数形式
            //     modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用一对多级联删除
            //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 禁用多对多级联删除
            //     modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
