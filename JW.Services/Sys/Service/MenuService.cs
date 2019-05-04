using JW.Core;
using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    /// <summary>
    /// 系统菜单服务
    /// </summary>
    public partial class MenuService : BaseService<MenuEntity, IMenuRepository<MenuEntity>>, IMenuService<MenuEntity>
    {
        #region Fields

        private readonly IMenuRepository<MenuEntity> menuRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public MenuService(IMenuRepository<MenuEntity> menuRepository,
            Messages messages)
            : base(menuRepository)
        {
            this.menuRepository = menuRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(MenuEntity model)
        {
            if (model != null && model.M_NAME.IsNotNullOrEmpty() && model.M_CODE.IsNotNullOrEmpty())
            {
                model.M_ICON = model.M_ICON ?? "";
                model.M_LINK = model.M_LINK ?? "";
                int result = 0;
                if (model.M_ID > 0)
                {
                    result = menuRepository.Update(model);
                }
                else
                {
                    result = menuRepository.Add(model);
                }
                if (result > 0)
                {
                    messages.Msg = "提交成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "同级菜单存在相同的数据";
                }
                else
                {
                    messages.Msg = "提交失败！！";
                }
            }
            else
            {
                messages.Msg = "请填写必填字段信息";
            }

            return messages;
        }

        #endregion

        #region Methods Async

        public async Task<Messages> SetAsync(int id, string action)
        {
            if (id > 0 && action.IsNotNullOrEmpty())
            {
                bool result = false;
                if (action == "undisabled")
                {
                    #region 启用
                    result = await menuRepository.UpdateDisabledByIdAsync(id, false);
                    #endregion
                }
                else if (action == "disabled")
                {
                    #region 禁用
                    result = await menuRepository.UpdateDisabledByIdAsync(id, true);
                    #endregion
                }
                else if (action == "CK-AA-up")
                {
                    #region 上移
                    result = menuRepository.ChangeSort(id, MoveType.Up);
                    #endregion
                }
                else if (action == "CK-AA-down")
                {
                    #region 下移
                    result = menuRepository.ChangeSort(id, MoveType.Down);
                    #endregion
                }
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public Task<IEnumerable<MenuEntity>> GetListByRolesAsync(string roles)
        {
            return menuRepository.GetListByRolesAsync(roles);
        }

        /// <summary>
        /// 获取所有菜单模块列表
        /// </summary> 
        /// <param name="menuId">菜单编号</param>
        /// <returns>PageDataModel<MenuEntity></returns>
        public Task<BasePagedListModel<MenuEntity>> GetAllListAsync(int menuId)
        { 
            return menuRepository.GetAllListAsync(menuId);
        }

        /// <summary>
        /// 获取可用菜单模块列表
        /// </summary> 
        /// <returns>PageDataModel<MenuEntity></returns>
        public Task<BasePagedListModel<MenuEntity>> GetCanUseListAsync()
        { 
            return menuRepository.GetCanUseListAsync();
        }

        #endregion

    }
}
