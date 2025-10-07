using Microsoft.AspNetCore.Http;

namespace Service.Abstruct
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
