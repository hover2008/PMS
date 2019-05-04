using System;
using System.Collections.Generic;
using System.Reflection;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 实现这个接口的类为引擎中的各种服务提供关于类型的信息
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 查找类型的类
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="onlyConcreteClasses">指示是否只查找具体类的值</param>
        /// <returns>IEnumerable<Type></returns>
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>
        /// 查找类型的类
        /// </summary>
        /// <param name="assignTypeFrom">从中分配类型</param>
        /// <param name="onlyConcreteClasses">指示是否只查找具体类的值</param>
        /// <returns>IEnumerable<Type></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>
        /// 查找类型的类
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="assemblies">Assemblies</param>
        /// <param name="onlyConcreteClasses">指示是否只查找具体类的值</param>
        /// <returns>IEnumerable<Type></returns>
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        /// <summary>
        /// 查找类型的类
        /// </summary>
        /// <param name="assignTypeFrom">从中分配类型</param>
        /// <param name="assemblies">Assemblies</param>
        /// <param name="onlyConcreteClasses">指示是否只查找具体类的值</param>
        /// <returns>IEnumerable<Type></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        /// <summary>
        /// 获取与当前实现相关的程序集
        /// </summary>
        /// <returns>程序集列表</returns>
        IList<Assembly> GetAssemblies();

    }
}
