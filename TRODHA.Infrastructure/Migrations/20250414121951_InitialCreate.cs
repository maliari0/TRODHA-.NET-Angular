using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TRODHA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportanceLevels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportanceLevels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    PeriodUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImportanceLevelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                    table.CheckConstraint("CK_Goals_Frequency", "Frequency BETWEEN 1 AND 6");
                    table.CheckConstraint("CK_Goals_PeriodUnit", "PeriodUnit IN ('günde', 'haftada', 'ayda', 'yılda')");
                    table.ForeignKey(
                        name: "FK_Goals_ImportanceLevels_ImportanceLevelId",
                        column: x => x.ImportanceLevelId,
                        principalTable: "ImportanceLevels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    RecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.RecommendationId);
                    table.ForeignKey(
                        name: "FK_Recommendations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserNotes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_UserNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalStatuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_GoalStatuses_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoteImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteImages", x => x.ImageId);
                    table.CheckConstraint("CK_NoteImages_ImageType", "ImageType IN ('jpeg', 'png', 'gif', 'bmp', 'tiff')");
                    table.ForeignKey(
                        name: "FK_NoteImages_UserNotes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "UserNotes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ImportanceLevels",
                columns: new[] { "LevelId", "LevelName" },
                values: new object[,]
                {
                    { 1, "düşük" },
                    { 2, "orta" },
                    { 3, "yüksek" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ImportanceLevelId",
                table: "Goals",
                column: "ImportanceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalStatuses_GoalId",
                table: "GoalStatuses",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalStatuses_UserId",
                table: "GoalStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportanceLevels_LevelName",
                table: "ImportanceLevels",
                column: "LevelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteImages_NoteId",
                table: "NoteImages",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_UserId",
                table: "Recommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotes_UserId",
                table: "UserNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalStatuses");

            migrationBuilder.DropTable(
                name: "NoteImages");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "UserNotes");

            migrationBuilder.DropTable(
                name: "ImportanceLevels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
