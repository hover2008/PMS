﻿namespace JW.Domain.PMS.ResposneEntity
{
    public class SelectDWLBEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public int PId { get; set; }
    }
}
