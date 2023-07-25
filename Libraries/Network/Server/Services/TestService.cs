using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using StudyHelper.Library.Network.Shared;

using Test = StudyHelper.Library.Network.Shared.Test;

namespace StudyHelper.Library.Network.Server
{
    public class TestService : Test.TestBase
    {
        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        public override Task<TestResponse> Test(TestRequest request, ServerCallContext context)
        {
            var requestTime = DateTime.UtcNow;

            return Task.FromResult(new TestResponse()
            {
                Message = $"Responce on request ({request.Message})",
                TimeProcessing = new TimeProcessingGrpc()
                {
                    RequestTime = ToTimestamp(requestTime),
                    ResponceTime = ToTimestamp(DateTime.UtcNow)
                }
            });
        }

        public static Timestamp ToTimestamp(DateTime? dateTime)
        {
            if (dateTime == null || !dateTime.HasValue) return new Timestamp();
            return Timestamp.FromDateTime(DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc));
        }
    }
}