using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentodeEventos.Migrations
{
    /// <inheritdoc />
    public partial class refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evento_categoria_CategoriaId",
                table: "evento");

            migrationBuilder.DropForeignKey(
                name: "FK_evento_local_LocalId",
                table: "evento");

            migrationBuilder.DropIndex(
                name: "IX_evento_CategoriaId",
                table: "evento");

            migrationBuilder.DropIndex(
                name: "IX_evento_LocalId",
                table: "evento");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "evento");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "evento");

            migrationBuilder.RenameColumn(
                name: "IdLocal",
                table: "evento",
                newName: "idlocal");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "evento",
                newName: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_evento_idcategoria",
                table: "evento",
                column: "idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_evento_idlocal",
                table: "evento",
                column: "idlocal");

            migrationBuilder.AddForeignKey(
                name: "FK_evento_categoria_idcategoria",
                table: "evento",
                column: "idcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_evento_local_idlocal",
                table: "evento",
                column: "idlocal",
                principalTable: "local",
                principalColumn: "idlocal",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evento_categoria_idcategoria",
                table: "evento");

            migrationBuilder.DropForeignKey(
                name: "FK_evento_local_idlocal",
                table: "evento");

            migrationBuilder.DropIndex(
                name: "IX_evento_idcategoria",
                table: "evento");

            migrationBuilder.DropIndex(
                name: "IX_evento_idlocal",
                table: "evento");

            migrationBuilder.RenameColumn(
                name: "idlocal",
                table: "evento",
                newName: "IdLocal");

            migrationBuilder.RenameColumn(
                name: "idcategoria",
                table: "evento",
                newName: "IdCategoria");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "evento",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalId",
                table: "evento",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_evento_CategoriaId",
                table: "evento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_evento_LocalId",
                table: "evento",
                column: "LocalId");

            migrationBuilder.AddForeignKey(
                name: "FK_evento_categoria_CategoriaId",
                table: "evento",
                column: "CategoriaId",
                principalTable: "categoria",
                principalColumn: "idcategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_evento_local_LocalId",
                table: "evento",
                column: "LocalId",
                principalTable: "local",
                principalColumn: "idlocal");
        }
    }
}
