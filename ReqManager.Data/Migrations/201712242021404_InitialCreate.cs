namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.IO;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            #region Create Table



            CreateTable(
                "ART.ARTIFACT_TYPE",
                c => new
                {
                    ArtifactTypeID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ArtifactTypeID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "PROJ.PROJECT_ARTIFACT",
                c => new
                {
                    ProjectArtifactID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    ArtifactTypeID = c.Int(nullable: false),
                    ImportanceID = c.Int(nullable: false),
                    ProjectID = c.Int(nullable: false),
                    code = c.String(maxLength: 25),
                    path = c.String(nullable: false, maxLength: 500),
                    description = c.String(nullable: false, maxLength: 500),
                    creationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ProjectArtifactID)
                .ForeignKey("ART.ARTIFACT_TYPE", t => t.ArtifactTypeID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("PROJ.PROJECT", t => t.ProjectID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.ArtifactTypeID)
                .Index(t => t.ImportanceID)
                .Index(t => t.ProjectID)
                .Index(t => t.code, unique: true);

            CreateTable(
                "ART.HISTORY_PROJECT_ARTIFACT",
                c => new
                {
                    HistoryArtefactID = c.Int(nullable: false, identity: true),
                    ProjectArtifactID = c.Int(nullable: false),
                    descriptionTypeArtifact = c.String(nullable: false, maxLength: 50),
                    descriptionImportance = c.String(nullable: false, maxLength: 50),
                    path = c.String(nullable: false, maxLength: 300),
                    description = c.String(nullable: false, maxLength: 500),
                    creationDate = c.DateTime(nullable: false),
                    login = c.String(nullable: false, maxLength: 25),
                })
                .PrimaryKey(t => t.HistoryArtefactID)
                .ForeignKey("PROJ.PROJECT_ARTIFACT", t => t.ProjectArtifactID)
                .Index(t => t.ProjectArtifactID);

            CreateTable(
                "PROJ.IMPORTANCE",
                c => new
                {
                    ImportanceID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ImportanceID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "REQ.REQUIREMENT",
                c => new
                {
                    RequirementID = c.Int(nullable: false, identity: true),
                    ProjectID = c.Int(nullable: false),
                    RequirementTemplateID = c.Int(),
                    CreationUserID = c.Int(nullable: false),
                    RequirementStatusID = c.Int(nullable: false),
                    RequirementTypeID = c.Int(nullable: false),
                    RequirementSubTypeID = c.Int(),
                    ImportanceID = c.Int(nullable: false),
                    versionNumber = c.Int(nullable: false),
                    code = c.String(maxLength: 25),
                    description = c.String(nullable: false),
                    title = c.String(nullable: false, maxLength: 100),
                    creationDate = c.DateTime(nullable: false),
                    startDate = c.DateTime(),
                    endDate = c.DateTime(),
                    preTraceability = c.Boolean(nullable: false),
                    cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.RequirementID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("PROJ.PROJECT", t => t.ProjectID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .ForeignKey("REQ.REQUIREMENT_TEMPLATE", t => t.RequirementTemplateID)
                .ForeignKey("REQ.REQUIREMENT_TYPE", t => t.RequirementTypeID)
                .ForeignKey("REQ.REQUIREMENT_STATUS", t => t.RequirementStatusID)
                .ForeignKey("REQ.REQUIREMENT_SUB_TYPE", t => t.RequirementSubTypeID)
                .Index(t => new { t.RequirementID, t.versionNumber }, unique: true, name: "IX_REQUIREMENT")
                .Index(t => t.ProjectID)
                .Index(t => t.RequirementTemplateID)
                .Index(t => t.CreationUserID)
                .Index(t => t.RequirementStatusID)
                .Index(t => t.RequirementTypeID)
                .Index(t => t.RequirementSubTypeID)
                .Index(t => t.ImportanceID)
                .Index(t => t.code, unique: true);

            CreateTable(
                "LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS",
                c => new
                {
                    LinkArtifactRequirementID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    ProjectArtifactID = c.Int(nullable: false),
                    RequirementID = c.Int(nullable: false),
                    TypeLinkID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    code = c.String(maxLength: 25),
                })
                .PrimaryKey(t => t.LinkArtifactRequirementID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .ForeignKey("LINK.TYPE_LINK", t => t.TypeLinkID)
                .ForeignKey("PROJ.PROJECT_ARTIFACT", t => t.ProjectArtifactID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementID)
                .Index(t => t.CreationUserID)
                .Index(t => new { t.ProjectArtifactID, t.RequirementID, t.TypeLinkID }, unique: true, name: "IX_artifact_requirement")
                .Index(t => t.code, unique: true);

            CreateTable(
                "LINK.LINK_ARTIFACT_ATTRIBUTES",
                c => new
                {
                    ArtefactAttributeID = c.Int(nullable: false, identity: true),
                    AttributeID = c.Int(nullable: false),
                    LinkArtifactRequirementID = c.Int(nullable: false),
                    value = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ArtefactAttributeID)
                .ForeignKey("LINK.ATTRIBUTES", t => t.AttributeID)
                .ForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", t => t.LinkArtifactRequirementID)
                .Index(t => t.AttributeID)
                .Index(t => t.LinkArtifactRequirementID);

            CreateTable(
                "LINK.ATTRIBUTES",
                c => new
                {
                    AttributeID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.AttributeID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "LINK.ATTRIBUTES_TYPE_LINK",
                c => new
                {
                    AttributesTypeLinkID = c.Int(nullable: false, identity: true),
                    AttributeID = c.Int(nullable: false),
                    TypeLinkID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AttributesTypeLinkID)
                .ForeignKey("LINK.ATTRIBUTES", t => t.AttributeID)
                .ForeignKey("LINK.TYPE_LINK", t => t.TypeLinkID)
                .Index(t => new { t.AttributeID, t.TypeLinkID }, unique: true, name: "IX_attribute_type");

            CreateTable(
                "LINK.TYPE_LINK",
                c => new
                {
                    TypeLinkID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    description = c.String(nullable: false, maxLength: 50),
                    creationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.TypeLinkID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "LINK.LINK_BETWEEN_REQUIREMENT",
                c => new
                {
                    LinkRequirementsID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    TypeLinkID = c.Int(nullable: false),
                    RequirementOriginID = c.Int(nullable: false),
                    RequirementTargetID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    code = c.String(maxLength: 25),
                })
                .PrimaryKey(t => t.LinkRequirementsID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementOriginID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementTargetID)
                .ForeignKey("LINK.TYPE_LINK", t => t.TypeLinkID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.TypeLinkID)
                .Index(t => new { t.RequirementOriginID, t.RequirementTargetID }, unique: true, name: "IX_LINK_BETWEEN_REQUIREMENT")
                .Index(t => t.code, unique: true);

            CreateTable(
                "LINK.LINK_REQUIREMENT_ATTRIBUTES",
                c => new
                {
                    RequirementAttributeID = c.Int(nullable: false, identity: true),
                    AttributeID = c.Int(nullable: false),
                    LinkRequirementsID = c.Int(nullable: false),
                    value = c.String(nullable: false),
                })
                .PrimaryKey(t => t.RequirementAttributeID)
                .ForeignKey("LINK.ATTRIBUTES", t => t.AttributeID)
                .ForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", t => t.LinkRequirementsID)
                .Index(t => t.AttributeID)
                .Index(t => t.LinkRequirementsID);

            CreateTable(
                "ACCESS.USERS",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 100),
                    nickName = c.String(nullable: false, maxLength: 10),
                    password = c.String(maxLength: 1000),
                    verificationCode = c.String(maxLength: 10),
                    email = c.String(nullable: false, maxLength: 50),
                    login = c.String(nullable: false, maxLength: 15),
                    dateOfBirth = c.DateTime(nullable: false),
                    profession = c.String(nullable: false, maxLength: 30),
                    document = c.String(nullable: false, maxLength: 20),
                    active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.UserID)
                .Index(t => t.email, unique: true)
                .Index(t => t.login, unique: true)
                .Index(t => t.document, unique: true);

            CreateTable(
                "PROJ.HISTORY_PROJECT",
                c => new
                {
                    HistoryProjectID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    ProjectID = c.Int(nullable: false),
                    descriptionPhases = c.String(nullable: false, maxLength: 50),
                    startDate = c.DateTime(nullable: false),
                    endDate = c.DateTime(nullable: false),
                    changedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.HistoryProjectID)
                .ForeignKey("PROJ.PROJECT", t => t.ProjectID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.ProjectID);

            CreateTable(
                "PROJ.PROJECT",
                c => new
                {
                    ProjectID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    ProjectPhasesID = c.Int(nullable: false),
                    description = c.String(nullable: false, maxLength: 255),
                    pathForTraceability = c.String(nullable: false, maxLength: 300),
                    environmentalInformation = c.String(nullable: false, maxLength: 1000),
                    managementInformation = c.String(nullable: false, maxLength: 1000),
                    startDate = c.DateTime(nullable: false),
                    endDate = c.DateTime(),
                    creationDate = c.DateTime(nullable: false),
                    code = c.String(maxLength: 25),
                    cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("PROJ.PROJECT_PHASES", t => t.ProjectPhasesID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.ProjectPhasesID)
                .Index(t => t.code, unique: true);

            CreateTable(
                "PROJ.PROJECT_PHASES",
                c => new
                {
                    ProjectPhasesID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ProjectPhasesID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "PROJ.STAKEHOLDERS_PROJECT",
                c => new
                {
                    StakeholdersProjectID = c.Int(nullable: false, identity: true),
                    ProjectID = c.Int(nullable: false),
                    StakeholderID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    description = c.String(nullable: false, maxLength: 255),
                    importanceValue = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.StakeholdersProjectID)
                .ForeignKey("PROJ.PROJECT", t => t.ProjectID)
                .ForeignKey("PROJ.STAKEHOLDERS", t => t.StakeholderID)
                .Index(t => new { t.ProjectID, t.StakeholderID }, unique: true, name: "IX_STAKEHOLDERS_PROJECT");

            CreateTable(
                "PROJ.STAKEHOLDER_REQUIREMENT",
                c => new
                {
                    StakeholderRequirementID = c.Int(nullable: false, identity: true),
                    StakeholdersProjectID = c.Int(nullable: false),
                    RequirementID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    importanceValue = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.StakeholderRequirementID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementID)
                .ForeignKey("PROJ.STAKEHOLDERS_PROJECT", t => t.StakeholdersProjectID)
                .Index(t => t.StakeholdersProjectID)
                .Index(t => t.RequirementID);

            CreateTable(
                "REQ.REQUIREMENT_REQUEST_FOR_CHANGES",
                c => new
                {
                    RequirementRequestForChangesID = c.Int(nullable: false, identity: true),
                    StakeholderRequirementID = c.Int(nullable: false),
                    RequestStatusID = c.Int(nullable: false),
                    request = c.String(nullable: false, maxLength: 1000),
                    creationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.RequirementRequestForChangesID)
                .ForeignKey("REQ.REQUEST_STATUS", t => t.RequestStatusID)
                .ForeignKey("PROJ.STAKEHOLDER_REQUIREMENT", t => t.StakeholderRequirementID)
                .Index(t => t.StakeholderRequirementID)
                .Index(t => t.RequestStatusID);

            CreateTable(
                "REQ.REQUEST_STATUS",
                c => new
                {
                    RequestStatusID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.RequestStatusID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "REQ.STAKEHOLDER_REQUIREMENT_APPROVAL",
                c => new
                {
                    StakeholderRequirementApprovalID = c.Int(nullable: false, identity: true),
                    StakeholderRequirementID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    description = c.String(nullable: false, maxLength: 1000),
                    approved = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.StakeholderRequirementApprovalID)
                .ForeignKey("PROJ.STAKEHOLDER_REQUIREMENT", t => t.StakeholderRequirementID)
                .Index(t => t.StakeholderRequirementID);

            CreateTable(
                "PROJ.STAKEHOLDERS",
                c => new
                {
                    StakeholderID = c.Int(nullable: false, identity: true),
                    UserID = c.Int(nullable: false),
                    ClassificationID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.StakeholderID)
                .ForeignKey("PROJ.STAKEHOLDER_CLASSIFICATION", t => t.ClassificationID)
                .ForeignKey("ACCESS.USERS", t => t.UserID)
                .Index(t => new { t.UserID, t.ClassificationID }, unique: true, name: "IX_STAKEHOLDERS");

            CreateTable(
                "PROJ.STAKEHOLDER_CLASSIFICATION",
                c => new
                {
                    ClassificationID = c.Int(nullable: false, identity: true),
                    description = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.ClassificationID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "TASK.HISTORY_TASK",
                c => new
                {
                    HistoryTaskID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    TaskID = c.Int(nullable: false),
                    startDate = c.DateTime(nullable: false),
                    endDate = c.DateTime(nullable: false),
                    description = c.String(nullable: false, maxLength: 1000),
                    descriptionImportance = c.String(nullable: false, maxLength: 50),
                    changedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.HistoryTaskID)
                .ForeignKey("TASK.TASK", t => t.TaskID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.TaskID);

            CreateTable(
                "TASK.TASK",
                c => new
                {
                    TaskID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    ImportanceID = c.Int(nullable: false),
                    StatusTaskID = c.Int(nullable: false),
                    TaskTypeTemplateID = c.Int(nullable: false),
                    TaskTypeID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    description = c.String(nullable: false, maxLength: 1000),
                    startDate = c.DateTime(nullable: false),
                    code = c.String(maxLength: 25),
                    endDate = c.DateTime(),
                    StatusTask_TaskStatusID = c.Int(),
                    TaskType_TypeTaskID = c.Int(),
                })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("TASK.STATUS_TASK", t => t.StatusTask_TaskStatusID)
                .ForeignKey("TASK.TASK_TYPE", t => t.TaskType_TypeTaskID)
                .ForeignKey("TASK.TASK_TYPE_TEMPLATE", t => t.TaskTypeTemplateID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.ImportanceID)
                .Index(t => t.TaskTypeTemplateID)
                .Index(t => t.code, unique: true)
                .Index(t => t.StatusTask_TaskStatusID)
                .Index(t => t.TaskType_TypeTaskID);

            CreateTable(
                "TASK.STATUS_TASK",
                c => new
                {
                    TaskStatusID = c.Int(nullable: false, identity: true),
                    description = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.TaskStatusID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "TASK.TASK_TYPE",
                c => new
                {
                    TypeTaskID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.TypeTaskID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "TASK.TASK_TYPE_TEMPLATE",
                c => new
                {
                    TaskTypeTemplateID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    TaskTypeID = c.Int(nullable: false),
                    templateHtml = c.String(nullable: false, maxLength: 1000),
                    creationDate = c.DateTime(nullable: false),
                    TaskType_TypeTaskID = c.Int(),
                })
                .PrimaryKey(t => t.TaskTypeTemplateID)
                .ForeignKey("TASK.TASK_TYPE", t => t.TaskType_TypeTaskID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.TaskType_TypeTaskID);

            CreateTable(
                "TASK.USER_TASK",
                c => new
                {
                    UserTaskID = c.Int(nullable: false, identity: true),
                    UserID = c.Int(nullable: false),
                    TaskID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.UserTaskID)
                .ForeignKey("TASK.TASK", t => t.TaskID)
                .ForeignKey("ACCESS.USERS", t => t.UserID)
                .Index(t => new { t.UserID, t.TaskID }, unique: true, name: "IX_USER_TASK");

            CreateTable(
                "REQ.REQUIREMENT_TEMPLATE",
                c => new
                {
                    RequirementTemplateID = c.Int(nullable: false, identity: true),
                    CreationUserID = c.Int(nullable: false),
                    RequirementTypeID = c.Int(nullable: false),
                    description = c.String(nullable: false, maxLength: 50),
                    templateHtml = c.String(nullable: false),
                    createDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.RequirementTemplateID)
                .ForeignKey("REQ.REQUIREMENT_TYPE", t => t.RequirementTypeID)
                .ForeignKey("ACCESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.RequirementTypeID, unique: true);

            CreateTable(
                "REQ.REQUIREMENT_TYPE",
                c => new
                {
                    RequirementTypeID = c.Int(nullable: false, identity: true),
                    abbreviation = c.String(maxLength: 4),
                    description = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.RequirementTypeID)
                .Index(t => t.abbreviation, unique: true)
                .Index(t => t.description, unique: true);

            CreateTable(
                "REQ.REQUIREMENT_SUB_TYPE",
                c => new
                {
                    RequirementSubTypeID = c.Int(nullable: false, identity: true),
                    RequirementTypeID = c.Int(nullable: false),
                    description = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.RequirementSubTypeID)
                .ForeignKey("REQ.REQUIREMENT_TYPE", t => t.RequirementTypeID)
                .Index(t => t.RequirementTypeID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "ACCESS.USER_ROLE",
                c => new
                {
                    UserRoleID = c.Int(nullable: false, identity: true),
                    RoleID = c.Int(nullable: false),
                    UserID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("ACCESS.ROLE", t => t.RoleID)
                .ForeignKey("ACCESS.USERS", t => t.UserID)
                .Index(t => new { t.RoleID, t.UserID }, unique: true, name: "IX_USER_ROLE");

            CreateTable(
                "ACCESS.ROLE",
                c => new
                {
                    RoleID = c.Int(nullable: false, identity: true),
                    name = c.String(maxLength: 50),
                    description = c.String(maxLength: 255),
                })
                .PrimaryKey(t => t.RoleID)
                .Index(t => t.name, unique: true);

            CreateTable(
                "ACCESS.ROLE_CONTROLLER_ACTION",
                c => new
                {
                    RoleControllerActionID = c.Int(nullable: false, identity: true),
                    RoleID = c.Int(nullable: false),
                    ControllerActionID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.RoleControllerActionID)
                .ForeignKey("ACCESS.CONTROLLER_ACTION", t => t.ControllerActionID)
                .ForeignKey("ACCESS.ROLE", t => t.RoleID)
                .Index(t => new { t.RoleID, t.ControllerActionID }, unique: true, name: "IX_ROLE_CONTROLLER_ACTION");

            CreateTable(
                "ACCESS.CONTROLLER_ACTION",
                c => new
                {
                    ControllerActionID = c.Int(nullable: false, identity: true),
                    controller = c.String(nullable: false, maxLength: 100),
                    action = c.String(nullable: false, maxLength: 255),
                    IsGet = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ControllerActionID)
                .Index(t => new { t.controller, t.action }, unique: true, name: "IX_CONTROLLER_ACTION");

            CreateTable(
                "REQ.REQUIREMENT_CHARACTERISTICS",
                c => new
                {
                    RequirementCharacteristicsID = c.Int(nullable: false, identity: true),
                    CharacteristicsID = c.Int(nullable: false),
                    RequirementID = c.Int(nullable: false),
                    check = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.RequirementCharacteristicsID)
                .ForeignKey("REQ.CHARACTERISTICS", t => t.CharacteristicsID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementID)
                .Index(t => new { t.CharacteristicsID, t.RequirementID }, unique: true, name: "IX_REQUIREMENT_CHARACTERISTICS");

            CreateTable(
                "REQ.CHARACTERISTICS",
                c => new
                {
                    CharacteristicsID = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 30),
                    description = c.String(nullable: false, maxLength: 500),
                    required = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.CharacteristicsID)
                .Index(t => t.name, unique: true);

            CreateTable(
                "REQ.REQUIREMENT_STATUS",
                c => new
                {
                    RequirementStatusID = c.Int(nullable: false, identity: true),
                    description = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.RequirementStatusID)
                .Index(t => t.description, unique: true);

            CreateTable(
                "REQ.REQUIREMENT_VERSIONS",
                c => new
                {
                    RequirementVersionsID = c.Int(nullable: false, identity: true),
                    RequirementRequestForChangesID = c.Int(),
                    RequirementID = c.Int(),
                    RequirementTypeID = c.Int(nullable: false),
                    RequirementSubTypeID = c.Int(),
                    ImportanceID = c.Int(nullable: false),
                    RequirementStatusID = c.Int(nullable: false),
                    RequirementTemplateID = c.Int(),
                    versionNumber = c.Int(nullable: false),
                    title = c.String(nullable: false, maxLength: 100),
                    description = c.String(nullable: false),
                    rationale = c.String(nullable: false, maxLength: 1000),
                    creationDate = c.DateTime(nullable: false),
                    startDate = c.DateTime(),
                    endDate = c.DateTime(),
                    preTraceability = c.Boolean(nullable: false),
                    cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.RequirementVersionsID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("REQ.REQUIREMENT", t => t.RequirementID)
                .ForeignKey("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", t => t.RequirementRequestForChangesID)
                .ForeignKey("REQ.REQUIREMENT_STATUS", t => t.RequirementStatusID)
                .ForeignKey("REQ.REQUIREMENT_SUB_TYPE", t => t.RequirementSubTypeID)
                .ForeignKey("REQ.REQUIREMENT_TEMPLATE", t => t.RequirementTemplateID)
                .ForeignKey("REQ.REQUIREMENT_TYPE", t => t.RequirementTypeID)
                .Index(t => t.RequirementRequestForChangesID)
                .Index(t => t.RequirementID)
                .Index(t => t.RequirementTypeID)
                .Index(t => t.RequirementSubTypeID)
                .Index(t => t.ImportanceID)
                .Index(t => t.RequirementStatusID)
                .Index(t => t.RequirementTemplateID);

            CreateTable(
                "TASK.SUBTASK",
                c => new
                {
                    SubtaskID = c.Int(nullable: false, identity: true),
                    StatusTaskID = c.Int(nullable: false),
                    TaskTypeID = c.Int(nullable: false),
                    UserTaskID = c.Int(nullable: false),
                    creationDate = c.DateTime(nullable: false),
                    description = c.String(nullable: false, maxLength: 1000),
                    startDate = c.DateTime(nullable: false),
                    endDate = c.DateTime(),
                    code = c.String(maxLength: 25),
                    StatusTask_TaskStatusID = c.Int(),
                    TaskType_TypeTaskID = c.Int(),
                })
                .PrimaryKey(t => t.SubtaskID)
                .ForeignKey("TASK.STATUS_TASK", t => t.StatusTask_TaskStatusID)
                .ForeignKey("TASK.TASK_TYPE", t => t.TaskType_TypeTaskID)
                .ForeignKey("TASK.USER_TASK", t => t.UserTaskID)
                .Index(t => t.UserTaskID)
                .Index(t => t.code, unique: true)
                .Index(t => t.StatusTask_TaskStatusID)
                .Index(t => t.TaskType_TypeTaskID);
            #endregion

            #region Triggers

            Sql(@"CREATE TRIGGER PROJ.GenerateCodeProject
                ON PROJ.PROJECT
                AFTER INSERT
                AS
                BEGIN
	                BEGIN TRY
		                BEGIN TRANSACTION;

                UPDATE PROJ.PROJECT SET code = (SELECT 'PRJ' + CONVERT(nvarchar, MAX(P.ProjectID))
                FROM PROJ.PROJECT AS P) WHERE ProjectID = (SELECT ProjectID from inserted)

		                COMMIT TRANSACTION;
	                END TRY
                BEGIN CATCH
	                SELECT
                        ERROR_NUMBER() as ErrorNumber,
                        ERROR_MESSAGE() as ErrorMessage;
                        ROLLBACK TRANSACTION;
                END CATCH
                END");

            Sql(@"CREATE TRIGGER TASK.GenerateCodeTask
ON TASK.TASK
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE TASK.TASK SET code = (SELECT 'T' + CONVERT(nvarchar, MAX(T.TaskID))
FROM TASK.TASK AS T) WHERE TaskID = (SELECT TaskID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");
            Sql(@"CREATE TRIGGER TASK.GenerateCodeSubtask
ON TASK.SUBTASK
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE TASK.SUBTASK SET code = (SELECT 'ST' + CONVERT(nvarchar, MAX(T.SubtaskID))
FROM TASK.SUBTASK AS T) WHERE SubtaskID = (SELECT SubtaskID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");

            Sql(@"CREATE TRIGGER REQ.GenerateCodeRequirement
ON REQ.REQUIREMENT
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE REQ.REQUIREMENT SET code = (SELECT 'REQ' + CONVERT(nvarchar, MAX(R.RequirementID))
FROM REQ.REQUIREMENT AS R) WHERE RequirementID = (SELECT RequirementID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");

            Sql(@"CREATE TRIGGER PROJ.GenerateCodeProjectArtifact
ON PROJ.PROJECT_ARTIFACT
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE PROJ.PROJECT_ARTIFACT SET code = (SELECT 'ART' + CONVERT(nvarchar, MAX(A.ProjectArtifactID))
FROM PROJ.PROJECT_ARTIFACT AS A) WHERE ProjectArtifactID = (SELECT ProjectArtifactID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");

            Sql(@"CREATE TRIGGER LINK.GenerateCodeLinkRequirementsArtifacts
ON LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS SET code = (SELECT 'R-A' + CONVERT(nvarchar, MAX(A.LinkArtifactRequirementID))
FROM LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS AS A) WHERE LinkArtifactRequirementID = (SELECT LinkArtifactRequirementID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");

            Sql(@"CREATE TRIGGER LINK.GenerateCodeLinkBetweenArtifacts
ON LINK.LINK_BETWEEN_REQUIREMENT
AFTER INSERT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;

UPDATE LINK.LINK_BETWEEN_REQUIREMENT SET code = (SELECT 'R-R' + CONVERT(nvarchar, MAX(A.LinkRequirementsID))
FROM LINK.LINK_BETWEEN_REQUIREMENT AS A) WHERE LinkRequirementsID = (SELECT LinkRequirementsID from inserted)

		COMMIT TRANSACTION;
	END TRY
BEGIN CATCH
	SELECT
        ERROR_NUMBER() as ErrorNumber,
        ERROR_MESSAGE() as ErrorMessage;
        ROLLBACK TRANSACTION;
END CATCH
END
GO");
            #endregion

            #region Insert Data

            Sql(@"SET IDENTITY_INSERT [ACCESS].[USERS] ON
                INSERT INTO ACCESS.USERS
                (UserID, name, nickName, password, verificationCode, email, login, dateOfBirth, profession, document, active)
                VALUES
                (1, 'Admin System', 'Admin', '63-43-8E-03-E7-26-E1-9C-AB-F2-7A-55-C8-03-B1-1D',
                'fhkp42KdHp', 'reqmanager@gmail.com', 'admin', '05-03-1994', 'Manager', '9999999999', 1)
                SET IDENTITY_INSERT [ACCESS].[USERS] OFF");

            Sql(@"SET IDENTITY_INSERT [ACCESS].[ROLE] ON
                INSERT [ACCESS].[ROLE] ([RoleID], [name], [description]) VALUES (1, N'Project Manager', N'This is the person with authority to manage a project. This includes leading the planning and the development of all project deliverables.')
                INSERT [ACCESS].[ROLE] ([RoleID], [name], [description]) VALUES (2, N'Client', N'This is the people (or groups) that are the direct beneficiaries of a project or service. They are the people for whom the project is being undertaken.')
                INSERT [ACCESS].[ROLE] ([RoleID], [name], [description]) VALUES (3, N'Analyst', N'The Analyst is responsible for ensuring that the requirements of the business clients are captured and documented correctly before a solution is developed and implemented.')
                INSERT [ACCESS].[ROLE] ([RoleID], [name], [description]) VALUES (4, N'Developer', N'The Developer is responsible for the actual building of the solution.')
                SET IDENTITY_INSERT [ACCESS].[ROLE] OFF");

            Sql(@"INSERT INTO ACCESS.USER_ROLE
                SELECT 1, 1");

            Sql(@"SET IDENTITY_INSERT [REQ].[REQUIREMENT_TYPE] ON
                INSERT [REQ].[REQUIREMENT_TYPE] ([RequirementTypeID], [description], [abbreviation]) VALUES (1, N'Stories User', N'SU')
                INSERT [REQ].[REQUIREMENT_TYPE] ([RequirementTypeID], [description], [abbreviation]) VALUES (2, N'Functional Requirement', N'FR')
                INSERT [REQ].[REQUIREMENT_TYPE] ([RequirementTypeID], [description], [abbreviation]) VALUES (3, N'Non-Functional Requirements', N'NFR')
                SET IDENTITY_INSERT [REQ].[REQUIREMENT_TYPE] OFF");

            Sql(@"SET IDENTITY_INSERT [REQ].[REQUIREMENT_STATUS] ON
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (6, N'Completed')
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (2, N'Development')
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (1, N'In Analysis')
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (4, N'In Test')
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (5, N'Maintenance')
                INSERT [REQ].[REQUIREMENT_STATUS] ([RequirementStatusID], [description]) VALUES (3, N'Validation')
                SET IDENTITY_INSERT [REQ].[REQUIREMENT_STATUS] OFF");

            Sql(@"SET IDENTITY_INSERT [REQ].[REQUEST_STATUS] ON
                INSERT [REQ].[REQUEST_STATUS] ([RequestStatusID], [description]) VALUES (3, N'Approved')
                INSERT [REQ].[REQUEST_STATUS] ([RequestStatusID], [description]) VALUES (2, N'In Analysis')
                INSERT [REQ].[REQUEST_STATUS] ([RequestStatusID], [description]) VALUES (4, N'Rejected')
                INSERT [REQ].[REQUEST_STATUS] ([RequestStatusID], [description]) VALUES (1, N'Requested')
                SET IDENTITY_INSERT [REQ].[REQUEST_STATUS] OFF");

            Sql(@"SET IDENTITY_INSERT [PROJ].[PROJECT_PHASES] ON
                INSERT [PROJ].[PROJECT_PHASES] ([ProjectPhasesID], [description]) VALUES (1, N'Analysis')
                INSERT [PROJ].[PROJECT_PHASES] ([ProjectPhasesID], [description]) VALUES (5, N'Coding')
                INSERT [PROJ].[PROJECT_PHASES] ([ProjectPhasesID], [description]) VALUES (3, N'Design')
                INSERT [PROJ].[PROJECT_PHASES] ([ProjectPhasesID], [description]) VALUES (6, N'Testing')
                SET IDENTITY_INSERT [PROJ].[PROJECT_PHASES] OFF");

            Sql(@"SET IDENTITY_INSERT [PROJ].[IMPORTANCE] ON
                INSERT [PROJ].[IMPORTANCE] ([ImportanceID], [description]) VALUES (3, N'Critical')
                INSERT [PROJ].[IMPORTANCE] ([ImportanceID], [description]) VALUES (1, N'Regular')
                INSERT [PROJ].[IMPORTANCE] ([ImportanceID], [description]) VALUES (4, N'Urgent')
                SET IDENTITY_INSERT [PROJ].[IMPORTANCE] OFF");

            #region Actions

            Sql(@"SET IDENTITY_INSERT [ACCESS].[CONTROLLER_ACTION] ON
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (1, N'StakeholderClassificationController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (2, N'StakeholderClassificationController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (3, N'StakeholderClassificationController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (4, N'StakeholderClassificationController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (5, N'StakeholderClassificationController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (6, N'StakeholderClassificationController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (7, N'StakeholderClassificationController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (8, N'StakeholderClassificationController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (9, N'StakeholderClassificationController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (10, N'StakeholdersController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (11, N'StakeholdersController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (12, N'StakeholdersController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (13, N'StakeholdersController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (14, N'StakeholdersController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (15, N'StakeholdersController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (16, N'StakeholdersController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (17, N'StakeholdersController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (18, N'StakeholdersController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (19, N'ArtifactTypeController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (20, N'ArtifactTypeController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (21, N'ArtifactTypeController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (22, N'ArtifactTypeController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (23, N'ArtifactTypeController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (24, N'ArtifactTypeController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (25, N'ArtifactTypeController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (26, N'ArtifactTypeController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (27, N'ArtifactTypeController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (28, N'AttributesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (29, N'AttributesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (30, N'AttributesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (31, N'AttributesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (32, N'AttributesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (33, N'AttributesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (34, N'AttributesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (35, N'AttributesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (36, N'AttributesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (37, N'AttributesTypeLinkController', N'CreateNewAttributeForTypeLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (38, N'AttributesTypeLinkController', N'GetAttributesOfTypeLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (39, N'AttributesTypeLinkController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (40, N'AttributesTypeLinkController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (41, N'AttributesTypeLinkController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (42, N'AttributesTypeLinkController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (43, N'AttributesTypeLinkController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (44, N'AttributesTypeLinkController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (45, N'AttributesTypeLinkController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (46, N'AttributesTypeLinkController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (47, N'AttributesTypeLinkController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (48, N'CharacteristicsController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (49, N'CharacteristicsController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (50, N'CharacteristicsController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (51, N'CharacteristicsController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (52, N'CharacteristicsController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (53, N'CharacteristicsController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (54, N'CharacteristicsController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (55, N'CharacteristicsController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (56, N'CharacteristicsController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (57, N'ControllerActionController', N'Refresh', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (58, N'ControllerActionController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (59, N'ControllerActionController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (60, N'ControllerActionController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (61, N'ControllerActionController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (62, N'ControllerActionController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (63, N'ControllerActionController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (64, N'ControllerActionController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (65, N'ControllerActionController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (66, N'ControllerActionController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (67, N'FileController', N'RenderFile', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (68, N'FileController', N'GetFolders', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (69, N'LinkArtifactAttributesController', N'CreateNewAttributeForLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (70, N'LinkArtifactAttributesController', N'GetAttributes', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (71, N'LinkArtifactAttributesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (72, N'LinkArtifactAttributesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (73, N'LinkArtifactAttributesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (74, N'LinkArtifactAttributesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (75, N'LinkArtifactAttributesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (76, N'LinkArtifactAttributesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (77, N'LinkArtifactAttributesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (78, N'LinkArtifactAttributesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (79, N'LinkArtifactAttributesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (80, N'LinkBetweenRequirementsArtifactController', N'GetLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (81, N'LinkBetweenRequirementsArtifactController', N'GetWithCode', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (82, N'LinkBetweenRequirementsArtifactController', N'CreateNewLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (83, N'LinkBetweenRequirementsArtifactController', N'GetLinkArtifactsRequirementsFromProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (84, N'LinkBetweenRequirementsArtifactController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (85, N'LinkBetweenRequirementsArtifactController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (86, N'LinkBetweenRequirementsArtifactController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (87, N'LinkBetweenRequirementsArtifactController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (88, N'LinkBetweenRequirementsArtifactController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (89, N'LinkBetweenRequirementsArtifactController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (90, N'LinkBetweenRequirementsArtifactController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (91, N'LinkBetweenRequirementsArtifactController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (92, N'LinkBetweenRequirementsArtifactController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (93, N'LinkBetweenRequirementsController', N'GetWithCode', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (94, N'LinkBetweenRequirementsController', N'CreateNewLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (95, N'LinkBetweenRequirementsController', N'GetLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (96, N'LinkBetweenRequirementsController', N'GetLinkRequirementsFromProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (97, N'LinkBetweenRequirementsController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (98, N'LinkBetweenRequirementsController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (99, N'LinkBetweenRequirementsController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (100, N'LinkBetweenRequirementsController', N'Edit', 1)
                GO
                print 'Processed 100 total records'
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (101, N'LinkBetweenRequirementsController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (102, N'LinkBetweenRequirementsController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (103, N'LinkBetweenRequirementsController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (104, N'LinkBetweenRequirementsController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (105, N'LinkBetweenRequirementsController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (106, N'LinkRequirementAttributesController', N'CreateNewAttributeForLink', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (107, N'LinkRequirementAttributesController', N'GetAttributes', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (108, N'LinkRequirementAttributesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (109, N'LinkRequirementAttributesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (110, N'LinkRequirementAttributesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (111, N'LinkRequirementAttributesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (112, N'LinkRequirementAttributesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (113, N'LinkRequirementAttributesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (114, N'LinkRequirementAttributesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (115, N'LinkRequirementAttributesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (116, N'LinkRequirementAttributesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (117, N'LoginController', N'Login', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (118, N'LoginController', N'LogOut', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (119, N'LoginController', N'ResetPassword', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (120, N'ImportanceController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (121, N'ImportanceController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (122, N'ImportanceController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (123, N'ImportanceController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (124, N'ImportanceController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (125, N'ImportanceController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (126, N'ImportanceController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (127, N'ImportanceController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (128, N'ImportanceController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (129, N'ProjectArtifactController', N'GetWithCode', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (130, N'ProjectArtifactController', N'GetArtifactsFromProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (131, N'ProjectArtifactController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (132, N'ProjectArtifactController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (133, N'ProjectArtifactController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (134, N'ProjectArtifactController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (135, N'ProjectArtifactController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (136, N'ProjectArtifactController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (137, N'ProjectArtifactController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (138, N'ProjectArtifactController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (139, N'ProjectArtifactController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (140, N'ProjectPhasesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (141, N'ProjectPhasesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (142, N'ProjectPhasesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (143, N'ProjectPhasesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (144, N'ProjectPhasesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (145, N'ProjectPhasesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (146, N'ProjectPhasesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (147, N'ProjectPhasesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (148, N'ProjectPhasesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (149, N'ProjectsController', N'PrintDocumentRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (150, N'ProjectsController', N'GetRequirementsFromProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (151, N'ProjectsController', N'GetFolders', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (152, N'ProjectsController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (153, N'ProjectsController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (154, N'ProjectsController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (155, N'ProjectsController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (156, N'ProjectsController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (157, N'ProjectsController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (158, N'ProjectsController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (159, N'ProjectsController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (160, N'ProjectsController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (161, N'RequestStatusController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (162, N'RequestStatusController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (163, N'RequestStatusController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (164, N'RequestStatusController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (165, N'RequestStatusController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (166, N'RequestStatusController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (167, N'RequestStatusController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (168, N'RequestStatusController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (169, N'RequestStatusController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (170, N'RequirementCharacteristicsController', N'Check', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (171, N'RequirementCharacteristicsController', N'CreateNewRequirementCharacterisc', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (172, N'RequirementCharacteristicsController', N'CreateConfirmed', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (173, N'RequirementCharacteristicsController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (174, N'RequirementCharacteristicsController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (175, N'RequirementCharacteristicsController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (176, N'RequirementCharacteristicsController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (177, N'RequirementCharacteristicsController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (178, N'RequirementCharacteristicsController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (179, N'RequirementCharacteristicsController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (180, N'RequirementCharacteristicsController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (181, N'RequirementCharacteristicsController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (182, N'RequirementController', N'GetRequirementCostByProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (183, N'RequirementController', N'GetWithCode', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (184, N'RequirementController', N'PrintDocumentRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (185, N'RequirementController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (186, N'RequirementController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (187, N'RequirementController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (188, N'RequirementController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (189, N'RequirementController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (190, N'RequirementController', N'DeleteConfirmed', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (191, N'RequirementController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (192, N'RequirementController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (193, N'RequirementController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (194, N'RequirementRequestForChangesController', N'ChangeStatus', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (195, N'RequirementRequestForChangesController', N'RequestNewChange', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (196, N'RequirementRequestForChangesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (197, N'RequirementRequestForChangesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (198, N'RequirementRequestForChangesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (199, N'RequirementRequestForChangesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (200, N'RequirementRequestForChangesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (201, N'RequirementRequestForChangesController', N'DeleteConfirmed', 0)
                GO
                print 'Processed 200 total records'
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (202, N'RequirementRequestForChangesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (203, N'RequirementRequestForChangesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (204, N'RequirementRequestForChangesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (205, N'RequirementStatusController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (206, N'RequirementStatusController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (207, N'RequirementStatusController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (208, N'RequirementStatusController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (209, N'RequirementStatusController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (210, N'RequirementStatusController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (211, N'RequirementStatusController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (212, N'RequirementStatusController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (213, N'RequirementStatusController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (214, N'RequirementSubTypeController', N'GetSubType', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (215, N'RequirementSubTypeController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (216, N'RequirementSubTypeController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (217, N'RequirementSubTypeController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (218, N'RequirementSubTypeController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (219, N'RequirementSubTypeController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (220, N'RequirementSubTypeController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (221, N'RequirementSubTypeController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (222, N'RequirementSubTypeController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (223, N'RequirementSubTypeController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (224, N'RequirementTemplateController', N'GetTemplatesOfRequirementType', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (225, N'RequirementTemplateController', N'GetTemplateHtml', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (226, N'RequirementTemplateController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (227, N'RequirementTemplateController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (228, N'RequirementTemplateController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (229, N'RequirementTemplateController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (230, N'RequirementTemplateController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (231, N'RequirementTemplateController', N'DeleteConfirmed', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (232, N'RequirementTemplateController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (233, N'RequirementTemplateController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (234, N'RequirementTemplateController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (235, N'RequirementTypesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (236, N'RequirementTypesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (237, N'RequirementTypesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (238, N'RequirementTypesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (239, N'RequirementTypesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (240, N'RequirementTypesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (241, N'RequirementTypesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (242, N'RequirementTypesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (243, N'RequirementTypesController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (244, N'RoleController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (245, N'RoleController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (246, N'RoleController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (247, N'RoleController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (248, N'RoleController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (249, N'RoleController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (250, N'RoleController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (251, N'RoleController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (252, N'RoleController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (253, N'RoleControllerActionController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (254, N'RoleControllerActionController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (255, N'RoleControllerActionController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (256, N'RoleControllerActionController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (257, N'RoleControllerActionController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (258, N'RoleControllerActionController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (259, N'RoleControllerActionController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (260, N'RoleControllerActionController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (261, N'RoleControllerActionController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (262, N'StakeholderRequirementApprovalController', N'Approve', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (263, N'StakeholderRequirementApprovalController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (264, N'StakeholderRequirementApprovalController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (265, N'StakeholderRequirementApprovalController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (266, N'StakeholderRequirementApprovalController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (267, N'StakeholderRequirementApprovalController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (268, N'StakeholderRequirementApprovalController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (269, N'StakeholderRequirementApprovalController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (270, N'StakeholderRequirementApprovalController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (271, N'StakeholderRequirementApprovalController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (272, N'StakeholderRequirementController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (273, N'StakeholderRequirementController', N'Add', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (274, N'StakeholderRequirementController', N'GetStakeholdersFromRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (275, N'StakeholderRequirementController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (276, N'StakeholderRequirementController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (277, N'StakeholderRequirementController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (278, N'StakeholderRequirementController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (279, N'StakeholderRequirementController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (280, N'StakeholderRequirementController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (281, N'StakeholderRequirementController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (282, N'StakeholderRequirementController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (283, N'StakeholdersProjectController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (284, N'StakeholdersProjectController', N'Add', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (285, N'StakeholdersProjectController', N'GetStakeholdersFromProject', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (286, N'StakeholdersProjectController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (287, N'StakeholdersProjectController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (288, N'StakeholdersProjectController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (289, N'StakeholdersProjectController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (290, N'StakeholdersProjectController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (291, N'StakeholdersProjectController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (292, N'StakeholdersProjectController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (293, N'StakeholdersProjectController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (294, N'TrackingController', N'TrackingProjectRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (295, N'TrackingController', N'TrackingRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (296, N'TrackingController', N'TrackingRequirements', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (297, N'TrackingController', N'TrackingProjectArtifact', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (298, N'TrackingController', N'TrackingArtifact', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (299, N'TrackingController', N'TrackingArtifacts', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (300, N'TrackingController', N'TrackingLinkBetweenRequirement', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (301, N'TrackingController', N'TrackingLinkBetweenRequirements', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (302, N'TrackingController', N'TrackingLinkBetweenRequirementArtifact', 1)
                GO
                print 'Processed 300 total records'
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (303, N'TrackingController', N'TrackingLinkBetweenRequirementArtifacts', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (304, N'TypeLinkController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (305, N'TypeLinkController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (306, N'TypeLinkController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (307, N'TypeLinkController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (308, N'TypeLinkController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (309, N'TypeLinkController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (310, N'TypeLinkController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (311, N'TypeLinkController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (312, N'TypeLinkController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (313, N'UserController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (314, N'UserController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (315, N'UserController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (316, N'UserController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (317, N'UserController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (318, N'UserController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (319, N'UserController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (320, N'UserController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (321, N'UserController', N'GetFilter', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (322, N'UsersRolesController', N'Index', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (323, N'UsersRolesController', N'Details', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (324, N'UsersRolesController', N'Create', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (325, N'UsersRolesController', N'Edit', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (326, N'UsersRolesController', N'Delete', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (327, N'UsersRolesController', N'DeleteConfirmed', 0)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (328, N'UsersRolesController', N'Get', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (329, N'UsersRolesController', N'GetAll', 1)
                INSERT [ACCESS].[CONTROLLER_ACTION] ([ControllerActionID], [controller], [action], [IsGet]) VALUES (330, N'UsersRolesController', N'GetFilter', 1)
                SET IDENTITY_INSERT [ACCESS].[CONTROLLER_ACTION] OFF");
            #endregion

            Sql(@"INSERT INTO ACCESS.ROLE_CONTROLLER_ACTION
                SELECT 1, ControllerActionID FROM ACCESS.CONTROLLER_ACTION");

            Sql(@"SET IDENTITY_INSERT [REQ].[CHARACTERISTICS] ON
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (1, N'Unitary', N'The requirement addresses one and only one thing.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (2, N'Consistent', N'The requirement does not contradict any other requirement and is fully consistent with all authoritative external documentation.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (3, N'Unambiguous', N'The requirement is concisely stated without recourse to technical jargon, acronyms (unless defined elsewhere in the Requirements document), or other esoteric verbiage.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (5, N'Complete', N'The requirement is fully stated in one place with no missing information.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (7, N'Traceable', N'The requirement meets all or part of a business need as stated by stakeholders and authoritatively documented.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (8, N'Testable', N'Testers should be able to verify whether the requirement is implemented correctly. The test should either pass or fail.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (9, N'Clear', N'Requirements should not contain unnecessary verbiage or information. They should be stated clearly and simply.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (10, N'Relevant', N'Stakeholders are interested in the requirement and it is important to the project.', 1)
                INSERT [REQ].[CHARACTERISTICS] ([CharacteristicsID], [name], [description], [required]) VALUES (12, N'Implementable', N'The requirement can be met computationally.', 1)
                SET IDENTITY_INSERT [REQ].[CHARACTERISTICS] OFF");

            Sql(@"SET IDENTITY_INSERT [LINK].[ATTRIBUTES] ON
                INSERT [LINK].[ATTRIBUTES] ([AttributeID], [description]) VALUES (1, N'Additional Notes')
                INSERT [LINK].[ATTRIBUTES] ([AttributeID], [description]) VALUES (3, N'Rationale')
                INSERT [LINK].[ATTRIBUTES] ([AttributeID], [description]) VALUES (2, N'Restrictions')
                SET IDENTITY_INSERT [LINK].[ATTRIBUTES] OFF");

            Sql(@"SET IDENTITY_INSERT [ART].[ARTIFACT_TYPE] ON
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (2, N'Architecture Design Documentation')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (6, N'Codes')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (3, N'Entity–relationship Diagram')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (5, N'Quick Start Guides')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (9, N'Reference Manual')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (1, N'Requirements Documentation')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (4, N'Technical Documentation')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (8, N'Test Case Specification')
                INSERT [ART].[ARTIFACT_TYPE] ([ArtifactTypeID], [description]) VALUES (7, N'Test Plan')
                SET IDENTITY_INSERT [ART].[ARTIFACT_TYPE] OFF");

            Sql(@"SET IDENTITY_INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ON
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (5, N'Customers')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (3, N'Developer')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (1, N'End User')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (2, N'System Administrators')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (6, N'System Analyst')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (7, N'System Engineer')
                INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] ([ClassificationID], [description]) VALUES (4, N'Tester')
                SET IDENTITY_INSERT [PROJ].[STAKEHOLDER_CLASSIFICATION] OFF");

            Sql(@"SET IDENTITY_INSERT [LINK].[TYPE_LINK] ON
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (1, 1, N'Satisfaction', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (2, 1, N'Dependency', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (3, 1, N'Evolution', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (4, 1, N'Rationale', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (5, 1, N'Resource', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (6, 1, N'Liability', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (7, 1, N'Representation', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (8, 1, N'Allocation', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (9, 1, N'Aggregation', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (10, 1, N'Restrictions', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (11, 1, N'Preconditions', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (12, 1, N'Examples', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (13, 1, N'Purpose', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (14, 1, N'Test Case', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (15, 1, N'Refined', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (16, 1, N'Generalized', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (17, 1, N'Similar', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (18, 1, N'Comparison', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (19, 1, N'Contradiction', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (20, 1, N'Manual', GETDATE())
                INSERT [LINK].[TYPE_LINK] ([TypeLinkID], [CreationUserID], [description], [creationDate]) VALUES (21, 1, N'Derivation', GETDATE())
                SET IDENTITY_INSERT [LINK].[TYPE_LINK] OFF");

            Sql(@"SET IDENTITY_INSERT [REQ].[REQUIREMENT_TEMPLATE] ON
                    INSERT [REQ].[REQUIREMENT_TEMPLATE] ([RequirementTemplateID], [CreationUserID], [RequirementTypeID], [description], [templateHtml], [createDate]) VALUES (1, 1, 1, N'Template for Stories User (English)', N'<p><strong>As a</strong> :</p><p><strong>I want</strong> :</p><p><strong>so that</strong> :</p>', CAST(0x0000A85400843036 AS DateTime))
                    INSERT [REQ].[REQUIREMENT_TEMPLATE] ([RequirementTemplateID], [CreationUserID], [RequirementTypeID], [description], [templateHtml], [createDate]) VALUES (2, 1, 2, N'Template for Functional Requirement (English)', N'<p><strong>Description</strong>:&nbsp;</p><p><strong>Input</strong>:&nbsp;</p><p><strong>Process</strong>:&nbsp;</p><p><strong>Output</strong>:&nbsp;</p><p><strong>Rationale</strong>:&nbsp;</p>', CAST(0x0000A854008A5CFB AS DateTime))
                    INSERT [REQ].[REQUIREMENT_TEMPLATE] ([RequirementTemplateID], [CreationUserID], [RequirementTypeID], [description], [templateHtml], [createDate]) VALUES (3, 1, 3, N'Template for Non-Functional Requirements (English)', N'<p><strong>Description</strong>:&nbsp;</p><p><strong>Rationale</strong>:&nbsp;</p>', CAST(0x0000A854008AC4D1 AS DateTime))
                    SET IDENTITY_INSERT [REQ].[REQUIREMENT_TEMPLATE] OFF");

            Sql(@"SET IDENTITY_INSERT [REQ].[REQUIREMENT_SUB_TYPE] ON
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (1, 3, N'Accessibility')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (2, 3, N'Configuration management')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (3, 3, N'Documentation')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (4, 3, N'Efficiency ')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (5, 3, N'Fault tolerance')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (6, 3, N'Maintainability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (7, 3, N'Performance')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (8, 3, N'Portability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (9, 3, N'Readability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (10, 3, N'Response time')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (11, 3, N'Reusability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (12, 3, N'Safety')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (13, 3, N'Scalability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (14, 3, N'Security')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (15, 3, N'Stability')
                INSERT [REQ].[REQUIREMENT_SUB_TYPE] ([RequirementSubTypeID], [RequirementTypeID], [description]) VALUES (16, 3, N'Testability')
                SET IDENTITY_INSERT [REQ].[REQUIREMENT_SUB_TYPE] OFF");

            Sql(@"INSERT INTO LINK.ATTRIBUTES_TYPE_LINK
                SELECT A.AttributeID, TL.TypeLinkID FROM LINK.TYPE_LINK AS TL, LINK.ATTRIBUTES AS A");

            #endregion
        }

        public override void Down()
        {
            DropForeignKey("TASK.SUBTASK", "UserTaskID", "TASK.USER_TASK");
            DropForeignKey("TASK.SUBTASK", "TaskType_TypeTaskID", "TASK.TASK_TYPE");
            DropForeignKey("TASK.SUBTASK", "StatusTask_TaskStatusID", "TASK.STATUS_TASK");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementTemplateID", "REQ.REQUIREMENT_TEMPLATE");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementSubTypeID", "REQ.REQUIREMENT_SUB_TYPE");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementStatusID", "REQ.REQUIREMENT_STATUS");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementRequestForChangesID", "REQ.REQUIREMENT_REQUEST_FOR_CHANGES");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementID", "REQ.REQUIREMENT");
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "ImportanceID", "PROJ.IMPORTANCE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementSubTypeID", "REQ.REQUIREMENT_SUB_TYPE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementStatusID", "REQ.REQUIREMENT_STATUS");
            DropForeignKey("REQ.REQUIREMENT_CHARACTERISTICS", "RequirementID", "REQ.REQUIREMENT");
            DropForeignKey("REQ.REQUIREMENT_CHARACTERISTICS", "CharacteristicsID", "REQ.CHARACTERISTICS");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "RequirementID", "REQ.REQUIREMENT");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "ProjectArtifactID", "PROJ.PROJECT_ARTIFACT");
            DropForeignKey("LINK.LINK_ARTIFACT_ATTRIBUTES", "LinkArtifactRequirementID", "LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS");
            DropForeignKey("LINK.LINK_ARTIFACT_ATTRIBUTES", "AttributeID", "LINK.ATTRIBUTES");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "TypeLinkID", "LINK.TYPE_LINK");
            DropForeignKey("ACCESS.USER_ROLE", "UserID", "ACCESS.USERS");
            DropForeignKey("ACCESS.USER_ROLE", "RoleID", "ACCESS.ROLE");
            DropForeignKey("ACCESS.ROLE_CONTROLLER_ACTION", "RoleID", "ACCESS.ROLE");
            DropForeignKey("ACCESS.ROLE_CONTROLLER_ACTION", "ControllerActionID", "ACCESS.CONTROLLER_ACTION");
            DropForeignKey("LINK.TYPE_LINK", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("REQ.REQUIREMENT_TEMPLATE", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("REQ.REQUIREMENT_TEMPLATE", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT_SUB_TYPE", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementTemplateID", "REQ.REQUIREMENT_TEMPLATE");
            DropForeignKey("REQ.REQUIREMENT", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("PROJ.PROJECT_ARTIFACT", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("TASK.HISTORY_TASK", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("TASK.USER_TASK", "UserID", "ACCESS.USERS");
            DropForeignKey("TASK.USER_TASK", "TaskID", "TASK.TASK");
            DropForeignKey("TASK.TASK", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("TASK.TASK_TYPE_TEMPLATE", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("TASK.TASK_TYPE_TEMPLATE", "TaskType_TypeTaskID", "TASK.TASK_TYPE");
            DropForeignKey("TASK.TASK", "TaskTypeTemplateID", "TASK.TASK_TYPE_TEMPLATE");
            DropForeignKey("TASK.TASK", "TaskType_TypeTaskID", "TASK.TASK_TYPE");
            DropForeignKey("TASK.TASK", "StatusTask_TaskStatusID", "TASK.STATUS_TASK");
            DropForeignKey("TASK.TASK", "ImportanceID", "PROJ.IMPORTANCE");
            DropForeignKey("TASK.HISTORY_TASK", "TaskID", "TASK.TASK");
            DropForeignKey("PROJ.HISTORY_PROJECT", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("PROJ.PROJECT", "CreationUserID", "ACCESS.USERS");
            DropForeignKey("PROJ.STAKEHOLDERS", "UserID", "ACCESS.USERS");
            DropForeignKey("PROJ.STAKEHOLDERS_PROJECT", "StakeholderID", "PROJ.STAKEHOLDERS");
            DropForeignKey("PROJ.STAKEHOLDERS", "ClassificationID", "PROJ.STAKEHOLDER_CLASSIFICATION");
            DropForeignKey("PROJ.STAKEHOLDER_REQUIREMENT", "StakeholdersProjectID", "PROJ.STAKEHOLDERS_PROJECT");
            DropForeignKey("REQ.STAKEHOLDER_REQUIREMENT_APPROVAL", "StakeholderRequirementID", "PROJ.STAKEHOLDER_REQUIREMENT");
            DropForeignKey("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", "StakeholderRequirementID", "PROJ.STAKEHOLDER_REQUIREMENT");
            DropForeignKey("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", "RequestStatusID", "REQ.REQUEST_STATUS");
            DropForeignKey("PROJ.STAKEHOLDER_REQUIREMENT", "RequirementID", "REQ.REQUIREMENT");
            DropForeignKey("PROJ.STAKEHOLDERS_PROJECT", "ProjectID", "PROJ.PROJECT");
            DropForeignKey("REQ.REQUIREMENT", "ProjectID", "PROJ.PROJECT");
            DropForeignKey("PROJ.PROJECT", "ProjectPhasesID", "PROJ.PROJECT_PHASES");
            DropForeignKey("PROJ.PROJECT_ARTIFACT", "ProjectID", "PROJ.PROJECT");
            DropForeignKey("PROJ.HISTORY_PROJECT", "ProjectID", "PROJ.PROJECT");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", "TypeLinkID", "LINK.TYPE_LINK");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", "RequirementTargetID", "REQ.REQUIREMENT");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", "RequirementOriginID", "REQ.REQUIREMENT");
            DropForeignKey("LINK.LINK_REQUIREMENT_ATTRIBUTES", "LinkRequirementsID", "LINK.LINK_BETWEEN_REQUIREMENT");
            DropForeignKey("LINK.LINK_REQUIREMENT_ATTRIBUTES", "AttributeID", "LINK.ATTRIBUTES");
            DropForeignKey("LINK.ATTRIBUTES_TYPE_LINK", "TypeLinkID", "LINK.TYPE_LINK");
            DropForeignKey("LINK.ATTRIBUTES_TYPE_LINK", "AttributeID", "LINK.ATTRIBUTES");
            DropForeignKey("REQ.REQUIREMENT", "ImportanceID", "PROJ.IMPORTANCE");
            DropForeignKey("PROJ.PROJECT_ARTIFACT", "ImportanceID", "PROJ.IMPORTANCE");
            DropForeignKey("ART.HISTORY_PROJECT_ARTIFACT", "ProjectArtifactID", "PROJ.PROJECT_ARTIFACT");
            DropForeignKey("PROJ.PROJECT_ARTIFACT", "ArtifactTypeID", "ART.ARTIFACT_TYPE");
            DropIndex("TASK.SUBTASK", new[] { "TaskType_TypeTaskID" });
            DropIndex("TASK.SUBTASK", new[] { "StatusTask_TaskStatusID" });
            DropIndex("TASK.SUBTASK", new[] { "code" });
            DropIndex("TASK.SUBTASK", new[] { "UserTaskID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementTemplateID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementStatusID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "ImportanceID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementSubTypeID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementTypeID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementRequestForChangesID" });
            DropIndex("REQ.REQUIREMENT_STATUS", new[] { "description" });
            DropIndex("REQ.CHARACTERISTICS", new[] { "name" });
            DropIndex("REQ.REQUIREMENT_CHARACTERISTICS", "IX_REQUIREMENT_CHARACTERISTICS");
            DropIndex("ACCESS.CONTROLLER_ACTION", "IX_CONTROLLER_ACTION");
            DropIndex("ACCESS.ROLE_CONTROLLER_ACTION", "IX_ROLE_CONTROLLER_ACTION");
            DropIndex("ACCESS.ROLE", new[] { "name" });
            DropIndex("ACCESS.USER_ROLE", "IX_USER_ROLE");
            DropIndex("REQ.REQUIREMENT_SUB_TYPE", new[] { "description" });
            DropIndex("REQ.REQUIREMENT_SUB_TYPE", new[] { "RequirementTypeID" });
            DropIndex("REQ.REQUIREMENT_TYPE", new[] { "description" });
            DropIndex("REQ.REQUIREMENT_TYPE", new[] { "abbreviation" });
            DropIndex("REQ.REQUIREMENT_TEMPLATE", new[] { "RequirementTypeID" });
            DropIndex("REQ.REQUIREMENT_TEMPLATE", new[] { "CreationUserID" });
            DropIndex("TASK.USER_TASK", "IX_USER_TASK");
            DropIndex("TASK.TASK_TYPE_TEMPLATE", new[] { "TaskType_TypeTaskID" });
            DropIndex("TASK.TASK_TYPE_TEMPLATE", new[] { "CreationUserID" });
            DropIndex("TASK.TASK_TYPE", new[] { "description" });
            DropIndex("TASK.STATUS_TASK", new[] { "description" });
            DropIndex("TASK.TASK", new[] { "TaskType_TypeTaskID" });
            DropIndex("TASK.TASK", new[] { "StatusTask_TaskStatusID" });
            DropIndex("TASK.TASK", new[] { "code" });
            DropIndex("TASK.TASK", new[] { "TaskTypeTemplateID" });
            DropIndex("TASK.TASK", new[] { "ImportanceID" });
            DropIndex("TASK.TASK", new[] { "CreationUserID" });
            DropIndex("TASK.HISTORY_TASK", new[] { "TaskID" });
            DropIndex("TASK.HISTORY_TASK", new[] { "CreationUserID" });
            DropIndex("PROJ.STAKEHOLDER_CLASSIFICATION", new[] { "description" });
            DropIndex("PROJ.STAKEHOLDERS", "IX_STAKEHOLDERS");
            DropIndex("REQ.STAKEHOLDER_REQUIREMENT_APPROVAL", new[] { "StakeholderRequirementID" });
            DropIndex("REQ.REQUEST_STATUS", new[] { "description" });
            DropIndex("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", new[] { "RequestStatusID" });
            DropIndex("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", new[] { "StakeholderRequirementID" });
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", new[] { "RequirementID" });
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", new[] { "StakeholdersProjectID" });
            DropIndex("PROJ.STAKEHOLDERS_PROJECT", "IX_STAKEHOLDERS_PROJECT");
            DropIndex("PROJ.PROJECT_PHASES", new[] { "description" });
            DropIndex("PROJ.PROJECT", new[] { "code" });
            DropIndex("PROJ.PROJECT", new[] { "ProjectPhasesID" });
            DropIndex("PROJ.PROJECT", new[] { "CreationUserID" });
            DropIndex("PROJ.HISTORY_PROJECT", new[] { "ProjectID" });
            DropIndex("PROJ.HISTORY_PROJECT", new[] { "CreationUserID" });
            DropIndex("ACCESS.USERS", new[] { "document" });
            DropIndex("ACCESS.USERS", new[] { "login" });
            DropIndex("ACCESS.USERS", new[] { "email" });
            DropIndex("LINK.LINK_REQUIREMENT_ATTRIBUTES", new[] { "LinkRequirementsID" });
            DropIndex("LINK.LINK_REQUIREMENT_ATTRIBUTES", new[] { "AttributeID" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "code" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", "IX_LINK_BETWEEN_REQUIREMENT");
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "TypeLinkID" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "CreationUserID" });
            DropIndex("LINK.TYPE_LINK", new[] { "description" });
            DropIndex("LINK.TYPE_LINK", new[] { "CreationUserID" });
            DropIndex("LINK.ATTRIBUTES_TYPE_LINK", "IX_attribute_type");
            DropIndex("LINK.ATTRIBUTES", new[] { "description" });
            DropIndex("LINK.LINK_ARTIFACT_ATTRIBUTES", new[] { "LinkArtifactRequirementID" });
            DropIndex("LINK.LINK_ARTIFACT_ATTRIBUTES", new[] { "AttributeID" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", new[] { "code" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "IX_artifact_requirement");
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", new[] { "CreationUserID" });
            DropIndex("REQ.REQUIREMENT", new[] { "code" });
            DropIndex("REQ.REQUIREMENT", new[] { "ImportanceID" });
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementSubTypeID" });
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementTypeID" });
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementStatusID" });
            DropIndex("REQ.REQUIREMENT", new[] { "CreationUserID" });
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementTemplateID" });
            DropIndex("REQ.REQUIREMENT", new[] { "ProjectID" });
            DropIndex("REQ.REQUIREMENT", "IX_REQUIREMENT");
            DropIndex("PROJ.IMPORTANCE", new[] { "description" });
            DropIndex("ART.HISTORY_PROJECT_ARTIFACT", new[] { "ProjectArtifactID" });
            DropIndex("PROJ.PROJECT_ARTIFACT", new[] { "code" });
            DropIndex("PROJ.PROJECT_ARTIFACT", new[] { "ProjectID" });
            DropIndex("PROJ.PROJECT_ARTIFACT", new[] { "ImportanceID" });
            DropIndex("PROJ.PROJECT_ARTIFACT", new[] { "ArtifactTypeID" });
            DropIndex("PROJ.PROJECT_ARTIFACT", new[] { "CreationUserID" });
            DropIndex("ART.ARTIFACT_TYPE", new[] { "description" });
            DropTable("TASK.SUBTASK");
            DropTable("REQ.REQUIREMENT_VERSIONS");
            DropTable("REQ.REQUIREMENT_STATUS");
            DropTable("REQ.CHARACTERISTICS");
            DropTable("REQ.REQUIREMENT_CHARACTERISTICS");
            DropTable("ACCESS.CONTROLLER_ACTION");
            DropTable("ACCESS.ROLE_CONTROLLER_ACTION");
            DropTable("ACCESS.ROLE");
            DropTable("ACCESS.USER_ROLE");
            DropTable("REQ.REQUIREMENT_SUB_TYPE");
            DropTable("REQ.REQUIREMENT_TYPE");
            DropTable("REQ.REQUIREMENT_TEMPLATE");
            DropTable("TASK.USER_TASK");
            DropTable("TASK.TASK_TYPE_TEMPLATE");
            DropTable("TASK.TASK_TYPE");
            DropTable("TASK.STATUS_TASK");
            DropTable("TASK.TASK");
            DropTable("TASK.HISTORY_TASK");
            DropTable("PROJ.STAKEHOLDER_CLASSIFICATION");
            DropTable("PROJ.STAKEHOLDERS");
            DropTable("REQ.STAKEHOLDER_REQUIREMENT_APPROVAL");
            DropTable("REQ.REQUEST_STATUS");
            DropTable("REQ.REQUIREMENT_REQUEST_FOR_CHANGES");
            DropTable("PROJ.STAKEHOLDER_REQUIREMENT");
            DropTable("PROJ.STAKEHOLDERS_PROJECT");
            DropTable("PROJ.PROJECT_PHASES");
            DropTable("PROJ.PROJECT");
            DropTable("PROJ.HISTORY_PROJECT");
            DropTable("ACCESS.USERS");
            DropTable("LINK.LINK_REQUIREMENT_ATTRIBUTES");
            DropTable("LINK.LINK_BETWEEN_REQUIREMENT");
            DropTable("LINK.TYPE_LINK");
            DropTable("LINK.ATTRIBUTES_TYPE_LINK");
            DropTable("LINK.ATTRIBUTES");
            DropTable("LINK.LINK_ARTIFACT_ATTRIBUTES");
            DropTable("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS");
            DropTable("REQ.REQUIREMENT");
            DropTable("PROJ.IMPORTANCE");
            DropTable("ART.HISTORY_PROJECT_ARTIFACT");
            DropTable("PROJ.PROJECT_ARTIFACT");
            DropTable("ART.ARTIFACT_TYPE");
        }
    }
}
