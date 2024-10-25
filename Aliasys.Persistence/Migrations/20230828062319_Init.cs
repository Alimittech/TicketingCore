using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aliasys.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "org");

            migrationBuilder.EnsureSchema(
                name: "rgn");

            migrationBuilder.EnsureSchema(
                name: "srv");

            migrationBuilder.EnsureSchema(
                name: "stm");

            migrationBuilder.EnsureSchema(
                name: "usr");

            migrationBuilder.CreateTable(
                name: "OperationUnit",
                schema: "org",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "org",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "rgn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CapitalName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ContinentName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestType",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BriefName = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRootCause",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootCauseName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRootCause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSubCause",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCauseName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSubCause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "System",
                schema: "stm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentSystemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    ExtentionNumber = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    PersonCode = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoll",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RollName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoll", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "org",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    DomainName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ParentId_FK = table.Column<int>(type: "int", nullable: false),
                    RegionId_FK = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Organization_ParentId_FK",
                        column: x => x.ParentId_FK,
                        principalSchema: "org",
                        principalTable: "Organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Organization_Region_RegionId_FK",
                        column: x => x.RegionId_FK,
                        principalSchema: "rgn",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePhase",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRequestTypeId_FK = table.Column<int>(type: "int", nullable: false),
                    PhaseName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePhase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePhase_ServiceRequestType_ServiceRequestTypeId_FK",
                        column: x => x.ServiceRequestTypeId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequestType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceState",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRequestTypeId_FK = table.Column<int>(type: "int", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceState_ServiceRequestType_ServiceRequestTypeId_FK",
                        column: x => x.ServiceRequestTypeId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequestType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserGroupId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_UserGroup_UserGroupId_FK",
                        column: x => x.UserGroupId_FK,
                        principalSchema: "usr",
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInGroup",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupId_FK = table.Column<int>(type: "int", nullable: false),
                    UserId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInGroup_UserGroup_UserGroupId_FK",
                        column: x => x.UserGroupId_FK,
                        principalSchema: "usr",
                        principalTable: "UserGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserInGroup_User_UserId_FK",
                        column: x => x.UserId_FK,
                        principalSchema: "usr",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInRoll",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId_FK = table.Column<int>(type: "int", nullable: false),
                    RollId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRoll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInRoll_UserRoll_RollId_FK",
                        column: x => x.RollId_FK,
                        principalSchema: "usr",
                        principalTable: "UserRoll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInRoll_User_UserId_FK",
                        column: x => x.UserId_FK,
                        principalSchema: "usr",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationUnitDependency",
                schema: "org",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId_FK = table.Column<int>(type: "int", nullable: false),
                    OperationUnitId_FK = table.Column<int>(type: "int", nullable: false),
                    ParentOperationUnitId_FK = table.Column<int>(type: "int", nullable: false),
                    ManagerId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationUnitDependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationUnitDependency_OperationUnit_OperationUnitId_FK",
                        column: x => x.OperationUnitId_FK,
                        principalSchema: "org",
                        principalTable: "OperationUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationUnitDependency_OperationUnit_ParentOperationUnitId_FK",
                        column: x => x.ParentOperationUnitId_FK,
                        principalSchema: "org",
                        principalTable: "OperationUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationUnitDependency_Organization_OrganizationId_FK",
                        column: x => x.OrganizationId_FK,
                        principalSchema: "org",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationUnitDependency_User_ManagerId_FK",
                        column: x => x.ManagerId_FK,
                        principalSchema: "usr",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInOrgOpunitPos",
                schema: "usr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId_FK = table.Column<int>(type: "int", nullable: false),
                    OrganizationId_FK = table.Column<int>(type: "int", nullable: false),
                    OperationUnitId_FK = table.Column<int>(type: "int", nullable: false),
                    PositionId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInOrgOpunitPos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInOrgOpunitPos_OperationUnit_OperationUnitId_FK",
                        column: x => x.OperationUnitId_FK,
                        principalSchema: "org",
                        principalTable: "OperationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInOrgOpunitPos_Organization_OrganizationId_FK",
                        column: x => x.OrganizationId_FK,
                        principalSchema: "org",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInOrgOpunitPos_Position_PositionId_FK",
                        column: x => x.PositionId_FK,
                        principalSchema: "org",
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInOrgOpunitPos_User_UserId_FK",
                        column: x => x.UserId_FK,
                        principalSchema: "usr",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCauseCategory",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCategoryId_FK = table.Column<int>(type: "int", nullable: false),
                    ServiceRootCauseId_FK = table.Column<int>(type: "int", nullable: false),
                    ServiceSubCauseId_FK = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCauseCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCauseCategory_ServiceCategory_ServiceCategoryId_FK",
                        column: x => x.ServiceCategoryId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCauseCategory_ServiceRootCause_ServiceRootCauseId_FK",
                        column: x => x.ServiceRootCauseId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRootCause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCauseCategory_ServiceSubCause_ServiceSubCauseId_FK",
                        column: x => x.ServiceSubCauseId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceSubCause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestNumber = table.Column<string>(type: "varchar(30)", nullable: false),
                    OwnerUserId_FK = table.Column<int>(type: "int", nullable: false),
                    ServiceCategoryId_FK = table.Column<int>(type: "int", nullable: false),
                    ServiceRequestTypeId_FK = table.Column<int>(type: "int", nullable: false),
                    ServicePriority = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OccurDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceAffected = table.Column<bool>(type: "bit", nullable: false),
                    ImpactOn = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_ServiceCategory_ServiceCategoryId_FK",
                        column: x => x.ServiceCategoryId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequest_ServiceRequestType_ServiceRequestTypeId_FK",
                        column: x => x.ServiceRequestTypeId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequestType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequest_User_OwnerUserId_FK",
                        column: x => x.OwnerUserId_FK,
                        principalSchema: "usr",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestChild",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentServiceRequestId_FK = table.Column<long>(type: "bigint", nullable: false),
                    ChildServiceRequestId_FK = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestChild_ServiceRequest_ChildServiceRequestId_FK",
                        column: x => x.ChildServiceRequestId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequestChild_ServiceRequest_ParentServiceRequestId_FK",
                        column: x => x.ParentServiceRequestId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestLifeCycle",
                schema: "srv",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRequestId_FK = table.Column<long>(type: "bigint", nullable: false),
                    ServiceStateId_FK = table.Column<int>(type: "int", nullable: false),
                    ServicePhaseId_FK = table.Column<int>(type: "int", nullable: false),
                    RootCauseId = table.Column<int>(type: "int", nullable: true),
                    SubCauseId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessAction = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProcessUserId = table.Column<int>(type: "int", nullable: false),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    AssignedGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestLifeCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestLifeCycle_ServicePhase_ServicePhaseId_FK",
                        column: x => x.ServicePhaseId_FK,
                        principalSchema: "srv",
                        principalTable: "ServicePhase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequestLifeCycle_ServiceRequest_ServiceRequestId_FK",
                        column: x => x.ServiceRequestId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequestLifeCycle_ServiceState_ServiceStateId_FK",
                        column: x => x.ServiceStateId_FK,
                        principalSchema: "srv",
                        principalTable: "ServiceState",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "org",
                table: "OperationUnit",
                columns: new[] { "Id", "Code", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "Name", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, 1020, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8386), null, false, "CEO", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8398) },
                    { 2, 1020, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8401), null, false, "Deputy CEO", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8402) },
                    { 3, 1030, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8404), null, false, "Financial", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8405) },
                    { 4, 1040, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8407), null, false, "Human Resources", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8407) },
                    { 5, 1060, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8410), null, false, "Quality and Systems", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8411) },
                    { 6, 1070, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8413), null, false, "Internal Commercial", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8414) },
                    { 7, 1080, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8415), null, false, "Foreign Commercial", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8416) },
                    { 8, 1110, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8418), null, false, "Sales", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8419) },
                    { 9, 1130, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8420), null, false, "Marketing", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8421) },
                    { 10, 1140, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8423), null, false, "After Sales Service", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8424) },
                    { 11, 1160, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8425), null, false, "Technical", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8426) },
                    { 12, 1170, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8430), null, false, "Warehouse", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8431) },
                    { 13, 1180, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8432), null, false, "Sales Engineering", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8433) },
                    { 14, 1190, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8434), null, false, "Logestics", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8435) },
                    { 15, 1200, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8437), null, false, "Software", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8438) },
                    { 16, 1090, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8439), null, false, "Legal", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(8440) }
                });

            migrationBuilder.InsertData(
                schema: "org",
                table: "Position",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "Name", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(850), null, false, "Chairman", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(858) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(861), null, false, "CEO", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(862) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(864), null, false, "Deputy CEO", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(865) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(866), null, false, "Consultant", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(867) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(869), null, false, "Senior manager", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(870) },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(871), null, false, "Manager", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(872) },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(874), null, false, "Team Leader", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(874) },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(876), null, false, "Supervisor", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(877) },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(879), null, false, "Senior Expert", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(880) },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(881), null, false, "Junior Expert", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(882) },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(884), null, false, "Jobholder", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(885) },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(886), null, false, "Secretary", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(887) },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(888), null, false, "Collector", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(890) },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(891), null, false, "Watchman", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(892) },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(893), null, false, "Worker", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(894) },
                    { 16, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(896), null, false, "Junior Developer", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(897) },
                    { 17, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(898), null, false, "Senior Developer", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(899) },
                    { 18, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(901), null, false, "Charge", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(902) }
                });

            migrationBuilder.InsertData(
                schema: "rgn",
                table: "Region",
                columns: new[] { "Id", "CapitalName", "ContinentName", "CountryName", "Flag" },
                values: new object[,]
                {
                    { 1, "Tehran", "Asia", "Iran", null },
                    { 2, "Dubai", "Asia", "United Arab Emirates", null }
                });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceRequestType",
                columns: new[] { "Id", "BriefName", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "Name", "RequestType", "UpdatedDateTime" },
                values: new object[] { 1, "TT", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(2686), null, false, "Trouble Ticket", "SW_Truoble_Ticket", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(2697) });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceRootCause",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "RootCauseName", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(429), null, false, "Hardware Fault", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(443) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(446), null, false, "Software Fault", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(447) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(449), null, false, "Software Requirement", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(450) }
                });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceSubCause",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "SubCauseName", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1502), null, false, "SSL Issue", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1510) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1513), null, false, "License", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1514) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1516), null, false, "Database Performance", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1517) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1518), null, false, "User Mistake", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1519) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1521), null, false, "Wrong Configuration", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1522) },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1524), null, false, "Report Development", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1525) },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1526), null, false, "Process Development", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1527) },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1528), null, false, "User Access", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1529) },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1531), null, false, "Form Adjustment", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1531) },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1533), null, false, "Wrong Master Data", new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(1534) }
                });

            migrationBuilder.InsertData(
                schema: "stm",
                table: "System",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "Description", "IsDeleted", "Name", "ParentSystemId", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1804), null, null, false, "Systems", 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1811) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1814), null, null, false, "Portfolio Portal", 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1815) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1817), null, null, false, "System Settings", 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1818) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1820), null, null, false, "Service System", 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1821) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1822), null, null, false, "Organization Management", 3, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1824) },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1825), null, null, false, "System Management", 3, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1826) },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1827), null, null, false, "Service Management", 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1828) },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1830), null, null, false, "Create Service", 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1831) },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1833), null, null, false, "Get Service", 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1834) },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1835), null, null, false, "Organization", 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1836) },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1837), null, null, false, "Operation Unit", 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1839) },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1841), null, null, false, "Position", 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1842) },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1843), null, null, false, "System", 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1844) },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1845), null, null, false, "Roll Management", 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1846) },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1848), null, null, false, "User Account", 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(1849) }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "User",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "DisplayName", "Email", "ExtentionNumber", "ImageName", "IsActive", "IsDeleted", "PersonCode", "PhoneNumber", "UpdatedDateTime", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3478), null, "Mohajer, Ehsan", "e.mohajer@aliasys.co", "500", "028", true, false, 28, "09121863720", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3486), "e.mohajer" },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3491), null, "Moslemi, Amir", "a.moslemi@aliasys.co", "700", "003", true, false, 3, "09126220378", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3493), "a.moslemi" },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3496), null, "Moradi, Edris", "ed.moradi@aliasys.co", "750", null, true, false, 24, "09126353086", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3497), "ed.moradi" },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3499), null, "Basiri, Mohammad", "m.basiri@aliasys.co", "950", "072", true, false, 72, "09122584866", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3500), "m.basiri" },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3502), null, "Dadras, Majid", "m.dadras@aliasys.co", "200", "094", true, false, 94, "09125469645", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3503), "m.dadras" },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3505), null, "Aghababaei, Ali", "a.aghababaei@aliasys.co", "300", "015", true, false, 15, "09121724565", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3506), "a.aghababaei" },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3508), null, "Afsaneh Khojasteh", "a.khojasteh@aliasys.co", "800", "019", true, false, 19, "09123907133", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3509), "a.khojasteh" },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3512), null, "Eskandari, Pegah", "p.eskandari@aliasys.co", "610", "016", true, false, 16, "09125774563", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3513), "p.eskandari" },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3517), null, "Vafaei Nia, Mojgan", "m.vafaeinia@aliasys.co", "105", "117", true, false, 117, "09122993517", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3518), "m.vafaeinia" },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3539), null, "Hoseini, Hamidreza ", "h.hoseini@aliasys.co", "400", "039", true, false, 39, "09352566788", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3541), "h.hoseini" },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3544), null, "Vesali, Mostafa", "m.vesali@aliasys.co", "660", null, true, false, 118, "09127708514", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3545), "m.vesali" },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3547), null, "Akhani, Malek", "m.akhani@aliasys.co", "450", "095", true, false, 95, "09127617152", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3548), "m.akhani" },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3550), null, "Jalayer, Hasti", "H.jalayer@aliasys.co", "605", "022", true, false, 22, "09123279790", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3551), "H.jalayer" },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3554), null, "Yusefi, Rouhollah", "r.yusefi@aliasys.co", "550", "120", true, false, 120, "09126469135", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3555), "r.yusefi" },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3557), null, "Sepandasa, Taghi", "b.sepandasa@aliasys.co", "310", "014", true, false, 14, "09121435662", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3558), "b.sepandasa" },
                    { 16, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3560), null, "Azaresh, Javad", "j.azaresh@aliasys.co", "480", "044", true, false, 44, "09396080127", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3561), "j.azaresh" },
                    { 17, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3563), null, "Kouchaki, Mehdi", "m.kouchaki@aliasys.co", "965", "106", true, false, 106, "09123185463", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3564), "m.kouchaki" },
                    { 18, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3567), null, "Aghakasiri, Hamidreza", "h.aghakasiri@aliasys.co", "960", "115", true, false, 115, "09126108395", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3567), "h.aghakasiri" },
                    { 19, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3570), null, "Jafari, Roghayeh", "r.jafari@aliasys.co", "575", "109", true, false, 109, "09101744763", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3570), "r.jafari" },
                    { 20, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3573), null, "Naseri, Erfan", "e.naseri@aliasys.co", "957", "119", true, false, 119, "09025367966", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3574), "e.naseri" },
                    { 21, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3577), null, "Jafari, Mohammad", "m.jafari@aliasys.co", "823", "032", true, false, 32, "09127186105", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3578), "m.jafari" },
                    { 22, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3580), null, "Aziznezhad, Ronak", "r.aziznezhad@aliasys.co", "560", "002", true, false, 2, "09124691582", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3581), "r.aziznezhad" },
                    { 23, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3584), null, "Azaresh, Reza", "r.azaresh@aliasys.co", "430", "010", true, false, 10, "09126704771", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3584), "r.azaresh" },
                    { 24, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3587), null, "Khanmohammadi, Fatemeh", "f.khanmohammadi@aliasys.co", "563", "011", true, false, 11, "09107877517", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3588), "f.khanmohammadi" },
                    { 25, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3590), null, "Asadollahi, Ardavan", "a.asadollahi@aliasys.co", "000", null, true, false, 13, "09126955076", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3591), "a.asadollahi" },
                    { 26, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3593), null, "Ehteshamzadeh, Saman", "s.ehteshamzadeh@aliasys.co", "217", "018", true, false, 18, "09372423534", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3594), "s.ehteshamzadeh" },
                    { 27, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3596), null, "Shamloo, Alireza", "a.shamloo@aliasys.co", "821", "021", true, false, 21, "09128921450", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3597), "a.shamloo" },
                    { 28, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3599), null, "Jafari, Yousef", "y.jafari@aliasys.co@aliasys.co", "967", "025", true, false, 25, "09120762408", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3600), "y.jafari" },
                    { 29, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3602), null, "Safizadeh, Mohammad", "m.safizadeh@aliasys.co", "831", null, false, false, 27, "09120755724", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3603), "m.safizadeh" },
                    { 30, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3605), null, "Ziraki, Shahab", "sh.ziraki@aliasys.co", "325", "030", true, false, 30, "09304975026", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3606), "sh.ziraki" },
                    { 31, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3608), null, "Ketabi, Faraz", "f.ketabi@aliasys.co", "333", "031", true, false, 31, "09126988850", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3609), "f.ketabi" },
                    { 32, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3612), null, "Adaei, Elnaz", "e.adaei@aliasys.co", "825", "034", true, false, 34, "09129358259", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3613), "e.adaeii" },
                    { 33, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3616), null, "Hadi, Sajad", "s.hadi@aliasys.co", "833", "035", true, false, 35, "09126365952", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3617), "s.hadi" },
                    { 34, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3619), null, "Hemmati, Behroz", "b.hemmati@aliasys.co", "317", "037", true, false, 37, "09120581940", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3620), "b.hemmati" },
                    { 35, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3623), null, "Shokri, Farid", "f.shokri@aliasys.co", "321", "043", true, false, 43, "09127767506", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3624), "f.shokri" },
                    { 36, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3626), null, "Tabasi, Pegah", "p.tabasi@aliasys.co", "610", "048", true, false, 48, "09106708373", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3627), "p.tabasi" },
                    { 37, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3629), null, "Tavakoli, Fatemeh", "f.tavakoli@aliasys.co", "815", "057", true, false, 57, "09380902891", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3630), "f.tavakoli" },
                    { 38, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3632), null, "Sharafifard, Faezeh", "f.sharafifard@aliasys.co", "671", "063", true, false, 63, "09129405474", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3633), "f.sharafifard" },
                    { 39, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3635), null, "Boujarnezhad, Tina", "t.boujarnezhad@aliasys.co", "573", "064", true, false, 64, "09354292249", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3636), "t.boujarnezhad" },
                    { 40, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3638), null, "Rezaei, Anahita", "a.rezaei@aliasys.co", "621", "065", true, false, 65, "09111160272", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3639), "a.rezaei" },
                    { 41, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3642), null, "Afshar, Mehrnaz", "m.afshar@aliasys.co", "819", "068", true, false, 68, "09125038412", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3643), "m.afshar" },
                    { 42, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3645), null, "Beygi, Narges", "n.beygi@aliasys.co", "115", "073", true, false, 73, "09124972631", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3646), "n.beygi" },
                    { 43, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3648), null, "Norouzi, Negin", "n.norouzi@aliasys.co", "569", "075", true, false, 75, "09372571925", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3649), "n.norouzi" },
                    { 44, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3652), null, "Kafi, Farzad", "f.kafi@aliasys.co", "329", "086", true, false, 86, "09384001555", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3653), "f.kafi" },
                    { 45, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3655), null, "Mosaferi, Hosien", "h.mosaferi@aliasys.co", "136", "092", true, false, 92, "09126095320", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3656), "h.mosaferi" },
                    { 46, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3659), null, "Mandegar, Mahshid", "m.mandegar@aliasys.co", "580", "093", true, false, 93, "09123725132", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3659), "m.mandegar" },
                    { 47, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3662), null, "Mahmoudi, Saman", "s.mahmoudi@aliasys.co", "327", "096", true, false, 96, "09357436629", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3663), "s.mahmoudi" },
                    { 48, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3665), null, "Marookian, Preni", "p.marookian@aliasys.co", "625", "099", true, false, 99, "09125175300", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3666), "p.marookian" },
                    { 49, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3668), null, "Golmohamadi, Ali", "a.golmohamadi@aliasys.co", "435", "107", true, false, 107, "09909020595", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3669), "a.golmohamadi" },
                    { 50, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3671), null, "Abedini, Samareh", "s.abedini@aliasys.co", "567", "111", true, false, 111, "09122680578", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3672), "s.abedini" },
                    { 51, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3674), null, "Zare, Alireza", "a.zare@aliasys.co", "571", "112", true, false, 112, "09192018804", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3675), "a.zare" },
                    { 52, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3677), null, "Nemati, Amir", "a.nemati@aliasys.co", "326", "113", true, false, 113, "09197219526", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3678), "a.nemati" },
                    { 53, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3681), null, "Saraei, Mahsa", "m.saraei@aliasys.co", "619", "114", true, false, 114, "09352522040", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3681), "m.saraei" },
                    { 54, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3683), null, "Malih, Saeedeh", "s.malih@aliasys.co", "415", null, true, false, 121, "09127565012", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3684), "s.malih" },
                    { 55, new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3686), null, "Majloubi, Maral", "m.majloubi@aliasys.co", "119", null, true, false, 122, "09123571140", new DateTime(2023, 8, 28, 9, 53, 18, 940, DateTimeKind.Local).AddTicks(3687), "m.majloubi" }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "UserGroup",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "GroupName", "IsDeleted", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2729), null, "ERP Group", false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2740) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2743), null, "CRM Group", false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2744) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2745), null, "Technical Group", false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(2746) }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "UserRoll",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "Description", "IsDeleted", "RollName", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6039), null, "Full Access", false, "Admin", new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6048) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6052), null, "Manager Access", false, "Manager", new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6053) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6055), null, "Service Provider Access", false, "Service_Provider", new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6056) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6059), null, "User Access", false, "User", new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(6060) }
                });

            migrationBuilder.InsertData(
                schema: "org",
                table: "Organization",
                columns: new[] { "Id", "Address", "CreatedDateTime", "DeletedDateTime", "DomainName", "IsDeleted", "Name", "ParentId_FK", "Phone", "RegionId_FK", "UpdatedDateTime" },
                values: new object[] { 1, "No. 32, Mohajer Aly., North Sohrevardi St., Seyed Khandan Brg., Tehran, Iran", new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(7232), null, "Aliasys.co", false, "Aliasys Company", 1, "00982182455000", 1, new DateTime(2023, 8, 28, 9, 53, 18, 938, DateTimeKind.Local).AddTicks(7277) });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceCategory",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "Name", "UpdatedDateTime", "UserGroupId_FK" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(1260), null, false, "ERP", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(1272), 1 },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(1275), null, false, "CRM", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(1276), 2 }
                });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServicePhase",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "PhaseName", "ServiceRequestTypeId_FK", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9797), null, false, "Creation", 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9809) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9832), null, false, "Handle", 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9833) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9835), null, false, "Process", 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9837) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9838), null, false, "Reject", 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9839) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9841), null, false, "Confirm", 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(9842) }
                });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceState",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "ServiceRequestTypeId_FK", "StateName", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6284), null, false, 1, "Draft", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6296) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6299), null, false, 1, "Running", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6301) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6303), null, false, 1, "Cancelled", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6304) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6307), null, false, 1, "Closed", new DateTime(2023, 8, 28, 9, 53, 18, 943, DateTimeKind.Local).AddTicks(6308) }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "UserInGroup",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "UpdatedDateTime", "UserGroupId_FK", "UserId_FK" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7920), null, false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7933), 1, 4 },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7936), null, false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7937), 1, 17 },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7939), null, false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7940), 1, 19 },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7942), null, false, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(7943), 2, 18 }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "UserInRoll",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "RollId_FK", "UpdatedDateTime", "UserId_FK" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1379), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1395), 1 },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1398), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1399), 2 },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1401), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1402), 3 },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1404), null, false, 1, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1405), 4 },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1406), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1407), 5 },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1409), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1410), 6 },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1412), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1413), 7 },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1415), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1415), 8 },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1417), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1418), 9 },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1419), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1420), 10 },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1422), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1422), 11 },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1424), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1425), 12 },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1427), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1428), 13 },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1431), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1432), 14 },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1433), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1434), 15 },
                    { 16, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1436), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1437), 16 },
                    { 17, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1439), null, false, 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1440), 17 },
                    { 18, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1442), null, false, 1, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1443), 18 },
                    { 19, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1445), null, false, 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1446), 19 },
                    { 20, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1447), null, false, 3, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1448), 20 },
                    { 21, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1450), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1451), 21 },
                    { 22, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1452), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1453), 22 },
                    { 23, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1455), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1456), 23 },
                    { 24, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1458), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1459), 24 },
                    { 25, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1460), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1461), 25 },
                    { 26, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1463), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1464), 26 },
                    { 27, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1466), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1466), 27 },
                    { 28, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1468), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1469), 28 },
                    { 29, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1470), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1471), 29 },
                    { 30, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1472), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1474), 30 },
                    { 31, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1475), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1476), 31 },
                    { 32, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1478), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1479), 32 },
                    { 33, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1480), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1481), 33 },
                    { 34, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1482), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1483), 34 },
                    { 35, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1485), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1486), 35 },
                    { 36, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1487), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1488), 36 },
                    { 37, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1490), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1491), 37 },
                    { 38, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1492), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1493), 38 },
                    { 39, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1495), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1496), 39 },
                    { 40, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1518), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1519), 40 },
                    { 41, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1521), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1522), 41 },
                    { 42, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1523), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1524), 42 },
                    { 43, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1526), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1527), 43 },
                    { 44, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1528), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1529), 44 },
                    { 45, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1530), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1531), 45 },
                    { 46, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1533), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1533), 46 },
                    { 47, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1535), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1536), 47 },
                    { 48, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1537), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1538), 48 },
                    { 49, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1539), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1540), 49 },
                    { 50, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1542), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1543), 50 },
                    { 51, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1544), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1545), 51 },
                    { 52, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1547), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1548), 52 },
                    { 53, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1549), null, false, 4, new DateTime(2023, 8, 28, 9, 53, 18, 942, DateTimeKind.Local).AddTicks(1550), 53 }
                });

            migrationBuilder.InsertData(
                schema: "org",
                table: "OperationUnitDependency",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "ManagerId_FK", "OperationUnitId_FK", "OrganizationId_FK", "ParentOperationUnitId_FK", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9800), null, false, 1, 1, 1, 1, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9819) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9824), null, false, 2, 2, 1, 1, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9825) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9828), null, false, 14, 3, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9829) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9831), null, false, 9, 4, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9832) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9834), null, false, 10, 5, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9835) },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9837), null, false, 21, 6, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9839) },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9841), null, false, 8, 7, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9842) },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9843), null, false, 7, 8, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9844) },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9846), null, false, 11, 9, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9847) },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9848), null, false, 16, 10, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9849) },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9851), null, false, 6, 11, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9852) },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9853), null, false, 12, 12, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9854) },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9856), null, false, 5, 13, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9857) },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9858), null, false, 13, 14, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9859) },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9861), null, false, 4, 15, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 939, DateTimeKind.Local).AddTicks(9862) }
                });

            migrationBuilder.InsertData(
                schema: "srv",
                table: "ServiceCauseCategory",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "ServiceCategoryId_FK", "ServiceRootCauseId_FK", "ServiceSubCauseId_FK", "UpdatedDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8887), null, false, 1, 1, 1, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8900) },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8903), null, false, 1, 2, 2, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8904) },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8906), null, false, 1, 2, 3, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8907) },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8909), null, false, 1, 2, 4, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8910) },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8912), null, false, 1, 2, 5, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8913) },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8916), null, false, 1, 3, 6, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8916) },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8918), null, false, 1, 3, 7, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8919) },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8921), null, false, 1, 3, 8, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8921) },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8923), null, false, 1, 3, 9, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8924) },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8926), null, false, 1, 3, 10, new DateTime(2023, 8, 28, 9, 53, 18, 947, DateTimeKind.Local).AddTicks(8926) }
                });

            migrationBuilder.InsertData(
                schema: "usr",
                table: "UserInOrgOpunitPos",
                columns: new[] { "Id", "CreatedDateTime", "DeletedDateTime", "IsDeleted", "OperationUnitId_FK", "OrganizationId_FK", "PositionId_FK", "UpdatedDateTime", "UserId_FK" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4799), null, false, 1, 1, 2, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4816), 1 },
                    { 2, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4821), null, false, 2, 1, 3, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4822), 2 },
                    { 3, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4824), null, false, 3, 1, 4, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4825), 3 },
                    { 4, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4827), null, false, 15, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4828), 4 },
                    { 5, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4830), null, false, 13, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4831), 5 },
                    { 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4833), null, false, 11, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4834), 6 },
                    { 7, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4835), null, false, 8, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4836), 7 },
                    { 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4838), null, false, 7, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4839), 8 },
                    { 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4841), null, false, 4, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4842), 9 },
                    { 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4844), null, false, 5, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4845), 10 },
                    { 11, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4846), null, false, 9, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4847), 11 },
                    { 12, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4849), null, false, 12, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4850), 12 },
                    { 13, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4852), null, false, 14, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4853), 13 },
                    { 14, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4855), null, false, 3, 1, 6, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4856), 14 },
                    { 15, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4858), null, false, 11, 1, 7, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4859), 15 },
                    { 16, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4862), null, false, 10, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4862), 16 },
                    { 17, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4864), null, false, 15, 1, 18, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4865), 17 },
                    { 18, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4867), null, false, 15, 1, 17, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4868), 18 },
                    { 19, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4870), null, false, 15, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4871), 19 },
                    { 20, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4872), null, false, 15, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4873), 20 },
                    { 21, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4875), null, false, 6, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4876), 21 },
                    { 22, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4877), null, false, 3, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4878), 22 },
                    { 23, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4880), null, false, 12, 1, 8, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4881), 23 },
                    { 24, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4882), null, false, 3, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4883), 24 },
                    { 25, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4885), null, false, 3, 1, 4, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4886), 25 },
                    { 26, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4887), null, false, 13, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4888), 26 },
                    { 27, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4890), null, false, 8, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4891), 27 },
                    { 28, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4892), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4893), 28 },
                    { 29, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4895), null, false, 8, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4896), 29 },
                    { 30, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4898), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4899), 30 },
                    { 31, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4901), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4901), 31 },
                    { 32, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4903), null, false, 8, 1, 11, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4905), 32 },
                    { 33, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4906), null, false, 8, 1, 11, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4908), 33 },
                    { 34, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4909), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4910), 34 },
                    { 35, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4912), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4913), 35 },
                    { 36, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4914), null, false, 14, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4915), 36 },
                    { 37, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4917), null, false, 8, 1, 12, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4918), 37 },
                    { 38, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4919), null, false, 9, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4920), 38 },
                    { 39, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4922), null, false, 3, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4923), 39 },
                    { 40, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4924), null, false, 14, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4925), 40 },
                    { 41, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4927), null, false, 8, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4928), 41 },
                    { 42, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4929), null, false, 1, 1, 18, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4930), 42 },
                    { 43, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4932), null, false, 3, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4933), 43 },
                    { 44, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4934), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4935), 44 },
                    { 45, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4937), null, false, 4, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4938), 45 },
                    { 46, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4940), null, false, 8, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4941), 46 },
                    { 47, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4943), null, false, 11, 1, 9, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4943), 47 },
                    { 48, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4945), null, false, 7, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4946), 48 },
                    { 49, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4948), null, false, 12, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4948), 49 },
                    { 50, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4950), null, false, 3, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4951), 50 },
                    { 51, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4952), null, false, 3, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4953), 51 },
                    { 52, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4955), null, false, 11, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4956), 52 },
                    { 53, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4958), null, false, 7, 1, 10, new DateTime(2023, 8, 28, 9, 53, 18, 941, DateTimeKind.Local).AddTicks(4958), 53 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationUnitDependency_ManagerId_FK",
                schema: "org",
                table: "OperationUnitDependency",
                column: "ManagerId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_OperationUnitDependency_OperationUnitId_FK",
                schema: "org",
                table: "OperationUnitDependency",
                column: "OperationUnitId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_OperationUnitDependency_OrganizationId_FK_OperationUnitId_FK_ParentOperationUnitId_FK_ManagerId_FK",
                schema: "org",
                table: "OperationUnitDependency",
                columns: new[] { "OrganizationId_FK", "OperationUnitId_FK", "ParentOperationUnitId_FK", "ManagerId_FK" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationUnitDependency_ParentOperationUnitId_FK",
                schema: "org",
                table: "OperationUnitDependency",
                column: "ParentOperationUnitId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentId_FK",
                schema: "org",
                table: "Organization",
                column: "ParentId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_RegionId_FK",
                schema: "org",
                table: "Organization",
                column: "RegionId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_Name",
                schema: "srv",
                table: "ServiceCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_UserGroupId_FK",
                schema: "srv",
                table: "ServiceCategory",
                column: "UserGroupId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCauseCategory_ServiceCategoryId_FK",
                schema: "srv",
                table: "ServiceCauseCategory",
                column: "ServiceCategoryId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCauseCategory_ServiceRootCauseId_FK",
                schema: "srv",
                table: "ServiceCauseCategory",
                column: "ServiceRootCauseId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCauseCategory_ServiceSubCauseId_FK",
                schema: "srv",
                table: "ServiceCauseCategory",
                column: "ServiceSubCauseId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePhase_ServiceRequestTypeId_FK_PhaseName",
                schema: "srv",
                table: "ServicePhase",
                columns: new[] { "ServiceRequestTypeId_FK", "PhaseName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_OwnerUserId_FK",
                schema: "srv",
                table: "ServiceRequest",
                column: "OwnerUserId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_RequestNumber",
                schema: "srv",
                table: "ServiceRequest",
                column: "RequestNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceCategoryId_FK",
                schema: "srv",
                table: "ServiceRequest",
                column: "ServiceCategoryId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceRequestTypeId_FK",
                schema: "srv",
                table: "ServiceRequest",
                column: "ServiceRequestTypeId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestChild_ChildServiceRequestId_FK",
                schema: "srv",
                table: "ServiceRequestChild",
                column: "ChildServiceRequestId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestChild_ParentServiceRequestId_FK_ChildServiceRequestId_FK",
                schema: "srv",
                table: "ServiceRequestChild",
                columns: new[] { "ParentServiceRequestId_FK", "ChildServiceRequestId_FK" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestLifeCycle_ServicePhaseId_FK",
                schema: "srv",
                table: "ServiceRequestLifeCycle",
                column: "ServicePhaseId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestLifeCycle_ServiceRequestId_FK",
                schema: "srv",
                table: "ServiceRequestLifeCycle",
                column: "ServiceRequestId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestLifeCycle_ServiceStateId_FK",
                schema: "srv",
                table: "ServiceRequestLifeCycle",
                column: "ServiceStateId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestType_Name",
                schema: "srv",
                table: "ServiceRequestType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRootCause_RootCauseName",
                schema: "srv",
                table: "ServiceRootCause",
                column: "RootCauseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceState_ServiceRequestTypeId_FK_StateName",
                schema: "srv",
                table: "ServiceState",
                columns: new[] { "ServiceRequestTypeId_FK", "StateName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSubCause_SubCauseName",
                schema: "srv",
                table: "ServiceSubCause",
                column: "SubCauseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "usr",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "usr",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupName",
                schema: "usr",
                table: "UserGroup",
                column: "GroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_UserGroupId_FK_UserId_FK",
                schema: "usr",
                table: "UserInGroup",
                columns: new[] { "UserGroupId_FK", "UserId_FK" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroup_UserId_FK",
                schema: "usr",
                table: "UserInGroup",
                column: "UserId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserInOrgOpunitPos_OperationUnitId_FK",
                schema: "usr",
                table: "UserInOrgOpunitPos",
                column: "OperationUnitId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserInOrgOpunitPos_OrganizationId_FK",
                schema: "usr",
                table: "UserInOrgOpunitPos",
                column: "OrganizationId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserInOrgOpunitPos_PositionId_FK",
                schema: "usr",
                table: "UserInOrgOpunitPos",
                column: "PositionId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserInOrgOpunitPos_UserId_FK_OrganizationId_FK_OperationUnitId_FK_PositionId_FK",
                schema: "usr",
                table: "UserInOrgOpunitPos",
                columns: new[] { "UserId_FK", "OrganizationId_FK", "OperationUnitId_FK", "PositionId_FK" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoll_RollId_FK",
                schema: "usr",
                table: "UserInRoll",
                column: "RollId_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoll_UserId_FK_RollId_FK",
                schema: "usr",
                table: "UserInRoll",
                columns: new[] { "UserId_FK", "RollId_FK" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationUnitDependency",
                schema: "org");

            migrationBuilder.DropTable(
                name: "ServiceCauseCategory",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServiceRequestChild",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServiceRequestLifeCycle",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "System",
                schema: "stm");

            migrationBuilder.DropTable(
                name: "UserInGroup",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserInOrgOpunitPos",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "UserInRoll",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ServiceRootCause",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServiceSubCause",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServicePhase",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServiceRequest",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "ServiceState",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "OperationUnit",
                schema: "org");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "org");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "org");

            migrationBuilder.DropTable(
                name: "UserRoll",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ServiceCategory",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "User",
                schema: "usr");

            migrationBuilder.DropTable(
                name: "ServiceRequestType",
                schema: "srv");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "rgn");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "usr");
        }
    }
}
