using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Internal;

namespace WebApi
{

    public enum AppTable
    {
        TableName = 0,
    }

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppHistory> AppHistory { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InvitationKey> InvitationKeys { get; set; }

    }
}