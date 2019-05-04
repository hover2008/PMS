using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IUserRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Add(TEntity model, DataTable userRoleDT);
        int Modify(TEntity model, DataTable userRoleDT);
        TEntity GetModelByUserName(string userName);

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<bool> ModifyInfoAsync(TEntity model);
        Task<bool> ModifyPwdAsync(int id, string pwd, string encrypt);
        Task<IEnumerable<UserPermissionCodeEntity>> GetPermissionByUserIdAsync(int userId);
        Task<String> GetRoleByUserIdAsync(int userId);
        Task<bool> SetLockScreenAsync(int id, bool lockScreen);
        Task<bool> ModifyPhotoAsync(int id, string photo);
        Task<bool> DeleteByIdsAsync(IList<int> ids);

        Task<bool> UpdateByLoginAsync(int id, string ip, int errorTimes);

        Task<BasePagedListModel<UserEntity>> GetListAsync(UserSearchParam param);

        Task<BasePagedListModel<UserEntity>> GetCanUserListByRoleIdAsync(RoleUserSearchParam param); 

        #endregion
    }
}
