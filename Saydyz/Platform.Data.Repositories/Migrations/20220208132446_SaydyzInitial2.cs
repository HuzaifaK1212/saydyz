using Microsoft.EntityFrameworkCore.Migrations;

namespace Platform.Data.Repositories.Migrations
{
    public partial class SaydyzInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemTypeId",
                table: "Flavor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Flavor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flavor_ItemTypeId",
                table: "Flavor",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flavor_ItemType_ItemTypeId",
                table: "Flavor",
                column: "ItemTypeId",
                principalTable: "ItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flavor_ItemType_ItemTypeId",
                table: "Flavor");

            migrationBuilder.DropTable(
                name: "ItemType");

            migrationBuilder.DropIndex(
                name: "IX_Flavor_ItemTypeId",
                table: "Flavor");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "Flavor");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flavor");
        }
    }
}
