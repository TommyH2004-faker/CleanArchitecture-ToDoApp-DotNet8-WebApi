using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    ToDoListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.ToDoListId);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    ToDoItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToDoListId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.ToDoItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItems_ToDoLists_ToDoListId",
                        column: x => x.ToDoListId,
                        principalTable: "ToDoLists",
                        principalColumn: "ToDoListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 28, 12, 31, 48, 86, DateTimeKind.Local).AddTicks(9998), "john.doe@example.com", "john_doe" },
                    { 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(9), "jane.smith@example.com", "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "ToDoListId", "CreatedAt", "Description", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(228), "John's personal task list", "John's Personal Tasks", new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(229), 1 },
                    { 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(230), "Jane's work-related tasks", "Jane's Work Tasks", new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(231), 2 }
                });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "ToDoItemId", "CreatedAt", "Description", "DueDate", "IsCompleted", "Priority", "Title", "ToDoListId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(394), "Buy milk, eggs, and bread", new DateTime(2025, 12, 29, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(384), false, 2, "Buy groceries", 1, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(394) },
                    { 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(396), "Annual health checkup", new DateTime(2025, 12, 31, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(395), false, 3, "Schedule doctor appointment", 1, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(397) },
                    { 3, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(399), "Finish and submit the quarterly report", new DateTime(2025, 12, 30, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(398), false, 3, "Complete project report", 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(399) },
                    { 4, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(401), "Discuss project progress with the team", new DateTime(2025, 12, 29, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(400), false, 1, "Team meeting", 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(401) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_UserId",
                table: "ToDoLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
