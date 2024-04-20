using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class TestDBContext : DbContext
    {
            public TestDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
            {
                
            }
            public DbSet<Record> Records {get; set;}
    }
}