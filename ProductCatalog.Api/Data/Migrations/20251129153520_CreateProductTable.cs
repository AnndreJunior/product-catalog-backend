using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalog.Api.Data.Migrations;

/// <inheritdoc />
public partial class CreateProductTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) 
        => migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                description = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                price = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_products", x => x.id));

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) 
        => migrationBuilder.DropTable(name: "products");
}
