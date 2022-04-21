using Microsoft.EntityFrameworkCore;
using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models.Data
{
    public class DataContext : DbContext
    {
        protected DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<OrderEntity> Orders { get; set; } = null!;
        public virtual DbSet<OrderedProductEntity> OrderedProducts { get; set; } = null!;

    }
}
