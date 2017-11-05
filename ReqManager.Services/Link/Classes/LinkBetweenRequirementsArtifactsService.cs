using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void add(LinkBetweenRequirementsArtifactsEntity entity, List<LinkArtifactAttributesEntity> attributes)
        {
            try
            {
                unit.BeginTransaction();

                add(ref entity);
                attributes.ForEach(a => {
                    a.LinkArtifactRequirementID = entity.LinkArtifactRequirementID;
                    linkService.add(ref a);
                });

                unit.Commit();
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }
    }
}
