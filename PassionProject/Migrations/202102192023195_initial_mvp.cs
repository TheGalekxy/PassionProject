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
                "dbo.TeamsxPlayers",
                c => new
                {
                    TeamsxPlayersID = c.Int(nullable: false, identity: true),
                    TeamID = c.Int(nullable: false),
                    PlayerID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.TeamsxPlayersID)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.PlayerID);
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.Players", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "Team_TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "Player_PlayerID", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "Team_TeamID" });
            DropIndex("dbo.TeamPlayers", new[] { "Player_PlayerID" });
            DropIndex("dbo.Players", new[] { "TeamID" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
            DropTable("dbo.Maps");
            DropTable("dbo.TeamsxPlayers");

        }
    }
}
