using Storia.Core.Entities;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o serviço que gerencia a lógica de inventário.
    /// Define as operações de negócio relacionadas ao controle de estoque,
    /// servindo como uma abstração para a camada de aplicação.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Deduz uma quantidade específica de estoque de um produto,
        /// seguindo a estratégia FEFO (First-Expire, First-Out) e depois FIFO (First-In, First-Out).
        /// 
        /// Esta operação modifica as entidades de Lote na memória, preparando-as para serem salvas
        /// posteriormente pela Unidade de Trabalho (Unit of Work). Ela não salva os dados diretamente.
        /// </summary>
        /// <param name="product">
        /// A entidade do produto do qual o estoque será deduzido. 
        /// É crucial que a coleção 'product.Lots' já tenha sido carregada do banco de dados.
        /// </param>
        /// <param name="quantityToDeduct">A quantidade a ser deduzida do estoque.</param>
        /// <returns>
        /// O custo médio ponderado dos itens que foram deduzidos. Este valor é essencial
        /// para registrar o Custo da Mercadoria Vendida (CMV) na transação de venda.
        /// </returns>
        decimal DeductStock(Product product, decimal quantityToDeduct);
    }
}