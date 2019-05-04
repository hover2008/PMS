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
    /// 内容（文章、新闻）服务
    /// </summary>
    public partial class ContentService : BaseService<ContentEntity, IContentRepository<ContentEntity>>, IContentService<ContentEntity>
    {
        #region Fields

        private readonly IContentRepository<ContentEntity> contentRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public ContentService(IContentRepository<ContentEntity> contentRepository,
            Messages messages)
            : base(contentRepository)
        {
            this.contentRepository = contentRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(ContentEntity model, UserClaimModel userClaim)
        {
            if (model != null && model.C_TITLE.IsNotNullOrEmpty() && model.C_SUBTITLE.IsNotNullOrEmpty() && model.C_SUMMARY.IsNotNullOrEmpty() && model.C_AUTHOR.IsNotNullOrEmpty() && model.C_SOURCE.IsNotNullOrEmpty() && model.C_CONTENT.IsNotNullOrEmpty())
            {
                model.C_TITLE = model.C_TITLE.HtmlEncode();
                model.C_SUBTITLE = model.C_SUBTITLE.HtmlEncode();
                model.C_IMAGEURL = model.C_IMAGEURL?.HtmlEncode() ?? "";
                model.C_SUMMARY = model.C_SUMMARY.HtmlEncode();
                model.C_AUTHOR = model.C_AUTHOR.HtmlEncode();
                model.C_SOURCE = model.C_SOURCE.HtmlEncode();
                model.C_KEYWORDS = model.C_KEYWORDS?.HtmlEncode() ?? "";
                model.C_ADDUSERNAME = userClaim.UserName;
                model.C_LASTEDITUSERNAME = userClaim.UserName;
                int result = contentRepository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "提交成功！！";
                    messages.Success = true;
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

        public int GetMaxOrderId(int catid)
        {
            if (catid <= 0)
            {
                return 1;
            }
            return contentRepository.GetMaxOrderId(catid);
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
                        contentRepository.UpdateWeight(dt);
                        messages.Msg = "更新成功！！";
                        messages.Success = true;
                    }
                }
            }
            return messages;
        }

        #endregion

        #region Method Async

        public Task UpdateHitsByIdAsync(int id)
        {
            if (id > 0)
            {
               return contentRepository.UpdateHitsByIdAsync(id);
            }
            return Task.CompletedTask;
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
                bool result = await contentRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public async Task<Messages> SetByIdsAsync(IList<int> ids, string action, int steps, int wfid)
        {
            if (ids != null && ids.Count > 0 && action.IsNotNullOrEmpty())
            {
                bool result = false;
                if (action == "top")
                {
                    #region 置顶
                    result = await contentRepository.SetByIdsAsync(ids, 1, "C_ISTOP");
                    #endregion
                }
                else if (action == "canceltop")
                {
                    #region 取消置顶
                    result = await contentRepository.SetByIdsAsync(ids, 0, "C_ISTOP");
                    #endregion
                }
                else if (action == "rec")
                {
                    #region 推荐
                    result = await contentRepository.SetByIdsAsync(ids, 1, "C_ISREC");
                    #endregion
                }
                else if (action == "cancelrec")
                {
                    #region 取消推荐
                    result = await contentRepository.SetByIdsAsync(ids, 0, "C_ISREC");
                    #endregion
                }
                if (action == "CK-BH-unverify")
                {
                    #region 退稿
                    result = await contentRepository.SetByIdsAsync(ids, 0, "C_STATUS");
                    #endregion
                }
                else if (action == "CK-BH-verify")
                {
                    #region 通过审核
                    int flag = 99;
                    if (steps == 4)
                    {
                        flag = 99;
                    }
                    else if (steps == 3)
                    {
                        if (wfid > 3) { flag = 4; }
                    }
                    else if (steps == 2)
                    {
                        if (wfid > 2) { flag = 3; }
                    }
                    else if (steps == 1)
                    {
                        if (wfid > 1) { flag = 2; }
                    }
                    else
                    {
                        if (wfid > 0) { flag = 1; }
                    }
                    result = await contentRepository.SetByIdsAsync(ids, flag, "C_STATUS");
                    #endregion
                }
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public Task<BasePagedListModel<Content2StatusNameEntity>> GetListAsync(ContentSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return contentRepository.GetListAsync(param);
        }

        #endregion

    }
}
