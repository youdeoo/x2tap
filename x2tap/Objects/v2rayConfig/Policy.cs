using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		策略
	/// </summary>
	public class Policy
	{
		/// <summary>
		///		系统策略
		/// </summary>
		public SystemPolicy system = new SystemPolicy();
	}

	/// <summary>
	///		系统策略
	/// </summary>
	public class SystemPolicy
	{
		/// <summary>
		///		统计入口上行流量
		/// </summary>
		public bool statsInboundUplink = true;

		/// <summary>
		///		统计入口下行流量
		/// </summary>
		public bool statsInboundDownlink = true;
	}
}
