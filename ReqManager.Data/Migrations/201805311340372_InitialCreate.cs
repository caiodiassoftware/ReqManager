namespace ReqManager.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
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
                        rationale = c.String(maxLength: 1000),
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
                .Index(t => new { t.RequirementOriginID, t.RequirementTargetID, t.TypeLinkID }, unique: true, name: "IX_LINK_BETWEEN_REQUIREMENT")
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
                .Index(t => new { t.StakeholdersProjectID, t.RequirementID }, unique: true, name: "IX_STAKEHOLDER_REQUIREMENT");
            
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

            #region Insert Default Data

            //Login: admin
            //Password: 123456
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

            #region Roles

            Sql(@"SET IDENTITY_INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ON
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1, 1, 1)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (2, 1, 2)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (3, 1, 3)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (4, 1, 4)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (5, 1, 5)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (6, 1, 6)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (7, 1, 7)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (8, 1, 8)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (9, 1, 9)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (10, 1, 10)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (11, 1, 11)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (12, 1, 12)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (13, 1, 13)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (14, 1, 14)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (15, 1, 15)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (16, 1, 16)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (17, 1, 17)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (18, 1, 18)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (19, 1, 19)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (20, 1, 20)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (21, 1, 21)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (22, 1, 22)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (23, 1, 23)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (24, 1, 24)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (25, 1, 25)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (26, 1, 26)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (27, 1, 27)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (28, 1, 28)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (29, 1, 29)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (30, 1, 30)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (31, 1, 31)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (32, 1, 32)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (33, 1, 33)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (34, 1, 34)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (35, 1, 35)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (36, 1, 36)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (37, 1, 37)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (38, 1, 38)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (39, 1, 39)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (40, 1, 40)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (41, 1, 41)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (42, 1, 42)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (43, 1, 43)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (44, 1, 44)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (45, 1, 45)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (46, 1, 46)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (47, 1, 47)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (48, 1, 48)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (49, 1, 49)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (50, 1, 50)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (51, 1, 51)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (52, 1, 52)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (53, 1, 53)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (54, 1, 54)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (55, 1, 55)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (56, 1, 56)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (57, 1, 57)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (58, 1, 58)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (59, 1, 59)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (60, 1, 60)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (61, 1, 61)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (62, 1, 62)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (63, 1, 63)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (64, 1, 64)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (65, 1, 65)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (66, 1, 66)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (67, 1, 67)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (68, 1, 68)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (69, 1, 69)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (70, 1, 70)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (71, 1, 71)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (72, 1, 72)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (73, 1, 73)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (74, 1, 74)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (75, 1, 75)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (76, 1, 76)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (77, 1, 77)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (78, 1, 78)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (79, 1, 79)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (80, 1, 80)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (81, 1, 81)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (82, 1, 82)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (83, 1, 83)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (84, 1, 84)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (85, 1, 85)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (86, 1, 86)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (87, 1, 87)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (88, 1, 88)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (89, 1, 89)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (90, 1, 90)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (91, 1, 91)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (92, 1, 92)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (93, 1, 93)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (94, 1, 94)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (95, 1, 95)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (96, 1, 96)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (97, 1, 97)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (98, 1, 98)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (99, 1, 99)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (100, 1, 100)
                GO
                print 'Processed 100 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (101, 1, 101)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (102, 1, 102)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (103, 1, 103)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (104, 1, 104)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (105, 1, 105)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (106, 1, 106)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (107, 1, 107)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (108, 1, 108)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (109, 1, 109)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (110, 1, 110)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (111, 1, 111)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (112, 1, 112)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (113, 1, 113)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (114, 1, 114)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (115, 1, 115)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (116, 1, 116)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (117, 1, 117)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (118, 1, 118)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (119, 1, 119)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (120, 1, 120)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (121, 1, 121)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (122, 1, 122)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (123, 1, 123)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (124, 1, 124)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (125, 1, 125)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (126, 1, 126)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (127, 1, 127)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (128, 1, 128)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (129, 1, 129)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (130, 1, 130)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (131, 1, 131)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (132, 1, 132)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (133, 1, 133)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (134, 1, 134)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (135, 1, 135)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (136, 1, 136)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (137, 1, 137)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (138, 1, 138)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (139, 1, 139)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (140, 1, 140)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (141, 1, 141)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (142, 1, 142)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (143, 1, 143)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (144, 1, 144)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (145, 1, 145)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (146, 1, 146)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (147, 1, 147)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (148, 1, 148)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (149, 1, 149)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (150, 1, 150)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (151, 1, 151)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (152, 1, 152)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (153, 1, 153)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (154, 1, 154)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (155, 1, 155)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (156, 1, 156)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (157, 1, 157)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (158, 1, 158)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (159, 1, 159)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (160, 1, 160)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (161, 1, 161)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (162, 1, 162)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (163, 1, 163)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (164, 1, 164)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (165, 1, 165)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (166, 1, 166)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (167, 1, 167)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (168, 1, 168)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (169, 1, 169)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (170, 1, 170)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (171, 1, 171)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (172, 1, 172)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (173, 1, 173)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (174, 1, 174)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (175, 1, 175)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (176, 1, 176)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (177, 1, 177)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (178, 1, 178)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (179, 1, 179)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (180, 1, 180)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (181, 1, 181)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (182, 1, 182)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (183, 1, 183)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (184, 1, 184)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (185, 1, 185)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (186, 1, 186)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (187, 1, 187)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (188, 1, 188)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (189, 1, 189)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (190, 1, 190)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (191, 1, 191)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (192, 1, 192)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (193, 1, 193)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (194, 1, 194)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (195, 1, 195)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (196, 1, 196)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (197, 1, 197)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (198, 1, 198)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (199, 1, 199)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (200, 1, 200)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (201, 1, 201)
                GO
                print 'Processed 200 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (202, 1, 202)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (203, 1, 203)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (204, 1, 204)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (205, 1, 205)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (206, 1, 206)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (207, 1, 207)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (208, 1, 208)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (209, 1, 209)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (210, 1, 210)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (211, 1, 211)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (212, 1, 212)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (213, 1, 213)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (214, 1, 214)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (215, 1, 215)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (216, 1, 216)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (217, 1, 217)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (218, 1, 218)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (219, 1, 219)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (220, 1, 220)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (221, 1, 221)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (222, 1, 222)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (223, 1, 223)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (224, 1, 224)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (225, 1, 225)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (226, 1, 226)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (227, 1, 227)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (228, 1, 228)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (229, 1, 229)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (230, 1, 230)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (231, 1, 231)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (232, 1, 232)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (233, 1, 233)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (234, 1, 234)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (235, 1, 235)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (236, 1, 236)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (237, 1, 237)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (238, 1, 238)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (239, 1, 239)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (240, 1, 240)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (241, 1, 241)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (242, 1, 242)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (243, 1, 243)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (244, 1, 244)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (245, 1, 245)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (246, 1, 246)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (247, 1, 247)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (248, 1, 248)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (249, 1, 249)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (250, 1, 250)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (251, 1, 251)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (252, 1, 252)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (253, 1, 253)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (254, 1, 254)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (255, 1, 255)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (256, 1, 256)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (257, 1, 257)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (258, 1, 258)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (259, 1, 259)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (260, 1, 260)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (261, 1, 261)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (262, 1, 262)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (263, 1, 263)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (264, 1, 264)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (265, 1, 265)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (266, 1, 266)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (267, 1, 267)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (268, 1, 268)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (269, 1, 269)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (270, 1, 270)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (271, 1, 271)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (272, 1, 272)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (273, 1, 273)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (274, 1, 274)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (275, 1, 275)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (276, 1, 276)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (277, 1, 277)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (278, 1, 278)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (279, 1, 279)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (280, 1, 280)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (281, 1, 281)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (282, 1, 282)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (283, 1, 283)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (284, 1, 284)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (285, 1, 285)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (286, 1, 286)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (287, 1, 287)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (288, 1, 288)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (289, 1, 289)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (290, 1, 290)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (291, 1, 291)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (292, 1, 292)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (293, 1, 293)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (294, 1, 294)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (295, 1, 295)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (296, 1, 296)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (297, 1, 297)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (298, 1, 298)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (299, 1, 299)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (300, 1, 300)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (301, 1, 301)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (302, 1, 302)
                GO
                print 'Processed 300 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (303, 1, 303)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (304, 1, 304)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (305, 1, 305)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (306, 1, 306)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (307, 1, 307)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (308, 1, 308)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (309, 1, 309)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (310, 1, 310)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (311, 1, 311)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (312, 1, 312)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (313, 1, 313)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (314, 1, 314)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (315, 1, 315)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (316, 1, 316)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (317, 1, 317)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (318, 1, 318)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (319, 1, 319)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (320, 1, 320)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (321, 1, 321)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (322, 1, 322)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (323, 1, 323)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (324, 1, 324)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (325, 1, 325)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (326, 1, 326)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (327, 1, 327)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (328, 1, 328)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (329, 1, 329)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (330, 1, 330)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (678, 2, 1)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (679, 2, 2)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (680, 2, 7)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (681, 2, 8)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (682, 2, 9)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (683, 2, 10)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (684, 2, 11)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (685, 2, 16)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (686, 2, 17)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (687, 2, 18)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (688, 2, 19)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (689, 2, 20)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (690, 2, 25)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (691, 2, 26)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (692, 2, 27)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (693, 2, 28)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (694, 2, 29)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (695, 2, 34)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (696, 2, 35)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (697, 2, 36)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (698, 2, 38)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (699, 2, 39)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (700, 2, 40)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (701, 2, 45)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (702, 2, 46)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (703, 2, 47)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (704, 2, 48)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (705, 2, 49)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (706, 2, 54)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (707, 2, 55)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (708, 2, 56)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (709, 2, 58)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (710, 2, 59)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (711, 2, 64)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (712, 2, 65)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (713, 2, 66)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (714, 2, 68)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (715, 2, 70)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (716, 2, 71)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (717, 2, 72)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (718, 2, 77)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (719, 2, 78)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (720, 2, 79)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (721, 2, 80)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (722, 2, 81)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (723, 2, 83)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (724, 2, 84)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (725, 2, 85)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (726, 2, 90)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (727, 2, 91)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (728, 2, 92)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (729, 2, 93)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (730, 2, 95)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (731, 2, 96)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (732, 2, 97)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (733, 2, 98)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (734, 2, 103)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (735, 2, 104)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (736, 2, 105)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (737, 2, 107)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (738, 2, 108)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (739, 2, 109)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (740, 2, 114)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (741, 2, 115)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (742, 2, 116)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (743, 2, 120)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (744, 2, 121)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (745, 2, 126)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (746, 2, 127)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (747, 2, 128)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (748, 2, 129)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (749, 2, 130)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (750, 2, 132)
                GO
                print 'Processed 400 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (751, 2, 133)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (752, 2, 137)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (753, 2, 138)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (754, 2, 139)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (755, 2, 140)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (756, 2, 141)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (757, 2, 146)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (758, 2, 147)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (759, 2, 148)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (760, 2, 150)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (761, 2, 151)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (762, 2, 152)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (763, 2, 154)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (764, 2, 158)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (765, 2, 159)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (766, 2, 160)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (767, 2, 161)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (768, 2, 162)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (769, 2, 167)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (770, 2, 168)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (771, 2, 169)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (772, 2, 174)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (773, 2, 175)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (774, 2, 179)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (775, 2, 180)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (776, 2, 181)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (777, 2, 182)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (778, 2, 183)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (779, 2, 185)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (780, 2, 186)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (781, 2, 191)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (782, 2, 192)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (783, 2, 193)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (784, 2, 196)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (785, 2, 197)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (786, 2, 202)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (787, 2, 203)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (788, 2, 204)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (789, 2, 205)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (790, 2, 206)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (791, 2, 211)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (792, 2, 212)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (793, 2, 213)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (794, 2, 214)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (795, 2, 215)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (796, 2, 216)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (797, 2, 221)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (798, 2, 222)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (799, 2, 223)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (800, 2, 224)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (801, 2, 225)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (802, 2, 226)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (803, 2, 227)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (804, 2, 232)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (805, 2, 233)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (806, 2, 234)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (807, 2, 235)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (808, 2, 236)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (809, 2, 241)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (810, 2, 242)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (811, 2, 243)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (812, 2, 244)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (813, 2, 245)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (814, 2, 250)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (815, 2, 251)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (816, 2, 252)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (817, 2, 253)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (818, 2, 254)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (819, 2, 259)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (820, 2, 260)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (821, 2, 261)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (822, 2, 263)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (823, 2, 264)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (824, 2, 269)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (825, 2, 270)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (826, 2, 271)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (827, 2, 274)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (828, 2, 276)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (829, 2, 277)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (830, 2, 280)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (831, 2, 281)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (832, 2, 282)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (833, 2, 285)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (834, 2, 286)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (835, 2, 287)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (836, 2, 291)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (837, 2, 292)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (838, 2, 293)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (839, 2, 304)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (840, 2, 305)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (841, 2, 310)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (842, 2, 311)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (843, 2, 312)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (844, 2, 313)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (845, 2, 314)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (846, 2, 319)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (847, 2, 320)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (848, 2, 321)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (849, 2, 322)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (850, 2, 323)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (851, 2, 328)
                GO
                print 'Processed 500 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (852, 2, 329)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (853, 2, 330)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (854, 3, 1)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (855, 3, 2)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (856, 3, 7)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (857, 3, 8)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (858, 3, 9)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (859, 3, 10)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (860, 3, 11)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (861, 3, 16)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (862, 3, 17)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (863, 3, 18)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (864, 3, 19)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (865, 3, 20)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (866, 3, 25)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (867, 3, 26)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (868, 3, 27)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (869, 3, 28)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (870, 3, 29)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (871, 3, 34)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (872, 3, 35)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (873, 3, 36)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (874, 3, 38)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (875, 3, 39)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (876, 3, 40)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (877, 3, 45)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (878, 3, 46)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (879, 3, 47)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (880, 3, 48)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (881, 3, 49)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (882, 3, 54)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (883, 3, 55)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (884, 3, 56)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (885, 3, 58)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (886, 3, 59)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (887, 3, 64)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (888, 3, 65)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (889, 3, 66)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (890, 3, 68)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (891, 3, 70)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (892, 3, 71)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (893, 3, 72)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (894, 3, 77)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (895, 3, 78)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (896, 3, 79)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (897, 3, 80)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (898, 3, 81)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (899, 3, 83)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (900, 3, 84)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (901, 3, 85)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (902, 3, 90)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (903, 3, 91)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (904, 3, 92)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (905, 3, 93)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (906, 3, 95)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (907, 3, 96)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (908, 3, 97)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (909, 3, 98)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (910, 3, 103)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (911, 3, 104)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (912, 3, 105)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (913, 3, 107)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (914, 3, 108)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (915, 3, 109)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (916, 3, 114)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (917, 3, 115)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (918, 3, 116)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (919, 3, 120)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (920, 3, 121)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (921, 3, 126)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (922, 3, 127)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (923, 3, 128)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (924, 3, 129)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (925, 3, 130)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (926, 3, 132)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (927, 3, 133)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (928, 3, 137)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (929, 3, 138)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (930, 3, 139)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (931, 3, 140)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (932, 3, 141)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (933, 3, 146)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (934, 3, 147)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (935, 3, 148)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (936, 3, 150)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (937, 3, 151)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (938, 3, 152)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (939, 3, 154)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (940, 3, 158)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (941, 3, 159)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (942, 3, 160)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (943, 3, 161)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (944, 3, 162)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (945, 3, 167)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (946, 3, 168)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (947, 3, 169)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (948, 3, 174)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (949, 3, 175)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (950, 3, 179)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (951, 3, 180)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (952, 3, 181)
                GO
                print 'Processed 600 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (953, 3, 182)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (954, 3, 183)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (955, 3, 185)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (956, 3, 186)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (957, 3, 191)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (958, 3, 192)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (959, 3, 193)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (960, 3, 196)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (961, 3, 197)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (962, 3, 202)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (963, 3, 203)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (964, 3, 204)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (965, 3, 205)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (966, 3, 206)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (967, 3, 211)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (968, 3, 212)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (969, 3, 213)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (970, 3, 214)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (971, 3, 215)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (972, 3, 216)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (973, 3, 221)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (974, 3, 222)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (975, 3, 223)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (976, 3, 224)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (977, 3, 225)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (978, 3, 226)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (979, 3, 227)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (980, 3, 232)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (981, 3, 233)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (982, 3, 234)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (983, 3, 235)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (984, 3, 236)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (985, 3, 241)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (986, 3, 242)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (987, 3, 243)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (988, 3, 244)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (989, 3, 245)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (990, 3, 250)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (991, 3, 251)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (992, 3, 252)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (993, 3, 253)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (994, 3, 254)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (995, 3, 259)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (996, 3, 260)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (997, 3, 261)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (998, 3, 263)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (999, 3, 264)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1000, 3, 269)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1001, 3, 270)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1002, 3, 271)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1003, 3, 274)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1004, 3, 276)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1005, 3, 277)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1006, 3, 280)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1007, 3, 281)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1008, 3, 282)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1009, 3, 285)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1010, 3, 286)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1011, 3, 287)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1012, 3, 291)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1013, 3, 292)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1014, 3, 293)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1015, 3, 304)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1016, 3, 305)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1017, 3, 310)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1018, 3, 311)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1019, 3, 312)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1020, 3, 313)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1021, 3, 314)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1022, 3, 319)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1023, 3, 320)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1024, 3, 321)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1025, 3, 322)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1026, 3, 323)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1027, 3, 328)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1028, 3, 329)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1029, 3, 330)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1030, 4, 1)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1031, 4, 2)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1032, 4, 7)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1033, 4, 8)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1034, 4, 9)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1035, 4, 10)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1036, 4, 11)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1037, 4, 16)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1038, 4, 17)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1039, 4, 18)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1040, 4, 19)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1041, 4, 20)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1042, 4, 25)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1043, 4, 26)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1044, 4, 27)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1045, 4, 28)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1046, 4, 29)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1047, 4, 34)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1048, 4, 35)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1049, 4, 36)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1050, 4, 38)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1051, 4, 39)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1052, 4, 40)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1053, 4, 45)
                GO
                print 'Processed 700 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1054, 4, 46)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1055, 4, 47)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1056, 4, 48)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1057, 4, 49)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1058, 4, 54)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1059, 4, 55)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1060, 4, 56)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1061, 4, 58)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1062, 4, 59)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1063, 4, 64)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1064, 4, 65)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1065, 4, 66)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1066, 4, 68)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1067, 4, 70)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1068, 4, 71)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1069, 4, 72)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1070, 4, 77)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1071, 4, 78)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1072, 4, 79)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1073, 4, 80)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1074, 4, 81)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1075, 4, 83)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1076, 4, 84)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1077, 4, 85)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1078, 4, 90)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1079, 4, 91)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1080, 4, 92)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1081, 4, 93)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1082, 4, 95)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1083, 4, 96)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1084, 4, 97)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1085, 4, 98)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1086, 4, 103)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1087, 4, 104)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1088, 4, 105)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1089, 4, 107)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1090, 4, 108)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1091, 4, 109)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1092, 4, 114)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1093, 4, 115)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1094, 4, 116)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1095, 4, 120)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1096, 4, 121)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1097, 4, 126)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1098, 4, 127)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1099, 4, 128)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1100, 4, 129)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1101, 4, 130)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1102, 4, 132)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1103, 4, 133)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1104, 4, 137)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1105, 4, 138)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1106, 4, 139)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1107, 4, 140)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1108, 4, 141)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1109, 4, 146)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1110, 4, 147)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1111, 4, 148)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1112, 4, 150)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1113, 4, 151)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1114, 4, 152)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1115, 4, 154)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1116, 4, 158)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1117, 4, 159)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1118, 4, 160)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1119, 4, 161)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1120, 4, 162)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1121, 4, 167)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1122, 4, 168)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1123, 4, 169)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1124, 4, 174)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1125, 4, 175)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1126, 4, 179)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1127, 4, 180)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1128, 4, 181)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1129, 4, 182)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1130, 4, 183)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1131, 4, 185)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1132, 4, 186)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1133, 4, 191)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1134, 4, 192)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1135, 4, 193)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1136, 4, 196)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1137, 4, 197)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1138, 4, 202)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1139, 4, 203)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1140, 4, 204)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1141, 4, 205)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1142, 4, 206)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1143, 4, 211)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1144, 4, 212)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1145, 4, 213)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1146, 4, 214)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1147, 4, 215)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1148, 4, 216)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1149, 4, 221)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1150, 4, 222)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1151, 4, 223)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1152, 4, 224)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1153, 4, 225)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1154, 4, 226)
                GO
                print 'Processed 800 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1155, 4, 227)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1156, 4, 232)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1157, 4, 233)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1158, 4, 234)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1159, 4, 235)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1160, 4, 236)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1161, 4, 241)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1162, 4, 242)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1163, 4, 243)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1164, 4, 244)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1165, 4, 245)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1166, 4, 250)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1167, 4, 251)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1168, 4, 252)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1169, 4, 253)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1170, 4, 254)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1171, 4, 259)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1172, 4, 260)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1173, 4, 261)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1174, 4, 263)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1175, 4, 264)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1176, 4, 269)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1177, 4, 270)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1178, 4, 271)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1179, 4, 274)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1180, 4, 276)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1181, 4, 277)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1182, 4, 280)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1183, 4, 281)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1184, 4, 282)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1185, 4, 285)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1186, 4, 286)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1187, 4, 287)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1188, 4, 291)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1189, 4, 292)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1190, 4, 293)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1191, 4, 304)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1192, 4, 305)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1193, 4, 310)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1194, 4, 311)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1195, 4, 312)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1196, 4, 313)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1197, 4, 314)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1198, 4, 319)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1199, 4, 320)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1200, 4, 321)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1201, 4, 322)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1202, 4, 323)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1203, 4, 328)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1204, 4, 329)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1205, 4, 330)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1206, 2, 67)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1207, 3, 67)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1208, 4, 67)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1210, 3, 69)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1211, 3, 73)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1212, 3, 74)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1213, 3, 75)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1214, 3, 76)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1215, 3, 82)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1216, 3, 86)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1217, 3, 87)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1218, 3, 88)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1219, 3, 89)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1220, 3, 94)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1221, 3, 99)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1222, 3, 100)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1223, 3, 101)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1224, 3, 102)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1225, 3, 106)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1226, 3, 110)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1227, 3, 111)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1228, 3, 112)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1229, 3, 113)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1230, 3, 117)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1231, 3, 118)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1232, 3, 119)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1233, 3, 131)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1234, 3, 134)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1235, 3, 135)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1236, 3, 136)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1237, 3, 149)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1238, 3, 153)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1239, 3, 155)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1240, 3, 156)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1241, 3, 157)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1242, 3, 163)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1243, 3, 164)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1244, 3, 165)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1245, 3, 166)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1246, 3, 170)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1247, 3, 171)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1248, 3, 172)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1249, 3, 173)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1250, 3, 176)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1251, 3, 177)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1252, 3, 178)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1253, 3, 184)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1254, 3, 187)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1255, 3, 188)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1256, 3, 189)
                GO
                print 'Processed 900 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1257, 3, 190)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1258, 3, 194)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1259, 3, 195)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1260, 3, 198)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1261, 3, 199)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1262, 3, 200)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1263, 3, 201)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1264, 3, 262)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1265, 3, 265)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1266, 3, 266)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1267, 3, 267)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1268, 3, 268)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1269, 3, 272)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1270, 3, 273)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1271, 3, 275)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1272, 3, 278)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1273, 3, 279)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1274, 3, 283)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1275, 3, 284)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1276, 3, 288)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1277, 3, 289)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1278, 3, 290)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1279, 3, 294)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1280, 3, 295)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1281, 3, 296)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1282, 3, 297)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1283, 3, 298)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1284, 3, 299)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1285, 3, 300)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1286, 3, 301)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1287, 3, 302)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1288, 3, 303)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1289, 4, 69)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1290, 4, 73)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1291, 4, 74)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1292, 4, 75)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1293, 4, 76)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1294, 4, 82)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1295, 4, 86)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1296, 4, 87)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1297, 4, 88)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1298, 4, 89)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1299, 4, 94)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1300, 4, 99)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1301, 4, 100)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1302, 4, 101)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1303, 4, 102)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1304, 4, 106)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1305, 4, 110)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1306, 4, 111)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1307, 4, 112)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1308, 4, 113)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1309, 4, 117)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1310, 4, 118)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1311, 4, 119)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1312, 4, 131)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1313, 4, 134)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1314, 4, 135)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1315, 4, 136)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1316, 4, 149)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1317, 4, 153)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1318, 4, 155)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1319, 4, 156)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1320, 4, 157)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1321, 4, 163)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1322, 4, 164)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1323, 4, 165)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1324, 4, 166)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1325, 4, 170)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1326, 4, 171)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1327, 4, 172)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1328, 4, 173)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1329, 4, 176)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1330, 4, 177)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1331, 4, 178)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1332, 4, 184)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1333, 4, 187)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1334, 4, 188)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1335, 4, 189)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1336, 4, 190)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1337, 4, 194)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1338, 4, 195)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1339, 4, 198)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1340, 4, 199)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1341, 4, 200)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1342, 4, 201)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1343, 4, 262)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1344, 4, 265)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1345, 4, 266)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1346, 4, 267)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1347, 4, 268)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1348, 4, 272)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1349, 4, 273)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1350, 4, 275)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1351, 4, 278)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1352, 4, 279)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1353, 4, 283)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1354, 4, 284)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1355, 4, 288)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1356, 4, 289)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1357, 4, 290)
                GO
                print 'Processed 1000 total records'
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1358, 4, 294)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1359, 4, 295)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1360, 4, 296)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1361, 4, 297)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1362, 4, 298)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1363, 4, 299)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1364, 4, 300)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1365, 4, 301)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1366, 4, 302)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1367, 4, 303)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1368, 2, 117)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1369, 2, 118)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1370, 2, 119)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1371, 2, 194)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1372, 2, 195)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1373, 2, 198)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1374, 2, 199)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1375, 2, 200)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1376, 2, 201)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1377, 2, 262)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1378, 2, 265)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1379, 2, 266)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1380, 2, 267)
                INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] ([RoleControllerActionID], [RoleID], [ControllerActionID]) VALUES (1381, 2, 268)
                SET IDENTITY_INSERT [ACCESS].[ROLE_CONTROLLER_ACTION] OFF");

            #endregion

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
            #region Drop Tables and Constraints

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
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", "IX_STAKEHOLDER_REQUIREMENT");
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

            #endregion
        }
    }
}
