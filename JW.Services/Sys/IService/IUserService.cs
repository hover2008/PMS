using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IUserService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(UserParam model); 
        UserEntity GetModelByUserName(string userName);

        #endregion

        #region Methods Async

        Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<Messages> ModifyInfoAsync(TEntity model); 
        Task<IEnumerable<UserPermissionCodeEntity>> GetPermissionByUserIdAsync(int userId);
        Task<String> GetRoleByUserIdAsync(int userId);
        Task SetLockScreenAsync(int id, bool lockScreen);
        Task<Messages> ModifyPhotoAsync(int id, string photo);
        Task<UserDetialsEntity> GetDetialsEntityAsync(int id);
        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<(bool Succeeded, string Msg, int UserId)> Login(string userName, string userPwd);
        Task<Messages> ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword);
        Task UpdateByLoginAsync(int id, string ip, int errorTimes);
        Task<BasePagedListModel<UserEntity>> GetListAsync(UserSearchParam param);
        Task<BasePagedListModel<UserEntity>> GetCanUserListByRoleIdAsync(RoleUserSearchParam param);

        #endregion
    }
}
