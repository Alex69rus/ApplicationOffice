﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationOffice.Approvals.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applications_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit_approvers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    ApproverId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_approvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_unit_approvers_units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_unit_approvers_users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_approvers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_approvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_approvers_applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_approvers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "application_fields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_fields_applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "units",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Title" },
                values: new object[] { 1L, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Отдел ИТ-разработки" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "Email", "ModifiedAt", "Name", "UnitId" },
                values: new object[] { 1L, new DateTime(1970, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "PP.Petrov@mail.ru", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Петров Пётр Петрович", 1L });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "Email", "ModifiedAt", "Name", "UnitId" },
                values: new object[] { 2L, new DateTime(1987, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "II.Ivanov@mail.ru", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Иванов Иван Иванович", 1L });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "Email", "ModifiedAt", "Name", "UnitId" },
                values: new object[] { 3L, new DateTime(1990, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AA.Alexeev@mail.ru", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Алексеев Алексей Алексеевич", 1L });

            migrationBuilder.InsertData(
                table: "unit_approvers",
                columns: new[] { "Id", "ApplicationType", "ApproverId", "CreatedAt", "ModifiedAt", "Title", "UnitId" },
                values: new object[] { 1L, 1, 1L, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Руководитель отдела", 1L });

            migrationBuilder.InsertData(
                table: "unit_approvers",
                columns: new[] { "Id", "ApplicationType", "ApproverId", "CreatedAt", "ModifiedAt", "Title", "UnitId" },
                values: new object[] { 2L, 1, 2L, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Отдел кадров", 1L });

            migrationBuilder.CreateIndex(
                name: "IX_application_approvers_ApplicationId",
                table: "application_approvers",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_application_approvers_UserId",
                table: "application_approvers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_application_fields_ApplicationId",
                table: "application_fields",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_applications_AuthorId",
                table: "applications",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_unit_approvers_ApproverId",
                table: "unit_approvers",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_unit_approvers_UnitId",
                table: "unit_approvers",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UnitId",
                table: "users",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_approvers");

            migrationBuilder.DropTable(
                name: "application_fields");

            migrationBuilder.DropTable(
                name: "unit_approvers");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "units");
        }
    }
}
