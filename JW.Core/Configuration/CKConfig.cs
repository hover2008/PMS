namespace JW.Core.Configuration
{
    /// <summary>
    /// ���ֲ��������ò���
    /// </summary>
    public partial class CKConfig
    {
        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ���������������ʾ�����������ڿ��������б����ԣ��������ã�
        /// </summary>
        public bool DisplayFullErrorStack { get; set; }

        /// <summary>
        /// ��ȡ�����þ�̬���ݵġ�Cache�ؼ�������ֵ
        /// </summary>
        public string StaticFilesCacheControl { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�ѹ����Ӧ
        /// </summary>
        public bool UseResponseCompression { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�Ӧ��ʹ��Redis���������л��棨���������ڴ滺����ʹ��Ĭ��ֵ��
        /// </summary>
        public bool RedisCachingEnabled { get; set; }
		
        /// <summary>
        /// Redis�����ַ�����������Redis����
        /// </summary>
        public string RedisCachingConnectionString { get; set; }
		
        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ���ݱ���ϵͳ�Ƿ�Ӧ����Ϊ��Redis���ݿ��б�����Կ
        /// </summary>
        public bool PersistDataProtectionKeysToRedis { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�Ӧ������������
        /// </summary>
        public bool IgnoreStartupTasks { get; set; }
         
        /// <summary>
        /// AES������Կ
        /// </summary> 
        public string AESEncryptKey { get; set; }
		
        /// <summary>
        /// DES������Կ
        /// </summary> 
        public string DESEncryptKey { get; set; }
    }
}