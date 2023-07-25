using Grpc.Net.Client;
using StudyHelper.Library.Network.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using File = StudyHelper.Library.Network.Shared.File;

namespace StudyHelper.Library.Network.Client.Grpc
{
    public class FileClient
    {
        private readonly string _address;
        private readonly GrpcChannel _channel;
        private readonly File.FileClient _client;

        public FileClient(string address = "https://localhost:7189")
        {
            _address = address;
            _channel = GrpcChannel.ForAddress(_address);
            _client = new File.FileClient(_channel);
        }

        public async Task<CreateFileResponse> CreateFileAsync()
        {
            var request = new CreateFileRequest()
            { 

            };

            var response = await _client.CreateFileAsync(request);

            return response;
        }
    }
}
