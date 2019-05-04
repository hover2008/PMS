using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.CMS.IRepository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.CMS.IService;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 模型服务
    /// </summary>
    public partial class ModelService : BaseService<ModelEntity, IModelRepository<ModelEntity>>, IModelService<ModelEntity>
    {
        #region Fields

        private readonly IModelRepository<ModelEntity> modelRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public ModelService(IModelRepository<ModelEntity> modelRepository,
            Messages messages)
            : base(modelRepository)
        {
            this.modelRepository = modelRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(ModelEntity model)
        {
            if (model != null && model.M_NAME.IsNotNullOrEmpty() && model.M_TABLENAME.IsNotNullOrEmpty() && model.M_MANAGEURL.IsNotNullOrEmpty())
            {
                model.M_DESCRIPTION = model.M_DESCRIPTION ?? "";
                int result = modelRepository.Save(model);
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

        public int GetMaxOrderId()
        {
            return modelRepository.GetMaxOrderId();
        }  

        public Messages UpdateOrderId(IList<int> ids, IList<int> orderids)
        {
            if (ids != null && ids.Count > 0 && orderids != null && orderids.Count > 0)
            {
                if (ids.Count == orderids.Count)
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Columns.Add("id", typeof(int));
                        dt.Columns.Add("oriderid", typeof(int));
                        int j = 0;
                        foreach (int i in ids)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = i;
                            dr[1] = orderids[j];
                            dt.Rows.Add(dr);
                            j++;
                        }
                        modelRepository.UpdateOrderId(dt);
                        messages.Msg = "更新成功！！";
                        messages.Success = true;
                    }
                }
            }
            return messages;
        }

        #endregion

        #region Methods Async

        public async Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled)
        {
            if (id > 0)
            {
                bool result = await modelRepository.UpdateDisabledByIdAsync(id, disabled);
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="param">搜索实体</param>
        public Task<BasePagedListModel<ModelEntity>> GetListAsync(ModelSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            
            return modelRepository.GetListAsync(param);
        }

        /// <summary>
        /// 获取可用于下拉框选择的Model数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SelectModelEntity>> GetSelectCanUseListAsync()
        {
            return modelRepository.GetSelectCanUseListAsync();
        }

        #endregion
    }
}
