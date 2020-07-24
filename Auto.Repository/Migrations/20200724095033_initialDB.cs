using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Auto.Repository.Migrations
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 2500, nullable: true),
                    CityCode = table.Column<string>(maxLength: 50, nullable: false),
                    ParentCode = table.Column<string>(maxLength: 50, nullable: false),
                    LogogramName = table.Column<string>(maxLength: 50, nullable: false),
                    MergerName = table.Column<string>(maxLength: 100, nullable: false),
                    MergerLogogramName = table.Column<string>(maxLength: 50, nullable: false),
                    AreaLevel = table.Column<string>(maxLength: 50, nullable: false),
                    LevelType = table.Column<int>(nullable: false),
                    TelephoneCode = table.Column<string>(maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: true),
                    CompleteSpelling = table.Column<string>(maxLength: 50, nullable: false),
                    LogogramSpelling = table.Column<string>(maxLength: 50, nullable: false),
                    Center = table.Column<string>(maxLength: 50, nullable: true),
                    NameFirstChar = table.Column<string>(maxLength: 50, nullable: false),
                    Longitude = table.Column<string>(maxLength: 50, nullable: true),
                    Latitude = table.Column<string>(maxLength: 50, nullable: true),
                    Version = table.Column<string>(maxLength: 50, nullable: false),
                    CreatorUserId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserID = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 2500, nullable: true),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    BusinessLicense = table.Column<string>(nullable: false),
                    LegalPerson = table.Column<string>(maxLength: 50, nullable: false),
                    LegalPersonIDCardNo = table.Column<string>(maxLength: 50, nullable: false),
                    LegalPersonPhone = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    SupportEMail = table.Column<string>(nullable: true),
                    AreaID = table.Column<int>(nullable: false),
                    CorporationAddress = table.Column<string>(maxLength: 200, nullable: false),
                    CreatorUserId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserID = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 2500, nullable: true),
                    CorporationID = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserID = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Department_Corporation_CorporationID",
                        column: x => x.CorporationID,
                        principalTable: "Corporation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 2500, nullable: true),
                    ParentID = table.Column<int>(nullable: false),
                    DeptmentID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserID = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Position_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(maxLength: 2500, nullable: true),
                    EmployeeGender = table.Column<int>(nullable: false),
                    PositionID = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserID = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_Position_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Position",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_CorporationID",
                table: "Department",
                column: "CorporationID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PositionID",
                table: "Employee",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Position_DepartmentID",
                table: "Position",
                column: "DepartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Corporation");
        }
    }
}
