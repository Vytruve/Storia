using Storia.Core.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Storia.Core.Interfaces
{
	/// <summary>
	/// Contrato para o padr�o Unit of Work.
	/// Agrupa m�ltiplos reposit�rios e gerencia a transa��o de banco de dados,
	/// garantindo que todas as opera��es sejam salvas juntas (atomicidade).
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Obt�m a inst�ncia do reposit�rio de produtos.
		/// </summary>
		IProductRepository Products { get; }

		/// <summary>
		/// Obt�m a inst�ncia do reposit�rio de vendas.
		/// </summary>
		ISaleRepository Sales { get; }

		/// <summary>
		/// Salva todas as altera��es feitas no contexto do banco de dados
		/// como uma �nica transa��o.
		/// </summary>
		/// <returns>O n�mero de registros afetados no banco de dados.</returns>
		Task<int> CompleteAsync();
	}
}