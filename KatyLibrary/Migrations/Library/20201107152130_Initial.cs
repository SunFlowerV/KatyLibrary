using Microsoft.EntityFrameworkCore.Migrations;

namespace KatyLibrary.Migrations.Library
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Authors",
            //    columns: table => new
            //    {
            //        AuthorId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 50, nullable: false),
            //        Biography = table.Column<string>(nullable: true),
            //        Foto = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Authors", x => x.AuthorId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Genres",
            //    columns: table => new
            //    {
            //        GenreId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 150, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Genres", x => x.GenreId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Books",
            //    columns: table => new
            //    {
            //        BookId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 150, nullable: false),
            //        AuthorId = table.Column<int>(nullable: false),
            //        BookCover = table.Column<string>(nullable: true),
            //        YearOfPubliching = table.Column<int>(nullable: false),
            //        NumberOfPages = table.Column<int>(nullable: false),
            //        Annotation = table.Column<string>(nullable: true),
            //        IsBestSeller = table.Column<bool>(nullable: false, defaultValue: false),
            //        Text = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Books", x => x.BookId);
            //        table.ForeignKey(
            //            name: "FK_Books_Authors_AuthorId",
            //            column: x => x.AuthorId,
            //            principalTable: "Authors",
            //            principalColumn: "AuthorId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GenreBook",
            //    columns: table => new
            //    {
            //        GenreId = table.Column<int>(nullable: false),
            //        BookId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GenreBook", x => new { x.GenreId, x.BookId });
            //        table.ForeignKey(
            //            name: "FK_GenreBook_Books_BookId",
            //            column: x => x.BookId,
            //            principalTable: "Books",
            //            principalColumn: "BookId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_GenreBook_Genres_GenreId",
            //            column: x => x.GenreId,
            //            principalTable: "Genres",
            //            principalColumn: "GenreId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Books_AuthorId",
            //    table: "Books",
            //    column: "AuthorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_GenreBook_BookId",
            //    table: "GenreBook",
            //    column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreBook");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
