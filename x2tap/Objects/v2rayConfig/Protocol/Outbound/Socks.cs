using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig.Protocol.Outbound
{
	/// <summary>
	///		Socks 协议
	/// </summary>
	public class Socks
	{
		/// <summary>
		///		服务器
		/// </summary>
		public List<SocksServer> servers = new List<SocksServer>();
	}

	/// <summary>
	///		Socks 服务器配置
	/// </summary>
	public class SocksServer
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
		///		用户
		/// </summary>
		public List<SocksUser> users = new List<SocksUser>();
	}

	/// <summary>
	///		Socks 用户
	/// </summary>
	public class SocksUser
	{
		/// <summary>
		///		用户名
		/// </summary>
		public string username;
		
		/// <summary>
		///		密码
		/// </summary>
		public string password;
	}
}
