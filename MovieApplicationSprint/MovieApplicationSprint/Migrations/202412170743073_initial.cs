namespace MovieApplicationSprint.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        MovieId = c.String(maxLength: 128),
                        TheaterId = c.String(maxLength: 128),
                        ShowTimeId = c.String(maxLength: 128),
                        BookingDate = c.DateTime(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Movies", t => t.MovieId)
                .ForeignKey("dbo.ShowTimes", t => t.ShowTimeId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MovieId)
                .Index(t => t.TheaterId)
                .Index(t => t.ShowTimeId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Synopsis = c.String(),
                        Genre = c.String(),
                        Language = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Actor = c.String(),
                        Poster = c.String(),
                        Trailer = c.String(),
                        Director = c.String(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.ShowTimes",
                c => new
                    {
                        ShowTimeId = c.String(nullable: false, maxLength: 128),
                        MovieTheaterId = c.String(maxLength: 128),
                        ShowDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ShowTimeId)
                .ForeignKey("dbo.MovieTheaters", t => t.MovieTheaterId)
                .Index(t => t.MovieTheaterId);
            
            CreateTable(
                "dbo.MovieTheaters",
                c => new
                    {
                        MovieTheaterId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.String(maxLength: 128),
                        TheaterId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MovieTheaterId)
                .ForeignKey("dbo.Movies", t => t.MovieId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .Index(t => t.MovieId)
                .Index(t => t.TheaterId);
            
            CreateTable(
                "dbo.Theaters",
                c => new
                    {
                        TheaterId = c.String(nullable: false, maxLength: 128),
                        TheaterName = c.String(),
                        Location = c.String(),
                        SeatCount = c.Int(nullable: false),
                        TheaterType = c.String(),
                        City = c.String(),
                        TicketPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TheaterId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        BookingId = c.String(maxLength: 128),
                        PaymentMethod = c.String(),
                        Amount = c.Int(nullable: false),
                        PaymentStatus = c.String(),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        MovieId = c.String(maxLength: 128),
                        Ratings = c.String(),
                        ReviewText = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Movies", t => t.MovieId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.String(nullable: false, maxLength: 128),
                        BookingId = c.String(maxLength: 128),
                        ShowTimeId = c.String(maxLength: 128),
                        SeatNumber = c.String(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.ShowTimes", t => t.ShowTimeId)
                .Index(t => t.BookingId)
                .Index(t => t.ShowTimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ShowTimeId", "dbo.ShowTimes");
            DropForeignKey("dbo.Tickets", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Payments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Bookings", "ShowTimeId", "dbo.ShowTimes");
            DropForeignKey("dbo.ShowTimes", "MovieTheaterId", "dbo.MovieTheaters");
            DropForeignKey("dbo.MovieTheaters", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.MovieTheaters", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Bookings", "MovieId", "dbo.Movies");
            DropIndex("dbo.Tickets", new[] { "ShowTimeId" });
            DropIndex("dbo.Tickets", new[] { "BookingId" });
            DropIndex("dbo.Reviews", new[] { "MovieId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropIndex("dbo.MovieTheaters", new[] { "TheaterId" });
            DropIndex("dbo.MovieTheaters", new[] { "MovieId" });
            DropIndex("dbo.ShowTimes", new[] { "MovieTheaterId" });
            DropIndex("dbo.Bookings", new[] { "ShowTimeId" });
            DropIndex("dbo.Bookings", new[] { "TheaterId" });
            DropIndex("dbo.Bookings", new[] { "MovieId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Reviews");
            DropTable("dbo.Payments");
            DropTable("dbo.Users");
            DropTable("dbo.Theaters");
            DropTable("dbo.MovieTheaters");
            DropTable("dbo.ShowTimes");
            DropTable("dbo.Movies");
            DropTable("dbo.Bookings");
        }
    }
}
