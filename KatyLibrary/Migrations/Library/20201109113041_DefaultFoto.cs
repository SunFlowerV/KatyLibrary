using Microsoft.EntityFrameworkCore.Migrations;

namespace KatyLibrary.Migrations.Library
{
    public partial class DefaultFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "Authors",
                nullable: true,
                defaultValue: "~/images/NotAvatar.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "~/images/NotAvatar.png");
        }
    }
}
