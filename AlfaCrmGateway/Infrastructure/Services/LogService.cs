using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Services
{
    public static class LogService
    {
        public static ILog Current { get; set; }

        public static QueryWriteService QueryLog { get; set; }
    }
}