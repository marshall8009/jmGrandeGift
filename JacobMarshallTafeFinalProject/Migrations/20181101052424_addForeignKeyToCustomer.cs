using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JacobMarshallTafeFinalProject.Migrations
{
    public partial class addForeignKeyToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "TblCustomer",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "TblCustomer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblCustomer_UserID",
                table: "TblCustomer",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCustomer_AspNetUsers_UserID",
                table: "TblCustomer",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCustomer_AspNetUsers_UserID",
                table: "TblCustomer");

            migrationBuilder.DropIndex(
                name: "IX_TblCustomer_UserID",
                table: "TblCustomer");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "TblCustomer",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TblCustomer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
