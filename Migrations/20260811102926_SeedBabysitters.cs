using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabySphere.Migrations
{
    public partial class SeedBabysitters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Babysitters",
                columns: new[]
                {
                    "Id",
                    "Experience",
                    "HourlyRate",
                    "Name",
                    "Rating",
                    "Skills"
                },
                values: new object[,]
                {
                    {
                        1, 4, 22.00m, "Emily Johnson", 4.8,
                        "Infant Care, Feeding, Bedtime Routine"
                    },
                    {
                        2, 3, 20.00m, "Sarah Williams", 4.6,
                        "Toddler Care, Activities, Meal Preparation"
                    },
                    {
                        3, 5, 25.00m, "Michael Brown", 4.9,
                        "Child Safety, Homework Help, Outdoor Activities"
                    },
                    {
                        4, 2, 18.00m, "Jessica Miller", 4.4,
                        "Infant Care, Diaper Changing, Playtime"
                    },
                    {
                        5, 6, 28.00m, "Daniel Wilson", 4.7,
                        "Special Needs Care, Homework Help, Meal Preparation"
                    },
                    {
                        6, 1, 17.00m, "Olivia Davis", 4.2,
                        "Toddler Care, Playtime, Storytelling"
                    },
                    {
                        7, 7, 30.00m, "James Anderson", 4.9,
                        "Infant Care, Child Safety, First Aid"
                    },
                    {
                        8, 4, 23.00m, "Sophia Taylor", 4.5,
                        "Homework Help, Arts and Crafts, Activities"
                    },
                    {
                        9, 3, 21.00m, "Emma Thompson", 4.7,
                        "Feeding, Infant Care, Bedtime Routine"
                    },
                    {
                        10, 5, 26.00m, "David Clark", 4.6,
                        "Outdoor Activities, Homework Help, Meal Preparation"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Babysitters",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5,
                    6, 7, 8, 9, 10
                });
        }
    }
}