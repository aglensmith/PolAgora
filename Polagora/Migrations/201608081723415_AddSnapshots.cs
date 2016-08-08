namespace Polagora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSnapshots : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
