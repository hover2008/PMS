using JW.Domain.Sys.Entity;
using System.Collections.Generic;

namespace JW.Domain.Sys.ResposneEntity
{
    public class UserDetialsEntity
    {
        public UserEntity User { get; set; }

        public IEnumerable<string> RoleNames { get; set; } = new List<string>();
    }
}
