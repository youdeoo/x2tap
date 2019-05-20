using System.Collections.Generic;

namespace x2tap.Objects.Information
{
	public class Outbound
	{
		public string protocol;

		public object settings;

		public OutboundStream streamSettings = new OutboundStream();

		public string tag;
	}

	public class OutboundStream
	{
		public string network = "tcp";

		public string security = "none";

		public OutboundStreamTLS tlsSettings;

		public object tcpSettings;

		public OutboundStreamKCP kcpSettings;

		public OutboundStreamWebSocket wsSettings;

		public OutboundStreamHTTP2 httpSettings;

		public OutboundStreamQUIC quicSettings;
	}

	public class OutboundStreamTLS
	{
		public bool allowInsecure = false;

		public bool allowInsecureCipher = false;
	}

	public class OutboundStreamTCP
	{
		public object header = new OutboundStreamTCPNoneHeader();
	}

	public class OutboundStreamTCPNoneHeader
	{
		public string type = "none";
	}

	public class OutboundStreamTCPHTTPHeader
	{
		public string type = "http";

		public OutboundStreamTCPHTTPRequestHeader request;
	}

	public class OutboundStreamTCPHTTPRequestHeader
	{
		public string version = "1.1";

		public string method = "GET";

		public List<string> path = new List<string>()
		{
			"/"
		};

		public Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();
	}

	public class OutboundStreamKCP
	{
		public Dictionary<string, string> header = new Dictionary<string, string>();
	}

	public class OutboundStreamWebSocket
	{
		public string path = "/";

		public Dictionary<string, string> headers = new Dictionary<string, string>();
	}

	public class OutboundStreamHTTP2
	{
		public List<string> host;

		public string path = "/";
	}

	public class OutboundStreamQUIC
	{
		public string security = "none";

		public string key = "";

		public Dictionary<string, string> header;
	}
}
