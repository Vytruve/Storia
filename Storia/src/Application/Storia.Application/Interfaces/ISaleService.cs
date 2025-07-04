using Storia.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o serviço de aplicação que gerencia a lógica de negócio para vendas.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Registra uma nova venda no sistema.
        /// Esta é a operação central do ponto de venda (PDV).
        /// </summary>
        /// <param name="saleDto">O DTO contendo os detalhes da venda a ser criada.</param>
        /// <returns>O DTO da venda recém-criada com os detalhes completos.</returns>
        Task<SaleDetailDto> CreateSaleAsync(SaleDetailDto saleDto);

        /// <summary>
        /// Obtém os detalhes de uma venda específica pelo seu ID.
        /// </summary>
        /// <param name="saleId">O ID da venda.</param>
        /// <returns>Um DTO detalhado da venda ou null se não encontrada.</returns>
        Task<SaleDetailDto?> GetSaleByIdAsync(Guid saleId);

        /// <summary>
        /// Obtém uma lista de todas as vendas realizadas, geralmente para um histórico ou relatório.
        /// </summary>
        /// <returns>Uma coleção de DTOs de vendas.</returns>
        Task<IEnumerable<SaleDetailDto>> GetAllSalesAsync();

        /// <summary>
        /// Obtém as vendas realizadas dentro de um intervalo de datas específico.
        /// </summary>
        /// <param name="startDate">A data de início do período.</param>
        /// <param name="endDate">A data de fim do período.</param>
        /// <returns>Uma coleção de DTOs de vendas realizadas no período.</returns>
        Task<IEnumerable<SaleDetailDto>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}