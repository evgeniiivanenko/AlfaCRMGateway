/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IAlfaCRMRequest : IEntity
    {
        string RequestBody { get; set; }
        int RequestType { get; set; }
        string Description { get; set; }
        DateTime DateCreated { get; set; }
        int ServiceId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface IAlfaCRMRequestRepository : IRepository<IAlfaCRMRequest> { }
}