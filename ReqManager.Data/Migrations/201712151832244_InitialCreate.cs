namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                        RequirementTemplateID = c.Int(nullable: false),
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
                    })
                .PrimaryKey(t => t.RequirementID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("PROJ.PROJECT", t => t.ProjectID)
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                "ACESS.USERS",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        nickName = c.String(nullable: false, maxLength: 10),
                        password = c.String(maxLength: 1000),
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("PROJ.PROJECT_PHASES", t => t.ProjectPhasesID)
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                        description = c.String(nullable: false, maxLength: 255),
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
                .ForeignKey("ACESS.USERS", t => t.UserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
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
                .ForeignKey("ACESS.USERS", t => t.UserID)
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
                .ForeignKey("ACESS.USERS", t => t.CreationUserID)
                .Index(t => t.CreationUserID)
                .Index(t => t.RequirementTypeID, unique: true);
            
            CreateTable(
                "REQ.REQUIREMENT_TYPE",
                c => new
                    {
                        RequirementTypeID = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RequirementTypeID)
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
                "ACESS.USER_ROLE",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("ACESS.ROLE", t => t.RoleID)
                .ForeignKey("ACESS.USERS", t => t.UserID)
                .Index(t => new { t.RoleID, t.UserID }, unique: true, name: "IX_USER_ROLE");
            
            CreateTable(
                "ACESS.ROLE",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.RoleID)
                .Index(t => t.name, unique: true);
            
            CreateTable(
                "ACESS.ROLE_CONTROLLER_ACTION",
                c => new
                    {
                        RoleControllerActionID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        ControllerActionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleControllerActionID)
                .ForeignKey("ACESS.CONTROLLER_ACTION", t => t.ControllerActionID)
                .ForeignKey("ACESS.ROLE", t => t.RoleID)
                .Index(t => new { t.RoleID, t.ControllerActionID }, unique: true, name: "IX_ROLE_CONTROLLER_ACTION");
            
            CreateTable(
                "ACESS.CONTROLLER_ACTION",
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
                        active = c.Boolean(nullable: false),
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
                        description = c.String(nullable: false, maxLength: 255),
                        active = c.Boolean(nullable: false),
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
                        RequirementRequestForChangesID = c.Int(nullable: false),
                        RequirementTypeID = c.Int(nullable: false),
                        RequirementSubTypeID = c.Int(),
                        ImportanceID = c.Int(nullable: false),
                        RequirementStatusID = c.Int(nullable: false),
                        RequirementTemplateID = c.Int(nullable: false),
                        versionNumber = c.Int(nullable: false),
                        title = c.String(nullable: false, maxLength: 100),
                        description = c.String(nullable: false),
                        rationale = c.String(nullable: false, maxLength: 1000),
                        creationDate = c.DateTime(nullable: false),
                        startDate = c.DateTime(),
                        endDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RequirementVersionsID)
                .ForeignKey("PROJ.IMPORTANCE", t => t.ImportanceID)
                .ForeignKey("REQ.REQUIREMENT_REQUEST_FOR_CHANGES", t => t.RequirementRequestForChangesID)
                .ForeignKey("REQ.REQUIREMENT_STATUS", t => t.RequirementStatusID)
                .ForeignKey("REQ.REQUIREMENT_SUB_TYPE", t => t.RequirementSubTypeID)
                .ForeignKey("REQ.REQUIREMENT_TEMPLATE", t => t.RequirementTemplateID)
                .ForeignKey("REQ.REQUIREMENT_TYPE", t => t.RequirementTypeID)
                .Index(t => new { t.RequirementRequestForChangesID, t.versionNumber }, unique: true, name: "IX_REQUIREMENT_VERSIONS")
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
            DropForeignKey("ACESS.USER_ROLE", "UserID", "ACESS.USERS");
            DropForeignKey("ACESS.USER_ROLE", "RoleID", "ACESS.ROLE");
            DropForeignKey("ACESS.ROLE_CONTROLLER_ACTION", "RoleID", "ACESS.ROLE");
            DropForeignKey("ACESS.ROLE_CONTROLLER_ACTION", "ControllerActionID", "ACESS.CONTROLLER_ACTION");
            DropForeignKey("LINK.TYPE_LINK", "CreationUserID", "ACESS.USERS");
            DropForeignKey("REQ.REQUIREMENT_TEMPLATE", "CreationUserID", "ACESS.USERS");
            DropForeignKey("REQ.REQUIREMENT_TEMPLATE", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT_SUB_TYPE", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementTypeID", "REQ.REQUIREMENT_TYPE");
            DropForeignKey("REQ.REQUIREMENT", "RequirementTemplateID", "REQ.REQUIREMENT_TEMPLATE");
            DropForeignKey("REQ.REQUIREMENT", "CreationUserID", "ACESS.USERS");
            DropForeignKey("PROJ.PROJECT_ARTIFACT", "CreationUserID", "ACESS.USERS");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENT", "CreationUserID", "ACESS.USERS");
            DropForeignKey("LINK.LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", "CreationUserID", "ACESS.USERS");
            DropForeignKey("TASK.HISTORY_TASK", "CreationUserID", "ACESS.USERS");
            DropForeignKey("TASK.USER_TASK", "UserID", "ACESS.USERS");
            DropForeignKey("TASK.USER_TASK", "TaskID", "TASK.TASK");
            DropForeignKey("TASK.TASK", "CreationUserID", "ACESS.USERS");
            DropForeignKey("TASK.TASK_TYPE_TEMPLATE", "CreationUserID", "ACESS.USERS");
            DropForeignKey("TASK.TASK_TYPE_TEMPLATE", "TaskType_TypeTaskID", "TASK.TASK_TYPE");
            DropForeignKey("TASK.TASK", "TaskTypeTemplateID", "TASK.TASK_TYPE_TEMPLATE");
            DropForeignKey("TASK.TASK", "TaskType_TypeTaskID", "TASK.TASK_TYPE");
            DropForeignKey("TASK.TASK", "StatusTask_TaskStatusID", "TASK.STATUS_TASK");
            DropForeignKey("TASK.TASK", "ImportanceID", "PROJ.IMPORTANCE");
            DropForeignKey("TASK.HISTORY_TASK", "TaskID", "TASK.TASK");
            DropForeignKey("PROJ.HISTORY_PROJECT", "CreationUserID", "ACESS.USERS");
            DropForeignKey("PROJ.PROJECT", "CreationUserID", "ACESS.USERS");
            DropForeignKey("PROJ.STAKEHOLDERS", "UserID", "ACESS.USERS");
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
            DropIndex("REQ.REQUIREMENT_VERSIONS", "IX_REQUIREMENT_VERSIONS");
            DropIndex("REQ.REQUIREMENT_STATUS", new[] { "description" });
            DropIndex("REQ.CHARACTERISTICS", new[] { "name" });
            DropIndex("REQ.REQUIREMENT_CHARACTERISTICS", "IX_REQUIREMENT_CHARACTERISTICS");
            DropIndex("ACESS.CONTROLLER_ACTION", "IX_CONTROLLER_ACTION");
            DropIndex("ACESS.ROLE_CONTROLLER_ACTION", "IX_ROLE_CONTROLLER_ACTION");
            DropIndex("ACESS.ROLE", new[] { "name" });
            DropIndex("ACESS.USER_ROLE", "IX_USER_ROLE");
            DropIndex("REQ.REQUIREMENT_SUB_TYPE", new[] { "description" });
            DropIndex("REQ.REQUIREMENT_SUB_TYPE", new[] { "RequirementTypeID" });
            DropIndex("REQ.REQUIREMENT_TYPE", new[] { "description" });
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
            DropIndex("ACESS.USERS", new[] { "document" });
            DropIndex("ACESS.USERS", new[] { "login" });
            DropIndex("ACESS.USERS", new[] { "email" });
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
            DropTable("ACESS.CONTROLLER_ACTION");
            DropTable("ACESS.ROLE_CONTROLLER_ACTION");
            DropTable("ACESS.ROLE");
            DropTable("ACESS.USER_ROLE");
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
            DropTable("ACESS.USERS");
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
