using JW.Core;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.CMS.IRepository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.CMS.IService;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 字典数据服务
    /// </summary>
    public partial class DictionaryService : BaseService<DictionaryEntity, IDictionaryRepository<DictionaryEntity>>, IDictionaryService<DictionaryEntity>
    {
        #region Fields

        private readonly IDictionaryRepository<DictionaryEntity> dicRepository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public DictionaryService(IDictionaryRepository<DictionaryEntity> dicRepository,
            Messages messages)
            : base(dicRepository)
        {
            this.dicRepository = dicRepository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(DictionaryEntity model)
        {
            if (model != null && model.D_NAME.IsNotNullOrEmpty())
            {
                model.D_REMARK = model.D_REMARK ?? "";
                int result = 0;
                if (model.D_ID > 0)
                {
                    result = dicRepository.Modify(model);
                }
                else
                {
                    result = dicRepository.Add(model);
                }
                if (result > 0)
                {
                    messages.Msg = "提交成功！！";
                    messages.Success = true;
                }
                else if (result == -10000)
                {
                    messages.Msg = "同级字典存在相同的数据";
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
                    result = await dicRepository.UpdateDisabledByIdAsync(id, false);
                    #endregion
                }
                else if (action == "disabled")
                {
                    #region 禁用
                    result = await dicRepository.UpdateDisabledByIdAsync(id, true);
                    #endregion
                }
                else if (action == "CK-BG-up")
                {
                    #region 上移
                    result = dicRepository.ChangeSort(id, MoveType.Up) > 0;
                    #endregion
                }
                else if (action == "CK-BG-down")
                {
                    #region 下移
                    result = dicRepository.ChangeSort(id, MoveType.Down) > 0;
                    #endregion
                }
                messages.Msg = result ? "操作成功！！" : "操作失败！！";
                messages.Success = result;
            }
            return messages;
        }

        public Task<String> GetNameByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Task.FromResult(String.Empty);
            }
            return dicRepository.GetNameByIdAsync(id);
        }

        public Task<IEnumerable<DictionaryEntity>> GetListByPIdAsync(int pid)
        {
            return dicRepository.GetListByPIdAsync(pid);
        }

        public Task<IEnumerable<DictionaryEntity>> GetAllListAsync()
        {
            return dicRepository.GetAllListAsync();
        }

        public Task<IEnumerable<DictionaryEntity>> GetSubsetListByIdAsync(int id)
        {
            return dicRepository.GetSubsetListByIdAsync(id);
        }

        public Task<IEnumerable<SelectDictionaryEntity>> GetSelectCanUseListAsync(int id)
        {
            return dicRepository.GetSelectCanUseListAsync(id);
        }

        #endregion

    }
}
