using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cafeteria.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    clientid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.clientid);
                });

            migrationBuilder.CreateTable(
                name: "inputmaterialcategories",
                columns: table => new
                {
                    inputmaterialcategoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    suggestionusagetype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inputmaterialcategories", x => x.inputmaterialcategoryid);
                });

            migrationBuilder.CreateTable(
                name: "menutimes",
                columns: table => new
                {
                    menutimeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    timestart = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    timeend = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menutimes", x => x.menutimeid);
                });

            migrationBuilder.CreateTable(
                name: "productcategories",
                columns: table => new
                {
                    productcategorieid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productcategories", x => x.productcategorieid);
                });

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
                name: "tables",
                columns: table => new
                {
                    tableid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    zonename = table.Column<int>(type: "integer", nullable: false),
                    isbusy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.tableid);
                });

            migrationBuilder.CreateTable(
                name: "unitofmeasurements",
                columns: table => new
                {
                    unitofmesurementid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    abreviation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unitofmeasurements", x => x.unitofmesurementid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    menuid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    dateregister = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    menutimeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.menuid);
                    table.ForeignKey(
                        name: "FK_menu_menutimes_menutimeid",
                        column: x => x.menutimeid,
                        principalTable: "menutimes",
                        principalColumn: "menutimeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    productcategoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productid);
                    table.ForeignKey(
                        name: "FK_product_productcategories_productcategoryid",
                        column: x => x.productcategoryid,
                        principalTable: "productcategories",
                        principalColumn: "productcategorieid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    passwordhash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    roleid = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "rawmaterial",
                columns: table => new
                {
                    rawmaterialid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    unitofmesurementid = table.Column<int>(type: "integer", nullable: false),
                    pricebase = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rawmaterial", x => x.rawmaterialid);
                    table.ForeignKey(
                        name: "FK_rawmaterial_unitofmeasurements_unitofmesurementid",
                        column: x => x.unitofmesurementid,
                        principalTable: "unitofmeasurements",
                        principalColumn: "unitofmesurementid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    companyid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.companyid);
                    table.ForeignKey(
                        name: "FK_company_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updateat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cancelat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cancelreason = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    total = table.Column<float>(type: "real", nullable: true),
                    subtotal = table.Column<float>(type: "real", nullable: true),
                    iva = table.Column<float>(type: "real", nullable: true),
                    discount = table.Column<float>(type: "real", nullable: true),
                    paymenttype = table.Column<int>(type: "integer", nullable: true),
                    clientid = table.Column<int>(type: "integer", nullable: true),
                    cancelbyid = table.Column<int>(type: "integer", nullable: true),
                    finishbyid = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    ispaid = table.Column<bool>(type: "boolean", nullable: false),
                    tableid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderid);
                    table.ForeignKey(
                        name: "FK_orders_clients_clientid",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "clientid");
                    table.ForeignKey(
                        name: "FK_orders_tables_tableid",
                        column: x => x.tableid,
                        principalTable: "tables",
                        principalColumn: "tableid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_users_cancelbyid",
                        column: x => x.cancelbyid,
                        principalTable: "users",
                        principalColumn: "userid");
                    table.ForeignKey(
                        name: "FK_orders_users_finishbyid",
                        column: x => x.finishbyid,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    warehouseid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.warehouseid);
                    table.ForeignKey(
                        name: "FK_warehouse_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid");
                });

            migrationBuilder.CreateTable(
                name: "orderproduct",
                columns: table => new
                {
                    orderproductid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderproduct", x => x.orderproductid);
                    table.ForeignKey(
                        name: "FK_orderproduct_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderproduct_product_productid",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_userid",
                table: "company",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_menu_menutimeid",
                table: "menu",
                column: "menutimeid");

            migrationBuilder.CreateIndex(
                name: "IX_orderproduct_orderid",
                table: "orderproduct",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orderproduct_productid",
                table: "orderproduct",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_cancelbyid",
                table: "orders",
                column: "cancelbyid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_clientid",
                table: "orders",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_finishbyid",
                table: "orders",
                column: "finishbyid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_tableid",
                table: "orders",
                column: "tableid");

            migrationBuilder.CreateIndex(
                name: "IX_product_productcategoryid",
                table: "product",
                column: "productcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_rawmaterial_unitofmesurementid",
                table: "rawmaterial",
                column: "unitofmesurementid");

            migrationBuilder.CreateIndex(
                name: "IX_users_roleid",
                table: "users",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_userid",
                table: "warehouse",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "inputmaterialcategories");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "orderproduct");

            migrationBuilder.DropTable(
                name: "rawmaterial");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "menutimes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "unitofmeasurements");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "productcategories");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
