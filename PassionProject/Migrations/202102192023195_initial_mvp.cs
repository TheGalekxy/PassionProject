namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_mvp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        PlayerRank = c.String(),
                    })
                .PrimaryKey(t => t.PlayerID);

            CreateTable(
                "dbo.Teams",
                c => new
                {
                    TeamID = c.Int(nullable: false, identity: true),
                    TeamName = c.String(),
                })
                .PrimaryKey(t => t.TeamID);


            CreateTable(
                "dbo.Games",
                c => new
                {
                    GameId = c.Int(nullable: false, identity: true),
                    MapId = c.Int(nullable: false),
                    teamOneId = c.Int(nullable: false),
                    teamTwoId = c.Int(nullable: false),
                    teamOneScore = c.Int(nullable: false),
                    teamTwoScore = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Maps", t => t.MapId, cascadeDelete: true);
                // .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                // .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true);

            CreateTable(
                "dbo.Maps",
                c => new
                {
                    MapID = c.Int(nullable: false, identity: true),
                    mapName = c.String(),
                })

                .PrimaryKey(t => t.MapID);




            CreateTable(
                "dbo.TeamPlayers",
                c => new
                {
                    Player_PlayerID = c.Int(nullable: false),
                    Team_TeamID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Player_PlayerID, t.Team_TeamID })
                .ForeignKey("dbo.Players", t => t.Player_PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_TeamID, cascadeDelete: true)
                .Index(t => t.Player_PlayerID)
                .Index(t => t.Team_TeamID);
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.Players", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "Team_TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "Player_PlayerID", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "Team_TeamID" });
            DropIndex("dbo.TeamPlayers", new[] { "Player_PlayerID" });
            DropIndex("dbo.Players", new[] { "TeamID" });
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
        }
    }
}
