using System;

namespace JW.Core.Caching
{
    /// <summary>
    /// ����������ӿ�
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// ��ȡ��������ָ����������ֵ
        /// </summary>
        /// <typeparam name="T">������Ŀ����</typeparam>
        /// <param name="key">������Ĺؼ���</param>
        /// <returns>��ָ����Կ�����Ļ���ֵ</returns>
        T Get<T>(string key);

        /// <summary>
        /// ��ָ���ļ��Ͷ�����ӵ�������
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        /// <param name="data">����ļ�ֵ</param>
        /// <param name="cacheTime">�����ڻ���ʱ��</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// ��ȡһ��ֵ����ֵָʾ��ָ����Կ������ֵ�Ƿ񱻻���
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        /// <returns>��������ڻ����У���Ϊtrue������Ϊfalse</returns>
        bool IsSet(string key);

        /// <summary>
        /// �ӻ������Ƴ�����ָ������ֵ
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        void Remove(string key);

        /// <summary>
        /// ����ģʽ�Ƴ���
        /// </summary>
        /// <param name="pattern">�ַ�����ģʽ</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// ������л�������
        /// </summary>
        void Clear();
    }
}
