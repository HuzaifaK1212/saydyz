using Microsoft.EntityFrameworkCore.Migrations;

namespace Platform.Data.Repositories.Migrations
{
    public partial class AreaChannelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ChannelId",
                table: "Order",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AreaId",
                table: "Customer",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Area_AreaId",
                table: "Customer",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Channel_ChannelId",
                table: "Order",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Area_AreaId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Channel_ChannelId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropIndex(
                name: "IX_Order_ChannelId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Customer_AreaId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
