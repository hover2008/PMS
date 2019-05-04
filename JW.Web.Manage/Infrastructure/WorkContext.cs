using JW.Core.Configuration;
using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using JW.Services.Sys.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using ICookieManager = JW.Core.Helper.Cookies.ICookieManager;

namespace JW.Web.Manage.Infrastructure
{
    public class WorkContext : IWorkContext
    {
        #region Fields

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICookieManager cookieManager;
        private readonly IUserService<UserEntity> userService;
        private readonly ProjectConfig projectConfig;

        #endregion

        #region Ctor

        public WorkContext(IHttpContextAccessor httpContextAccessor,
            ICookieManager cookieManager,
            IUserService<UserEntity> userService,
            ProjectConfig projectConfig)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cookieManager = cookieManager;
            this.userService = userService;
            this.projectConfig = projectConfig;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 用户Key[唯一标识]
        /// </summary>
        // TODO 设置Cookie过期时间为默认的1天
        public virtual string UserKey => cookieManager.GetOrSet<string>(projectConfig.CookieNamePrefix + "UserKey", () => Guid.NewGuid().ToString(), projectConfig.CookieExpire);

        /// <summary>
        /// 获取当前登录用户编号
        /// </summary>
        /// <returns></returns>
        public async Task<UserClaimModel> GetCurrentUserClaim()
        {
            UserClaimModel userClaimModel = new UserClaimModel();
            int userId = 0;
            var auth = await httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (auth.Succeeded)
            {
                string[] info = auth.Principal.Identity.Name.Split("|||");
                Int32.TryParse(info[0], out userId);
                userClaimModel.UserName = info[1]; 
            }
            userClaimModel.UserId = userId;
            return userClaimModel;
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserEntity> GetCurrentUser()
        {
            var userClaim = await GetCurrentUserClaim();
            return await userService.GetModelByIdAsync(userClaim.UserId);
        }

        #endregion

    }
}
