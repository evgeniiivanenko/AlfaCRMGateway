/* 
 ВНИМАНИЕ!!! Данный файл был сгенерирован автоматически, поэтому все изменения, внесенные Вами,
 могут быть утеряны при последующих генерациях.
*/

using TRIINKOM.Dal.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities
{
    using System;
    using System.ComponentModel;

    public partial interface IService : IEntity
    {
        bool IsEripAcquiringType { get; }
        bool IsDirectAcquiringType { get; }
    }
}