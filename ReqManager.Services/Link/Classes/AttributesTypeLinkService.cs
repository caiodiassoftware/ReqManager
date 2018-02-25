using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;
using System;
using System.Collections.Generic;

namespace ReqManager.Services.Link.Classes
{
    public class AttributesTypeLinkService : ServiceBase<AttributesTypeLink, AttributesTypeLinkEntity>, IAttributesTypeLinkService
    {
        private IAttributesTypeLinkRepository repository { get; set; }

        public AttributesTypeLinkService(IAttributesTypeLinkRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
            this.repository = repository;
        }

        public IEnumerable<AttributesTypeLinkEntity> GetByTypeLink(int TypeLinkID)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.filter(l => l.TypeLinkID == TypeLinkID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
