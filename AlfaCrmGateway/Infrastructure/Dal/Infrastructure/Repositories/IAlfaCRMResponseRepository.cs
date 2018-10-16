using System;
using System.Collections.Generic;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    partial interface IAlfaCRMResponseRepository
    {
        int Save(IAlfaCRMResponse response);
    }
}