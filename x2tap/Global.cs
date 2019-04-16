using System.Collections.Generic;

namespace x2tap
{
    public static class Global
    {
		/// <summary>
		///		订阅链接
		/// </summary>
		public static List<string> SubscriptionLinks = new List<string>();

        /// <summary>
        ///     v2ray 代理
        /// </summary>
        public static List<Objects.Server.v2ray> V2RayProxies = new List<Objects.Server.v2ray>();

        /// <summary>
        ///     Shadowsocks 代理
        /// </summary>
        public static List<Objects.Server.Shadowsocks> ShadowsocksProxies = new List<Objects.Server.Shadowsocks>();

		/// <summary>
		///		ShadowsocksR 代理
		/// </summary>
		public static List<Objects.Server.ShadowsocksR> ShadowsocksRProxies = new List<Objects.Server.ShadowsocksR>();

		/// <summary>
		///		模式
		/// </summary>
		public static List<Objects.Mode> Modes = new List<Objects.Mode>();

        /// <summary>
        ///     视图
        /// </summary>
        public static class Views
        {
            /// <summary>
            ///     主窗体
            /// </summary>
            public static View.MainForm MainForm;

            /// <summary>
            ///     高级设置
            /// </summary>
            public static View.AdvancedForm AdvancedForm;

            /// <summary>
            ///     订阅窗体
            /// </summary>
            public static View.SubscribeForm SubscribeForm;

            /// <summary>
            ///     服务器配置窗体
            /// </summary>
            public static class Server
            {
                /// <summary>
                ///     V2Ray
                /// </summary>
                public static View.Server.V2Ray V2Ray;

                /// <summary>
                ///     Shadowsocks
                /// </summary>
                public static View.Server.Shadowsocks Shadowsocks;

				/// <summary>
				///		ShadowsocksR
				/// </summary>
				public static View.Server.ShadowsocksR ShadowsocksR;
            }
        }

        /// <summary>
        ///     配置
        /// </summary>
        public static class Config
        {
            /// <summary>
            ///     v2ray 日志等级
            /// </summary>
            public static int V2RayLoggingLevel = 0;

            /// <summary>
            ///     适配器地址
            /// </summary>
            public static string AdapterAddress = "";

            /// <summary>
            ///     适配器网关
            /// </summary>
            public static string AdapterGateway = "";

            /// <summary>
            ///     TUN/TAP
            /// </summary>
            public static class TUNTAP
            {
                /// <summary>
                ///     地址
                /// </summary>
                public static string Address = "10.0.236.10";

                /// <summary>
                ///     掩码
                /// </summary>
                public static string Netmask = "255.255.255.0";

                /// <summary>
                ///     网关
                /// </summary>
                public static string Gateway = "10.0.236.1";

				/// <summary>
				///		DNS
				/// </summary>
				public static string DNS = "127.0.0.1";

				/// <summary>
				///		使用自定义 DNS 设置
				/// </summary>
				public static bool UseCustomDNS = false;
            }
        }
    }
}