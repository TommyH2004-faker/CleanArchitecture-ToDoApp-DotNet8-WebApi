using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "ToDoItemId",
                table: "ToDoItems",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoLists",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoLists",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoItems",
                newName: "ToDoItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "ToDoListId", "CreatedAt", "Description", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1890), "John's personal task list", "John's Personal Tasks", new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1908), 1 },
                    { 2, new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1910), "Jane's work-related tasks", "Jane's Work Tasks", new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1910), 2 }
                });
        }
    }
}
