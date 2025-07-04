using Storia.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces
{
	/// <summary>
	/// Contrato para o padrão Unit of Work.
	/// Agrupa múltiplos repositórios e gerencia a transação de banco de dados,
	/// garantindo que todas as operações sejam salvas juntas (atomicidade).
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Obtém a instância do repositório de produtos.
		/// </summary>
		IProductRepository Products { get; }

		/// <summary>
		/// Obtém a instância do repositório de vendas.
		/// </summary>
		ISaleRepository Sales { get; }

		/// <summary>
		/// Salva todas as alterações feitas no contexto do banco de dados
		/// como uma única transação.
		/// </summary>
		/// <returns>O número de registros afetados no banco de dados.</returns>
		Task<int> CompleteAsync();
	}
}