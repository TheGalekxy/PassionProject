namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_mvp1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamsxPlayers",
                c => new
                    {
                        TeamsxPlayersID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamsxPlayersID)
                .ForeignKey("dbo.Players", t => t.PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamsxPlayers", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.TeamsxPlayers", "PlayerID", "dbo.Players");
            DropIndex("dbo.TeamsxPlayers", new[] { "PlayerID" });
            DropIndex("dbo.TeamsxPlayers", new[] { "TeamID" });
            DropTable("dbo.TeamsxPlayers");
        }
    }
}
