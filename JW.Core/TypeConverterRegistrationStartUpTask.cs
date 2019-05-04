using JW.Core.ComponentModel;
using JW.Core.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;

namespace JW.Core
{
    /// <summary>
    /// 注册自定义类型转换器的启动任务
    /// </summary>
    public class TypeConverterRegistrationStartUpTask : IStartupTask
    {
        /// <summary>
        /// 执行一个任务
        /// </summary>
        public void Execute()
        {
            //lists
            TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            TypeDescriptor.AddAttributes(typeof(List<decimal>), new TypeConverterAttribute(typeof(GenericListTypeConverter<decimal>)));
            TypeDescriptor.AddAttributes(typeof(List<string>), new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));

            //dictionaries
            TypeDescriptor.AddAttributes(typeof(Dictionary<int, int>), new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<int, int>)));
        }

        /// <summary>
        /// 获取这个启动任务实现的顺序
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
