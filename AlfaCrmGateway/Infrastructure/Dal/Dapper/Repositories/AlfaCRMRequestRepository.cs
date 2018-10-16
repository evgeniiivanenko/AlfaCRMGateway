using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class AlfaCRMRequestRepository
    {
        public int Save(string requestBody, int requestType, string description, int serviceId)
        {
            return DataContext.QuerySingleProc<int>
                ("AlfaCRMRequestSave", "alf", new
                {
                    RequestBody = requestBody,
                    RequestType = requestType,
                    Description = description,
                    ServiceId = serviceId
                });
        }
    }
}