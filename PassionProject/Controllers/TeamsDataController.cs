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

        //This code is mostly scaffolded from the base models and database context
        //New > WebAPIController with Entity Framework Read/Write Actions
        //Choose model "Player"
        //Choose context "Varsity Data Context"
        //Note: The base scaffolded code needs many improvements for a fully
        //functioning MVP.

        // GET: api/TeamsData/GetTeams
        // TODO: Searching Logic?
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
        /// Gets a list of players in the database alongside a status code (200 OK).
        /// </summary>
        /// <param name="id">The input teamid</param>
        /// <returns>A list of players associated with the team</returns>
        /// <example>
        /// GET: api/TeamData/GetPlayersForTeam
        /// </example>
        [ResponseType(typeof(IEnumerable<PlayerDto>))]
        public IHttpActionResult GetPlayersForTeam(int id)
        {
            List<Player> Players = db.Players
                .Where(p => p.TeamID == id)
                .ToList();
            List<PlayerDto> PlayerDtos = new List<PlayerDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Player in Players)
            {
                PlayerDto NewPlayer = new PlayerDto
                {
                    PlayerID = Player.PlayerID,
                    PlayerRank = Player.PlayerRank,
                    PlayerName = Player.PlayerName,
                    TeamID = Player.TeamID
                };
                PlayerDtos.Add(NewPlayer);
            }

            return Ok(PlayerDtos);
        }


        /// <summary>
        /// Finds a particular Team in the database with a 200 status code. If the Team is not found, return 404.
        /// </summary>
        /// <param name="id">The Team id</param>
        /// <returns>Information about the Team, including Team id, bio, first and last name, and teamid</returns>
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

    }
}