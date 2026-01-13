using Data.Entites.Identity;
using Infrastructure.DataContext;
using Infrastructure.InfastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, Infrastructure.AbstractRepository.IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> userTokens;
        #endregion
        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            userTokens = dbContext.Set<UserRefreshToken>();

        }
        #endregion
        #region Handle Function
        #endregion
    }

}
