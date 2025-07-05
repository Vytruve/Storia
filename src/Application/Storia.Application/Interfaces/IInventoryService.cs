using Storia.Core.Entities;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o servi�o que gerencia a l�gica de invent�rio.
    /// Define as opera��es de neg�cio relacionadas ao controle de estoque,
    /// servindo como uma abstra��o para a camada de aplica��o.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Deduz uma quantidade espec�fica de estoque de um produto,
        /// seguindo a estrat�gia FEFO (First-Expire, First-Out) e depois FIFO (First-In, First-Out).
        /// 
        /// Esta opera��o modifica as entidades de Lote na mem�ria, preparando-as para serem salvas
        /// posteriormente pela Unidade de Trabalho (Unit of Work). Ela n�o salva os dados diretamente.
        /// </summary>
        /// <param name="product">
        /// A entidade do produto do qual o estoque ser� deduzido. 
        /// � crucial que a cole��o 'product.Lots' j� tenha sido carregada do banco de dados.
        /// </param>
        /// <param name="quantityToDeduct">A quantidade a ser deduzida do estoque.</param>
        /// <returns>
        /// O custo m�dio ponderado dos itens que foram deduzidos. Este valor � essencial
        /// para registrar o Custo da Mercadoria Vendida (CMV) na transa��o de venda.
        /// </returns>
        decimal DeductStock(Product product, decimal quantityToDeduct);
    }
}