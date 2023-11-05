using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Nest;
using AECademyHubServer.Shared.Object;
using Object = AECademyHubServer.Shared.Object.Object;

namespace AECademyHubServer.Server.Services.ObjectService
{
    public class ObjectService : IObjectService
    {
        private readonly DbContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ElasticClient _elasticClient;

        public ObjectService(DbContext context, BlobServiceClient blobServiceClient, ElasticClient elasticClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
            _elasticClient = elasticClient;
        }

        public async Task<ServiceResponse<Object>> HandleObjectAsync(ObjectRequest request, IFormFile file)
        {
            // Begin a transaction to ensure atomicity
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Add the ObjectRequest to the database
                var obj = new Object(request);
                await _context.Set<Object>().AddAsync(obj);
                await _context.SaveChangesAsync();

                // Upload the file to Azure Blob Storage
                string folderName = request.Guid.ToString();
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(folderName);
                await containerClient.CreateIfNotExistsAsync();
                BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                // Index the object in Elasticsearch
                var elasticObject = new
                {
                    request.Guid,
                    file.FileName,
                    FileType = file.ContentType,
                    request.Description
                };
                var indexResponse = await _elasticClient.IndexDocumentAsync(elasticObject);

                // Check if the indexing was successful
                if (!indexResponse.IsValid)
                {
                    // Optionally, handle the error, possibly by logging it
                    throw new Exception("Failed to index the document in Elasticsearch.");
                }

                await transaction.CommitAsync();
                return new ServiceResponse<Object>()
                {
                    Data = obj,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                // TODO: Add your logging mechanism here

                await transaction.RollbackAsync();
                return new ServiceResponse<Object>()
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                }; ;
            }
        }
    }
}
