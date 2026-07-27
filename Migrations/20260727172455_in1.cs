using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BabySphere.Migrations
{
    /// <inheritdoc />
    public partial class in1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BabyProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyProducts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BabyProducts",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price", "Quantity", "Rating" },
                values: new object[,]
                {
                    { 1, "Travel", "Comfortable stroller for outdoor travel.", "/images/stroller.jpg", "Baby Stroller", 149.99m, 15, 4.7000000000000002 },
                    { 2, "Travel", "Safe and secure car seat for babies.", "/images/carseat.jpg", "Baby Car Seat", 199.99m, 10, 4.5 },
                    { 3, "Travel", "Spacious diaper bag for parents.", "/images/diaperbag.jpg", "Diaper Bag", 39.99m, 19, 4.9000000000000004 },
                    { 4, "Feeding", "BPA-free feeding bottle.", "/images/bottle.jpg", "Baby Bottle", 12.99m, 15, 4.2999999999999998 },
                    { 5, "Feeding", "Comfortable feeding chair for babies.", "/images/highchair.jpg", "High Chair", 89.99m, 8, 4.2999999999999998 },
                    { 6, "Feeding", "Soft bibs to keep clothes clean.", "/images/bibs.jpg", "Baby Bibs", 9.99m, 23, 3.7999999999999998 },
                    { 7, "Care", "Gentle lotion for baby skin.", "/images/lotion.jpg", "Baby Lotion", 8.99m, 12, 4.7999999999999998 },
                    { 8, "Care", "Tear-free shampoo for babies.", "/images/shampoo.jpg", "Baby Shampoo", 7.99m, 17, 4.7999999999999998 },
                    { 9, "Care", "Soft and comfortable diapers.", "/images/diapers.jpg", "Diapers Pack", 24.99m, 20, 4.5999999999999996 },
                    { 10, "Toys", "Educational blocks for learning.", "/images/blocks.jpg", "Building Blocks", 19.99m, 23, 3.7000000000000002 },
                    { 11, "Toys", "Soft plush teddy bear.", "/images/teddy.jpg", "Teddy Bear", 14.99m, 27, 3.6000000000000001 },
                    { 12, "Toys", "Colorful rattle toy for babies.", "/images/rattle.jpg", "Baby Rattle", 6.99m, 16, 4.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyProducts");
        }
    }
}
