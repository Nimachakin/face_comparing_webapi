using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FaceRecognitionApi.Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 128, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "date", nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    Initials = table.Column<string>(unicode: false, nullable: true),
                    ContactPhone = table.Column<string>(unicode: false, nullable: true),
                    RequestedRoleId = table.Column<Guid>(nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    BirthPlace = table.Column<string>(unicode: false, nullable: false),
                    PhotoFaceId = table.Column<Guid>(nullable: true),
                    PhotoProfileId = table.Column<Guid>(nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "current_date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoFaces",
                schema: "dbo",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Mime = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "current_date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoFaces", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoProfiles",
                schema: "dbo",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(nullable: false),
                    RowId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Mime = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "current_date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoProfiles", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(maxLength: 128, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<Guid>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(maxLength: 128, nullable: false),
                    RoleId = table.Column<Guid>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoFaceDescriptors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhotoFaceId = table.Column<Guid>(nullable: false),
                    Descriptor = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoFaceDescriptors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoFaceDescriptors_PhotoFace",
                        column: x => x.PhotoFaceId,
                        principalSchema: "dbo",
                        principalTable: "PhotoFaces",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoFaceDescriptors_PhotoFaceId",
                schema: "dbo",
                table: "PhotoFaceDescriptors",
                column: "PhotoFaceId");

            migrationBuilder.CreateIndex(
                name: "UQ__Photo__FFEE743086D66C32",
                schema: "dbo",
                table: "PhotoFaces",
                column: "RowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PK_PhotoProfile",
                schema: "dbo",
                table: "PhotoProfiles",
                column: "RowId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PhotoFaceDescriptors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PhotoProfiles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PhotoFaces",
                schema: "dbo");
        }
    }
}
