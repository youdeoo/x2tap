using System;
using System.Text;

namespace x2tap.Utils
{
    public static class Util
    {
        /// <summary>
        ///     计算流量
        /// </summary>
        /// <param name="bandwidth">流量</param>
        /// <returns>带单位的流量字符串</returns>
        public static string ComputeBandwidth(long bandwidth)
        {
            string[] units = {"KB", "MB", "GB", "TB", "PB"};
            double result = bandwidth;
            var i = -1;

            do
            {
                i++;
            } while ((result /= 1024) > 1024);

            return string.Format("{0} {1}", Math.Round(result, 2), units[i]);
        }

		/// <summary>
		///		URL 安全的 Base64 解密
		/// </summary>
		/// <param name="text">Base64 后的字符串</param>
		/// <returns>解密后的结果</returns>
		public static string UrlSafeBase64Decode(string text)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(text.Replace("-", "+").Replace("_", "/").PadRight(text.Length + (4 - text.Length % 4) % 4, '=')));
		}
    }
}