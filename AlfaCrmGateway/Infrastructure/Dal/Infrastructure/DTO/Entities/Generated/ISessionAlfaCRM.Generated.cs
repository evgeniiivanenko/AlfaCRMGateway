/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface ISessionAlfaCRM : IEntity
    {
        string Token { get; set; }
        DateTime DateAccess { get; set; }
        int AuthAlfaCRMId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface ISessionAlfaCRMRepository : IRepository<ISessionAlfaCRM> { }
}