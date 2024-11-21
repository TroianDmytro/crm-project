using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace CRM_Server_API.Blobs
{
    public class BlobModul
    {
        private readonly string _containerName;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobModul(BlobServiceClient blobServiceClient,
                              IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = configuration.GetConnectionString("ContainerName")!;
        }

        public async Task<string?> Download(string pathFile)
        {
            if (pathFile == null)
                return null;
            try
            {
                string fileName = Path.GetFileName(pathFile);

                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                //передайом название файла в blob для получения клиента
                BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
                BlobDownloadInfo download = await blobClient.DownloadAsync();

                // Чтение содержимого файла в память
                using (var memoryStream = new MemoryStream())
                {
                    await download.Content.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);
                    return base64String;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async Task<string> Upload(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
                throw new ArgumentNullException(nameof(avatar));

            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = blobContainerClient.GetBlobClient(avatar.FileName.Replace(".", Guid.NewGuid().ToString() + "."));

            await using var stream = avatar.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = avatar.ContentType });

            var fileUrl = blobClient.Uri.ToString();
            if (fileUrl is null)
                throw new BadImageFormatException("Image is invalid.");
            return fileUrl;
        }


        public async Task<bool> DeleteFile(string? pathFile)
        {
            try
            {
                if(pathFile == null)
                    throw new ArgumentNullException(nameof(pathFile));

                string fileName = Path.GetFileName(pathFile);
                //получение контейнера
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

                //передайом название файла в blob для получения клиента
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                //удаление файла
                await blobClient.DeleteAsync();
            }
            catch (Exception)
            {
                return false;
            }


            return true;

        }

    }


}
