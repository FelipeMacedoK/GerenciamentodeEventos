using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentodeEventos.Migrations
{
    /// <inheritdoc />
    public partial class working : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_evento_IdEvento",
                table: "feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_feedback_pessoa_IdPessoa",
                table: "feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_inscricao_evento_IdEvento",
                table: "inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_inscricao_pessoa_IdPessoa",
                table: "inscricao");

            migrationBuilder.RenameColumn(
                name: "IdPessoa",
                table: "inscricao",
                newName: "idpessoa");

            migrationBuilder.RenameColumn(
                name: "IdEvento",
                table: "inscricao",
                newName: "idevento");

            migrationBuilder.RenameIndex(
                name: "IX_inscricao_IdPessoa",
                table: "inscricao",
                newName: "IX_inscricao_idpessoa");

            migrationBuilder.RenameIndex(
                name: "IX_inscricao_IdEvento",
                table: "inscricao",
                newName: "IX_inscricao_idevento");

            migrationBuilder.RenameColumn(
                name: "IdPessoa",
                table: "feedback",
                newName: "idpessoa");

            migrationBuilder.RenameColumn(
                name: "IdEvento",
                table: "feedback",
                newName: "idevento");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_IdPessoa",
                table: "feedback",
                newName: "IX_feedback_idpessoa");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_IdEvento",
                table: "feedback",
                newName: "IX_feedback_idevento");

            migrationBuilder.AlterColumn<int>(
                name: "idpessoa",
                table: "inscricao",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idevento",
                table: "inscricao",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idpessoa",
                table: "feedback",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idevento",
                table: "feedback",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_evento_idevento",
                table: "feedback",
                column: "idevento",
                principalTable: "evento",
                principalColumn: "idevento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_pessoa_idpessoa",
                table: "feedback",
                column: "idpessoa",
                principalTable: "pessoa",
                principalColumn: "idpessoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inscricao_evento_idevento",
                table: "inscricao",
                column: "idevento",
                principalTable: "evento",
                principalColumn: "idevento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inscricao_pessoa_idpessoa",
                table: "inscricao",
                column: "idpessoa",
                principalTable: "pessoa",
                principalColumn: "idpessoa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_evento_idevento",
                table: "feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_feedback_pessoa_idpessoa",
                table: "feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_inscricao_evento_idevento",
                table: "inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_inscricao_pessoa_idpessoa",
                table: "inscricao");

            migrationBuilder.RenameColumn(
                name: "idpessoa",
                table: "inscricao",
                newName: "IdPessoa");

            migrationBuilder.RenameColumn(
                name: "idevento",
                table: "inscricao",
                newName: "IdEvento");

            migrationBuilder.RenameIndex(
                name: "IX_inscricao_idpessoa",
                table: "inscricao",
                newName: "IX_inscricao_IdPessoa");

            migrationBuilder.RenameIndex(
                name: "IX_inscricao_idevento",
                table: "inscricao",
                newName: "IX_inscricao_IdEvento");

            migrationBuilder.RenameColumn(
                name: "idpessoa",
                table: "feedback",
                newName: "IdPessoa");

            migrationBuilder.RenameColumn(
                name: "idevento",
                table: "feedback",
                newName: "IdEvento");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_idpessoa",
                table: "feedback",
                newName: "IX_feedback_IdPessoa");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_idevento",
                table: "feedback",
                newName: "IX_feedback_IdEvento");

            migrationBuilder.AlterColumn<int>(
                name: "IdPessoa",
                table: "inscricao",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "IdEvento",
                table: "inscricao",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "IdPessoa",
                table: "feedback",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "IdEvento",
                table: "feedback",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_evento_IdEvento",
                table: "feedback",
                column: "IdEvento",
                principalTable: "evento",
                principalColumn: "idevento");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_pessoa_IdPessoa",
                table: "feedback",
                column: "IdPessoa",
                principalTable: "pessoa",
                principalColumn: "idpessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_inscricao_evento_IdEvento",
                table: "inscricao",
                column: "IdEvento",
                principalTable: "evento",
                principalColumn: "idevento");

            migrationBuilder.AddForeignKey(
                name: "FK_inscricao_pessoa_IdPessoa",
                table: "inscricao",
                column: "IdPessoa",
                principalTable: "pessoa",
                principalColumn: "idpessoa");
        }
    }
}
