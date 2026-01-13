using Core.Besec;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Features.Instractor.Command.Model
{
    public class AddInstractorCommand : IRequest<Response<string>>
    {

        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }

        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public IFormFile? Image { get; set; }

        public int DID { get; set; }
    }
}
