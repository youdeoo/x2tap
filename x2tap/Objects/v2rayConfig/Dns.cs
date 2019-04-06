using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		DNS
	/// </summary>
	public class Dns
	{
		/// <summary>
		///		客户端绑定 IP
		/// </summary>
		public string clientIp = Global.Config.adapterAddress;

		/// <summary>
		///		域名到 IP 绑定列表
		/// </summary>
		public Dictionary<string, string> hosts = new Dictionary<string, string>()
		{
			{ "localhost", "127.0.0.1" },
			{ "domain:lan", "127.0.0.1" },
			{ "domain:arpa", "127.0.0.1" },
			{ "domain:local", "127.0.0.1" }
		};

		/// <summary>
		///		服务器列表
		/// </summary>
		public List<object> servers = new List<object>()
		{
			"8.8.8.8"
		};
	}

	/// <summary>
	///		DNS 服务器
	/// </summary>
	public class DnsServer
	{
		/// <summary>
		///		地址
		/// </summary>
		public string address;

		/// <summary>
		///		端口
		/// </summary>
		public int port = 53;

		/// <summary>
		///		域名列表
		/// </summary>
		public List<string> domains = new List<string>();
	}
}
