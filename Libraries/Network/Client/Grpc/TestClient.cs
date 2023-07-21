using Grpc.Net.Client;
using StudyHelper.Library.Network.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace StudyHelper.Library.Network.Client.Grpc
{
    public class TestClient
    {
        private readonly string _address;
        private readonly GrpcChannel _channel;
        private readonly Test.TestClient _client;

        public TestClient(string address = "https://localhost:7189")
        {
            _address = address;
            _channel = GrpcChannel.ForAddress(_address);
            _client = new Test.TestClient(_channel);
        }

        public async Task<TestResponse> TestWithDelayAsync(string str)
        {
            var request = new TestRequest()
            {
                Message = str
            };

            var response = await _client.TestAsync(request);

            return response;
        }
    }
}
