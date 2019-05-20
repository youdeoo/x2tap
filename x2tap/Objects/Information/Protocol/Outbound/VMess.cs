using System.Collections.Generic;

namespace x2tap.Objects.Information.Protocol.Outbound
{
	public class VMess
	{
		public List<VMessServer> vnext = new List<VMessServer>();
	}

	public class VMessServer
	{
		public string address;

		public int port;

		public List<VMessUser> users = new List<VMessUser>();
	}

	public class VMessUser
	{
		public string id;

		public int alterId;

		public string security;
	}
}
