using System;
using System.Security.Cryptography;
using System.Text;

namespace JW.Core.Helper
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public sealed class RandomHelper
    {
        #region Fields

        private static readonly Random randomSeed = new Random();

        #endregion

        #region Methods

        /// <summary>
        /// 随机生成的数是否大于0.5
        /// </summary>
        /// <returns></returns>
        public static bool GetRandomBool()
        {
            return (randomSeed.NextDouble() > 0.5);
        }

        /// <summary>
        /// 根据时间区间生成随机时间
        /// </summary>
        /// <param name="min">最小时间</param>
        /// <param name="max">最大时间</param>
        /// <returns></returns>
        public static DateTime GetRandomDateTime(DateTime min, DateTime max)
        {
            if (max <= min)
            {
                throw new ArgumentException("开始时间必须小于结束时间");
            }
            long ticks = min.Ticks;
            double num3 = ((Convert.ToDouble(max.Ticks) - Convert.ToDouble(ticks)) * randomSeed.NextDouble()) + Convert.ToDouble(ticks);
            return new DateTime(Convert.ToInt64(num3));
        }

        /// <summary>
        /// 生成随机数字代码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// 在指定的范围内返回一个随机的数字
        /// </summary>
        /// <param name="min">最小数字</param>
        /// <param name="max">最大数字</param>
        /// <returns>int</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// 生成规定长度的随机字符
        /// </summary>
        /// <param name="chars">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string GetRandomString(string chars, int len)
        {
            StringBuilder builder = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                builder.Append(chars[randomSeed.Next(chars.Length)]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 根据时间+4位随机数字生成随机字符
        /// </summary>
        /// <returns></returns>
        public static string GetRandomStringByTime()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmssfff") + GenerateRandomDigitCode(4));
        }
        
        /// <summary>
        /// 随机生成字母
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="lowerCase">是否区分大小写</param>
        /// <returns></returns>
        public static string GetRandomWord(int len, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder(len);
            int num = lowerCase ? 0x61 : 0x41;
            for (int i = 0; i < len; i++)
            {
                builder.Append((char) ((ushort) ((26.0 * randomSeed.NextDouble()) + num)));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 根据26个字母（大小写）和1~9数字组成的字符串中随机生成一定长度的字符串
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns>返回字符串</returns>
        public static string CreateRandomStr(int len)
        {
            return GetRandomString("123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ", len);
        }

        /// <summary>
        /// 生成随机字母
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <param name="sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string RandomNum(int length, bool sleep)
        {
            if (sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string result = "";
            int n = Pattern.Length;
            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

        #endregion 
    }
}

