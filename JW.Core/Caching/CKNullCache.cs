namespace JW.Core.Caching
{
    /// <summary>
    /// ��ʾ�ջ��棨�޻��棩
    /// </summary>
    public partial class CKNullCache : IStaticCacheManager
    {
        /// <summary>
        /// ��ȡ��������ָ����������ֵ
        /// </summary>
        /// <typeparam name="T">������Ŀ����</typeparam>
        /// <param name="key">������Ĺؼ���</param>
        /// <returns>��ָ����Կ�����Ļ���ֵ</returns>
        public virtual T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// ��ָ���ļ��Ͷ�����ӵ�������
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        /// <param name="data">����ļ�ֵ</param>
        /// <param name="cacheTime">�����ڻ���ʱ��</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
        }

        /// <summary>
        /// ��ȡһ��ֵ����ֵָʾ��ָ����Կ������ֵ�Ƿ񱻻���
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        /// <returns>��������ڻ����У���Ϊtrue������Ϊfalse</returns>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// �ӻ������Ƴ�����ָ������ֵ
        /// </summary>
        /// <param name="key">������Ĺؼ���</param>
        public virtual void Remove(string key)
        {
        }

        /// <summary>
        /// ����ģʽ�Ƴ���
        /// </summary>
        /// <param name="pattern">�ַ�����ģʽ</param>
        public virtual void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// ������л�������
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public virtual void Dispose()
        { 
        }
    }
}