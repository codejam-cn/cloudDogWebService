using System.Data.Entity;

namespace cdws.Service
{
    public class CdwsContext : DbContext
    {
        public static CdwsContext Db = new CdwsContext();
        public CdwsContext()
            : base("name=DefaultConnection")
        {
            
        }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<CdwsItem> Cdwses { get; set; }
    }

    
}