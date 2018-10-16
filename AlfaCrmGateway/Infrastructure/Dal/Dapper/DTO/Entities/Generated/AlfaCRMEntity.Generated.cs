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

    internal class AlfaCRMEntity : EntityBase, IAlfaCRMEntity
    {
        public int EntityId { get; set; }
        public int CustomerId { get; set; }
        public int PersonalAccountId { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{

    internal partial class AlfaCRMEntityRepository : RepositoryBase<IAlfaCRMEntity>, IAlfaCRMEntityRepository { public AlfaCRMEntityRepository(IDataContext ctx) : base(ctx) { } }
}