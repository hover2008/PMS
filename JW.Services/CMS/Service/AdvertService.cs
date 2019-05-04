using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.CMS.IRepository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Domain.Shared;
using JW.Services.CMS.IService;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 广告服务
    /// </summary>
    public partial class AdvertService : BaseService<AdvertEntity, IAdvertRepository<AdvertEntity>>, IAdvertService<AdvertEntity>
    {
        #region Fields

        private readonly IAdvertRepository<AdvertEntity> advertRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public AdvertService(IAdvertRepository<AdvertEntity> advertRepository,
            Messages messages)
            : base(advertRepository)
        {
            this.advertRepository = advertRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(AdvertEntity model, UserClaimModel userClaim)
        {
            if (model != null && model.A_TITLE.IsNotNullOrEmpty() && model.A_URL.IsNotNullOrEmpty())
            { 
                model.A_PicUrl = model.A_PicUrl ?? "";
                model.A_AddMan = userClaim.UserName;
                model.A_SUMMARY = model.A_SUMMARY ?? "";
                int result = advertRepository.Save(model);
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
        
        public int GetMaxOrderId(int typeId)
        {
            if (typeId <= 0)
            {
                return 1;
            }
            return advertRepository.GetMaxOrderId(typeId);
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
                        advertRepository.UpdateOrderId(dt);
                        messages.Msg = "更新成功！！";
                        messages.Success = true;
                    }
                }
            }
            return messages;
        }

        #endregion

        #region Methods Async

        public async Task<Messages> UpdateStateByIdAsync(int id, byte state)
        {
            if (id > 0)
            {
                bool result = await advertRepository.UpdateStateByIdAsync(id, state);
                messages.Msg = result ? "设置成功！！" : "设置失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">用英文逗号隔开字符串编号组，如："6,7,8"</param>
        /// <returns></returns>
        public async Task<Messages> DeleteByIdsAsync(IList<int> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                bool result = await advertRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 获取广告列表
        /// </summary> 
        /// <param name="entity">搜索实体</param>
        /// <returns>PageDataModel<AdvertEntity></returns>
        public Task<BasePagedListModel<AdvertEntity>> GetListAsync(AdvertSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return advertRepository.GetListAsync(param);
        }

        #endregion
    }
}
