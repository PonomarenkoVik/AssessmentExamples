using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EngineDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnginePower = table.Column<float>(type: "real", nullable: false),
                    EngineSpeed = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    Width = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FabricNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EngineTypeId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Engines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EngineId = table.Column<int>(type: "int", nullable: false),
                    RepairType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EngineTypes",
                columns: new[] { "Id", "DisplayType", "EnginePower", "EngineSpeed", "Height", "Type", "Weight", "Width" },
                values: new object[,]
                {
                    { 1, "АИР200м4, 37 kWt, 1500 1/s", 37f, 1500, null, "АИР200м4", null, null },
                    { 2, "АИМ132м2, 11 kWt, 2900 1/s", 11f, 2900, null, "АИМ132м2", null, null },
                    { 3, "АИИ160S6, 11 kWt, 1000 1/s", 11f, 1000, null, "АИИ160S6", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "Surname" },
                values: new object[,]
                {
                    { 1, "Viktor", "Ponomarenko" },
                    { 2, "Oleksiy", "Babkin" },
                    { 3, "Serhiy", "Lisovets" }
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "EngineTypeId", "FabricNumber", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "F45GT76765", 1 },
                    { 2, 2, "FFGHJH7765", 1 },
                    { 3, 2, "FGJKLD6578", 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Description", "EngineId", "RepairType", "State", "UserId" },
                values: new object[,]
                {
                    { 1, "Common repairment", 1, 0, 0, 1 },
                    { 2, "Medium repairment", 2, 0, 2, 1 },
                    { 3, "Overhaul repairment", 3, 2, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engines_EngineTypeId",
                table: "Engines",
                column: "EngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_UserId",
                table: "Engines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EngineId",
                table: "Orders",
                column: "EngineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
