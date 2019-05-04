using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.Enum;
using JW.Services.IService;
using JW.Services.Sys.IService;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.Sys.Service
{
    /// <summary>
    /// 角色菜单权限码服务
    /// </summary>
    public partial class RoleMenuPurviewCodeService : BaseService<RoleMenuPurviewCodeEntity, IRoleMenuPurviewCodeRepository<RoleMenuPurviewCodeEntity>>, IRoleMenuPurviewCodeService<RoleMenuPurviewCodeEntity> 
    {
        #region Fields

        private readonly IRoleMenuPurviewCodeRepository<RoleMenuPurviewCodeEntity> rmpcRepository;
        private readonly ILogService<LogEntity> logService;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public RoleMenuPurviewCodeService(IRoleMenuPurviewCodeRepository<RoleMenuPurviewCodeEntity> rmpcRepository,
            ILogService<LogEntity> logService,
            Messages messages)
            : base(rmpcRepository)
        {
            this.rmpcRepository = rmpcRepository;
            this.logService = logService;
            this.messages = messages;
        }

        #endregion

        #region Methods


        #endregion

        #region Method Async

        public async Task<Messages> BatchSaveAsync(int roleId, IList<string> codes, UserClaimModel userClaim)
        {
            if (roleId > 0 && codes != null && codes.Count > 0)
            {
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("r_id", typeof(int));
                    dt.Columns.Add("mpc_code", typeof(string));
                    foreach (string code in codes)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = roleId;
                        dr[1] = code;
                        dt.Rows.Add(dr);
                    }
                    bool result = rmpcRepository.BatchSave(dt, roleId);
                    messages.Msg = result ? "更新角色权限成功！" : "更新角色权限失败";
                    messages.Success = result;
                    await logService.AddLogAsync(OperatorLogEnum.Update, string.Format("更新角色编号为：{0}权限,{1}", roleId, messages.Msg), userClaim.UserId, userClaim.UserName);
                }
            }
            return messages;
        }

        public Task<IEnumerable<RoleMenuPurviewCodeEntity>> GetPurviewCodeListByRoleIdAsync(int roleId)
        {
            return rmpcRepository.GetPurviewCodeListByRoleIdAsync(roleId);
        }

        #endregion
    }
}
