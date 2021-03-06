using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class UpdateTeam
    {

        //Information about the team
        public TeamDto team { get; set; }

        //Needed for a dropdownlist which presents the team with a choice of players to play for
        public IEnumerable<PlayerDto> allplayers { get; set; }
    }
}