﻿using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Data.PMS.IRepository;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.ResposneEntity;
using JW.Services.IService;
using JW.Services.PMS.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.Service
{
    /// <summary>
    /// 模型服务
    /// </summary>
    public partial class DWLBService : BaseService<DWLBEntity, IDWLBRepository<DWLBEntity>>, IDWLBService<DWLBEntity>
    {
        #region Fields

        private readonly IDWLBRepository<DWLBEntity> repository;
        private readonly Messages messages;

        #endregion

        #region Ctor

        public DWLBService(IDWLBRepository<DWLBEntity> repository,
            Messages messages)
            : base(repository)
        {
            this.repository = repository;
            this.messages = messages;
        }

        #endregion

        #region Methods

        public Messages Save(DWLBEntity model)
        {
            if (model != null && model.Name.IsNotNullOrEmpty())
            { 
                int result = repository.Save(model);
                if (result > 0)
                {
                    messages.Msg = "保存成功！！";
                    messages.Success = true;
                }
                else if (result == -9999)
                {
                    messages.Msg = "存在相同分类代码的数据";
                }
                else if (result == -10000)
                {
                    messages.Msg = "存在相同分类名称的数据";
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

        public async Task<IEnumerable<DWLBEntity>> GetAllListAsync()
        {
            return await repository.GetAllListAsync();
        }

        public async Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync(int id = 0)
        {
            return await repository.GetSelectCanUseListAsync(id);
        }

        #endregion
    }
}
