using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class AuthAlfaCRMRepository
    {
        public IAuthAlfaCRM GetByToken(string token)
        {

            return DataContext.QuerySingleProc<IAuthAlfaCRM>
                ("AuthAlfaCRMGetByTokenAlfaCRM", "alf", new
                {
                    Token = token
                });
        }

        public IAuthAlfaCRM GetByTokenExpressPay(string token)
        {
            return DataContext.QuerySingleProc<IAuthAlfaCRM>
                ("AuthAlfaCRMGetByTokenExpressPay", "alf", new
                {
                    Token = token
                });
        }
    }
}