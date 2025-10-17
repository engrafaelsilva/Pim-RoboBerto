using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IA_RoboBerto.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"pgcrypto\";");


            migrationBuilder.CreateTable(
     name: "categoria",
     columns: table => new
     {
         cat_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
         cat_nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_categoria", x => x.cat_codigo);
     });


            migrationBuilder.CreateTable(
       name: "departamento",
       columns: table => new
       {
           dep_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
           dep_nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
       },
       constraints: table =>
       {
           table.PrimaryKey("PK_departamento", x => x.dep_codigo);
       });

            migrationBuilder.CreateTable(
      name: "role",
      columns: table => new
      {
          role_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
          role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
      },
      constraints: table =>
      {
          table.PrimaryKey("PK_role", x => x.role_codigo);
      });


            migrationBuilder.CreateTable(
     name: "usuario",
     columns: table => new
     {
         usu_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
         usu_nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
         usu_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
         usu_senha_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
         usu_telefone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
         usu_datacriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
         dep_codigo = table.Column<Guid>(type: "uuid", nullable: true)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_usuario", x => x.usu_codigo);
         table.ForeignKey(
             name: "FK_usuario_departamento_dep_codigo",
             column: x => x.dep_codigo,
             principalTable: "departamento",
             principalColumn: "dep_codigo",
             onDelete: ReferentialAction.SetNull);
     });

            migrationBuilder.CreateTable(
                name: "chamado",
                columns: table => new
                {
                    cha_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    cha_usuario_autor = table.Column<Guid>(type: "uuid", nullable: false),
                    cha_usuario_tecnico = table.Column<Guid>(type: "uuid", nullable: true),
                    cat_codigo = table.Column<Guid>(type: "uuid", nullable: false),
                    cha_status = table.Column<int>(type: "integer", nullable: false),
                    cha_prioridade = table.Column<int>(type: "integer", nullable: false),
                    cha_titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cha_sugestao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cha_descricao = table.Column<string>(type: "text", nullable: false),
                    cha_aberto_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    cha_resolvido_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cha_sla_vence_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    cha_resolvido_com_ia = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chamado", x => x.cha_codigo);
                    table.ForeignKey(
                        name: "FK_chamado_categoria_cat_codigo",
                        column: x => x.cat_codigo,
                        principalTable: "categoria",
                        principalColumn: "cat_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chamado_usuario_cha_usuario_autor",
                        column: x => x.cha_usuario_autor,
                        principalTable: "usuario",
                        principalColumn: "usu_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chamado_usuario_cha_usuario_tecnico",
                        column: x => x.cha_usuario_tecnico,
                        principalTable: "usuario",
                        principalColumn: "usu_codigo",
                        onDelete: ReferentialAction.SetNull);
                });



            migrationBuilder.CreateTable(
                name: "role_usuario",
                columns: table => new
                {
                    usu_codigo = table.Column<Guid>(type: "uuid", nullable: false),
                    role_codigo = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_usuario", x => new { x.usu_codigo, x.role_codigo });
                    table.ForeignKey(
                        name: "FK_role_usuario_role",
                        column: x => x.role_codigo,
                        principalTable: "role",
                        principalColumn: "role_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_usuario_usuario",
                        column: x => x.usu_codigo,
                        principalTable: "usuario",
                        principalColumn: "usu_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
    name: "mensagem",
    columns: table => new
    {
        men_codigo = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
        usu_codigo = table.Column<Guid>(type: "uuid", nullable: false),
        men_data_hora_texto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
        men_texto = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
        cha_codigo = table.Column<Guid>(type: "uuid", nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_mensagem", x => x.men_codigo);
        table.ForeignKey(
            name: "FK_mensagem_chamado_cha_codigo",
            column: x => x.cha_codigo,
            principalTable: "chamado",
            principalColumn: "cha_codigo",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_mensagem_usuario_usu_codigo",
            column: x => x.usu_codigo,
            principalTable: "usuario",
            principalColumn: "usu_codigo");
    });


            migrationBuilder.CreateIndex(
                name: "IX_chamado_cat_codigo",
                table: "chamado",
                column: "cat_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_chamado_cha_usuario_autor",
                table: "chamado",
                column: "cha_usuario_autor");

            migrationBuilder.CreateIndex(
                name: "IX_chamado_cha_usuario_tecnico",
                table: "chamado",
                column: "cha_usuario_tecnico");

            migrationBuilder.CreateIndex(
                name: "IX_mensagem_cha_codigo",
                table: "mensagem",
                column: "cha_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_mensagem_usu_codigo",
                table: "mensagem",
                column: "usu_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_role_usuario_role_codigo",
                table: "role_usuario",
                column: "role_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_dep_codigo",
                table: "usuario",
                column: "dep_codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mensagem");

            migrationBuilder.DropTable(
                name: "role_usuario");

            migrationBuilder.DropTable(
                name: "chamado");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "departamento");
        }
    }
}
