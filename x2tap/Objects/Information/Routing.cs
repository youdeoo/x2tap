using System.Collections.Generic;

namespace x2tap.Objects.Information
{
	public class Routing
	{
		public string strategy = "rules";

		public RoutingSetting settings = new RoutingSetting();
	}

	public class RoutingSetting
	{
		public string domainStrategy = "IPIfNonMatch";

		public List<RoutingRule> rules = new List<RoutingRule>()
		{
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
			},
			new RoutingRule()
			{
				domain = new List<string>()
				{
					"geosite:cn"
				},
				outboundTag = "directOutbound"
			},
			new RoutingRule()
			{
				ip = new List<string>()
				{
					"geoip:cn",
					"geoip:private"
				},
				outboundTag = "directOutbound"
			}
		};
	}

	public class RoutingRule
	{
		public string type = "field";

		public List<string> domain;

		public List<string> ip;

		public string outboundTag;
	}
}
