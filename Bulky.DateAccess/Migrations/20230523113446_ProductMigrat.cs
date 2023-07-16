using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bulky.DateAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductMigrat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titel = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    Auther = table.Column<string>(type: "text", nullable: false),
                    Listprice = table.Column<double>(type: "double precision", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    price50 = table.Column<double>(type: "double precision", nullable: false),
                    price100 = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Auther", "Description", "ISBN", "Listprice", "Titel", "price", "price100", "price50" },
                values: new object[] { 1, "Mostafa Salem", "this book talk about nothing if you want to west your time look at it", "w0333334w", 70.0, "worldNight", 65.0, 55.0, 60.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
