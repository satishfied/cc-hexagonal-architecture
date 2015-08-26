namespace Recruiting.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Screening",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Candidate = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScreeningAspect",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Excercise_ID = c.Guid(nullable: false),
                        KnowledgeDomain_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Screening", t => t.Excercise_ID)
                .ForeignKey("dbo.Screening", t => t.KnowledgeDomain_ID)
                .Index(t => t.Excercise_ID)
                .Index(t => t.KnowledgeDomain_ID);
            
            CreateTable(
                "dbo.Score",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        Remarks = c.String(),
                        Scoring = c.Int(nullable: false),
                        ScreeningAspect_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ScreeningAspect", t => t.ScreeningAspect_ID, cascadeDelete: true)
                .Index(t => t.ScreeningAspect_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScreeningAspect", "KnowledgeDomain_ID", "dbo.Screening");
            DropForeignKey("dbo.ScreeningAspect", "Excercise_ID", "dbo.Screening");
            DropForeignKey("dbo.Score", "ScreeningAspect_ID", "dbo.ScreeningAspect");
            DropIndex("dbo.Score", new[] { "ScreeningAspect_ID" });
            DropIndex("dbo.ScreeningAspect", new[] { "KnowledgeDomain_ID" });
            DropIndex("dbo.ScreeningAspect", new[] { "Excercise_ID" });
            DropTable("dbo.Score");
            DropTable("dbo.ScreeningAspect");
            DropTable("dbo.Screening");
        }
    }
}
