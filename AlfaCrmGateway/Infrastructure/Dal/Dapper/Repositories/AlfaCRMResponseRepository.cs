using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class AlfaCRMResponseRepository
    {
        public int Save(IAlfaCRMResponse response)
        {
            return DataContext.QuerySingleProc<int>
                ("AlfaCRMResponseSave", "alf", new
                {
                    response.ResponseBody,
                    response.ResponseType,
                    response.Description,
                    response.ServiceId,
                    response.RequestId
                });
        }
    }
}