using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllAboutBooks.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddProductsTableAndSeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                InternationalStandardBookNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ListPrice = table.Column<double>(type: "float", nullable: false),
                Price = table.Column<double>(type: "float", nullable: false),
                Price50 = table.Column<double>(type: "float", nullable: false),
                Price100 = table.Column<double>(type: "float", nullable: false),
                CategoryId = table.Column<long>(type: "bigint", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "Author", "CategoryId", "Description", "ImageUrl", "InternationalStandardBookNumber", "ListPrice", "Price", "Price100", "Price50", "Title" },
            values: new object[,]
            {
                { 1L, "Billy Spark", 1L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt.", null, "SWD9999001", 99.0, 90.0, 80.0, 85.0, "Fortune of Time" },
                { 2L, "Nancy Hoover", 3L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", null, "CAW777777701", 40.0, 30.0, 20.0, 25.0, "Dark Skies" },
                { 3L, "Julian Button", 1L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", null, "RITO5555501", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                { 4L, "Abby Muscles", 2L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", null, "WS3333333301", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                { 5L, "Ron Parker", 3L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", null, "SOTJ1111111101", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" },
                { 6L, "Laura Phantom", 2L, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", null, "FOT000000001", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Products_CategoryId",
            table: "Products",
            column: "CategoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Products");
    }
}
