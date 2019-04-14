using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects
{
	/// <summary>
	///		ShadowsocksR 配置
	/// </summary>
	public class ShadowsocksRConfig
	{
		/// <summary>
		///		服务器地址
		/// </summary>
		public string server;

		/// <summary>
		///		服务器端口
		/// </summary>
		public int server_port;

		/// <summary>
		///		加密方式
		/// </summary>
		public string method;

		/// <summary>
		///		密码
		/// </summary>
		public string password;

		/// <summary>
		///		协议
		/// </summary>
		public string protocol;

		/// <summary>
		///		协议参数
		/// </summary>
		public string protocol_param;

		/// <summary>
		///		混淆
		/// </summary>
		public string obfs;

		/// <summary>
		///		混淆参数
		/// </summary>
		public string obfs_param;

		/// <summary>
		///		本地地址
		/// </summary>
		public string local_address = "0.0.0.0";

		/// <summary>
		///		本地端口
		/// </summary>
		public int local_port = 2810;

		/// <summary>
		///		UDP
		/// </summary>
		public bool udp = true;

		/// <summary>
		///		超时
		/// </summary>
		public int timeout = 300;
	}
}
