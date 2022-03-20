using Microsoft.EntityFrameworkCore;

namespace HealthDAL
{
    public class FileDBContext : DbContext
    {
        public FileDBContext()
        {

        }

        public FileDBContext(DbContextOptions<FileDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<FileDAL> Files { get; set; }
    }
}
