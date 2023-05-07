using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllAboutBooksWeb.Migrations;

/// <inheritdoc />
public partial class SeedCategoryTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "DisplayOrder", "Name" },
            values: new object[,]
            {
                { 1L, 1L, "Action" },
                { 2L, 2L, "SciFi" },
                { 3L, 3L, "History" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 1L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 2L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 3L);
    }
}
