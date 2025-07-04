using Storia.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Application.Interfaces
{
    /// <summary>
    /// Contrato para o servi�o de aplica��o que gerencia a l�gica de neg�cio para vendas.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Registra uma nova venda no sistema.
        /// Esta � a opera��o central do ponto de venda (PDV).
        /// </summary>
        /// <param name="saleDto">O DTO contendo os detalhes da venda a ser criada.</param>
        /// <returns>O DTO da venda rec�m-criada com os detalhes completos.</returns>
        Task<SaleDetailDto> CreateSaleAsync(SaleDetailDto saleDto);

        /// <summary>
        /// Obt�m os detalhes de uma venda espec�fica pelo seu ID.
        /// </summary>
        /// <param name="saleId">O ID da venda.</param>
        /// <returns>Um DTO detalhado da venda ou null se n�o encontrada.</returns>
        Task<SaleDetailDto?> GetSaleByIdAsync(Guid saleId);

        /// <summary>
        /// Obt�m uma lista de todas as vendas realizadas, geralmente para um hist�rico ou relat�rio.
        /// </summary>
        /// <returns>Uma cole��o de DTOs de vendas.</returns>
        Task<IEnumerable<SaleDetailDto>> GetAllSalesAsync();

        /// <summary>
        /// Obt�m as vendas realizadas dentro de um intervalo de datas espec�fico.
        /// </summary>
        /// <param name="startDate">A data de in�cio do per�odo.</param>
        /// <param name="endDate">A data de fim do per�odo.</param>
        /// <returns>Uma cole��o de DTOs de vendas realizadas no per�odo.</returns>
        Task<IEnumerable<SaleDetailDto>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}