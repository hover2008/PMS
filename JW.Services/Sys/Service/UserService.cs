using JW.Core.Configuration;
using JW.Core.Data.Base;
using JW.Core.Encrypt;
using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Core.ResponseResult;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.Enum;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using JW.Services.IService;
using JW.Services.Sys.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.Sys.Service
{
    /// <summary>
    /// 系统用户服务
    /// </summary>
    public class UserService : BaseService<UserEntity, IUserRepository<UserEntity>>, IUserService<UserEntity>
    {
        #region Fields

        private readonly SysManageSecurityConfig config;
        private readonly IUserRepository<UserEntity> userRepository;
        private readonly IRoleUserService<Role2UserEntity> roleUserService;
        private readonly ILogService<LogEntity> logService;
        private readonly IWebHelper webHelper;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public UserService(SysManageSecurityConfig config,
            IUserRepository<UserEntity> userRepository,
            IRoleUserService<Role2UserEntity> roleUserService,
            ILogService<LogEntity> logService,
            IWebHelper webHelper,
            Messages messages)
            : base(userRepository)
        {
            this.config = config;
            this.userRepository = userRepository;
            this.roleUserService = roleUserService;
            this.logService = logService;
            this.webHelper = webHelper;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(UserParam model)
        {
            if (model != null && model.U_NAME.IsNotNullOrEmpty() && model.U_REALNAME.IsNotNullOrEmpty())
            {
                UserEntity user = new UserEntity();
                user.U_ID = model.U_ID;
                user.U_NAME = model.U_NAME;
                if (model.U_PWD.IsNotNullOrEmpty())
                {
                    string encrypt = RandomHelper.CreateRandomStr(6);
                    user.U_ENCRYPT = encrypt;
                    user.U_PWD = MD5Encrypt.GetPass(model.U_PWD, encrypt);
                }
                user.U_REALNAME = model.U_REALNAME;
                user.U_EMAIL = model.U_EMAIL ?? "";
                user.U_MOBILE = model.U_MOBILE ?? "";
                user.U_TEL = model.U_TEL ?? "";
                user.U_PHOTO = model.U_PHOTO ?? "";
                using (DataTable roleDT = new DataTable())
                {
                    roleDT.Columns.Add("roleid", typeof(int));
                    roleDT.Columns.Add("userid", typeof(int));
                    if (model.RoleIds.IsNotNullOrEmpty())
                    {
                        string[] userRoleArr = model.RoleIds.TrimEnd(',').Split(',');
                        if (userRoleArr != null && userRoleArr.Length > 0)
                        {
                            foreach (string s in userRoleArr)
                            {
                                DataRow dr = roleDT.NewRow();
                                dr[0] = Convert.ToInt32(s);
                                dr[1] = model.U_ID;
                                roleDT.Rows.Add(dr);
                            }
                        }
                    }
                    int result = 0;
                    if (model.U_ID > 0)
                    {
                        result = userRepository.Modify(user, roleDT);
                    }
                    else
                    {
                        result = userRepository.Add(user, roleDT);
                    }
                    if (result > 0)
                    {
                        messages.Msg = "保存成功";
                        messages.Success = true;
                    }
                    else if (result == -10000)
                    {
                        messages.Msg = "存在相同用户名的数据";
                    }
                    else
                    {
                        messages.Msg = "保存失败";
                    }
                } 
            } 
            return messages;
        }

        public UserEntity GetModelByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
            return userRepository.GetModelByUserName(userName);
        }

        #endregion

        #region Methods Async 

        public async Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled)
        {
            if (id > 0)
            {
                bool result = await userRepository.UpdateDisabledByIdAsync(id, disabled);
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public async Task<Messages> ModifyInfoAsync(UserEntity model)
        {
            if (model != null)
            {
                bool result = await userRepository.ModifyInfoAsync(model);
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public Task<IEnumerable<UserPermissionCodeEntity>> GetPermissionByUserIdAsync(int userId)
        {
            return userRepository.GetPermissionByUserIdAsync(userId);
        }

        public Task<String> GetRoleByUserIdAsync(int userId)
        {
            return userRepository.GetRoleByUserIdAsync(userId);
        }

        public Task SetLockScreenAsync(int id, bool lockScreen)
        {
            return userRepository.SetLockScreenAsync(id, lockScreen);
        }

        public async Task<Messages> ModifyPhotoAsync(int id, string photo)
        {
            if (id > 0 && photo.IsNotNullOrEmpty())
            {
                bool result = await userRepository.ModifyPhotoAsync(id, photo);
                messages.Msg = result ? "修改成功！！" : "修改失败";
                messages.Success = result;
            }
            return messages;
        }

        public async Task<UserDetialsEntity> GetDetialsEntityAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            UserDetialsEntity userDetials = new UserDetialsEntity();
            userDetials.User = await this.GetModelByIdAsync(id);

            userDetials.RoleNames =await roleUserService.GetRoleNameListByUserIdAsync(id);
            return userDetials;
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
                bool result = await userRepository.DeleteByIdsAsync(ids);
                messages.Msg = result ? "删除成功！！" : "删除失败！！";
                messages.Success = result;
            }
            return messages;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">密码</param> 
        /// <returns>Task<(bool Succeeded, string Msg)></returns>
        public async Task<(bool Succeeded, string Msg, int UserId)> Login(string userName, string userPwd)
        {
            bool succeeded = false;
            string msg = string.Empty;
            int userId = 0;
            UserEntity user = this.GetModelByUserName(userName);
            if (user != null && user.U_ID > 0)
            {
                userId = user.U_ID;
                int errorTimes = 0;
                string pwd1 = MD5Encrypt.MD5(userPwd + user.U_ENCRYPT);
                if (user.U_DISABLED == false)
                {
                    //登录错误次数
                    int maxLoginFailedTimes = config.MaxLoginFailedTimes;
                    if (maxLoginFailedTimes <= 0)
                    {
                        maxLoginFailedTimes = 5;
                    }
                    if (user.U_ERRORTIMES < maxLoginFailedTimes)
                    {
                        if (user.U_PWD == pwd1)
                        {
                            succeeded = true;
                            msg = "登录系统，成功";
                        }
                        else
                        {
                            errorTimes = user.U_ERRORTIMES + 1;
                            int sErrorTimes = maxLoginFailedTimes - errorTimes;
                            if (sErrorTimes > 0)
                            {
                                msg = "密码错误，您今天还可尝试" + sErrorTimes + "次";
                            }
                            else
                            {
                                msg = "您今天登录错误次数过多，今天不可再登录，欢迎明天回来";
                            }
                        }
                    }
                    else
                    {
                        errorTimes = user.U_ERRORTIMES + 1;
                        msg = "您今天登录错误次数过多，今天不可再登录，欢迎明天回来";
                    }
                    //更新用户登录信息
                    await this.UpdateByLoginAsync(user.U_ID, webHelper.GetCurrentIpAddress(), errorTimes);
                }
                else
                {
                    msg = "登录系统，该用户状态为禁止登录";
                }
            }
            else
            {
                msg = "用户名不存在";
            }
            //记录登录日志
            await logService.AddLogAsync(OperatorLogEnum.Login, msg, userId, userName);
            return (succeeded, msg, userId);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="currentPassword">当前密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>Task<(bool Succeeded, string Msg)></returns>
        public async Task<Messages> ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword)
        {
            if (currentPassword.IsNotNullOrEmpty() && newPassword.IsNotNullOrEmpty())
            {
                currentPassword = MD5Encrypt.MD5(currentPassword + user.U_ENCRYPT);
                if (currentPassword == user.U_PWD)
                {
                    string encrypt = RandomHelper.CreateRandomStr(6);
                    newPassword = MD5Encrypt.MD5(newPassword + encrypt);
                    bool result = await userRepository.ModifyPwdAsync(user.U_ID, newPassword, encrypt);
                    messages.Msg = result ? "修改密码成功，请重新登录！！" : "修改密码失败！！";
                    messages.Success = result;
                }
                else
                {
                    messages.Msg = "旧密码错误！！";
                }
            }
            await logService.AddLogAsync(OperatorLogEnum.Update, messages.Msg, user.U_ID, user.U_NAME);
            return messages;
        }

        public Task UpdateByLoginAsync(int id, string ip, int errorTimes)
        {
            return userRepository.UpdateByLoginAsync(id, ip, errorTimes);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="entity">用户搜索实体</param>
        /// <returns>PageDataModel<UserEntity></returns>
        public Task<BasePagedListModel<UserEntity>> GetListAsync(UserSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return userRepository.GetListAsync(param);
        }

        /// <summary>
        /// 根据角色编号获取可用用户列表
        /// </summary> 
        /// <param name="entity">角色用户搜索实体</param>
        /// <returns>PageDataModel<LogEntity></returns>
        public Task<BasePagedListModel<UserEntity>> GetCanUserListByRoleIdAsync(RoleUserSearchParam param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));

            return userRepository.GetCanUserListByRoleIdAsync(param);
        }

        #endregion
    }
}
