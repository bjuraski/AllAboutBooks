using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllAboutBooks.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddImageUrlFieldToProduct : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ImageUrl",
            table: "Products",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 1L,
            column: "ImageUrl",
            value: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 2L,
            column: "ImageUrl",
            value: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 3L,
            column: "ImageUrl",
            value: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 4L,
            column: "ImageUrl",
            value: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 5L,
            column: "ImageUrl",
            value: "");

        migrationBuilder.UpdateData(
            table: "Products",
            keyColumn: "Id",
            keyValue: 6L,
            column: "ImageUrl",
            value: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ImageUrl",
            table: "Products");
    }
}
