using System.Collections.Generic;

namespace x2tap.Objects.Information
{
	public class DNS
	{
		public List<object> servers = new List<object>()
		{
			"1.1.1.1",
			new DNSServer()
			{
				address = "1.2.4.8",
				domains = new List<string>()
				{
					"geosite:cn"
				}
			}
		};
	}

	public class DNSServer
	{
		public string address;

		public int port = 53;

		public List<string> domains = new List<string>();
	}
}
