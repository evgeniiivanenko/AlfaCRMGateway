/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IServiceIntegration : IEntity
    {
        int ServiceId { get; set; }
        string TokenAlfaCRM { get; set; }
        DateTime DateCreated { get; set; }
        int CreatedId { get; set; }
        DateTime? DateUpdated { get; set; }
        int? UpdatedId { get; set; }
        DateTime? ExpiredDate { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories
{
    using DTO.Entities;

    public partial interface IServiceIntegrationRepository : IRepository<IServiceIntegration> { }
}