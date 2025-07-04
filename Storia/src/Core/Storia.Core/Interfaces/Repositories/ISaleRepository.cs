using Storia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrato para o reposit�rio de vendas.
    /// Define as opera��es de dados para a entidade Sale e suas entidades relacionadas.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Busca uma venda pelo seu identificador �nico.
        /// A implementa��o deve garantir que os SaleItems associados sejam carregados.
        /// </summary>
        /// <param name="id">O Guid da venda.</param>
        /// <returns>A entidade Sale ou null se n�o for encontrada.</returns>
        Task<Sale?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retorna uma cole��o de todas as vendas.
        /// Idealmente, deve suportar pagina��o em uma implementa��o real.
        /// </summary>
        /// <returns>Uma cole��o de vendas.</returns>
        Task<IEnumerable<Sale>> GetAllAsync();

        /// <summary>
        /// Retorna uma cole��o de vendas realizadas dentro de um intervalo de datas espec�fico.
        /// Essencial para relat�rios financeiros e de vendas.
        /// </summary>
        /// <param name="startDate">A data de in�cio do per�odo.</param>
        /// <param name="endDate">A data de fim do per�odo.</param>
        /// <returns>Uma cole��o de vendas que ocorreram no intervalo especificado.</returns>
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Adiciona uma nova entidade de venda ao banco de dados.
        /// A implementa��o deve garantir que a venda e todos os seus SaleItems sejam salvos
        /// em uma �nica transa��o at�mica.
        /// </summary>
        /// <param name="sale">A entidade Sale a ser adicionada.</param>
        void Add(Sale sale);

        // Nota: Opera��es de Update e Delete em Vendas s�o geralmente desaconselhadas
        // em sistemas de ERP por raz�es de auditoria e integridade.
        // Em vez de deletar, geralmente se cria uma transa��o de "devolu��o" ou "estorno".
        // Por isso, n�o incluiremos UpdateAsync ou DeleteAsync por padr�o aqui.
    }
}