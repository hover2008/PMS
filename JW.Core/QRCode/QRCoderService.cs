using QRCoder;

namespace JW.Core.QRCode
{
    /// <summary>
    /// QRCoder二维码服务
    /// </summary>
    public class QRCoderService : IQRCodeService
    {
        #region Fields

        /// <summary>
        /// 二维码尺寸
        /// </summary>
        private int size;
        /// <summary>
        /// 容错级别
        /// </summary>
        private QRCodeGenerator.ECCLevel level;

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化QRCoder组件二维码服务
        /// </summary>
        public QRCoderService()
        {
            size = 10;
            level = QRCodeGenerator.ECCLevel.L;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        public IQRCodeService Size(QRSize size)
        {
            return Size((int)size);
        }

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        public IQRCodeService Size(int size)
        {
            this.size = size;
            return this;
        }

        /// <summary>
        /// 容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        public IQRCodeService Correction(ErrorCorrectionLevel level)
        {
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    this.level = QRCodeGenerator.ECCLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    this.level = QRCodeGenerator.ECCLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    this.level = QRCodeGenerator.ECCLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    this.level = QRCodeGenerator.ECCLevel.H;
                    break;
            }
            return this;
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        public byte[] CreateQrCode(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, level);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(data);
            return qrCode.GetGraphic(size);
        }

        #endregion

    }
}
