using Microsoft.EntityFrameworkCore.Migrations;

namespace LetsFullProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROJETO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMAGEM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LIKE_CONTADOR = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJETO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROJETO_USUARIO_ID",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_LIKE_PROJETO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioLike = table.Column<int>(type: "int", nullable: false),
                    IdProjetoLike = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_LIKE_PROJETO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_LIKE_PROJETO_PROJETO_IdProjetoLike",
                        column: x => x.IdProjetoLike,
                        principalTable: "PROJETO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_USUARIO_LIKE_PROJETO_USUARIO_IdUsuarioLike",
                        column: x => x.IdUsuarioLike,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROJETO_IdUsuarioCadastro",
                table: "PROJETO",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LIKE_PROJETO_IdProjetoLike",
                table: "USUARIO_LIKE_PROJETO",
                column: "IdProjetoLike");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_LIKE_PROJETO_IdUsuarioLike",
                table: "USUARIO_LIKE_PROJETO",
                column: "IdUsuarioLike");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_LIKE_PROJETO");

            migrationBuilder.DropTable(
                name: "PROJETO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
