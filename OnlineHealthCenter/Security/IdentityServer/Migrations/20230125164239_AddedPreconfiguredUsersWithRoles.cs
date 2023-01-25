using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    public partial class AddedPreconfiguredUsersWithRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1972ace0-6e1e-4ff6-8827-3d7305ec1d36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "578b1baa-2e94-4baa-bcd4-022f9b6950da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91e01cd4-d63e-465b-82d8-611555b586e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "774c0d56-b524-4df8-947f-144c4c36ac42", "1b68ac95-3496-4fbc-a308-3558f3b8268f", "Patient", "PATIENT" },
                    { "89389551-b461-4d5a-a389-620dea355b1b", "524d760b-cf61-4323-b0eb-0ecd5531a497", "Doctor", "DOCTOR" },
                    { "e5cb6a4b-e09b-414e-9625-574ab845dcb5", "37d473c9-9e22-4b91-8881-e19fb541a2bb", "Nurse", "NURSE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba", 0, "bd950860-19ae-4ada-8d28-d9d7a82ca875", null, false, "Alan", "Stern", false, null, null, "ALANSTERN", "AQAAAAEAACcQAAAAECHyjcxYbzZShL6zzUzqr77zDtV1HpO6VhFeMUU31Ixu20fb0jIS3QV7duIzukSGMg==", null, false, "1af5b60f-54ff-40d1-bed0-0157ddc5e14b", false, "alanstern" },
                    { "66d4e6a9-cc76-4f99-872a-76d37573b5d2", 0, "f0004e54-2681-496d-96f2-3e62cc758f28", null, false, "Rachel", "Gray", false, null, null, "RACHELGRAY", "AQAAAAEAACcQAAAAEHFg7wBzJBf651CFBMGW81HXqStBO6+C1ashmlL+PO0NCLtBzvQFdreZFkn1JWAJCQ==", null, false, "6b9aebe6-bfad-4c03-8074-6e1fa9b04535", false, "rachelgray" },
                    { "a15a4178-2964-4973-b1fe-425ef1fdc0a4", 0, "583e9250-211d-47e8-b996-21083e753740", null, false, "James", "Brown", false, null, null, "JAMESBROWN", "AQAAAAEAACcQAAAAEP1oQdcOl/HRG96CEebG+lECmqpYiikXi/t8qvi/0nIjF8xIagARmILEpK4boBfIlA==", null, false, "5db354a7-ff79-4a6c-acd2-480c43a2feed", false, "jamesbrown" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "89389551-b461-4d5a-a389-620dea355b1b", "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e5cb6a4b-e09b-414e-9625-574ab845dcb5", "66d4e6a9-cc76-4f99-872a-76d37573b5d2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "774c0d56-b524-4df8-947f-144c4c36ac42", "a15a4178-2964-4973-b1fe-425ef1fdc0a4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "89389551-b461-4d5a-a389-620dea355b1b", "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e5cb6a4b-e09b-414e-9625-574ab845dcb5", "66d4e6a9-cc76-4f99-872a-76d37573b5d2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "774c0d56-b524-4df8-947f-144c4c36ac42", "a15a4178-2964-4973-b1fe-425ef1fdc0a4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "774c0d56-b524-4df8-947f-144c4c36ac42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89389551-b461-4d5a-a389-620dea355b1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5cb6a4b-e09b-414e-9625-574ab845dcb5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66d4e6a9-cc76-4f99-872a-76d37573b5d2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a15a4178-2964-4973-b1fe-425ef1fdc0a4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1972ace0-6e1e-4ff6-8827-3d7305ec1d36", "1aba92d3-deb9-4fda-82bc-db7df276eadc", "Doctor", "DOCTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "578b1baa-2e94-4baa-bcd4-022f9b6950da", "4d4a9efd-55be-477f-ad1c-bc0f9511c6c6", "Patient", "PATIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91e01cd4-d63e-465b-82d8-611555b586e4", "0e14af78-8307-4caa-8f83-7411fcbc675d", "Nurse", "NURSE" });
        }
    }
}
