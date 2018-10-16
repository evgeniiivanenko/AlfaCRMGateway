using System;
using System.Collections.Generic;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    partial interface IAlfaCRMEntityRepository
    {
        int Save(int entityId, int customerId, int invoiceNo);
        IAlfaCRMEntity GetByEntityId(int entityId);
    }
}