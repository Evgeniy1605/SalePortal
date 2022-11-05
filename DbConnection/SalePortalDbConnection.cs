using Microsoft.EntityFrameworkCore;
using SalePortal.Data;

namespace SalePortal.DbConnection
{
    public class SalePortalDbConnection : DbContext
    {
        public SalePortalDbConnection( DbContextOptions<SalePortalDbConnection> options ) : base( options )
        {

        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CommodityEntity> commodities { get; set; } 
    }
}
