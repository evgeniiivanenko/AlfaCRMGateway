using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class ServiceIntegrationRepository
    {
        public string GetTokenByServiceId(int serviceId)
        {
            return DataContext.QuerySingleProc<string>
                ("ServiceIntegrationGetTokenByServiceId", "alf", new
                {
                    ServiceId = serviceId
                });
        }
    }
}