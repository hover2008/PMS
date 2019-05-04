using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.CMS.IRepository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Services.CMS.IService;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 友情链接服务
    /// </summary>
    public partial class FriendLinksService : BaseService<FriendLinksEntity, IFriendLinksRepository<FriendLinksEntity>>, IFriendLinksService<FriendLinksEntity>
    {
        #region Fields

        private readonly IFriendLinksRepository<FriendLinksEntity> flRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public FriendLinksService(IFriendLinksRepository<FriendLinksEntity> flRepository,
            Messages messages)
            : base(flRepository)
        {
            this.flRepository = flRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(FriendLinksEntity model)
        {
            if (model != null && model.FL_NAME.IsNotNullOrEmpty() && model.FL_WEBURL.IsNotNullOrEmpty())
            {
                model.FL_TITLE = model.FL_TITLE ?? "";
                model.FL_LOGOURL = model.FL_LOGOURL ?? "";
                int result = flRepository.Save(model);
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
            return flRepository.GetMaxOrderId();
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
                        flRepository.UpdateOrderId(dt);
                        messages.Msg = "更新成功！！";
                        messages.Success = true;
                    }
                }
            }
            return messages;
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
                bool result = await flRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 获取友情链接列表
        /// </summary> 
        /// <param name="param">搜索实体</param>
        /// <returns>PageDataModel<FriendLinksEntity></returns>
        public Task<BasePagedListModel<FriendLinksEntity>> GetListAsync(FriendLinksSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return flRepository.GetListAsync(param);
        } 

        #endregion

    }
}
