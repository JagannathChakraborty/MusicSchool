using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace MusicSchool.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _container;

        public BlobService(string connectionString)
        {
            var blobService = new BlobServiceClient(connectionString);
            _container = blobService.GetBlobContainerClient("gallery");
            _container.CreateIfNotExists();
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = _container.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string blobUrl)
        {
            var blobName = Path.GetFileName(blobUrl);
            await _container.DeleteBlobIfExistsAsync(blobName);
        }
    }
}
