namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TEST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                        Image = c.String(),
                        nationality = c.String(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Team_ID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        TLA = c.String(),
                        Crest = c.String(),
                        Address = c.String(),
                        Website = c.String(),
                        Founded = c.String(),
                        ClubColors = c.String(),
                        Venues = c.String(),
                        LastUpdated = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Enemies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Team_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.Enemies", "Team_ID", "dbo.Teams");
            DropIndex("dbo.Enemies", new[] { "Team_ID" });
            DropIndex("dbo.Players", new[] { "Team_ID" });
            DropTable("dbo.Enemies");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
        }
    }
}
