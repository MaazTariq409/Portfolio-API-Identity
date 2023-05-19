using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_API.Migrations
{
    public partial class allModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_about_user_UserID",
                table: "about");

            migrationBuilder.DropForeignKey(
                name: "FK_educations_user_UserID",
                table: "educations");

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
                name: "FK_userExperiences_user_userID",
                table: "userExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_userExperiences_userProfiles_UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_userProfiles_about_AboutId",
                table: "userProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_user_UserID",
                table: "userProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_userProfiles_UserProfileId",
                table: "userProjects");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropIndex(
                name: "IX_userProjects_UserProfileId",
                table: "userProjects");

            migrationBuilder.DropIndex(
                name: "IX_userProfiles_AboutId",
                table: "userProfiles");

            migrationBuilder.DropIndex(
                name: "IX_userExperiences_UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropIndex(
                name: "IX_skills_UserId",
                table: "skills");

            migrationBuilder.DropIndex(
                name: "IX_educations_UserProfileId",
                table: "educations");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "userProjects");

            migrationBuilder.DropColumn(
                name: "AboutId",
                table: "userProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "userExperiences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "skills");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "educations");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "userProjects",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_userProjects_UserID",
                table: "userProjects",
                newName: "IX_userProjects_ProfileID");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "userExperiences",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_userExperiences_userID",
                table: "userExperiences",
                newName: "IX_userExperiences_ProfileID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "skills",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_skills_UserID",
                table: "skills",
                newName: "IX_skills_ProfileID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "educations",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_educations_UserID",
                table: "educations",
                newName: "IX_educations_ProfileID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "about",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_about_UserID",
                table: "about",
                newName: "IX_about_ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_about_userProfiles_ProfileID",
                table: "about",
                column: "ProfileID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_educations_userProfiles_ProfileID",
                table: "educations",
                column: "ProfileID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_skills_userProfiles_ProfileID",
                table: "skills",
                column: "ProfileID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userExperiences_userProfiles_ProfileID",
                table: "userExperiences",
                column: "ProfileID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_userProfiles_ProfileID",
                table: "userProjects",
                column: "ProfileID",
                principalTable: "userProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_about_userProfiles_ProfileID",
                table: "about");

            migrationBuilder.DropForeignKey(
                name: "FK_educations_userProfiles_ProfileID",
                table: "educations");

            migrationBuilder.DropForeignKey(
                name: "FK_skills_userProfiles_ProfileID",
                table: "skills");

            migrationBuilder.DropForeignKey(
                name: "FK_userExperiences_userProfiles_ProfileID",
                table: "userExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_userProjects_userProfiles_ProfileID",
                table: "userProjects");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "userProjects",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_userProjects_ProfileID",
                table: "userProjects",
                newName: "IX_userProjects_UserID");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "userExperiences",
                newName: "userID");

            migrationBuilder.RenameIndex(
                name: "IX_userExperiences_ProfileID",
                table: "userExperiences",
                newName: "IX_userExperiences_userID");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "skills",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_skills_ProfileID",
                table: "skills",
                newName: "IX_skills_UserID");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "educations",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_educations_ProfileID",
                table: "educations",
                newName: "IX_educations_UserID");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "about",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_about_ProfileID",
                table: "about",
                newName: "IX_about_UserID");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "userProjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AboutId",
                table: "userProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "userExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "educations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userProjects_UserProfileId",
                table: "userProjects",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_userProfiles_AboutId",
                table: "userProfiles",
                column: "AboutId");

            migrationBuilder.CreateIndex(
                name: "IX_userExperiences_UserProfileId",
                table: "userExperiences",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_UserId",
                table: "skills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_educations_UserProfileId",
                table: "educations",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_about_user_UserID",
                table: "about",
                column: "UserID",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_educations_user_UserID",
                table: "educations",
                column: "UserID",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_userExperiences_user_userID",
                table: "userExperiences",
                column: "userID",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userExperiences_userProfiles_UserProfileId",
                table: "userExperiences",
                column: "UserProfileId",
                principalTable: "userProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userProfiles_about_AboutId",
                table: "userProfiles",
                column: "AboutId",
                principalTable: "about",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_user_UserID",
                table: "userProjects",
                column: "UserID",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userProjects_userProfiles_UserProfileId",
                table: "userProjects",
                column: "UserProfileId",
                principalTable: "userProfiles",
                principalColumn: "Id");
        }
    }
}
