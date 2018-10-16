using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TRIINKOM.Infrastructure;


namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Services
{
    public class QueryWriteService
    {
        private readonly IAppController AppController;
    
        public QueryWriteService(IAppController appController)
        {
            AppController = appController;
        }

        public int WriteRequest(string requestBody, int requestType, string description, int serviceId)
        {
            var requestRepository = AppController.Get<IAlfaCRMRequestRepository>();

            int id = requestRepository.Save(requestBody, requestType, description, serviceId);

            return id;
        }

        public void WriteResponse(string responseBody, int responseType, string description, int serviceId, int requestId)
        {
            var responseRepository = AppController.Get<IAlfaCRMResponseRepository>();

            var response = AppController.Get<IAlfaCRMResponse>();

            response.ResponseBody = responseBody;
            response.ResponseType = responseType;
            response.Description = description;
            response.ServiceId = serviceId;
            response.RequestId = requestId;

            responseRepository.Save(response);
        }

    }
}