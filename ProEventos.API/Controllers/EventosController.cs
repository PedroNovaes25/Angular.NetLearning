using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        
        public EventosController(IEventoService eventoService)
        {
            this._eventoService = eventoService;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        //public async Task<IHttpActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar  {nameof(Evento)}´s. Erro: {ex} "
                    );

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByIdAsync(id, true);
                if (eventos == null)
                    return NotFound($"Nenhum {nameof(Evento)} foi encontrado");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar  {nameof(Evento)}. Erro: {ex} "
                    );

            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null)
                    return NotFound($"Nenhum {nameof(Evento)} de tema '{tema}' foi encontrado.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar  {nameof(Evento)}´s. Erro: {ex} "
                    );

            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(Evento eventoModel)
        {
            try
            {
                var eventos = await _eventoService.AddEventos(eventoModel, true);
                if (eventos == null)
                    return BadRequest($"Erro ao tentar adicionar '{nameof(Evento)}'.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar adicionar  {nameof(Evento)}. Erro: {ex} "
                    );

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento eventoModel)
        {
            try
            {
                var eventos = await _eventoService.UpdateEvento(id, eventoModel);
                if (eventos == null)
                    return BadRequest($"Erro ao tentar atualizar '{nameof(Evento)}'.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar atualizar  {nameof(Evento)}´s. Erro: {ex} "
                    );

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ?
                    Ok(true) /*OK("deletado")*/:
                    BadRequest($"Evento não deletado");
                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar  {nameof(Evento)}´s. Erro: {ex} "
                    );

            }
        }
    }
}


