using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlacementTest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_user_and_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "TestTakers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TestTakers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "TestTakers",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "DateDeleted",
                table: "TestTakers",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "TestTakers",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TestTakers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TestTakers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestTakers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TestTakers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TestTakers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "TestTakers",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "TestTakers",
                newName: "DateDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "TestTakers",
                newName: "DateCreated");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "TestTakers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
