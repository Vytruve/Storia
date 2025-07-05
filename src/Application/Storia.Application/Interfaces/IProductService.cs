using Storia.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o serviço de aplicação que gerencia a lógica de negócio para produtos.
    /// Esta interface é o que a camada de UI (MAUI) irá consumir.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Obtém os detalhes de um produto específico.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>Um DTO com os dados do produto ou null se não encontrado.</returns>
        Task<ProductDto?> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Obtém uma lista de todos os produtos cadastrados.
        /// </summary>
        /// <returns>Uma coleção de DTOs de produtos.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Cria um novo produto no sistema.
        /// </summary>
        /// <param name="productDto">O DTO contendo os dados do novo produto.</param>
        /// <returns>O DTO do produto recém-criado, incluindo seu novo ID.</returns>
        Task<ProductDto> CreateProductAsync(ProductDto productDto);

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="productDto">O DTO com os dados atualizados do produto.</param>
        Task UpdateProductAsync(ProductDto productDto);

        /// <summary>
        /// Deleta um produto do sistema.
        /// </summary>
        /// <param name="id">O ID do produto a ser deletado.</param>
        Task DeleteProductAsync(Guid id);

        /// <summary>
        /// Adiciona um novo lote de estoque a um produto existente.
        /// Esta é uma operação de negócio chave.
        /// </summary>
        /// <param name="productId">O ID do produto que está recebendo o estoque.</param>
        /// <param name="quantity">A quantidade de itens no lote.</param>
        /// <param name="purchasePrice">O preço de custo por item neste lote.</param>
        /// <param name="lotNumber">O número do lote do fornecedor (opcional).</param>
        /// <param name="expirationDate">A data de validade do lote (opcional).</param>
        Task AddStockAsync(Guid productId, decimal quantity, decimal purchasePrice, string? lotNumber, DateTimeOffset? expirationDate);
    }
}