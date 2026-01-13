using Data.Entites;
using Infrastructure.AbstractRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class InstractorService : IInstractorService
    {
        #region Feilds
        private readonly IInstrctorRepository _instrctorRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        #endregion

        #region Constructors
        public InstractorService(IInstrctorRepository instrctorRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _instrctorRepository = instrctorRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }


        #endregion

        #region Handle Function
        public async Task<bool> IsNameArExist(string name)
        {
            var isExist = await _instrctorRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(name)).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameARExistExcloudSelf(string name, int id)
        {
            var isExist = await _instrctorRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(name) & x.InsId != id).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string name)
        {
            var isExist = await _instrctorRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(name)).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcloudSelf(string name, int id)
        {
            var isExist = await _instrctorRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(name) & x.InsId != id).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            return true;
        }
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var ImageUrl = await _fileService.UploadImage("Instructors", file);
            switch (ImageUrl)
            {
                case "NoImage":
                    return "NotImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            instructor.Image = baseUrl + ImageUrl;
            try
            {
                await _instrctorRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }
        #endregion


    }
}
