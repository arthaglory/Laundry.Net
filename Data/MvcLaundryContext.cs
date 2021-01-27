using Microsoft.EntityFrameworkCore;
using MvcLaundry.Models;

namespace MvcLaundry.Data
{
    public class MvcLaundryContext : DbContext
    {
        public MvcLaundryContext (DbContextOptions<MvcLaundryContext> options)
            : base(options)
        {
        }

        public DbSet<JenisPakaian> JenisPakaian { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<Transaksi> Transaksi {get; set;}
        
    }
}