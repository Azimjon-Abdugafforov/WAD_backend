using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using WAD;
using WAD.Data;

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


        // GET: TicketController
        [HttpGet, Authorize]
        public async Task<ActionResult<List<Tickets>>> GetTicket()
        {
            return Ok(await _dbConnection.Tickets.ToListAsync());
        }

        // GET: TicketController/Details/5
        [HttpGet("{id}", Name = "GetTicketById"), Authorize]
        public async Task<ActionResult<List<Tickets>>> GetTicketById(int id)
        {
            return Ok(await _dbConnection.Tickets.FindAsync(id));
        }


        // GET: TicketController/Create
        [HttpPost, Authorize]
        public async Task<ActionResult<List<Tickets>>> InsertTicket(Tickets ticket)
        {
            _dbConnection.Tickets.Add(ticket);
            await _dbConnection.SaveChangesAsync();

            return Ok(await _dbConnection.Tickets.ToListAsync());
        }

        // GET: TicketController/Edit/5
        [HttpPut, Authorize]
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


        // GET: TicketController/Delete/5
        [HttpDelete("{id}"), Authorize]
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
