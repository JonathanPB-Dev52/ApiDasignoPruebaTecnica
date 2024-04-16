using CapaDatos.Models;
using CapaDatos.Reponse;
using CapaNegocio.Bissness.Validators;
using CapaNegocio.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CapaNegocio.Bissness
{
    public class PersonaBissness : IPersona
    {

        private readonly IConfiguration _configuration;
        public PersonaBissness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Persona> ConsultarId(int Id)
        {
            List<Persona> personas = new List<Persona>();
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            {
                var ExistPerson = disagnoContext.Personas.Where(x => x.Id == Id).ToList();
                if (ExistPerson.Count != 0)
                {
                    personas.AddRange(ExistPerson);

                    foreach (var item in personas)
                    {
                        item.PrimerNombre = item.PrimerNombre.Trim();
                        item.SegundoNombre = item.SegundoNombre.Trim();
                        item.PrimerApellido = item.PrimerApellido.Trim();
                        item.SegundoApellido = item.SegundoApellido.Trim();
                    }
                    return personas;
                }
                else
                {
                    return personas;
                }
            }
        }

        public (List<Persona>, int numTotal) ConsultarNombre(string nombre, int pageNumber, int pageSize)
        {
            Response response = new Response();
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            {
                int totalCount = disagnoContext.Personas
                    .Count(p => p.PrimerNombre.Trim() == nombre || p.PrimerApellido.Trim() == nombre);
                int startIndex = (pageNumber - 1) * pageSize;

                // Realizar la consulta paginada
                var personasConNombre = disagnoContext.Personas
                    .Where(p => p.PrimerNombre.Trim() == nombre || p.PrimerApellido.Trim() == nombre)
                    .OrderBy(p => p.Id)
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToList();

                // Aplicar trim a los nombres y apellidos
                foreach (var item in personasConNombre)
                {
                    item.PrimerNombre = item.PrimerNombre.Trim();
                    item.SegundoNombre = item.SegundoNombre?.Trim();
                    item.PrimerApellido = item.PrimerApellido.Trim();
                    item.SegundoApellido = item.SegundoApellido?.Trim();
                }

                response.listPersona = personasConNombre;
                response.totalRegistros = totalCount;

                return (personasConNombre, totalCount);
            }
        }

        public List<Persona> ConsultarPersonas()
        {
            List<Persona> personas = new List<Persona>();
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            {
                personas = disagnoContext.Personas.ToList();
                foreach (var item in personas)
                {
                    item.PrimerNombre = item.PrimerNombre.Trim();
                    item.SegundoNombre = item.SegundoNombre.Trim();
                    item.PrimerApellido = item.PrimerApellido.Trim();
                    item.SegundoApellido = item.SegundoApellido.Trim();
                }
            }
            return personas;
        }

        public string CrearPersona(Persona persona)
        {
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            {
                ValidatorsPersona validatorsPersona = new ValidatorsPersona();
                string validacionesStrings = validatorsPersona.ValidarNombre(persona.PrimerNombre, persona.SegundoNombre, persona.PrimerApellido, persona.SegundoApellido, persona.Sueldo);
                if (validacionesStrings == "ok")
                {
                    Persona persona1 = new Persona();
                    persona1.PrimerNombre = persona.PrimerNombre;
                    persona1.SegundoNombre = persona.SegundoNombre;
                    persona1.PrimerApellido = persona.PrimerApellido;
                    persona1.SegundoApellido = persona.SegundoApellido;
                    persona1.FechaNacimiento = persona.FechaNacimiento;
                    persona1.Sueldo = persona.Sueldo;
                    persona1.FechaCreacion = DateTime.Now;

                    disagnoContext.Add(persona1);
                    disagnoContext.SaveChanges();
                }
                else
                {
                    return validacionesStrings;
                }
                return "Creacion exitosa";
            }
        }

        public string EditarPersona(int Id, Persona persona)
        {
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            {
                var ExistPerson = disagnoContext.Personas.Where(x => x.Id == Id).ToList();
                if (ExistPerson.Count != 0)
                {
                    ValidatorsPersona validatorsPersona = new ValidatorsPersona();
                    string validacionesStrings = validatorsPersona.ValidarNombre(persona.PrimerNombre, persona.SegundoNombre, persona.PrimerApellido, persona.SegundoApellido, persona.Sueldo);
                    if (validacionesStrings == "ok")
                    {
                        Persona persona1 = new Persona();
                        LogUpdate logUpdate = new LogUpdate();
                        persona1 = ExistPerson[0];
                        persona1.PrimerNombre = persona.PrimerNombre;
                        persona1.SegundoNombre = persona.SegundoNombre;
                        persona1.PrimerApellido = persona.PrimerApellido;
                        persona1.SegundoApellido = persona.SegundoApellido;
                        persona1.FechaNacimiento = persona.FechaNacimiento;
                        persona1.Sueldo = persona.Sueldo;
                        persona1.FechaCreacion = persona1.FechaCreacion;

                        disagnoContext.Update(persona1);
                        logUpdate.IdPersona = persona1.Id;
                        logUpdate.ObjPersona = JsonConvert.SerializeObject(persona1);
                        logUpdate.FechaActualizacion = DateTime.Now;
                        disagnoContext.LogUpdates.Add(logUpdate);
                        disagnoContext.SaveChanges();
                        return "datos actualizados";
                    }
                    else
                    {
                        return validacionesStrings;
                    }
                }
                else
                {
                    return "Persona no encontrada";
                }
            }
        }

        public string EliminarPersona(int Id)
        {
            using (DasignoContext disagnoContext = new DasignoContext(_configuration))
            { 
                var ExistPerson = disagnoContext.Personas.Where(x=> x.Id == Id).ToList();
                if (ExistPerson.Count != 0)
                {
                    disagnoContext.Remove(ExistPerson[0]);
                    disagnoContext.SaveChanges();
                    return "Persona Eliminada";
                }
                else
                {
                    return "Persona no encontrada";
                }
            }
        }
    }
}
