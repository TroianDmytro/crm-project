using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Server_API.Models.Request;

namespace CRM_Server_API.Blobs
{
    public class PhotoBlobUploadResolver : IValueResolver<ProductRequest, ProductDTO, string?>
    {
        private readonly BlobModul _blobModul;

        public PhotoBlobUploadResolver(BlobModul blobModul)
        {
            _blobModul = blobModul;
        }

        public string? Resolve(ProductRequest source, ProductDTO destination, string? destMember, ResolutionContext context)
        {
            if (source.PhotoBlob == null)
                return null;

            return _blobModul.Upload(source.PhotoBlob).Result;
        }
    }
}
