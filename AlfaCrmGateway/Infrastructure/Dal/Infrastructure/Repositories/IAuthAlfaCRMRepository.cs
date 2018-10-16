using System;
using System.Collections.Generic;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    partial interface IAuthAlfaCRMRepository
    {
        IAuthAlfaCRM GetByToken(string token);
        IAuthAlfaCRM GetByTokenExpressPay(string token); 


    }
}