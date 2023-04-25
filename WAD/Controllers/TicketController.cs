using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Transactions;
using WAD.Data;
using WAD.Models;

namespace WAD_Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class TicketController : Controller
    {
        private readonly DataContext _dbConnection;
        public TicketController(DataContext dbContext)
        {
            _dbConnection = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Tickets>>> GetTicket()
        {
            return Ok(await _dbConnection.Tickets.ToListAsync());
        }

        [HttpGet("{id}", Name = "GetTicketById")]
        public async Task<ActionResult<List<Tickets>>> GetTicketById(int id)
        {
            return Ok(await _dbConnection.Tickets.FindAsync(id));
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> CreateTicket( Tickets model)
        {
            var ticket = new Tickets
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ActorsId = model.ActorsId,
                Status = model.Status
            };
           
            await _dbConnection.Tickets.AddAsync(ticket);
            await _dbConnection.SaveChangesAsync();

            return Ok(ticket.Id);
        }

       /* [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<Tickets>>> InsertTicket(Tickets ticket)
        {
            _dbConnection.Tickets.Add(ticket);
            await _dbConnection.SaveChangesAsync();

            return Ok(await _dbConnection.Tickets.ToListAsync());
        }
       */
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<Tickets>>> UpdateTicket(Tickets ticket)
        {
            var foundTicket = await _dbConnection.Tickets.FindAsync(ticket.Id);

            if (foundTicket == null)
            {
                return BadRequest("The ticket you are searching for does not exist");
            }

            foundTicket.Title = ticket.Title;
            foundTicket.Description = ticket.Description;
            foundTicket.CreatedAt = ticket.CreatedAt;
            foundTicket.UpdatedAt = ticket.UpdatedAt;
            foundTicket.Status = ticket.Status;
            foundTicket.ActorsId = ticket.ActorsId; 

            await _dbConnection.SaveChangesAsync();

            var updatedTicket = await _dbConnection.Tickets.FindAsync(ticket.Id);
            return Ok(updatedTicket);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<List<Tickets>>> Delete(int id)
        {
            var foundTicket = await _dbConnection.Tickets.FindAsync(id);
            if (foundTicket == null)
            {
                return BadRequest("The selected ticket does not exist in the database");
            }
            _dbConnection.Tickets.Remove(foundTicket);
            await _dbConnection.SaveChangesAsync();

            return Ok(await _dbConnection.Tickets.ToListAsync());
        }
    }
}
