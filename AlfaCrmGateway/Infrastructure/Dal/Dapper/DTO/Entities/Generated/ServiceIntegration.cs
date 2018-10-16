/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using System;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.DTO.Entities.EntitiesBase;
using TRIINKOM.Dal.Dapper;
using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.DTO.Entities
{
    using System;

    internal class ServiceIntegration : EntityBase, IServiceIntegration
    {
        public int ServiceId { get; set; }
        public string TokenAlfaCRM { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedId { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedId { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{

    internal partial class ServiceIntegrationRepository : RepositoryBase<IServiceIntegration>, IServiceIntegrationRepository { public ServiceIntegrationRepository(IDataContext ctx) : base(ctx) { } }
}