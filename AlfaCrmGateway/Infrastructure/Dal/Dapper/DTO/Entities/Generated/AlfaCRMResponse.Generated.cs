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

    internal class AlfaCRMResponse : EntityBase, IAlfaCRMResponse
    {
        public string ResponseBody { get; set; }
        public int ResponseType { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int ServiceId { get; set; }
        public int? RequestId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{

    internal partial class AlfaCRMResponseRepository : RepositoryBase<IAlfaCRMResponse>, IAlfaCRMResponseRepository { public AlfaCRMResponseRepository(IDataContext ctx) : base(ctx) { } }
}