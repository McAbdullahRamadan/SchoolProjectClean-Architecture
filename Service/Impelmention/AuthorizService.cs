using Data.DTORequset;
using Data.Entites.Identity;
using Data.Helpers;
using Data.Helpers.Results;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Abstruct;
using System.Security.Claims;
using static Data.Helpers.Results.ManageUserClaimsResult;

namespace Service.Impelmention
{
    public class AuthorizService : IAuthorizService
    {
        #region Fields
        private readonly RoleManager<RoleSys> _roleManager;
        private readonly UserManager<UserIdentity> _UserManager;
        private readonly ApplicationDbContext _dbContext;

        #endregion
        #region Constructors
        public AuthorizService(RoleManager<RoleSys> roleManager, UserManager<UserIdentity> UserManager, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _UserManager = UserManager;
            _dbContext = dbContext;


        }
        #endregion
        #region Handle Function
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new RoleSys();
            identityRole.Name = roleName;
            var Result = await _roleManager.CreateAsync(identityRole);
            if (Result.Succeeded)
                return "Success";
            return "Failed";
        }


        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "NotFound";
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("", result.Errors);
            return errors;

        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var result = await _roleManager.FindByIdAsync(roleId.ToString());
            if (result == null)
                return false;
            else
                return true;
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return "NotFound";
            var userRole = await _UserManager.GetUsersInRoleAsync(role.Name);
            if (userRole != null && userRole.Count() > 0)
                return "Used";
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("", result.Errors);
            return errors;

        }

        public async Task<List<RoleSys>> GetRoleAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<RoleSys> GetRoleByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<ManageUserRoleResult> GetManageRoleUser(UserIdentity User)
        {
            var response = new ManageUserRoleResult();
            var rolelist = new List<UserRoles>();
            //userroles
            //var userRole = await _UserManager.GetRolesAsync(User);
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = User.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await _UserManager.IsInRoleAsync(User, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolelist.Add(userrole);
            }
            response.UserRoles = rolelist;
            return response;




        }

        public async Task<string> UpdateUserRole(UpdateUserRoleRequest request)
        {
            //get BeginTransact
            var Transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {


                //get user 
                var user = await _UserManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //Get Role 
                var UserRole = await _UserManager.GetRolesAsync(user);
                var RemoveRole = await _UserManager.RemoveFromRolesAsync(user, UserRole);
                if (!RemoveRole.Succeeded)

                    return "FialedToRemoveRoles";

                //Has Role = True Or False
                var SelectedRoles = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.Name);
                var AddRolesresult = await _UserManager.AddToRolesAsync(user, SelectedRoles);
                if (!AddRolesresult.Succeeded)

                    return "FialedToAddNewRoles";
                await Transact.CommitAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                await Transact.RollbackAsync();
                return "FialedToAddUserRole";

            }



        }
        public async Task<ManageUserClaimsResult> GetManageUserClaimsData(UserIdentity user)
        {
            var response = new ManageUserClaimsResult();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;

            //Get User Claims
            var userclaims = await _UserManager.GetClaimsAsync(user);  //edit
                                                                       //create edit get print  
            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userclaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                userClaimsList.Add(userClaim);



            }
            response.Userclaims = userClaimsList;
            return response;


        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var Transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _UserManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                var userclaims = await _UserManager.GetClaimsAsync(user);
                var removeclaimsresult = await _UserManager.RemoveClaimsAsync(user, userclaims);
                if (!removeclaimsresult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var Claims = request.Userclaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var AddUserClaimsResult = await _UserManager.AddClaimsAsync(user, Claims);
                if (!AddUserClaimsResult.Succeeded)
                    return "FailedToAddNewClaims";
                await Transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await Transact.RollbackAsync();
                return "FailedToUpdateClaims";

            }

        }
        #endregion

    }
}
