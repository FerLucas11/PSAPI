using Microsoft.EntityFrameworkCore;
using ProjetoStefaniniESGreen.Models;

namespace ProjetoStefaniniESGreen.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
