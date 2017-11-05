﻿using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Link.Interfaces
{
    public interface ILinkBetweenRequirementsService : IService<LinkBetweenRequirementsEntity>
    {
        void add(LinkBetweenRequirementsEntity entity, List<LinkRequirementAttributesEntity> attributes);
    }
}
