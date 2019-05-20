using System.Collections.Generic;

namespace x2tap.Objects.Information
{
	public class Inbound
	{
		public string listen = "127.0.0.1";

		public int port;

		public string protocol;

		public object settings;

		public InboundSniffing sniffing = new InboundSniffing();

		public string tag = "defaultInbound";
	}

	public class InboundSniffing
	{
		public bool enabled = true;

		public List<string> destOverride = new List<string>()
		{
			"http",
			"tls"
		};
	}
}
