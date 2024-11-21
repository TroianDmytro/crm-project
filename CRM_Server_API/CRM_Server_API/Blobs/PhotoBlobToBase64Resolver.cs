using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Server_API.Models.Responce;

namespace CRM_Server_API.Blobs
{
    public class PhotoBlobToBase64Resolver : IValueResolver<ProductDTO, ProductResponce, string?>
    {
        private readonly BlobModul _blobModul;

        public PhotoBlobToBase64Resolver(BlobModul blobModul)
        {
            _blobModul = blobModul;
        }

        public string? Resolve(ProductDTO source, ProductResponce destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PhotoBlob))
                return null;

            return _blobModul.Download(source.PhotoBlob).Result; // Загружаем и конвертируем в Base64
        }
    }

}
