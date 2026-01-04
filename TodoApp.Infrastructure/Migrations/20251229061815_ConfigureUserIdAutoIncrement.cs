using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureUserIdAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "ToDoItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "ToDoItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "ToDoItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "ToDoItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ToDoLists",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoLists",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ToDoItems",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoItems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "ToDoItems",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoItems",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1890), new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1908) });

            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1910), new DateTime(2025, 12, 29, 13, 18, 15, 529, DateTimeKind.Local).AddTicks(1910) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ToDoLists",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoLists",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ToDoItems",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "ToDoItems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ToDoItems",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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

            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(228), new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(229) });

            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "ToDoListId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(230), new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(231) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 28, 12, 31, 48, 86, DateTimeKind.Local).AddTicks(9998), "john.doe@example.com", "john_doe" },
                    { 2, new DateTime(2025, 12, 28, 12, 31, 48, 87, DateTimeKind.Local).AddTicks(9), "jane.smith@example.com", "jane_smith" }
                });
        }
    }
}
