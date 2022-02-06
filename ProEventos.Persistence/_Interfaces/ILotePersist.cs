using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface ILotePersist
    {
        /// <summary>
        /// Método que retornará uma lista de Lotes por eventoId.
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento.</param>
        /// <returns>Lista de Lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        /// Método que retornará um lote específico.
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento.</param>
        /// <param name="loteId">Código chave da tabela Lote.</param>
        /// <returns>Apenas 1 Lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);

    }
}
