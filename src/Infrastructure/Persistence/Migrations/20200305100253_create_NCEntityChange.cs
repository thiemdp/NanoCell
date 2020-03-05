using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NanoCell.Infrastructure.Persistence.Migrations
{
    public partial class create_NCEntityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NCEntityChanges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    ChangeType = table.Column<int>(nullable: false),
                    TableName = table.Column<string>(maxLength: 255, nullable: true),
                    EntityId = table.Column<string>(maxLength: 255, nullable: true),
                    keyName = table.Column<string>(maxLength: 255, nullable: true),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCEntityChanges", x => x.Id);
                });
                  migrationBuilder.CreateIndex(
                  name: "IX_NCEntityChanges",
                  table: "NCEntityChanges",
                  columns: new string[] { "TableName", "EntityId","CreationTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NCEntityChanges");
        }
    }
}
