using Storia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrato para o repositório de produtos.
    /// Define as operações de dados padrão (CRUD) e outras consultas
    /// específicas para a entidade Product.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Busca um produto pelo seu identificador único.
        /// </summary>
        /// <param name="id">O Guid do produto.</param>
        /// <returns>A entidade Product ou null se não for encontrada.</returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Busca um produto pelo seu SKU (Stock Keeping Unit).
        /// </summary>
        /// <param name="sku">O código SKU do produto.</param>
        /// <returns>A entidade Product ou null se não for encontrada.</returns>
        Task<Product?> GetBySkuAsync(string sku);

        /// <summary>
        /// Retorna uma coleção de todos os produtos.
        /// </summary>
        /// <returns>Uma coleção de produtos.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        // A assinatura agora é síncrona, pois apenas modifica o Change Tracker do EF.
        // O 'save' real será feito pelo IUnitOfWork.CompleteAsync().
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product); // Alterado para receber a entidade para facilitar a implementação
    }
}