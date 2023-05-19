using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_API.Migrations
{
    public partial class userProfileAskills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skills_user_UserID",
                table: "skills");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "skills",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_skills_UserID",
                table: "skills",
                newName: "IX_skills_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "userProjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "userExperiences",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "skills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "educations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "userProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AboutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userProfiles_about_AboutId",
                        column: x => x.AboutId,
                        principalTable: "about",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userProfiles_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_UserProfileId",
                table: "userProjects",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_userExperiences_UserProfileId",
                table: "userExperiences",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_UserID",
                table: "skills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_educations_UserProfileId",
                table: "educations",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_userProfiles_AboutId",
                table: "userProfiles",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_userProfiles_UserID",
                table: "userProfiles",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_educations_userProfiles_UserProfileId",
                table: "educations",
                column: "UserProfileId",
                principalTable: "userProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_skills_user_UserId",
                table: "skills",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_skills_userProfiles_UserID",
                table: "skills",
                column: "UserID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userExperiences_userProfiles_UserProfileId",
                table: "userExperiences",
                column: "UserProfileId",
                principalTable: "userProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_userProfiles_UserProfileId",
                table: "userProjects",
                column: "UserProfileId",
                principalTable: "userProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_educations_userProfiles_UserProfileId",
                table: "educations");

            migrationBuilder.DropForeignKey(
                name: "FK_skills_user_UserId",
                table: "skills");

            migrationBuilder.DropForeignKey(
                name: "FK_skills_userProfiles_UserID",
                table: "skills");

            migrationBuilder.DropForeignKey(
                name: "FK_userExperiences_userProfiles_UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_userProfiles_UserProfileId",
                table: "userProjects");

            migrationBuilder.DropTable(
                name: "userProfiles");

            migrationBuilder.DropIndex(
                name: "IX_userProjects_UserProfileId",
                table: "userProjects");

            migrationBuilder.DropIndex(
                name: "IX_userExperiences_UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropIndex(
                name: "IX_skills_UserID",
                table: "skills");

            migrationBuilder.DropIndex(
                name: "IX_educations_UserProfileId",
                table: "educations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "userProjects");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "skills");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "educations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "skills",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_skills_UserId",
                table: "skills",
                newName: "IX_skills_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_skills_user_UserID",
                table: "skills",
                column: "UserID",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
