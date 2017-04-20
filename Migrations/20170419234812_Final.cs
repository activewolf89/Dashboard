using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Message_MessageId1",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_MessageId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessageReceiverId",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatorId",
                table: "Message",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessageReceiverId",
                table: "Message",
                column: "MessageReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_CreatorId",
                table: "Message",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_MessageReceiverId",
                table: "Message",
                column: "MessageReceiverId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_CreatorId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_MessageReceiverId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_CreatorId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_MessageReceiverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "MessageReceiverId",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessageId1",
                table: "Message",
                column: "MessageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Message_MessageId1",
                table: "Message",
                column: "MessageId1",
                principalTable: "Message",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserId",
                table: "Message",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
