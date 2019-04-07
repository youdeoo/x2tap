using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig.Protocol.Inbound
{
	/// <summary>
	///		Dekodomo 协议
	/// </summary>
	public class Dokodomo
	{
		/// <summary>
		///		将流量转发到此地址
		/// </summary>
		public string address;

		/// <summary>
		///		可接收的网络协议类型
		/// </summary>
		public string network;

		/// <summary>
		///		将流量转发到目标地址的指定端口
		/// </summary>
		public int port;
	}
}
