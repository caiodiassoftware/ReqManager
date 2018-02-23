using System;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;
using System.Linq;

namespace ReqManager.Services.Link.Classes
{
    public class LinkBetweenRequirementsArtifactsService : 
        ServiceBase<LinkBetweenRequirementsArtifacts, LinkBetweenRequirementsArtifactsEntity>, 
        ILinkBetweenRequirementsArtifactsService
    {
        private ILinkArtifactAttributesService linkService { get; set; }

        public LinkBetweenRequirementsArtifactsService(
            ILinkBetweenRequirementArtifactsRepository repository, 
            ILinkArtifactAttributesService linkService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.linkService = linkService;
        }

        public LinkBetweenRequirementsArtifactsEntity getWithCode(string code)
        {
            try
            {
                return filter(l => l.code == code).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LinkBetweenRequirementsArtifactsEntity get(string ArtifactCode, string RequirementCode)
        {
            try
            {
                return filter(l => l.ProjectArtifact.code == ArtifactCode && l.Requirement.code == RequirementCode).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
