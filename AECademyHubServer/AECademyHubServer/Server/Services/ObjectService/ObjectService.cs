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
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public ObjectService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<Object>> HandleObjectAsync(ObjectRequest request, IFormFile file)
        {
            // Add the ObjectRequest to the database
            var obj = new Object(request);
            var objRecord = await _context.Objects.FindAsync(obj.Guid);
            if (objRecord != null)
            {
                objRecord = obj;
                _context.Objects.Update(objRecord);
            }
            else
            {
                // If the object does not exist, create a new instance and add it to the database

                // Upload the file to Azure Blob Storage
                var blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("BlobConnection"));

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("aecademyhub");
                await containerClient.CreateIfNotExistsAsync();

                string folderName = request.Guid.ToString();
                BlobClient blobClient = containerClient.GetBlobClient("/" + folderName + "/" + file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }


                await _context.Objects.AddAsync(obj);
            }


            await _context.SaveChangesAsync();

            //// Index the object in Elasticsearch
            //var elasticObject = new
            //{
            //    obj.Guid,
            //    file.FileName,
            //    FileType = file.ContentType,
            //    obj.Description
            //};
            //var indexResponse = await _elasticClient.IndexDocumentAsync(elasticObject);

            //// Check if the indexing was successful
            //if (!indexResponse.IsValid)
            //{
            //    // Optionally, handle the error, possibly by logging it
            //    throw new Exception("Failed to index the document in Elasticsearch.");
            //}

            return new ServiceResponse<Object>()
            {
                Data = obj,
                Success = true,
            };
        }
    }
}
