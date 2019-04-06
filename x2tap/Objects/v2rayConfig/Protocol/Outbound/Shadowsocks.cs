using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig.Protocol.Outbound
{
	/// <summary>
	///		Shadowsocks 协议
	/// </summary>
	public class Shadowsocks
	{
		/// <summary>
		///		服务器
		/// </summary>
		public List<ShadowsocksServer> servers = new List<ShadowsocksServer>();
	}

	/// <summary>
	///		Shadowsocks 服务器配置
	/// </summary>
	public class ShadowsocksServer
	{
		/// <summary>
		///		地址
		/// </summary>
		public string address;

		/// <summary>
		///		端口
		/// </summary>
		public int port;

		/// <summary>
		///		加密方式
		/// </summary>
		public string method;

		/// <summary>
		///		密码
		/// </summary>
		public string password;
	}
}
