using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.PMS.IRepository;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.RequestParam;
using JW.Domain.PMS.ResposneEntity;
using JW.Services.IService;
using JW.Services.PMS.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 模型服务
    /// </summary>
    public partial class DJLBService : BaseService<DJLBEntity, IDJLBRepository<DJLBEntity>>, IDJLBService<DJLBEntity>
    {
        #region Fields

        private readonly IDJLBRepository<DJLBEntity> repository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public DJLBService(IDJLBRepository<DJLBEntity> repository,
            Messages messages)
            : base(repository)
        {
            this.repository = repository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(DJLBEntity model)
        {
            if (model != null && model.DJDLId>0 && model.DJQZ.IsNotNullOrEmpty() && model.Name.IsNotNullOrEmpty())
            {
                model.Remark = model.Remark ?? "";
                int result = repository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "保存成功！！";
                    messages.Success = true;
                }
                else if (result == -9999)
                {
                    messages.Msg = "存在相同单据前缀的数据";
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

        #endregion

        #region Methods Async

        public async Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled)
        {
            if (id > 0)
            {
                bool result = await repository.UpdateDisabledByIdAsync(id, disabled);
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="param">搜索实体</param>
        public Task<BasePagedListModel<ListDJLBEntity>> GetListAsync(DJLBSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            
            return repository.GetListAsync(param);
        }

        /// <summary>
        /// 获取可用于下拉框选择的Model数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SelectDJLBEntity>> GetSelectCanUseListAsync()
        {
            return repository.GetSelectCanUseListAsync();
        }

        #endregion
    }
}
