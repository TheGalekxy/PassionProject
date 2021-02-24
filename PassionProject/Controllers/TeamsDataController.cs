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

    }
}