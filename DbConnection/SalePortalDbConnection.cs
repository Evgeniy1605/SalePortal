using Microsoft.EntityFrameworkCore;
using SalePortal.Models;

namespace SalePortal.DbConnection
{
    public class SalePortalDbConnection : DbContext
    {
        public SalePortalDbConnection( DbContextOptions<SalePortalDbConnection> options ) : base( options )
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CommodityEntity> commodities { get; set; } 
    }
}
