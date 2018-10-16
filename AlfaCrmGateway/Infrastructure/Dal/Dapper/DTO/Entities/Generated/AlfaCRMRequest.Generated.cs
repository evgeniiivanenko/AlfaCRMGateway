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

    internal class AlfaCRMRequest : EntityBase, IAlfaCRMRequest
    {
        public string RequestBody { get; set; }
        public int RequestType { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int ServiceId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{

    internal partial class AlfaCRMRequestRepository : RepositoryBase<IAlfaCRMRequest>, IAlfaCRMRequestRepository { public AlfaCRMRequestRepository(IDataContext ctx) : base(ctx) { } }
}