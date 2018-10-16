using TRIINKOM.Infrastructure.Extensions;

namespace ERIP.Sites.AlfaCrmGateway.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://toster.ru/q/198981
    /// http://apigee.com/about/blog/technology/restful-api-design-what-about-errors
    /// </remarks>
    public class ErrorResultModel
    {
        public ResultError Error { get; private set; }

        public ErrorResultModel()
        {
            Error = new ResultError();
        }
    }

    /// <summary>
    /// 400 - Bad Request
    /// </summary>
    public class BadRequestResultModel : ErrorResultModel
    {
        public BadRequestResultModel(string errMsg, int errCode, string errorMsg = null)
        {
            Error.Code = 400;
            Error.Msg = errorMsg.IsNullOrWhiteSpace() ? errMsg : string.Format("{0}: {1}", errMsg, errorMsg);
            Error.MsgCode = errCode;
        }
    }

    /// <summary>
    /// 404 - Not Found
    /// </summary>
    public class NotFoundResultModel : ErrorResultModel
    {
        public NotFoundResultModel(string errMsg, int errCode, string errorMsg = null)
        {
            Error.Code = 404;
            Error.Msg = errorMsg.IsNullOrWhiteSpace() ? errMsg : string.Format("{0}: {1}", errMsg, errorMsg);
            Error.MsgCode = errCode;
        }
    }

    /// <summary>
    /// 500 - Internal Server Error
    /// </summary>
    public class InternalServerErrorResultModel : ErrorResultModel
    {
        public InternalServerErrorResultModel(string errMsg, int errCode, string errorMsg = null)
        {
            Error.Code = 500;
            Error.Msg = errorMsg.IsNullOrWhiteSpace() ? errMsg : string.Format("{0}: {1}", errMsg, errorMsg);
            Error.MsgCode = errCode;
        }
    }

    public class ResultError
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int MsgCode { get; set; }
    }

    public enum ErrorCode
    {
        BadRequest,
        BadRequestDate,
        BadRequestDateTime
    }
}