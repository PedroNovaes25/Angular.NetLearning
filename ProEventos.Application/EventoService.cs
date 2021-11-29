using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist, IMapper mapper)
        {
            this._geralPersist = geralPersist;
            this._eventoPersist = eventoPersist;
            this._mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(EventoDto eventoDTO, bool includePalestrante)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoDTO);

                _geralPersist.Add<Evento>(evento);
                if (await _geralPersist.SaveChangesAsync()) 
                {
                    var eventoFiltrado =  await _eventoPersist.GetEventosByIdAsync(evento.EventoCodigo, false);
                    var resultado = _mapper.Map<EventoDto>(eventoFiltrado);

                    return resultado;
                }

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

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto eventoModel)
        {
            try
            {

                var evento = await _eventoPersist.GetEventosByIdAsync(eventoId, false);
                if (evento == null)
                    return null;

                eventoModel.Id = evento.EventoCodigo;
                var eventoMapeado = _mapper.Map(eventoModel, evento);

                _geralPersist.Update(eventoMapeado);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoFiltrado = await _eventoPersist.GetEventosByIdAsync(eventoMapeado.EventoCodigo, false);
                    var resultado = _mapper.Map<EventoDto>(eventoFiltrado);

                    return resultado;
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
        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrante);
                if (eventos == null)
                    return null;

                var evento = _mapper.Map<EventoDto[]>(eventos);

                return evento;
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

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrante);
                if (eventos == null)
                    throw new Exception($"Evento não encontrado"); //Return null

                var evento = _mapper.Map<EventoDto[]>(eventos);

                return evento;
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

        public async Task<EventoDto> GetEventosByIdAsync(int eventoId, bool includePalestrante)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventosByIdAsync(eventoId, includePalestrante);
                if (eventos == null)
                    return null;

                var evento = _mapper.Map<EventoDto>(eventos);

                return evento;
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
