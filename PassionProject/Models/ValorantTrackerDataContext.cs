using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PassionProject.Models
{
    public class ValorantTrackerDataContext : DbContext
    {
        public ValorantTrackerDataContext() : base("name=ValorantTrackerDataContext")
        {

        }

        //Instruction to set the models as tables in our database.
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamsxPlayers> TeamsxPlayers { get; set; }
    }
}