namespace Polagora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSnapshots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TwitterID = c.String(),
                        FacebookID = c.String(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Party = c.String(),
                        TwitterURL = c.String(),
                        FacebookURL = c.String(),
                        CampaignURL = c.String(),
                        FacebookCoverPhoto = c.String(),
                        TwitterProfilePic = c.String(),
                        TwitterFollowers = c.Int(nullable: false),
                        FacebookLikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Snapshots",
                c => new
                    {
                        CandidateID = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        FacebookLikes = c.Int(nullable: false),
                        TwitterFollowers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CandidateID, t.Time })
                .ForeignKey("dbo.Candidates", t => t.CandidateID, cascadeDelete: true)
                .Index(t => t.CandidateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snapshots", "CandidateID", "dbo.Candidates");
            DropIndex("dbo.Snapshots", new[] { "CandidateID" });
            DropTable("dbo.Snapshots");
            DropTable("dbo.Candidates");
        }
    }
}
