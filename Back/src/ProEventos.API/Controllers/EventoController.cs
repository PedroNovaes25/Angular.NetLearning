using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<EventoController> _logger;

        public EventoController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return new Evento[] 
            {
                new Evento()
                {
                    EventoId = 1,
                    DataEvento = DateTime.Now.AddDays(5).ToString(),
                    Local = "SP",
                    Lote = "5° Lote",
                    QtdPessoas = 120,
                    Tema = "Angular 11 e .Net",
                    ImagemURL = "dvsvd",
                }
            };
        }
    }
}
