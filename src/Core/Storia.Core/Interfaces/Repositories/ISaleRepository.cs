using Storia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrato para o repositório de vendas.
    /// Define as operações de dados para a entidade Sale e suas entidades relacionadas.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Busca uma venda pelo seu identificador único.
        /// A implementação deve garantir que os SaleItems associados sejam carregados.
        /// </summary>
        /// <param name="id">O Guid da venda.</param>
        /// <returns>A entidade Sale ou null se não for encontrada.</returns>
        Task<Sale?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retorna uma coleção de todas as vendas.
        /// Idealmente, deve suportar paginação em uma implementação real.
        /// </summary>
        /// <returns>Uma coleção de vendas.</returns>
        Task<IEnumerable<Sale>> GetAllAsync();

        /// <summary>
        /// Retorna uma coleção de vendas realizadas dentro de um intervalo de datas específico.
        /// Essencial para relatórios financeiros e de vendas.
        /// </summary>
        /// <param name="startDate">A data de início do período.</param>
        /// <param name="endDate">A data de fim do período.</param>
        /// <returns>Uma coleção de vendas que ocorreram no intervalo especificado.</returns>
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Adiciona uma nova entidade de venda ao banco de dados.
        /// A implementação deve garantir que a venda e todos os seus SaleItems sejam salvos
        /// em uma única transação atômica.
        /// </summary>
        /// <param name="sale">A entidade Sale a ser adicionada.</param>
        void Add(Sale sale);

        // Nota: Operações de Update e Delete em Vendas são geralmente desaconselhadas
        // em sistemas de ERP por razões de auditoria e integridade.
        // Em vez de deletar, geralmente se cria uma transação de "devolução" ou "estorno".
        // Por isso, não incluiremos UpdateAsync ou DeleteAsync por padrão aqui.
    }
}