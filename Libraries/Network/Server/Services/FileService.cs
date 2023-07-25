using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using StudyHelper.Library.Network.Shared;

using File = StudyHelper.Library.Network.Shared.File;

namespace StudyHelper.Library.Network.Server
{
    public class FileService : File.FileBase
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public override Task<CreateFileResponse> CreateFile(CreateFileRequest request, ServerCallContext context)
        {
            string fileId = AddGuid();

            return Task.FromResult(new CreateFileResponse()
            {
                FileId = fileId
            });
        }

        public static string AddGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}