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
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this._geralPersist = geralPersist;
            this._eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEventos(Evento eventoModel, bool includePalestrante)
        {
            try
            {
                _geralPersist.Add<Evento>(eventoModel);
                if (await _geralPersist.SaveChangesAsync())
                    return await _eventoPersist.GetEventosByIdAsync(eventoModel.Id, false);

                throw new Exception($"Não foi possível Adicionar um novo evento");

            }
            catch (InvalidOperationException iEx)
            {
                throw iEx;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(AddEventos)}", ex);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsync(eventoId, false);
                if (evento == null)
                    throw new Exception($"Evento não encontrado"); //Return null

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();

                throw new Exception($"Não foi possível atualizar o evento");
            }
            catch (InvalidOperationException iEx)
            {
                throw new InvalidOperationException(iEx.Message, iEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(DeleteEvento)}", ex);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento eventoModel)
        {
            try
            {
                var evento = await _eventoPersist.GetEventosByIdAsync(eventoId, false);
                if (evento == null)
                    return null;

                eventoModel.Id = evento.Id;

                _geralPersist.Update(eventoModel);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventosByIdAsync(eventoModel.Id, false);
                }

                throw new Exception($"Não foi possível atualizar o evento");
            }
            catch (InvalidOperationException iEx)
            {
                throw new InvalidOperationException(iEx.Message, iEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(DeleteEvento)}", ex);
            }
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrante);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (InvalidOperationException iEx)
            {
                throw new InvalidOperationException(iEx.Message, iEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(GetAllEventosAsync)}", ex);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrante);
                if (eventos == null)
                    throw new Exception($"Evento não encontrado"); //Return null

                return eventos;
            }
            catch (InvalidOperationException iEx)
            {
                throw new InvalidOperationException(iEx.Message, iEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(GetAllEventosByTemaAsync)}", ex);
            }
        }

        public async Task<Evento> GetEventosByIdAsync(int eventoId, bool includePalestrante)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventosByIdAsync(eventoId, includePalestrante);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (InvalidOperationException iEx)
            {
                throw new InvalidOperationException(iEx.Message, iEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred on trying to perform {nameof(GetAllEventosByTemaAsync)}", ex);
            }
        }
    }
}
