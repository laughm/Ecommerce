using BEcommerce2011.Model;
using Microsoft.EntityFrameworkCore;

namespace BEcommerce2011.DAL
{
    public class EcommerceDbContext:DbContext
    {
        // 构造函数
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):base(options)
        {
            
        }

        // 映射
        public DbSet<UserInfo>? UserInfo { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<LoginCount> LoginCount { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<GoodsType> GoodsType { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<GoodsPropType> GoodsPropType { get; set; }
        public DbSet<GoodsProp> GoodsProp { get; set; }
        public DbSet<Goods> Goods { get; set; }
    }
}