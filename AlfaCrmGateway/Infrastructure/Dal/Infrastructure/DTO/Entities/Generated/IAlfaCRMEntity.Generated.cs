/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IAlfaCRMEntity : IEntity
    {
        int EntityId { get; set; }
        int CustomerId { get; set; }
        int PersonalAccountId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface IAlfaCRMEntityRepository : IRepository<IAlfaCRMEntity> { }
}