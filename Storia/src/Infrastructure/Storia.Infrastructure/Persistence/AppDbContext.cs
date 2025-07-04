using Microsoft.EntityFrameworkCore;
using Storia.Core.Entities;
using System.Reflection;

namespace Storia.Infrastructure.Persistence
{
    /// <summary>
    /// O contexto do banco de dados para a aplica��o Storia.
    /// Esta classe � o ponto de entrada do Entity Framework Core para interagir com o banco de dados.
    /// </summary>
    public class AppDbContext : DbContext
    {
        // Define as "tabelas" que o EF Core ir� gerenciar.
        public DbSet<Product> Products { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        /// <summary>
        /// Construtor usado para configurar o contexto, geralmente via inje��o de depend�ncia.
        /// </summary>
        /// <param name="options">As op��es de configura��o do DbContext (ex: connection string).</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configura o modelo de dados usando a Fluent API do EF Core.
        /// Este m�todo � chamado uma vez quando o modelo est� sendo criado.
        /// </summary>
        /// <param name="modelBuilder">A API para construir o modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Configura��o da Entidade Product ---
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.Sku).IsUnique(); // Garante que o SKU seja �nico no banco
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Sku).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Price).HasColumnType("decimal(18, 2)"); // Precis�o para valores monet�rios
            });

            // --- Configura��o da Entidade Lot ---
            modelBuilder.Entity<Lot>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Quantity).HasColumnType("decimal(18, 4)"); // Maior precis�o para quantidades
                entity.Property(l => l.PurchasePrice).HasColumnType("decimal(18, 2)");
                // Define o relacionamento: Um Produto tem muitos Lotes
                entity.HasOne(l => l.Product)
                      .WithMany(p => p.Lots)
                      .HasForeignKey(l => l.ProductId);
            });

            // --- Configura��o da Entidade Sale ---
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.Id);
                // A propriedade TotalAmount � calculada em C# e n�o deve existir no banco de dados.
                entity.Ignore(s => s.TotalAmount);
            });

            // --- Configura��o da Entidade SaleItem ---
            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(si => si.Id);
                entity.Property(si => si.QuantitySold).HasColumnType("decimal(18, 4)");
                entity.Property(si => si.PriceAtTimeOfSale).HasColumnType("decimal(18, 2)");
                entity.Property(si => si.CostAtTimeOfSale).HasColumnType("decimal(18, 2)");

                // Define o relacionamento: Uma Venda tem muitos Itens de Venda
                entity.HasOne(si => si.Sale)
                      .WithMany(s => s.Items)
                      .HasForeignKey(si => si.SaleId);

                // Define o relacionamento: Um Produto est� em muitos Itens de Venda
                entity.HasOne(si => si.Product)
                      .WithMany() // N�o h� propriedade de navega��o de volta de Product para SaleItem
                      .HasForeignKey(si => si.ProductId);
            });
        }
    }
}