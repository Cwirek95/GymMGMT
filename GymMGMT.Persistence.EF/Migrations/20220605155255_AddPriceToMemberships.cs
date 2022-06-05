using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymMGMT.Persistence.EF.Migrations
{
    public partial class AddPriceToMemberships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DefaultPrice",
                table: "MembershipTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Memberships",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultPrice",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Memberships");
        }
    }
}
