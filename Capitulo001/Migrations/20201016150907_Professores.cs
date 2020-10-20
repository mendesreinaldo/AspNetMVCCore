using Microsoft.EntityFrameworkCore.Migrations;

namespace Capitulo001.Migrations
{
    public partial class Professores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Departamentos_DepartamentoID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos");

            migrationBuilder.RenameTable(
                name: "Departamentos",
                newName: "Departamento");

            migrationBuilder.RenameIndex(
                name: "IX_Departamentos_InstituicaoID",
                table: "Departamento",
                newName: "IX_Departamento_InstituicaoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento",
                column: "DepartamentoID");

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProfessorID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    CursoID = table.Column<long>(nullable: false),
                    ProfessorID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => new { x.CursoID, x.ProfessorID });
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Departamento_DepartamentoID",
                table: "Cursos",
                column: "DepartamentoID",
                principalTable: "Departamento",
                principalColumn: "DepartamentoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Instituicoes_InstituicaoID",
                table: "Departamento",
                column: "InstituicaoID",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Departamento_DepartamentoID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Instituicoes_InstituicaoID",
                table: "Departamento");

            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departamento",
                table: "Departamento");

            migrationBuilder.RenameTable(
                name: "Departamento",
                newName: "Departamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Departamento_InstituicaoID",
                table: "Departamentos",
                newName: "IX_Departamentos_InstituicaoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departamentos",
                table: "Departamentos",
                column: "DepartamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Departamentos_DepartamentoID",
                table: "Cursos",
                column: "DepartamentoID",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos",
                column: "InstituicaoID",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
