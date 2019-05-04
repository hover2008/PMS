namespace JW.Core.Configuration
{
    /// <summary>
    /// ���ֲ������������ò���
    /// </summary>
    public partial class HostingConfig
    {
        /// <summary>
        /// ��ȡ�������Զ���ת����HTTPͷ������CF����IP��X-PROTEDDE-PROTO�ȣ�
        /// </summary>
        public string ForwardedHttpHeader { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�ʹ��Http��ȺHttps
        /// </summary>
        public bool UseHttpClusterHttps { get; set; }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�ʹ��Http XForwardedЭ��
        /// </summary>
        public bool UseHttpXForwardedProto { get; set; }
    }
}