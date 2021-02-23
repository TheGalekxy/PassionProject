﻿using System;
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
    public class PlayersDataController : ApiController
    {

        //This variable is our database access point
        private ValorantTrackerDataContext db = new ValorantTrackerDataContext();

        //This code is mostly scaffolded from the base models and database context
        //New > WebAPIController with Entity Framework Read/Write Actions
        //Choose model "Player"
        //Choose context "Varsity Data Context"
        //Note: The base scaffolded code needs many improvements for a fully
        //functioning MVP.

        // GET: api/PlayersData/GetPlayers
        // TODO: Searching Logic?
        public IEnumerable<PlayerDto> GetPlayers()
        {
            List<Player> Players = db.Players.ToList();
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

            return PlayerDtos;
        }


        // POST: api/PlayersData/AddPlayer
        // FORM DATA: Player JSON Object
        [ResponseType(typeof(Player))]
        [HttpPost]
        public IHttpActionResult AddPlayer([FromBody] Player player)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            db.SaveChanges();

            return Ok(player.PlayerID);
            // return CreatedAtRoute("DefaultApi", new { id = player.PlayerID }, player);
        }

        // POST: api/PlayersData/DeletePlayer/5
        [HttpPost]
        public IHttpActionResult DeletePlayer(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/PlayersData/UpdatePlayer/5
        // FORM DATA: Player JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlayer(int id, [FromBody] Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.PlayerID)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        /// <summary>
        /// Finds a particular player in the database with a 200 status code. If the player is not found, return 404.
        /// </summary>
        /// <param name="id">The player id</param>
        /// <returns>Information about the player, including player id, bio, first and last name, and teamid</returns>
        // <example>
        // GET: api/PlayersData/FindPlayer/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(PlayerDto))]
        public IHttpActionResult FindPlayer(int id)
        {
            //Find the data
            Player Player = db.Players.Find(id);
            //if not found, return 404 status code.
            if (Player == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            PlayerDto PlayerDto = new PlayerDto
            {
                PlayerID = Player.PlayerID,
                PlayerName = Player.PlayerName,
                PlayerRank = Player.PlayerRank,
                TeamID = Player.TeamID
            };


            //pass along data as 200 status code OK response
            return Ok(PlayerDto);
        }

        /// <summary>
        /// Finds a particular Team in the database given a player id with a 200 status code. If the Team is not found, return 404.
        /// </summary>
        /// <param name="id">The player id</param>
        /// <returns>Information about the Team, including Team id, bio, first and last name, and teamid</returns>
        // <example>
        // GET: api/TeamData/FindTeamForPlayer/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(TeamDto))]
        public IHttpActionResult FindTeamForPlayer(int id)
        {
            //Finds the first team which has any players
            //that match the input playerid
            Team Team = db.Teams
                .Where(t => t.Players.Any(p => p.PlayerID == id))
                .FirstOrDefault();
            //if not found, return 404 status code.
            if (Team == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            TeamDto TeamDto = new TeamDto
            {
                TeamID = Team.TeamID,
                TeamName = Team.TeamName
            };


            //pass along data as 200 status code OK response
            return Ok(TeamDto);
        }



        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.PlayerID == id) > 0;
        }

    }
}