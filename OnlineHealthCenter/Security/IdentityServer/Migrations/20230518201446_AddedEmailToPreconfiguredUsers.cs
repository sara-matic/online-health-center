using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    public partial class AddedEmailToPreconfiguredUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "774c0d56-b524-4df8-947f-144c4c36ac42",
                column: "ConcurrencyStamp",
                value: "b9adbf98-f93c-43e2-8a18-de33a0273da0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89389551-b461-4d5a-a389-620dea355b1b",
                column: "ConcurrencyStamp",
                value: "b42e5c30-bdf2-4179-a332-a7e199a14e5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5cb6a4b-e09b-414e-9625-574ab845dcb5",
                column: "ConcurrencyStamp",
                value: "3fbab2d8-6aba-4938-9f48-5dd337a15d7e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a2d354b-080c-4bd8-bc60-28c7b4f173dd", "alanstern@gmail.com", "AQAAAAEAACcQAAAAECTGZamHGrDG+yg/U45KZ5IvVQVSunX9t1an4ZFGzuJLgDm8ANnfxiqC6y+BL5T3AQ==", "8280ad1f-fa95-4c47-9568-76aab61d6fdb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66d4e6a9-cc76-4f99-872a-76d37573b5d2",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33cb9f97-7a45-4679-965a-8d3501e54bfd", "rachelgray@gmail.com", "AQAAAAEAACcQAAAAEBOmG1/SwGVuxCkFXcydWWtQqOPfKIfdypC++50XaijGnVxawqO0GPVDrk9BIYPRjQ==", "c143b51b-01f4-4ddf-8a7f-9d57bf092c1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a15a4178-2964-4973-b1fe-425ef1fdc0a4",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bc4f2f7-42d2-4000-8000-4cf83e7838fd", "jamesbrown@gmail.com", "AQAAAAEAACcQAAAAEEr+kyc2k84L3wz3+dAYTSWJ30JnLyCxWKWyr96U06nvDq1RlPTvh3vTqdD5j20zEw==", "fca2602c-8caf-45e8-8c21-3bfadcf9aca5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "774c0d56-b524-4df8-947f-144c4c36ac42",
                column: "ConcurrencyStamp",
                value: "1b68ac95-3496-4fbc-a308-3558f3b8268f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89389551-b461-4d5a-a389-620dea355b1b",
                column: "ConcurrencyStamp",
                value: "524d760b-cf61-4323-b0eb-0ecd5531a497");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5cb6a4b-e09b-414e-9625-574ab845dcb5",
                column: "ConcurrencyStamp",
                value: "37d473c9-9e22-4b91-8881-e19fb541a2bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd950860-19ae-4ada-8d28-d9d7a82ca875", null, "AQAAAAEAACcQAAAAECHyjcxYbzZShL6zzUzqr77zDtV1HpO6VhFeMUU31Ixu20fb0jIS3QV7duIzukSGMg==", "1af5b60f-54ff-40d1-bed0-0157ddc5e14b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66d4e6a9-cc76-4f99-872a-76d37573b5d2",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0004e54-2681-496d-96f2-3e62cc758f28", null, "AQAAAAEAACcQAAAAEHFg7wBzJBf651CFBMGW81HXqStBO6+C1ashmlL+PO0NCLtBzvQFdreZFkn1JWAJCQ==", "6b9aebe6-bfad-4c03-8074-6e1fa9b04535" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a15a4178-2964-4973-b1fe-425ef1fdc0a4",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "583e9250-211d-47e8-b996-21083e753740", null, "AQAAAAEAACcQAAAAEP1oQdcOl/HRG96CEebG+lECmqpYiikXi/t8qvi/0nIjF8xIagARmILEpK4boBfIlA==", "5db354a7-ff79-4a6c-acd2-480c43a2feed" });
        }
    }
}
