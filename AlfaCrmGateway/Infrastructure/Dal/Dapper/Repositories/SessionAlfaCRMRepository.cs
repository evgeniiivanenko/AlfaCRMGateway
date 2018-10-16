using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class SessionAlfaCRMRepository
    {
        public int Save(string token, int authAlfaCRMId)
        {
            return DataContext.QuerySingleProc<int>
                ("SessionAlfaCRMSave", "alf", new
                {
                    Token = token,
                    AuthAlfaCRMId = authAlfaCRMId
                });
        }
    }
}