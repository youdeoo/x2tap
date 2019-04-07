using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		入口
	/// </summary>
	public class Inbound
	{
		/// <summary>
		///		端口
		/// </summary>
		public int port;

		/// <summary>
		///		地址
		/// </summary>
		public string listen;

		/// <summary>
		///		协议
		/// </summary>
		public string protocol;
		
		/// <summary>
		///		设置
		/// </summary>
		public object settings;

		/// <summary>
		///		嗅探
		/// </summary>
		public object sniffing;

		/// <summary>
		///		标签
		/// </summary>
		public string tag;
	}

	/// <summary>
	///		入口嗅探
	/// </summary>
	public class InboundSniffing
	{
		/// <summary>
		///		是否启用
		/// </summary>
		public bool enabled = true;

		/// <summary>
		///		目标重写
		/// </summary>
		public List<string> destOverride = new List<string>()
		{
			"http",
			"tls"
		};
	}
}
