using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 使用磁盘文件系统的IO函数接口
    /// </summary>
    public interface ICKFileProvider : IFileProvider
    {
        /// <summary>
        /// 将字符串数组组合为路径
        /// </summary>
        /// <param name="paths">路径的一部分数组</param>
        /// <returns>组合路径</returns>
        string Combine(params string[] paths);

        /// <summary>
        /// 在指定路径中创建所有目录和子目录，除非它们已经存在
        /// </summary>
        /// <param name="path">创建目录</param>
        void CreateDirectory(string path);

        /// <summary>
        /// 创建或覆盖指定路径中的文件
        /// </summary>
        /// <param name="path">要创建的文件的路径和名称</param>
        void CreateFile(string path);

        /// <summary>
        /// 深度优先递归删除，处理在Windows资源管理器中打开的子目录
        /// </summary>
        /// <param name="path">目录路径</param>
        void DeleteDirectory(string path);

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="filePath">要删除的文件的名称。不支持通配符</param>
        void DeleteFile(string filePath);

        /// <summary>
        /// 确定给定路径是否引用磁盘上的现有目录
        /// </summary>
        /// <param name="path">测试路径</param>
        /// <returns>
        /// 如果路径引用现有目录，则为true；如果目录不存在，或者在尝试确定指定文件是否存在时发生错误，则为true
        /// </returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// 将文件或目录及其内容移动到新位置
        /// </summary>
        /// <param name="sourceDirName">要移动的文件或目录的路径</param>
        /// <param name="destDirName">目标目录</param>
        void DirectoryMove(string sourceDirName, string destDirName);

        /// <summary>
        /// 返回与指定路径中的搜索模式匹配的文件名的可枚举集合，并可选地搜索子目录
        /// </summary>
        /// <param name="directoryPath">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 与路径中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（*和？）的组合。字符，但不支持正则表达式
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是否搜索当前目录、当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 由路径指定的目录中与指定搜索模式匹配的文件的全名（包括路径）的可枚举集合
        /// </returns>
        IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern, bool topDirectoryOnly = true);

        /// <summary>
        /// 将现有文件复制到新文件。允许重写同名文件
        /// </summary>
        /// <param name="sourceFileName">要复制的文件</param>
        /// <param name="destFileName">目标文件的名称。这不能是目录</param>
        /// <param name="overwrite">如果可以覆盖目标文件，则为true；否则，为false</param>
        void FileCopy(string sourceFileName, string destFileName, bool overwrite = false);

        /// <summary>
        /// 确定指定文件是否存在
        /// </summary>
        /// <param name="filePath">要检查的文件</param>
        /// <returns>
        /// 如果调用方具有所需权限，则路径包含现有文件的名称；否则，为false
        /// </returns>
        bool FileExists(string filePath);

        /// <summary>
        /// 以字节为单位获取文件长度，或目录或非现有文件的长度为1
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的长度</returns>
        long FileLength(string path);

        /// <summary>
        /// 将指定的文件移动到新位置，提供指定新文件名的选项
        /// </summary>
        /// <param name="sourceFileName">要移动的文件的名称。可以包括相对路径或绝对路径</param>
        /// <param name="destFileName">文件的新路径和名称</param>
        void FileMove(string sourceFileName, string destFileName);

        /// <summary>
        /// 返回目录的绝对路径
        /// </summary>
        /// <param name="paths">路径的一部分数组</param>
        /// <returns>目录的绝对路径</returns>
        string GetAbsolutePath(params string[] paths);

        /// <summary>
        /// 返回指定文件或目录的创建日期和时间
        /// </summary>
        /// <param name="path">获取创建日期和时间信息的文件或目录</param>
        /// <returns>
        /// 设置为指定文件或目录的创建日期和时间的DealTimeType结构。此值在本地时间表示
        /// </returns>
        DateTime GetCreationTime(string path);

        /// <summary>
        /// 返回与指定目录中指定的搜索模式相匹配的子目录（包括它们的路径）的名称
        /// </summary>
        /// <param name="path">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 与路径中的子目录名匹配的搜索字符串。此参数可以包含有效文本和通配符的组合，但不支持正则表达式。
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是否搜索当前目录、当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 与指定的标准相匹配的子目录的完整名称（包括路径）的数组，或者如果没有找到目录，则是空数组
        /// </returns>
        string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true);

        /// <summary>
        /// 返回指定路径字符串的目录信息
        /// </summary>
        /// <param name="path">文件或目录的路径</param>
        /// <returns>
        /// 路径的目录信息
        /// </returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// 只返回指定路径字符串的目录名
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录名</returns>
        string GetDirectoryNameOnly(string path);

        /// <summary>
        /// 返回指定路径字符串的扩展名
        /// </summary>
        /// <param name="filePath">从中获取扩展的路径字符串</param>
        /// <returns>指定路径的扩展（包括.）</returns>
        string GetFileExtension(string filePath);

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="path">从中获取文件名和扩展名的路径字符串</param>
        /// <returns>路径中最后一个目录字符之后的字符</returns>
        string GetFileName(string path);

        /// <summary>
        /// 返回没有扩展名的指定路径字符串的文件名
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns>文件名，减去最后一个周期（.）及其后面的所有字符</returns>
        string GetFileNameWithoutExtension(string filePath);

        /// <summary>
        /// 返回与指定目录中指定的搜索模式相匹配的文件（包括它们的路径）的名称，使用一个值来确定是否搜索子目录
        /// </summary>
        /// <param name="directoryPath">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 搜索字符串匹配的路径中的文件名。此参数可以包含有效的文字路径和通配符（*和？）字符，但不支持正则表达式
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是否搜索当前目录、当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 指定目录中与指定搜索模式匹配的文件的完整名称（包括路径）数组，或者如果没有找到文件，则为空数组
        /// </returns>
        string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true);

        /// <summary>
        /// 返回上次访问指定文件或目录的日期和时间
        /// </summary>
        /// <param name="path">获取访问日期和时间信息的文件或目录</param>
        /// <returns>设置为指定文件的日期和时间的DealTime类型结构</returns>
        DateTime GetLastAccessTime(string path);

        /// <summary>
        /// 返回指定文件或目录上次写入的日期和时间
        /// </summary>
        /// <param name="path">获取写日期和时间信息的文件或目录</param>
        /// <returns>
        /// 将指定文件或目录最后写入的日期和时间设置的DealeTimeType结构。此值在本地时间表示
        /// </returns>
        DateTime GetLastWriteTime(string path);

        /// <summary>
        /// 返回指定的文件或目录最后写入的协调时间（UTC）中的日期和时间
        /// </summary>
        /// <param name="path">获取写日期和时间信息的文件或目录</param>
        /// <returns>
        /// 将指定文件或目录最后写入的日期和时间设置的DealeTime类型结构。此值以UTC时间表示
        /// </returns>
        DateTime GetLastWriteTimeUtc(string path);

        /// <summary>
        /// 检索指定路径的父目录
        /// </summary>
        /// <param name="directoryPath">检索父目录的路径</param>
        /// <returns>父目录，或如果路径为NULL是根目录，包括UNC服务器的根或共享名</returns>
        string GetParentDirectory(string directoryPath);

        /// <summary>
        /// 检查路径是否为目录
        /// </summary>
        /// <param name="path">检查路径</param>
        /// <returns>如果是目录则为true，否则为false</returns>
        bool IsDirectory(string path);

        /// <summary>
        /// 将虚拟路径映射到物理磁盘路径
        /// </summary>
        /// <param name="path">地图路径. E.g. "~/bin"</param>
        /// <returns>物理路径. E.g. "c:\inetpub\wwwroot\bin"</returns>
        string MapPath(string path);

        /// <summary>
        /// 将文件的内容读入字节数组
        /// </summary>
        /// <param name="filePath">阅读文件</param>
        /// <returns>包含文件内容的字节数组</returns>
        byte[] ReadAllBytes(string filePath);

        /// <summary>
        /// 打开一个文件，用指定的编码读取文件的所有行，然后关闭文件
        /// </summary>
        /// <param name="path">打开阅读的文件</param>
        /// <param name="encoding">应用于文件内容的编码</param>
        /// <returns>包含文件的所有行的字符串</returns>
        string ReadAllText(string path, Encoding encoding);

        /// <summary>
        /// 设置指定的文件最后写入的日期和时间，在协调通用时间（UTC）中
        /// </summary>
        /// <param name="path">用于设置日期和时间信息的文件</param>
        /// <param name="lastWriteTimeUtc">
        /// 一个包含DATE时间的DATETIME，用于设置路径的最后写入日期和时间。此值以UTC时间表示
        /// </param>
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

        /// <summary>
        /// 将指定的字节数组写入文件
        /// </summary>
        /// <param name="filePath">要写入的文件</param>
        /// <param name="bytes">写入文件的字节数</param>
        void WriteAllBytes(string filePath, byte[] bytes);

        /// <summary>
        /// 创建一个新文件，使用指定的编码将指定的字符串写入文件，然后关闭文件。如果目标文件已经存在，则覆盖该文件
        /// </summary>
        /// <param name="path">要写入的文件</param>
        /// <param name="contents">要写入文件的字符串</param>
        /// <param name="encoding">应用于字符串的编码</param>
        void WriteAllText(string path, string contents, Encoding encoding);
    }
}
