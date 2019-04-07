using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		路由
	/// </summary>
	public class Routing
	{
		public string strategy = "rules";

		/// <summary>
		///		路由设置
		/// </summary>
		public RoutingSettings settings = new RoutingSettings();
	}

	/// <summary>
	///		路由设置
	/// </summary>
	public class RoutingSettings
	{
		/// <summary>
		///		域名解析策略
		/// </summary>
		public string domainStrategy = "IPIfNonMatch";

		/// <summary>
		///		规则列表
		/// </summary>
		public List<RoutingRule> rules = new List<RoutingRule>()
		{
			new RoutingRule()
			{
				inboundTag = new List<string>()
				{
					"api"
				},
				outboundTag = "api"
			},
			new RoutingRule()
			{
				inboundTag = new List<string>()
				{
					"dnsInbound"
				},
				outboundTag = "dnsOutbound"
			},
			new RoutingRule()
			{
				ip = new List<string>()
				{
					"1.1.1.1"
				},
				outboundTag = "defaultOutbound"
			},
			new RoutingRule()
			{
				ip = new List<string>()
				{
					"1.2.4.8"
				},
				outboundTag = "directOutbound"
			}
		};
	}

	/// <summary>
	///		路由规则
	/// </summary>
	public class RoutingRule
	{
		/// <summary>
		///		类型
		/// </summary>
		public string type = "field";

		/// <summary>
		///		域名
		/// </summary>
		public List<string> domain;

		/// <summary>
		///		IP
		/// </summary>
		public List<string> ip;

		/// <summary>
		///		入口标签
		/// </summary>
		public List<string> inboundTag;

		/// <summary>
		///		出口标签
		/// </summary>
		public string outboundTag;
	}
}
