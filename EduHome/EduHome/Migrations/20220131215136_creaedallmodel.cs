using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome.Migrations
{
    public partial class creaedallmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(maxLength: 90, nullable: true),
                    Title = table.Column<string>(maxLength: 90, nullable: false),
                    Description = table.Column<string>(maxLength: 600, nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(maxLength: 90, nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: false),
                    Venue = table.Column<string>(maxLength: 100, nullable: false),
                    StarDate = table.Column<DateTime>(nullable: false),
                    FinisDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facultes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoticeVioards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeVioards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(maxLength: 100, nullable: true),
                    SearchIcon = table.Column<string>(maxLength: 100, nullable: false),
                    InfoTitle = table.Column<string>(maxLength: 100, nullable: false),
                    InfoDescriptionTop = table.Column<string>(maxLength: 300, nullable: false),
                    InfoDescriptionBottom = table.Column<string>(maxLength: 300, nullable: false),
                    InfoImg = table.Column<string>(maxLength: 100, nullable: true),
                    InfoLink = table.Column<string>(maxLength: 200, nullable: false),
                    FooterLogo = table.Column<string>(maxLength: 100, nullable: true),
                    FooterDedcription = table.Column<string>(maxLength: 250, nullable: false),
                    PhoneNumber1 = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber2 = table.Column<string>(maxLength: 50, nullable: false),
                    Mail = table.Column<string>(maxLength: 100, nullable: false),
                    WebAddress = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    VideoUrl = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderImg = table.Column<string>(maxLength: 80, nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Descrtiption = table.Column<string>(maxLength: 250, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SociaslMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialIcon = table.Column<string>(maxLength: 100, nullable: false),
                    SocialUrl = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SociaslMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    SurnameName = table.Column<string>(maxLength: 25, nullable: false),
                    Img = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    Img = table.Column<string>(maxLength: 70, nullable: false),
                    AboutMe = table.Column<string>(maxLength: 400, nullable: false),
                    Experience = table.Column<string>(maxLength: 100, nullable: false),
                    Degree = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNUmber = table.Column<string>(maxLength: 50, nullable: false),
                    Mail = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(maxLength: 90, nullable: true),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: false),
                    AboutCourse = table.Column<string>(maxLength: 400, nullable: false),
                    HowToApply = table.Column<string>(maxLength: 400, nullable: false),
                    Serftication = table.Column<string>(maxLength: 400, nullable: false),
                    Starts = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(maxLength: 30, nullable: false),
                    ClassDuration = table.Column<string>(maxLength: 30, nullable: false),
                    SkillLevel = table.Column<string>(maxLength: 30, nullable: false),
                    Language = table.Column<string>(maxLength: 30, nullable: false),
                    Student = table.Column<int>(nullable: false),
                    Assesment = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Img = table.Column<string>(maxLength: 70, nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SociamMediaSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialIcon = table.Column<string>(maxLength: 100, nullable: true),
                    SocialUrl = table.Column<string>(maxLength: 100, nullable: true),
                    SettingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SociamMediaSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SociamMediaSettings_Setting_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Setting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSpeakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    SpekaerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpeakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpeakers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSpeakers_Speakers_SpekaerId",
                        column: x => x.SpekaerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeakreId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerPositions_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacultyTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(nullable: false),
                    FaculteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Facultes_FaculteId",
                        column: x => x.FaculteId,
                        principalTable: "Facultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HobbieTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbieId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbieTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HobbieTeachers_Hobbies_HobbieId",
                        column: x => x.HobbieId,
                        principalTable: "Hobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbieTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionTeachers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Point = table.Column<int>(maxLength: 30, nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "socialTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(maxLength: 80, nullable: true),
                    Url = table.Column<string>(maxLength: 200, nullable: true),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_socialTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_socialTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(maxLength: 300, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    BlogId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coursetags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coursetags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coursetags_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coursetags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coursetags_CourseId",
                table: "Coursetags",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Coursetags_TagId",
                table: "Coursetags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeakers_EventId",
                table: "EventSpeakers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeakers_SpekaerId",
                table: "EventSpeakers",
                column: "SpekaerId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_FaculteId",
                table: "FacultyTeachers",
                column: "FaculteId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_TeacherId",
                table: "FacultyTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbieTeachers_HobbieId",
                table: "HobbieTeachers",
                column: "HobbieId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbieTeachers_TeacherId",
                table: "HobbieTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionTeachers_PositionId",
                table: "PositionTeachers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionTeachers_TeacherId",
                table: "PositionTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TeacherId",
                table: "Skills",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_socialTeachers_TeacherId",
                table: "socialTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SociamMediaSettings_SettingId",
                table: "SociamMediaSettings",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerPositions_PositionId",
                table: "SpeakerPositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerPositions_SpeakerId",
                table: "SpeakerPositions",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PositionId",
                table: "Students",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Coursetags");

            migrationBuilder.DropTable(
                name: "EventSpeakers");

            migrationBuilder.DropTable(
                name: "FacultyTeachers");

            migrationBuilder.DropTable(
                name: "HobbieTeachers");

            migrationBuilder.DropTable(
                name: "NoticeVioards");

            migrationBuilder.DropTable(
                name: "PositionTeachers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "socialTeachers");

            migrationBuilder.DropTable(
                name: "SociamMediaSettings");

            migrationBuilder.DropTable(
                name: "SociaslMedias");

            migrationBuilder.DropTable(
                name: "SpeakerPositions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Facultes");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
