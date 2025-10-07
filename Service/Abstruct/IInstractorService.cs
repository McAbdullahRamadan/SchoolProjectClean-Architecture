using Data.Entites;
using Microsoft.AspNetCore.Http;

namespace Service.Abstruct
{
    public interface IInstractorService
    {
        public Task<bool> IsNameEnExist(string name);
        public Task<bool> IsNameEnExistExcloudSelf(string name, int id);
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameARExistExcloudSelf(string name, int id);
        public Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);
    }
}
