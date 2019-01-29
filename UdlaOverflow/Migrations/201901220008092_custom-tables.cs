namespace UdlaOverflow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UO_Answer",
                c => new
                    {
                        UO_AnswerID = c.Int(nullable: false, identity: true),
                        UO_QuestionID = c.Int(nullable: false),
                        UO_UserID = c.Int(nullable: false),
                        UO_CategoryID = c.Int(nullable: false),
                        TopicAnswer = c.String(),
                        DescriptionAnswer = c.String(),
                        ApplicationUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UO_AnswerID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUsers_Id)
                .ForeignKey("dbo.UO_Question", t => t.UO_QuestionID, cascadeDelete: true)
                .Index(t => t.UO_QuestionID)
                .Index(t => t.ApplicationUsers_Id);
            
            CreateTable(
                "dbo.UO_Question",
                c => new
                    {
                        UO_QuestionID = c.Int(nullable: false, identity: true),
                        UO_UserID = c.Int(nullable: false),
                        UO_CategoryID = c.Int(nullable: false),
                        TitleQuestion = c.String(),
                        DescriptionQuestion = c.String(),
                        DateQuestion = c.DateTime(nullable: false),
                        ApplicationUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UO_QuestionID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUsers_Id)
                .ForeignKey("dbo.UO_Category", t => t.UO_CategoryID, cascadeDelete: true)
                .Index(t => t.UO_CategoryID)
                .Index(t => t.ApplicationUsers_Id);
            
            CreateTable(
                "dbo.UO_Category",
                c => new
                    {
                        UO_CategoryID = c.Int(nullable: false, identity: true),
                        DescriptionCategory = c.String(),
                    })
                .PrimaryKey(t => t.UO_CategoryID);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UO_Question", "UO_CategoryID", "dbo.UO_Category");
            DropForeignKey("dbo.UO_Answer", "UO_QuestionID", "dbo.UO_Question");
            DropForeignKey("dbo.UO_Question", "ApplicationUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UO_Answer", "ApplicationUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UO_Question", new[] { "ApplicationUsers_Id" });
            DropIndex("dbo.UO_Question", new[] { "UO_CategoryID" });
            DropIndex("dbo.UO_Answer", new[] { "ApplicationUsers_Id" });
            DropIndex("dbo.UO_Answer", new[] { "UO_QuestionID" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.UO_Category");
            DropTable("dbo.UO_Question");
            DropTable("dbo.UO_Answer");
        }
    }
}
