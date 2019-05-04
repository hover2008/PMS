using JW.Core.Configuration;
using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.Enum;
using JW.Domain.Sys.RequestParam;
using JW.Services.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public partial class LogService : BaseService<LogEntity, ILogRepository<LogEntity>> , ILogService<LogEntity>
    {
        #region Fields

        private readonly SysManageSecurityConfig config;
        private readonly ILogRepository<LogEntity> logRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHelper webHelper;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public LogService(SysManageSecurityConfig config,
            ILogRepository<LogEntity> logRepository,
            IHttpContextAccessor httpContextAccessor,
            IWebHelper webHelper,
            Messages messages)
            : base(logRepository)
        {
            this.config = config;
            this.logRepository = logRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.webHelper = webHelper;
            this.messages = messages;
        }

        #endregion

        #region Methods


        #endregion

        #region Methods Async

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="action">动作</param> 
        /// <param name="data">数据</param>
        /// <param name="userId">用户编号</param>
        /// <param name="userName">用户名称</param>
        /// <param name="ip">IP</param>
        public async Task<bool> AddLogAsync(string action, string data, int userId, string userName)
        {
            var model = new LogEntity(action, webHelper.GetRawUrl(httpContextAccessor.HttpContext.Request), httpContextAccessor.HttpContext.Request.Method, data, userId, userName, webHelper.GetCurrentIpAddress());
            return await this.AddAsync(model);
        }

        /// <summary>
        /// 添加后台管理操作日志
        /// </summary>
        /// <param name="opType">操作日志类型</param>
        /// <param name="data">数据</param>
        /// <param name="userId">用户编号</param>
        /// <param name="userName">用户名称</param>
        public async Task AddLogAsync(OperatorLogEnum opType, string data, int userId, string userName)
        { 
            if (config.IsLog)
            {
                await this.AddLogAsync(opType.GetDisplayDescription(), data, userId, userName);
            } 
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
                bool result = await logRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 获取日志列表
        /// </summary> 
        /// <param name="entity">日志搜索实体</param>
        /// <returns>PageDataModel<LogEntity></returns>
        public Task<BasePagedListModel<LogEntity>> GetListAsync(LogSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return logRepository.GetListAsync(param);
        }

        #endregion

    }
}
