using JW.Domain.CMS.Entity;
using System.Collections.Generic;
using System.Data;

namespace JW.Data.CMS.IRepository
{
    public partial interface ISettingRepository
    {
        int Save(DataTable dt, int groupId);
        IEnumerable<SettingEntity> GetConfigByGroupId(int groupId);
        string GetValuesByKeyAndGroupId(string keyName, int groupId);
    }
}
