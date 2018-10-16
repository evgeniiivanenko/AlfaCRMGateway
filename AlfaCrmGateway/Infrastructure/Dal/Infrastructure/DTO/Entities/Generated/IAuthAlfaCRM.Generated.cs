/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IAuthAlfaCRM : IEntity
    {
        int ServiceId { get; set; }
        string HostName { get; set; }
        string Branch { get; set; }
        string Email { get; set; }
        string ApiKey { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface IAuthAlfaCRMRepository : IRepository<IAuthAlfaCRM> { }
}