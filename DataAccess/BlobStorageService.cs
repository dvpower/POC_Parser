namespace POC837Parser.DataAccess
{
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using POC837Parser.Models;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly ILogger<BlobStorageService> _logger;

        public BlobStorageService(ILogger<BlobStorageService> logger)
        {
            _logger = logger;
            try
            {
                var blobServiceClient = new BlobServiceClient(BlobStorageConfig.ConnectionString);
                _containerClient = blobServiceClient.GetBlobContainerClient(BlobStorageConfig.ContainerName);
                _containerClient.CreateIfNotExists();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize BlobStorageService. Is the storage emulator running?");
            }
        }

        public async Task<string> UploadJsonBlobAsync(string json, string blobName)
        {
            if (_containerClient == null)
            {
                throw new InvalidOperationException("BlobStorageService is not properly initialized. Is the storage emulator running?");
            }

            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(BinaryData.FromString(json), overwrite: true);
            return blobClient.Uri.ToString();
        }

        public async Task<List<Guid>> ListSubmissionIdsAsync()
        {
            var submissionIds = new List<Guid>();

            try
            {
                await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
                {
                    if (Guid.TryParse(Path.GetFileNameWithoutExtension(blobItem.Name), out Guid submissionId))
                    {
                        submissionIds.Add(submissionId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing submission IDs");
                throw;
            }

            return submissionIds;
        }

        public async Task<EDI837Result> GetDocumentAsync(Guid submissionId)
        {
            try
            {
                string blobName = $"{submissionId}.json";
                BlobClient blobClient = _containerClient.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
                    string jsonContent = downloadResult.Content.ToString();

                    // Deserialize the JSON string to EDI837Result
                    EDI837Result result = JsonSerializer.Deserialize<EDI837Result>(jsonContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    _logger.LogWarning($"Document with submission ID {submissionId} not found.");
                    return null;
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Error deserializing document with submission ID {submissionId}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving document with submission ID {submissionId}");
                throw;
            }
        }
    }
}
