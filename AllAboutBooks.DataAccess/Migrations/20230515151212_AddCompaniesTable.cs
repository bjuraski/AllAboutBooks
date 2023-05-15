using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllAboutBooks.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddCompaniesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Companies",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Companies", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Companies",
            columns: new[] { "Id", "City", "Country", "Name", "PhoneNumber", "PostalCode", "StreetAddress" },
            values: new object[,]
            {
                { 1L, "Berlin", "Germany", "Tech Solution", "0214567890", "555666", "BundesStrasse 10" },
                { 2L, "Liverpool", "England", "Best Books Shop", "987654321", "13579", "Anfield Road 45" },
                { 3L, "Madrid", "Spain", "Fiesta", "8524972", "4582", "Santiago 22" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Companies");
    }
}
