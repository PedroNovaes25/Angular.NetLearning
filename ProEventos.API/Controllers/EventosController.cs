using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;
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
        private readonly DataContext contextdb;
        
        public EventosController(DataContext contextdb)
        {
            this.contextdb = contextdb;
        }


        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return contextdb.Eventos;
        }

        [HttpGet("{id}")]
        public Evento Get(int id)
        {
            return contextdb.Eventos.FirstOrDefault(x => x.EventoId.Equals(id));
        }
    }
}


