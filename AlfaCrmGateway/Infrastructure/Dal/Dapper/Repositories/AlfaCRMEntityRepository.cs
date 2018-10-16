using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using System.Collections.Generic;
using TRIINKOM.Dal.Dapper;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories
{
    internal partial class AlfaCRMEntityRepository
    {
        public int Save(int entityId, int customerId, int invoiceNo)
        {
            return DataContext.QuerySingleProc<int>
                ("AlfaCRMEntitySave", "alf", new
                {
                    EntityId    = entityId,
                    CustomerId  = customerId,
                    InvoiceNo   = invoiceNo
                });
        }

        public IAlfaCRMEntity GetByEntityId(int entityId)
        {
            return DataContext.QuerySingleProc<IAlfaCRMEntity>
                ("AlfaCRMEntityGetByEntityId", "alf", new
                {
                    EntityId = entityId
                });
        }
    }
}