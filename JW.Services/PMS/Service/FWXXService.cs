using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.PMS.IRepository;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.RequestParam;
using JW.Services.IService;
using JW.Services.PMS.IService;
using System;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 模型服务
    /// </summary>
    public partial class FWXXService : BaseService<FWXXEntity, IFWXXRepository<FWXXEntity>>, IFWXXService<FWXXEntity>
    {
        #region Fields

        private readonly IFWXXRepository<FWXXEntity> repository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public FWXXService(IFWXXRepository<FWXXEntity> repository,
            Messages messages)
            : base(repository)
        {
            this.repository = repository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(FWXXEntity model)
        {
            if (model != null && model.FWBH.IsNotNullOrEmpty() && model.FWWZ.IsNotNullOrEmpty() && model.FWMJ.IsNotNullOrEmpty())
            { 
                int result = repository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "保存成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "存在相同房屋编号的数据";
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
        public Task<BasePagedListModel<FWXXEntity>> GetListAsync(FWXXSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            
            return repository.GetListAsync(param);
        }

        #endregion
    }
}
