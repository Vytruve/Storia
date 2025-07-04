using Storia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrato para o reposit�rio de produtos.
    /// Define as opera��es de dados padr�o (CRUD) e outras consultas
    /// espec�ficas para a entidade Product.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Busca um produto pelo seu identificador �nico.
        /// </summary>
        /// <param name="id">O Guid do produto.</param>
        /// <returns>A entidade Product ou null se n�o for encontrada.</returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Busca um produto pelo seu SKU (Stock Keeping Unit).
        /// </summary>
        /// <param name="sku">O c�digo SKU do produto.</param>
        /// <returns>A entidade Product ou null se n�o for encontrada.</returns>
        Task<Product?> GetBySkuAsync(string sku);

        /// <summary>
        /// Retorna uma cole��o de todos os produtos.
        /// </summary>
        /// <returns>Uma cole��o de produtos.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        // A assinatura agora � s�ncrona, pois apenas modifica o Change Tracker do EF.
        // O 'save' real ser� feito pelo IUnitOfWork.CompleteAsync().
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product); // Alterado para receber a entidade para facilitar a implementa��o
    }
}