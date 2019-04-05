using System.Net;

namespace x2tap.Utils
{
    public static class Route
    {
        /// <summary>
        ///     路由工具位置
        /// </summary>
        public static string Location = "C:\\Windows\\System32\\route.exe";

		public static string TranslateCIDR(string cidr)
		{
			cidr = "/" + cidr;

			return cidr
				.Replace("/32", "255.255.255.255")
				.Replace("/31", "255.255.255.254")
				.Replace("/30", "255.255.255.252")
				.Replace("/29", "255.255.255.248")
				.Replace("/28", "255.255.255.240")
				.Replace("/27", "255.255.255.224")
				.Replace("/26", "255.255.255.192")
				.Replace("/25", "255.255.255.128")
				.Replace("/24", "255.255.255.0")
				.Replace("/23", "255.255.254.0")
				.Replace("/22", "255.255.252.0")
				.Replace("/21", "255.255.248.0")
				.Replace("/20", "255.255.240.0")
				.Replace("/19", "255.255.224.0")
				.Replace("/18", "255.255.192.0")
				.Replace("/17", "255.255.128.0")
				.Replace("/16", "255.255.0.0")
				.Replace("/15", "255.254.0.0")
				.Replace("/14", "255.252.0.0")
				.Replace("/13", "255.248.0.0")
				.Replace("/12", "255.240.0.0")
				.Replace("/11", "255.224.0.0")
				.Replace("/10", "255.192.0.0")
				.Replace("/9", "255.128.0.0")
				.Replace("/8", "255.0.0.0")
				.Replace("/7", "254.0.0.0")
				.Replace("/6", "252.0.0.0")
				.Replace("/5", "248.0.0.0")
				.Replace("/4", "240.0.0.0")
				.Replace("/3", "224.0.0.0")
				.Replace("/2", "192.0.0.0")
				.Replace("/1", "128.0.0.0")
				.Replace("/0", "0.0.0.0");
		}

        /// <summary>
        ///     增加路由规则
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="netmask">掩码</param>
        /// <param name="gateway">网关</param>
        /// <returns></returns>
        public static bool Add(string address, string netmask, string gateway)
        {
            return Shell.Execute(Location, "add", address, "mask", netmask, gateway).Ok;
        }

        /// <summary>
        ///     增加路由规则
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="netmask">掩码</param>
        /// <param name="gateway">网关</param>
        /// <param name="metric">跃点数</param>
        /// <returns></returns>
        public static bool Add(string address, string netmask, string gateway, int metric)
        {
            return Shell.Execute(Location, "add", address, "mask", netmask, gateway, "metric", metric.ToString()).Ok;
        }

        /// <summary>
        ///     删除路由规则
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns></returns>
        public static bool Delete(string address)
        {
            return Shell.Execute(Location, "delete", address).Ok;
        }

        /// <summary>
        ///     删除路由规则
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="netmask">掩码</param>
        /// <param name="gateway">网关</param>
        /// <returns></returns>
        public static bool Delete(string address, string netmask, string gateway)
        {
            return Shell.Execute(Location, "delete", address, "mask", netmask, gateway).Ok;
        }

        /// <summary>
        ///     修改路由规则
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="metric">跃点数</param>
        /// <returns></returns>
        public static bool Change(string address, string netmask, string gateway, int metric)
        {
            return Shell.Execute(Location, "change", address, "mask", netmask, gateway, "metric", metric.ToString()).Ok;
        }
    }
}