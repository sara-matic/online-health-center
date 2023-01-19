using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "774c0d56-b524-4df8-947f-144c4c36ac41", "1c08ea94-ffb6-4a10-8ea2-c2d371b83c6c", "Patient", "PATIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89389551-b461-4d5a-a389-620dea355b0b", "6f110dc1-2627-4876-a0d9-c6e4768f26cf", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e5cb6a4b-e09b-414e-9625-574ab845dcb4", "c84d4927-9a71-47d7-a9e4-f6c0d20d6b6c", "Nurse", "NURSE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "774c0d56-b524-4df8-947f-144c4c36ac41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89389551-b461-4d5a-a389-620dea355b0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5cb6a4b-e09b-414e-9625-574ab845dcb4");
        }
    }
}
