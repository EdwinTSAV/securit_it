using ApiBackend.Data;
using ApiBackend.DTOs;
using ApiBackend.Migrations;
using ApiBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class RondaController : BaseController
    {
        public RondaController(ApplicationDbContext context) : base(context) { }

        [Authorize(Roles = "SupervisorArea")]
        [HttpGet("rondas")]
        public IActionResult GetRondas()
        {
            User user = GetLoggedUser();
            string rol = GetUserRole();

            var rondas = _context.Rondas
                .Include(r => r.RondaEstado)
                .Where(r => r.SupervisorId == user.UserId)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.FechaInicio)
                .Select(r => new RondaDto
                {
                    Id = r.RondaId,
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin,
                    Nombre = r.Nombre,
                    Estado = r.RondaEstado.Nombre
                })
                .ToList();

            return Ok(rondas);
        }

        // GET: RondaController/Details/5
        [Authorize(Roles = "SupervisorArea")]
        [HttpGet("ronda/{id:int}")]
        public IActionResult GetRondaById(int id)
        {
            var user = GetLoggedUser();
            var rol = GetUserRole();

            var ronda = _context.Rondas
                .Include(r => r.RondaEstado)
                .Include(r => r.Supervisor)
                .Include(r => r.RondaPuestos)
                    .ThenInclude(rp => rp.Puesto)
                .Include(r => r.RondaPuestos)
                    .ThenInclude(rp => rp.RondaPuestoEstado)
                .FirstOrDefault(r => r.RondaId == id && !r.IsDeleted);

            if (ronda == null)
                return NotFound("Ronda no encontrada");

            // Seguridad: Supervisor solo ve lo suyo
            if (rol == "Supervisor" && ronda.SupervisorId != user.UserId)
                return Forbid();

            var supervisor = ronda.Supervisor.Nombre;
            
            var result = ronda.RondaPuestos.Select(rp => new RondaPuestoDto
            {
                Id = rp.RondaPuestoId,
                PuestoId = rp.PuestoId,
                RondaId = id,
                NombreSupervisor = supervisor,
                NombrePuesto = rp.Puesto.Nombre,
                FechaInicio = rp.FechaInicio,
                FechaFin = rp.FechaFin,
                Estado = rp.RondaPuestoEstado.Nombre
            });

            RondaDto rondaDto = new RondaDto
            {
                Id = ronda.RondaId,
                FechaInicio = ronda.FechaInicio,
                FechaFin = ronda.FechaFin,
                Nombre = ronda.Nombre,
                Estado = ronda.RondaEstado.Nombre,
                RondaPuestos = result
            };

            return Ok(rondaDto);
        }

        // POST: RondaController/Create
        [Authorize(Roles = "SupervisorArea")]
        [HttpPost("ronda/create")]
        public IActionResult IniciarRonda()
        {
            var supervisor = GetLoggedUser();

            var rondaActiva = _context.Rondas
            .Any(r => r.SupervisorId == supervisor.UserId && r.FechaFin == null);

            if (rondaActiva)
                return BadRequest("Ya tienes una ronda en proceso.");

            var ronda = new Ronda
            {
                SupervisorId = supervisor.UserId,
                FechaInicio = DateTime.UtcNow,
                Nombre = supervisor.Nombre + " - " + DateTime.UtcNow,
                CoordinadorId = null,
                EstadoId = 1,
                FechaFin = null
            };

            _context.Rondas.Add(ronda);
            _context.SaveChanges();

            var puestos = _context.Puestos
                .Where(r => !r.IsDeleted)
                .ToList();

            var rondaPuestos = puestos.Select(p => new RondaPuesto
            {
                RondaId = ronda.RondaId,
                PuestoId = p.PuestoId,
                FechaInicio = DateTime.UtcNow,
                FechaFin = null,
                RondaPuestoEstadoId = 1
            }).ToList();

            _context.RondaPuestos.AddRange(rondaPuestos);
            _context.SaveChanges(); 

            return CreatedAtAction(nameof(GetRondas), new
            {
                    id = ronda.RondaId
                },
            new
            {
                ronda.RondaId,
                ronda.Nombre,
                ronda.FechaInicio,
                Estado = "Pendiente"
            });
        }

        // PUT: RondaController/Edit/5
        [Authorize(Roles = "SupervisorArea")]
        [HttpPut("ronda/{id:int}/finalizar")]
        public IActionResult FinalizarRonda(int id)
        {
            var supervisor = GetLoggedUser();

            var ronda = _context.Rondas
            .FirstOrDefault(r => r.RondaId == id && !r.IsDeleted);

            if (ronda == null)
                return NotFound("Ronda no encontrada");

            if (ronda.SupervisorId != supervisor.UserId)
                return Forbid("No puedes finalizar una ronda que no es tuya");

            if (ronda.FechaFin != null)
                return BadRequest("La ronda ya fue finalizada");

            ronda.FechaFin = DateTime.UtcNow;
            ronda.EstadoId = 3;

            _context.SaveChanges();

            return Ok(new
            {
                ronda.RondaId,
                ronda.FechaInicio,
                ronda.FechaFin,
                Estado = "Finalizada"
            });
        }

        [Authorize(Roles = "Administrador")]

        // POST: RondaController/Delete/5
        [HttpDelete("ronda/{id:int}")]
        public ActionResult Delete(int id)
        {
            var ronda = _context.Rondas
            .FirstOrDefault(r => r.RondaId == id && !r.IsDeleted);

            if (ronda == null)
                return NotFound("Ronda no encontrada");

            ronda.IsDeleted = true;
            _context.SaveChanges();

            return NoContent(); //204
        }
    }
}
