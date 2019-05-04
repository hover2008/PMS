using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using JW.Services.IService;
using JW.Services.Sys.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.Sys.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public partial class RoleService : BaseService<RoleEntity, IRoleRepository<RoleEntity>>, IRoleService<RoleEntity>
    {
        #region Fields

        private readonly IRoleRepository<RoleEntity> roleRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public RoleService(IRoleRepository<RoleEntity> roleRepository,
            Messages messages)
            : base(roleRepository)
        {
            this.roleRepository = roleRepository;
            this.messages = messages;
        }

        #endregion

        #region Method

        public Messages Save(RoleEntity model)
        {
            if (model != null && model.R_NAME.IsNotNullOrEmpty())
            {
                int result = roleRepository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "保存成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "存在相同名称的数据";
                }
                else
                {
                    messages.Msg = "保存失败！！";
                }
            }
            else
            {
                messages.Msg = "请填写必填字段信息";
            }
            return messages;
        }

        public Messages GetMaxOrderId()
        {
            messages.Success = true;
            messages.Msg = roleRepository.GetMaxOrderId().ToString();
            return messages;
        }

        public void UpdateOrderId(DataTable dt)
        {
            roleRepository.UpdateOrderId(dt);
        } 

        #endregion

        #region Methods Async

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">用英文逗号隔开字符串编号组，如："6,7,8"</param>
        /// <returns></returns>
        public async Task<Messages> DeleteByIdsAsync(IList<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                bool result = await roleRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public Task<String> GetPermissionByRoleIdAsync(int roleId)
        {
            return roleRepository.GetPermissionByRoleIdAsync(roleId);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="param">角色搜索实体</param>
        /// <returns>PageDataModel<RoleEntity></returns>
        public Task<BasePagedListModel<RoleEntity>> GetListAsync(RoleSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return roleRepository.GetListAsync(param);
        }

        public Task<IEnumerable<SelectRoleEntity>> GetSelectListAsync()
        {
            return roleRepository.GetSelectListAsync();
        }

        #endregion
    }
}
