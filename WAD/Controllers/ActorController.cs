using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.Data;
using Microsoft.AspNetCore.Authorization;
using WAD.Models;

namespace WAD.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class ActorController : Controller
    {

        // GET: ActorController
        private readonly DataContext _dbConnection;
        public ActorController(DataContext dbContext)
        {
            _dbConnection = dbContext;
        }



        [HttpGet, Authorize]
        public async Task<ActionResult<List<Actors>>> GeTActors()
        {
            return Ok(await _dbConnection.Actors.ToListAsync());
        }

       
    }
}
