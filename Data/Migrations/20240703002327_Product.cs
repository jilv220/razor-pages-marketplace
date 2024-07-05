using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(12, 2)", nullable: false),
                    QtyInStock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            // Seed data
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Type", "Description", "UnitPrice", "QtyInStock" },
                values: new object[,]
                {
                    { "Coffee", "Food", "Some overpriced coffee. Who will buy this?", 10.99m, 100 },
                    { "iPhone XXX", "Electronics", "Is it the latest generation iPhone, or is it a scam?", 20.99m, 200 },
                    { "Hoverboard", "Gadgets", "Because walking is too mainstream.", 30.99m, 300 },
                    { "Magic Wand", "Toys", "Cast spells and impress your friends. Batteries not included.", 40.99m, 400 },
                    { "Invisible Cloak", "Fashion", "Perfect for sneaking around unnoticed. Does it really work?", 50.99m, 500 },
                    { "Robot Butler", "Appliances", "A butler that never complains or takes a day off.", 60.99m, 600 },
                    { "Time Machine", "Science", "Travel to the past or future. Use at your own risk.", 70.99m, 700 },
                    { "Smart Fridge", "Home", "Keeps your food fresh and orders groceries for you.", 80.99m, 800 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
