namespace POC837Parser.DataAccess
{
    using _837ParserPOC.DataModels;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using POC837Parser.Models;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class BlobStorageService
    {
        private readonly BlobContainerClient _submissionsContainerClient;
        private readonly BlobContainerClient _claimsContainerClient;
        private readonly ILogger<BlobStorageService> _logger;

        public BlobStorageService(ILogger<BlobStorageService> logger)
        {
            _logger = logger;
            try
            {
                var blobServiceClient = new BlobServiceClient(BlobStorageConfig.ConnectionString);
                _submissionsContainerClient = blobServiceClient.GetBlobContainerClient(BlobStorageConfig.SubmissionsContainerName);
                _submissionsContainerClient.CreateIfNotExists();

                _claimsContainerClient = blobServiceClient.GetBlobContainerClient(BlobStorageConfig.ClaimsContainerName);
                _claimsContainerClient.CreateIfNotExists();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize BlobStorageService. Is the storage emulator running?");
            }
        }

        public async Task<string> SaveSubmission(string json, string blobName)
        {
            if (_submissionsContainerClient == null)
            {
                throw new InvalidOperationException($"BlobStorageService (sublissions) is not properly initialized. Is the storage emulator running?");
            }

            var blobClient = _submissionsContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(BinaryData.FromString(json), overwrite: true);
            return blobClient.Uri.ToString();
        }

        public async Task<string> SaveClaim(string json, string blobName)
        {
            if (_claimsContainerClient == null)
            {
                throw new InvalidOperationException($"BlobStorageService (claims) is not properly initialized. Is the storage emulator running?");
            }

            var blobClient = _claimsContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(BinaryData.FromString(json), overwrite: true);
            return blobClient.Uri.ToString();
        }


        public async Task<List<Guid>> ListSubmissionIdsAsync()
        {
            var submissionIds = new List<Guid>();

            try
            {
                await foreach (BlobItem blobItem in _submissionsContainerClient.GetBlobsAsync())
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
                BlobClient blobClient = _submissionsContainerClient.GetBlobClient(blobName);

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


        public async Task<Claim> GetClaimAsync(Guid claimId)
        {
            try
            {
                string blobName = $"{claimId}.json";
                BlobClient blobClient = _claimsContainerClient.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
                    string jsonContent = downloadResult.Content.ToString();

                    // Deserialize the JSON string to EDI837Result
                    Claim result = JsonSerializer.Deserialize<Claim>(jsonContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result;
                }
                else
                {
                    _logger.LogWarning($"Document with claim ID {claimId} not found.");
                    return null;
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Error deserializing document with claim ID {claimId}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving document with claim ID {claimId}");
                throw;
            }
        }



    }
}
