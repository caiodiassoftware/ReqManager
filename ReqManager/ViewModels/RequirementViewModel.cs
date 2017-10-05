using ReqManager.Entities.Link;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ReqManager.ViewModels
{
    public class RequirementViewModel
    {
        public RequirementViewModel()
        {
            requirement = new RequirementEntity();
            linkReq = new List<LinkBetweenRequirementsEntity>();
            linkReqArt = new List<LinkBetweenRequirementsArtifactsEntity>();
        }

        public RequirementEntity requirement { get; set; }
        [DisplayName]
        public List<LinkBetweenRequirementsEntity> linkReq { get; set; }
        [DisplayName]
        public List<LinkBetweenRequirementsArtifactsEntity> linkReqArt { get; set; }
    }
}