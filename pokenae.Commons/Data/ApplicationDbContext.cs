using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace pokenae.Commons.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキスト
    /// </summary>
    public class ApplicationDbContext<TContext> : DbContext where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<TContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 論理削除のフィルタリング
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
                }
            }
        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type, "e");
            var prop = Expression.Property(param, "DeletedAt");
            var condition = Expression.Equal(prop, Expression.Constant(null));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }

        public override int SaveChanges()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";
            var programId = "Unknown"; // ここは適宜変更してください

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedProgramId = programId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedProgramId = programId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedProgramId = programId;
                    entry.Entity.Version++;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userId;
                    entry.Entity.DeletedProgramId = programId;
                }
            }

            return base.SaveChanges();
        }
    }
}
