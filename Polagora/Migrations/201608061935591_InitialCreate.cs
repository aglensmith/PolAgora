namespace Polagora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Candidates");
        }
    }
}
