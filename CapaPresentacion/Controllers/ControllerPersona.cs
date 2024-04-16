//using CapaDatos.Models;
using CapaDatos.Models;
using CapaDatos.Reponse;
using CapaNegocio.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CapaPresentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerPersona : ControllerBase
    {

        private readonly IPersona _persona;
        public ControllerPersona(IPersona persona)
        {
            _persona = persona;
        }

        [HttpGet]
        [Route("ConsultarPersonas")]
        public List<Persona> getPersonas()
        {
            List<Persona> list = new List<Persona>();
            list = _persona.ConsultarPersonas();
            return list;
        }
        [HttpGet]
        [Route("ConsultarPersonasId")]
        public List<Persona> getPersonasId(int Id)
        {
            List<Persona> list = new List<Persona>();
            list = _persona.ConsultarId(Id);
            return list;
        }
        [HttpGet]
        [Route("ConsultarPersonasNombre")]
        public IActionResult GetPersonasNombre(string nombre, int pagina, int registros)
        {
            var respuesta = _persona.ConsultarNombre(nombre, pagina, registros);
            var data = new
            {
                personas = respuesta.Item1,
                totalRegistros = respuesta.Item2
            };
            string jsonResponse = JsonConvert.SerializeObject(data);
            return Ok(jsonResponse);
        }

        [HttpPost]
        [Route("RegistrarPersona")]
        public string postPersonas(Persona persona)
        {
            string reponse;
            reponse = _persona.CrearPersona(persona);
            return reponse;
        }

        [HttpDelete]
        [Route("EliminarPersona")]
        public string deletePersonas(int Id)
        {
            string reponse;
            reponse = _persona.EliminarPersona(Id);
            return reponse;
        }
        [HttpPut]
        [Route("EditarPersona")]
        public string updatePersonas(int Id, Persona persona)
        {
            string reponse;
            reponse = _persona.EditarPersona(Id, persona);
            return reponse;
        }
    }
}
