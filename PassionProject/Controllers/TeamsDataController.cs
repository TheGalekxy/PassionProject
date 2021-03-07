using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;
using System.Diagnostics;

namespace PassionProject.Controllers
{
    public class TeamsDataController : ApiController
    {

        //This variable is our database access point
        private ValorantTrackerDataContext db = new ValorantTrackerDataContext();

        // This code is based off of the code written by Christine Bittle in her Varsity Project example.

        // GET: api/TeamsData/GetTeams
        [ResponseType(typeof(IEnumerable<TeamDto>))]
        public IEnumerable<TeamDto> GetTeams()
        {
            List<Team> Teams = db.Teams.ToList();
            List<TeamDto> TeamDtos = new List<TeamDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Team in Teams)
            {
                TeamDto NewTeam = new TeamDto
                {
                    TeamID = Team.TeamID,
                    TeamName = Team.TeamName,
                };
                TeamDtos.Add(NewTeam);
            }

            return TeamDtos;
        }



        /// <summary>
        /// Finds a particular Team in the database with a 200 status code. If the Team is not found, return 404.
        /// </summary>
        /// <param name="id">The Team id</param>
        /// <returns>Information about the Team, including Team id, team name</returns>
        // <example>
        // GET: api/TeamsData/FindTeam/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(TeamDto))]
        public IHttpActionResult FindTeam(int id)
        {
            //Find the data
            Team Team = db.Teams.Find(id);
            //if not found, return 404 status code.
            if (Team == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            TeamDto TeamDto = new TeamDto
            {
                TeamID = Team.TeamID,
                TeamName = Team.TeamName,
            };


            //pass along data as 200 status code OK response
            return Ok(TeamDto);
        }


        // POST: api/TeamsData/AddTeam
        // FORM DATA: Player JSON Object
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult AddTeam([FromBody] Team team)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return Ok(team.TeamID);
            // return CreatedAtRoute("DefaultApi", new { id = player.PlayerID }, player);
        }

        // POST: api/TeamsData/DeleteTeam/1
        [HttpPost]
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/TeamsData/UpdateTeam/1
        // FORM DATA: Player JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeam(int id, [FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.TeamID)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.TeamID == id) > 0;
        }

        /// <summary>
        /// Gets the palyers associated with a particular team, alongside a status code of 200 (OK)
        /// </summary>
        /// <param name="id">The Input team ID</param>
        /// <returns>
        /// A list of players for the team, stored in data transfer objects.
        /// </returns>
        public IHttpActionResult GetPlayersForTeam(int id)
        {


            //Retrieve by accessing the bridging table directly
            IEnumerable<TeamsxPlayers> TXPs = db.TeamsxPlayers
                .Where(txpEntry => txpEntry.TeamID == id)
                .Include(txpEntry => txpEntry.Player) //This step can be necessary if you can't grab the data
                .ToList();

            List<PlayerDto> playerDtos = new List<PlayerDto> { };
            foreach (var txpEntry in TXPs)
            {
                PlayerDto NewPlayer = new PlayerDto
                {
                    PlayerID = txpEntry.Player.PlayerID,
                    PlayerName = txpEntry.Player.PlayerName,
                    PlayerRank = txpEntry.Player.PlayerRank
                };
                playerDtos.Add(NewPlayer);

            }

            return Ok(playerDtos);



        }
    }
}