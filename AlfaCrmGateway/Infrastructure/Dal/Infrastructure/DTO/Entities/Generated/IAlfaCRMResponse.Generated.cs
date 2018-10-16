/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IAlfaCRMResponse : IEntity
    {
        string ResponseBody { get; set; }
        int ResponseType { get; set; }
        string Description { get; set; }
        DateTime DateCreated { get; set; }
        int ServiceId { get; set; }
        int? RequestId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface IAlfaCRMResponseRepository : IRepository<IAlfaCRMResponse> { }
}