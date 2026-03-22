using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Manager.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLanFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c926fbf-82dd-4291-8d56-f5e27eaf7672");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbdd5e7d-3f89-497b-a84f-2a5eddf3432a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd452e5b-ec08-4bca-ad04-a66246daea97");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47e06572-5580-4155-9811-82628dab1a3a", null, "Guest", "GUEST" },
                    { "918079f1-2b0a-46b9-8860-1046c5912a00", null, "Admin", "ADMIN" },
                    { "f7debb21-dc4c-413d-8644-e50cfae19559", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47e06572-5580-4155-9811-82628dab1a3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "918079f1-2b0a-46b9-8860-1046c5912a00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7debb21-dc4c-413d-8644-e50cfae19559");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c926fbf-82dd-4291-8d56-f5e27eaf7672", null, "Admin", "ADMIN" },
                    { "bbdd5e7d-3f89-497b-a84f-2a5eddf3432a", null, "Manager", "MANAGER" },
                    { "dd452e5b-ec08-4bca-ad04-a66246daea97", null, "Guest", "GUEST" }
                });
        }
    }
}
