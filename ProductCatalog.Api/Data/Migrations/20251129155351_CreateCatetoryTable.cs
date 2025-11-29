using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalog.Api.Data.Migrations;

/// <inheritdoc />
public partial class CreateCatetoryTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "category_id",
            table: "products",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.CreateTable(
            name: "categories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_categories", x => x.id));

        migrationBuilder.CreateIndex(
            name: "IX_products_category_id",
            table: "products",
            column: "category_id");

        migrationBuilder.AddForeignKey(
            name: "FK_products_categories_category_id",
            table: "products",
            column: "category_id",
            principalTable: "categories",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_products_categories_category_id",
            table: "products");

        migrationBuilder.DropTable(
            name: "categories");

        migrationBuilder.DropIndex(
            name: "IX_products_category_id",
            table: "products");

        migrationBuilder.DropColumn(
            name: "category_id",
            table: "products");
    }
}
