using Storia.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o servi�o de aplica��o que gerencia a l�gica de neg�cio para produtos.
    /// Esta interface � o que a camada de UI (MAUI) ir� consumir.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Obt�m os detalhes de um produto espec�fico.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>Um DTO com os dados do produto ou null se n�o encontrado.</returns>
        Task<ProductDto?> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Obt�m uma lista de todos os produtos cadastrados.
        /// </summary>
        /// <returns>Uma cole��o de DTOs de produtos.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Cria um novo produto no sistema.
        /// </summary>
        /// <param name="productDto">O DTO contendo os dados do novo produto.</param>
        /// <returns>O DTO do produto rec�m-criado, incluindo seu novo ID.</returns>
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
        /// Esta � uma opera��o de neg�cio chave.
        /// </summary>
        /// <param name="productId">O ID do produto que est� recebendo o estoque.</param>
        /// <param name="quantity">A quantidade de itens no lote.</param>
        /// <param name="purchasePrice">O pre�o de custo por item neste lote.</param>
        /// <param name="lotNumber">O n�mero do lote do fornecedor (opcional).</param>
        /// <param name="expirationDate">A data de validade do lote (opcional).</param>
        Task AddStockAsync(Guid productId, decimal quantity, decimal purchasePrice, string? lotNumber, DateTimeOffset? expirationDate);
    }
}