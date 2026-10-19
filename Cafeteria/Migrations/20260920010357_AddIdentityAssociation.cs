using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cafeteria.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityAssociation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_users_userid",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_cancelbyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_finishbyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_warehouse_users_userid",
                table: "warehouse");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.AlterColumn<string>(
                name: "userid",
                table: "warehouse",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "finishbyid",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cancelbyid",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "userid",
                table: "company",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Pay",
                table: "AspNetRoles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_company_AspNetUsers_userid",
                table: "company",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_AspNetUsers_cancelbyid",
                table: "orders",
                column: "cancelbyid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_AspNetUsers_finishbyid",
                table: "orders",
                column: "finishbyid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_warehouse_AspNetUsers_userid",
                table: "warehouse",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_AspNetUsers_userid",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_AspNetUsers_cancelbyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_AspNetUsers_finishbyid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_warehouse_AspNetUsers_userid",
                table: "warehouse");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pay",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "warehouse",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "finishbyid",
                table: "orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cancelbyid",
                table: "orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "company",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    pay = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    passwordhash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                    table.ForeignKey(
                        name: "FK_users_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "roleid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_roleid",
                table: "users",
                column: "roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_company_users_userid",
                table: "company",
                column: "userid",
                principalTable: "users",
                principalColumn: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_cancelbyid",
                table: "orders",
                column: "cancelbyid",
                principalTable: "users",
                principalColumn: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_finishbyid",
                table: "orders",
                column: "finishbyid",
                principalTable: "users",
                principalColumn: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_warehouse_users_userid",
                table: "warehouse",
                column: "userid",
                principalTable: "users",
                principalColumn: "userid");
        }
    }
}
