using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Services.IService;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    /// <summary>
    /// 系统菜单权限码服务
    /// </summary>
    public partial class MenuPurviewCodeService : BaseService<MenuPurviewCodeEntity, IMenuPurviewCodeRepository<MenuPurviewCodeEntity>>, IMenuPurviewCodeService<MenuPurviewCodeEntity>
    {
        #region Fields

        private readonly IMenuPurviewCodeRepository<MenuPurviewCodeEntity> mpcRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public MenuPurviewCodeService(IMenuPurviewCodeRepository<MenuPurviewCodeEntity> mpcRepository,
            Messages messages)
            : base(mpcRepository)
        {
            this.mpcRepository = mpcRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(MenuPurviewCodeEntity model, string mcode)
        {
            if (model != null && mcode.Length > 0)
            {
                model.MPC_CODE = mcode + "-" + model.MPC_CODE;
                int result = mpcRepository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "保存成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "存在相同的权益编码";
                }
                else
                {
                    messages.Msg = "保存失败！！";
                }
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return messages;
        }

        #endregion

        #region Methods Async

        public async Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled)
        {
            if (id > 0)
            {
                bool result = await mpcRepository.UpdateDisabledByIdAsync(id, disabled);
                messages.Success = result;
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
            }
            return messages;
        }

        /// <summary>
        /// 获取菜单按钮列表
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="menuId">菜单模块编号</param>
        /// <returns>PageDataModel<MenuPurviewCodeEntity></returns>
        public Task<BasePagedListModel<MenuPurviewCodeEntity>> GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0)
        {
            return mpcRepository.GetListByMenuIdAsync(pageIndex, pageSize, menuId);
        }

        /// <summary>
        /// 获取菜单按钮列表
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="menuId">菜单模块编号</param>
        /// <returns>PageDataModel<MenuPurviewCodeEntity></returns>
        public Task<BasePagedListModel<MenuPurviewCodeEntity>> GetCanUseListAsync(int pageIndex, int pageSize)
        {
            return mpcRepository.GetCanUseListAsync(pageIndex, pageSize);
        }

        #endregion
    }
}
