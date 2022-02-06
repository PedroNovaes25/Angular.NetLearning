using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _lotePersist = lotePersist;
            _mapper = mapper;
        }


        public async Task AddLote(int eventoId, LoteDto loteModel)
        {
            try
            {
                var lote = _mapper.Map<Lote>(loteModel);
                lote.EventoId = eventoId;
                _geralPersist.Add<Lote>(lote);

                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ILoteService
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] lotesModels)
        {
            var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
            if (lotes == null) return null;

            foreach (var loteModel in lotesModels)
            {
                if (loteModel.Id == 0)
                {
                    await AddLote(eventoId, loteModel);
                }
                else
                {
                    var lote = lotes.FirstOrDefault(lote => lote.Id == loteModel.Id);
                    loteModel.EventoId = eventoId;

                    _mapper.Map(loteModel, lote);
                    _geralPersist.Update<Lote>(lote);
                    await _geralPersist.SaveChangesAsync();
                }
            }

            var loteRetorno = await _lotePersist.GetLotesByEventoIdAsync(eventoId);

            return _mapper.Map<LoteDto[]>(loteRetorno);
        }
        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote não encontrado.");

            _geralPersist.Delete<Lote>(lote);
            return await _geralPersist.SaveChangesAsync();
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null)
                    return null;
                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null)
                    return null;
                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
