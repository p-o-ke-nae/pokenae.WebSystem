using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Data;
using pokenae.WebSystem.Core.Entities;

namespace pokenae.WebSystem.Infrastructure.Data
{
    /// <summary>
    /// WebSystem用のデータベースコンテキスト
    /// </summary>
    public class WebSystemDbContext : ApplicationDbContext<WebSystemDbContext>
    {
        public WebSystemDbContext(DbContextOptions<WebSystemDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
        }

        public DbSet<M1Page> Pages { get; set; }
        public DbSet<M1Tag> Tags { get; set; }
        public DbSet<T1PageTag> PageTags { get; set; }
        public DbSet<M1Role> Roles { get; set; }
        public DbSet<T1UserRole> UserRoles { get; set; }
        public DbSet<T1UserPageAccess> UserPageAccesses { get; set; }

        /// <summary>
        /// モデルの作成時にエンティティの制約を設定します
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 複合キーの設定
            modelBuilder.Entity<T1PageTag>()
                .HasKey(pt => new { pt.PageId, pt.TagId });

            modelBuilder.Entity<T1PageTag>()
                .HasOne(pt => pt.Page)
                .WithMany()
                .HasForeignKey(pt => pt.PageId);

            modelBuilder.Entity<T1PageTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PageTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<T1UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<T1UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            // Update M1Page configuration if necessary
            modelBuilder.Entity<M1Page>().ToTable("M1Page");
            modelBuilder.Entity<T1UserPageAccess>().ToTable("T1UserPageAccess");
        }
    }
}


