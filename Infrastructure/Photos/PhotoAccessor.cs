using Application.Interfaces;
using Application.Photos;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        IConfiguration _config;
        private readonly ILogger<PhotoAccessor> _logger;

        public PhotoAccessor(IConfiguration config, ILogger<PhotoAccessor> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<PhotoUploadResult> AddPhoto(IFormFile photo)
        {
            try
            {
                var client = await GetContainerClient();

                var id = Guid.NewGuid();
                var fileName = id.ToString();

                BlobClient blobClient = client.GetBlobClient(fileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    await photo.CopyToAsync(ms).ConfigureAwait(false);
                    ms.Position = 0;

                    await blobClient.UploadAsync(ms, true).ConfigureAwait(false);
                }

                //TODO integration with Azure CDN
                return new PhotoUploadResult
                {
                    Id = id,
                    Url = blobClient.Uri.AbsoluteUri
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while uploading image to azure");
                return new PhotoUploadResult();
            }
        }

        private async Task<BlobContainerClient> GetContainerClient()
        {
            string storageConnectionString = _config["azureStorageConnection"];
            const string storageContainerName = "socialsite";

            BlobServiceClient cloudBlobClient = new BlobServiceClient(storageConnectionString);
            BlobContainerClient containerClient = cloudBlobClient.GetBlobContainerClient(storageContainerName);

            await containerClient.CreateIfNotExistsAsync().ConfigureAwait(false);
            await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob).ConfigureAwait(false);

            return containerClient;
        }

        public async Task<bool> DeletePhoto(string photoId)
        {
            var client = await GetContainerClient();
            BlobClient blobClient = client.GetBlobClient(photoId);
            var result = await blobClient.DeleteIfExistsAsync();
            return result.Value;
        }
    }
}
