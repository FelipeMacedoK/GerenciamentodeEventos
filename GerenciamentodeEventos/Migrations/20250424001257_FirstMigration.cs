using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerenciamentodeEventos.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    idcategoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.idcategoria);
                });

            migrationBuilder.CreateTable(
                name: "local",
                columns: table => new
                {
                    idlocal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    logradouro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    bairro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_local", x => x.idlocal);
                });

            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    idpessoa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.idpessoa);
                });

            migrationBuilder.CreateTable(
                name: "evento",
                columns: table => new
                {
                    idevento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    datahora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    capacidade = table.Column<int>(type: "integer", nullable: false),
                    valor = table.Column<float>(type: "real", nullable: false),
                    situacaoinscricao = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: true),
                    LocalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evento", x => x.idevento);
                    table.ForeignKey(
                        name: "FK_evento_categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categoria",
                        principalColumn: "idcategoria");
                    table.ForeignKey(
                        name: "FK_evento_local_LocalId",
                        column: x => x.LocalId,
                        principalTable: "local",
                        principalColumn: "idlocal");
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    idfeedback = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nota = table.Column<int>(type: "integer", nullable: false),
                    comentario = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    datafeedback = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdEvento = table.Column<int>(type: "integer", nullable: true),
                    IdPessoa = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.idfeedback);
                    table.ForeignKey(
                        name: "FK_feedback_evento_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "evento",
                        principalColumn: "idevento");
                    table.ForeignKey(
                        name: "FK_feedback_pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "pessoa",
                        principalColumn: "idpessoa");
                });

            migrationBuilder.CreateTable(
                name: "inscricao",
                columns: table => new
                {
                    idinscricao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datainscricao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    numeroinscricao = table.Column<int>(type: "integer", nullable: false),
                    IdEvento = table.Column<int>(type: "integer", nullable: true),
                    IdPessoa = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscricao", x => x.idinscricao);
                    table.ForeignKey(
                        name: "FK_inscricao_evento_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "evento",
                        principalColumn: "idevento");
                    table.ForeignKey(
                        name: "FK_inscricao_pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "pessoa",
                        principalColumn: "idpessoa");
                });

            migrationBuilder.CreateTable(
                name: "organizador",
                columns: table => new
                {
                    idorganizador = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    biografia = table.Column<string>(type: "text", nullable: true),
                    idpessoa = table.Column<int>(type: "integer", nullable: false),
                    idevento = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizador", x => x.idorganizador);
                    table.ForeignKey(
                        name: "FK_organizador_evento_idevento",
                        column: x => x.idevento,
                        principalTable: "evento",
                        principalColumn: "idevento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organizador_pessoa_idpessoa",
                        column: x => x.idpessoa,
                        principalTable: "pessoa",
                        principalColumn: "idpessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evento_CategoriaId",
                table: "evento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_evento_LocalId",
                table: "evento",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_IdEvento",
                table: "feedback",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_IdPessoa",
                table: "feedback",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_inscricao_IdEvento",
                table: "inscricao",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_inscricao_IdPessoa",
                table: "inscricao",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_organizador_idevento",
                table: "organizador",
                column: "idevento");

            migrationBuilder.CreateIndex(
                name: "IX_organizador_idpessoa",
                table: "organizador",
                column: "idpessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "inscricao");

            migrationBuilder.DropTable(
                name: "organizador");

            migrationBuilder.DropTable(
                name: "evento");

            migrationBuilder.DropTable(
                name: "pessoa");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "local");
        }
    }
}
