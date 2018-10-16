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

    internal class AuthAlfaCRM : EntityBase, IAuthAlfaCRM
    {
        public int ServiceId { get; set; }
        public string HostName { get; set; }
        public string Branch { get; set; }
        public string Email { get; set; }
        public string ApiKey { get; set; }
    }
}

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{

    internal partial class AuthAlfaCRMRepository : RepositoryBase<IAuthAlfaCRM>, IAuthAlfaCRMRepository { public AuthAlfaCRMRepository(IDataContext ctx) : base(ctx) { } }
}