﻿using AutoMapper;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Extensions;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementService : ServiceBase<Requirement, RequirementEntity>, IRequirementService
    {
        private IRequirementVersionsService versionService { get; set; }
        private IRequirementRequestForChangesService requestService { get; set; }
        private IProjectService projectService { get; set; }
        private ICharacteristicsService characteristicsService { get; set; }
        private IRequirementCharacteristicsService requirementCharacteristicsService { get; set; }

        public RequirementService(
            IRequirementRepository repository,
            IProjectService projectService,
            IRequirementCharacteristicsService requirementCharacteristicsService,
            ICharacteristicsService characteristicsService,
            IRequirementRequestForChangesService requestService,
            IRequirementVersionsService versionService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.requirementCharacteristicsService = requirementCharacteristicsService;
            this.characteristicsService = characteristicsService;
            this.projectService = projectService;
            this.requestService = requestService;
            this.versionService = versionService;
        }

        public override void add(ref RequirementEntity entity, bool persistir = true)
        {
            try
            {
                if (checkProjectBalance(entity.ProjectID, entity.cost))
                {
                    unit.BeginTransaction();

                    ProjectEntity project = projectService.get(entity.ProjectID);
                    entity.preTraceability = projectService.isPreTraceability(project);
                    entity.versionNumber = 1;
                    entity.active = true;

                    if (entity.RequirementTemplateID.Equals(0))
                        entity.RequirementTemplateID = null;
                    if (entity.RequirementSubTypeID.Equals(0))
                        entity.RequirementSubTypeID = null;

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateAutomaticMapping<RequirementEntity, RequirementVersionsEntity>();
                    });

                    RequirementVersionsEntity version = new RequirementVersionsEntity();
                    version = Mapper.Map<RequirementEntity, RequirementVersionsEntity>(entity);
                    version.RequirementRequestForChangesID = null;
                    version.creationDate = DateTime.Now;
                    version.rationale = "First version of the requirement.";

                    base.add(ref entity, false);
                    version.RequirementID = entity.RequirementID;
                    versionService.add(ref version, false);

                    foreach (var item in characteristicsService.getRequiredCharacteristics())
                    {
                        RequirementCharacteristicsEntity req = new RequirementCharacteristicsEntity();
                        req.RequirementID = entity.RequirementID;
                        req.CharacteristicsID = item.CharacteristicsID;
                        req.check = false;
                        requirementCharacteristicsService.add(ref req, false);
                    }

                    unit.Commit();
                }
                else
                {
                    throw new ArgumentException("The inclusion of this requirement exceeds the project balance!");
                }
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }

        public void update(ref RequirementEntity entity, int RequirementRequestForChangesID, string rationale)
        {
            try
            {
                if (checkProjectBalance(entity.ProjectID, entity.cost))
                {
                    unit.BeginTransaction();

                    entity.versionNumber += 1;

                    if (entity.RequirementTemplateID.Equals(0))
                        entity.RequirementTemplateID = null;
                    if (entity.RequirementSubTypeID.Equals(0))
                        entity.RequirementSubTypeID = null;

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateAutomaticMapping<RequirementEntity, RequirementVersionsEntity>();
                    });

                    RequirementRequestForChangesEntity request = requestService.get(RequirementRequestForChangesID);

                    request.RequestStatusID = 3;

                    RequirementVersionsEntity version = new RequirementVersionsEntity();
                    version = Mapper.Map<RequirementEntity, RequirementVersionsEntity>(entity);
                    version.creationDate = DateTime.Now;
                    version.rationale = rationale;
                    version.RequirementRequestForChangesID = request.RequirementRequestForChangesID;
                    version.RequirementID = null;

                    versionService.add(ref version, false);
                    requestService.update(ref request, false);
                    update(ref entity, false);

                    unit.Commit();
                }
                else
                {
                    throw new ArgumentException("The inclusion of this requirement exceeds the project balance!");
                }
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }

        public IEnumerable<RequirementEntity> getRequirementsByProject(int ProjectID)
        {
            try
            {
                return getAll().Where(r => r.ProjectID.Equals(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RequirementEntity getWithCode(string code)
        {
            try
            {
                return getAll().Where(r => r.code.Equals(code)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal getRequirementCostByProject(int ProjectID)
        {
            try
            {
                return getRequirementsByProject(ProjectID).Sum(c => c.cost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool checkProjectBalance(int ProjectID, decimal requirementCost)
        {
            try
            {
                ProjectEntity project = projectService.get(ProjectID);
                return projectService.hasBalance(ProjectID) && 
                    getRequirementCostByProject(ProjectID) + requirementCost < project.cost ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
