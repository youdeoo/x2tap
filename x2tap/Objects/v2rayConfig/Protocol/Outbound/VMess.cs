using System.Collections.Generic;

namespace x2tap.Objects.v2rayConfig.Protocol.Outbound
{
	/// <summary>
	///		VMess 协议
	/// </summary>
	public class VMess
	{
		public List<VMessServer> vnext = new List<VMessServer>();
	}

	/// <summary>
	///		VMess 服务器配置
	/// </summary>
	public class VMessServer
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
		public List<VMessUser> users = new List<VMessUser>();
	}

	/// <summary>
	///		VMess 用户配置
	/// </summary>
	public class VMessUser
	{
		/// <summary>
		///		用户 ID
		/// </summary>
		public string id;

		/// <summary>
		///		额外 ID
		/// </summary>
		public int alterId;

		/// <summary>
		///		加密方式
		/// </summary>
		public string security;
	}
}
