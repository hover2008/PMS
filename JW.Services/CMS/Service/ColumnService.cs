using JW.Core;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.CMS.IRepository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.CMS.IService;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 栏目服务
    /// </summary>
    public partial class ColumnService : BaseService<ColumnEntity, IColumnRepository<ColumnEntity>>, IColumnService<ColumnEntity>
    {
        #region Fields

        private readonly IColumnRepository<ColumnEntity> columnRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public ColumnService(IColumnRepository<ColumnEntity> columnRepository,
            Messages messages)
            : base(columnRepository)
        {
            this.columnRepository = columnRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(ColumnEntity model)
        {
            if (model != null && model.C_NAME.IsNotNullOrEmpty())
            {
                model.C_NAME = model.C_NAME.HtmlEncode();
                model.C_ENAME = model.C_ENAME?.HtmlEncode() ?? "";
                model.C_Tips = model.C_Tips?.HtmlEncode() ?? "";
                model.C_IMAGE = model.C_IMAGE?.HtmlEncode() ?? "";
                model.C_SUMMARY = model.C_SUMMARY?.HtmlEncode() ?? "";
                model.C_LINK = model.C_LINK?.HtmlEncode() ?? "";
                model.C_MATETITLE = model.C_MATETITLE?.HtmlEncode() ?? "";
                model.C_MATEKEYWORDS = model.C_MATEKEYWORDS?.HtmlEncode() ?? "";
                model.C_MATEDESCRIPTION = model.C_MATEDESCRIPTION?.HtmlEncode() ?? "";
                model.C_CONTENT = model.C_CONTENT ?? "";
                int result = 0;
                if (model.C_ID > 0)
                {
                    result = columnRepository.Modify(model);
                }
                else
                {
                    result = columnRepository.Add(model);
                }
                if (result > 0)
                {
                    messages.Msg = "提交成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "同级栏目存在相同的数据";
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
                    result = await columnRepository.UpdateDisabledByIdAsync(id, false);
                    #endregion
                }
                else if (action == "disabled")
                {
                    #region 禁用
                    result = await columnRepository.UpdateDisabledByIdAsync(id, true);
                    #endregion
                }
                else if (action == "CK-BE-up")
                {
                    #region 上移
                    result = columnRepository.ChangeSort(id, MoveType.Up) > 0;
                    #endregion
                }
                else if (action == "CK-BE-down")
                {
                    #region 下移
                    result = columnRepository.ChangeSort(id, MoveType.Down) > 0;
                    #endregion
                }
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 除ID以外的可用栏目实体数据集合
        /// </summary>
        /// <param name="id">字典编号</param>
        /// <returns></returns>
        public Task<IEnumerable<SelectColumnEntity>> GetSelectCanUseListAsync(int id)
        {
            return columnRepository.GetSelectCanUseListAsync(id);
        }

        public Task<IEnumerable<Column2ModelEntity>> GetAllColumn2ModelListAsync()
        {
            return columnRepository.GetAllColumn2ModelListAsync();
        }

        public Task<IEnumerable<Column2Model2DictionaryEntity>> GetAllColumn2Model2DictionaryListAsync()
        {
            return columnRepository.GetAllColumn2Model2DictionaryListAsync();
        }

        #endregion
    }
}
