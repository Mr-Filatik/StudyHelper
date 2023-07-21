using Grpc.Core;
using StudyHelper.Application.Server;
using StudyHelper.Application.Server.Other;
using StudyHelper.Library.Network.Shared;

namespace StudyHelper.Application.Server.Services
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
                    RequestTime = Converters.ToTimestamp(requestTime),
                    ResponceTime = Converters.ToTimestamp(DateTime.UtcNow)
                }
            });
        }
    }
}