using Data.Entites.Identity;
using Infrastructure.InfastructureBases;

namespace Infrastructure.AbstractRepository
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
