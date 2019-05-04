using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.Enum;
using JW.Domain.Sys.RequestParam;
using JW.Services.IService;
using JW.Services.Sys.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.Sys.Service
{
    /// <summary>
    /// 角色用户服务
    /// </summary>
    public partial class RoleUserService : BaseService<Role2UserEntity, IRoleUserRepository<Role2UserEntity>>, IRoleUserService<Role2UserEntity>
    {
        #region Fields

        private readonly IRoleUserRepository<Role2UserEntity> roleUserRepository;
        private readonly ILogService<LogEntity> logService;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public RoleUserService(IRoleUserRepository<Role2UserEntity> roleUserRepository,
            ILogService<LogEntity> logService,
            Messages messages)
            : base(roleUserRepository)
        {
            this.roleUserRepository = roleUserRepository;
            this.logService = logService;
            this.messages = messages;
        }

        #endregion

        #region Methods

        #endregion

        #region Methods Async 

        public Task<IEnumerable<string>> GetRoleNameListByUserIdAsync(int userId)
        {
            return roleUserRepository.GetRoleNameListByUserIdAsync(userId);
        }

        public async Task<Messages> AddByUserIdsAsync(int roleId, List<int> userIds, UserClaimModel userClaim)
        {
            if (roleId > 0 && userIds != null && userIds.Count > 0)
            {
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("roleid", typeof(int));
                    dt.Columns.Add("userid", typeof(int));
                    foreach (int userid in userIds)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = roleId;
                        dr[1] = userid;
                        dt.Rows.Add(dr);
                    }
                    bool result = await roleUserRepository.AddByDataTableAsync(dt);
                    messages.Msg = result ? "添加成功！！" : "添加失败！！";
                    messages.Success = result;

                    await logService.AddLogAsync(OperatorLogEnum.Create, string.Format("批量增加角色编号为：{0}的用户编号为{1}{2}", roleId, string.Join(",", userIds), messages.Msg), userClaim.UserId, userClaim.UserName);
                }
            }
            return messages;
        }

        public async Task<Messages> DeleteByIdsAsync(int roleId, IList<int> userIds, UserClaimModel userClaim)
        {
            if (roleId > 0 && userIds != null && userIds.Count > 0)
            {
                bool result = await roleUserRepository.DeleteByIdsAsync(roleId, userIds);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
                await logService.AddLogAsync(OperatorLogEnum.Delete, string.Format("批量移除角色编号为：{0}的用户编号为{1}{2}", roleId, string.Join(",", userIds), messages.Msg), userClaim.UserId, userClaim.UserName);
            }
            return messages;
        }

        /// <summary>
        /// 根据角色编号获取用户列表
        /// </summary> 
        /// <param name="entity">角色用户搜索实体</param>
        /// <returns>PageDataModel<Role2UserEntity></returns>
        public Task<BasePagedListModel<Role2UserEntity>> GetListByRoleIdAsync(RoleUserSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return roleUserRepository.GetListByRoleIdAsync(param);

        }

        #endregion

    }
}
