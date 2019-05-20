using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace x2tap.Objects
{
	public class Server
	{
		/// <summary>
		///		备注（S5、SS、SR、V2）
		/// </summary>
		public string Remark = String.Empty;

		/// <summary>
		///		组名（S5、SS、SR、V2）
		/// </summary>
		public string GroupName = "00000000-0000-0000-0000-000000000000";

		/// <summary>
		///		类型（S5、SS、SR、V2）
		/// </summary>
		public string Type;

		/// <summary>
		///		地址（S5、SS、SR、V2）
		/// </summary>
		public string Address;

		/// <summary>
		///		端口（S5、SS、SR、V2）
		/// </summary>
		public int Port;

		/// <summary>
		///		密码（SS、SR）
		/// </summary>
		public string Password;

		/// <summary>
		///		用户 ID（V2）
		/// </summary>
		public string UserID;

		/// <summary>
		///		额外 ID（V2）
		/// </summary>
		public int AlterID;

		/// <summary>
		///		加密方式（SS、SR、V2 QUIC）
		/// </summary>
		public string EncryptMethod;

		/// <summary>
		///		传输协议（V2）
		/// </summary>
		public string TransferProtocol;

		/// <summary>
		///		伪装类型（V2）
		/// </summary>
		public string FakeType;

		/// <summary>
		///		伪装域名（V2：HTTP、WebSocket、HTTP/2）
		/// </summary>
		public string Host;

		/// <summary>
		///		传输路径（V2：WebSocket、HTTP/2）
		/// </summary>
		public string Path;

		/// <summary>
		///		QUIC 密钥（V2）
		/// </summary>
		public string QUICSecret;

		/// <summary>
		///		TLS 底层传输安全（V2）
		/// </summary>
		public bool TLSSecure;

		/// <summary>
		///		国家代码
		/// </summary>
		public string CountryCode = "NN";

		/// <summary>
		///		延迟（S5、SS、SR、V2）
		/// </summary>
		public int Delay = 999;

		/// <summary>
		///		获取备注
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (String.IsNullOrEmpty(Remark))
			{
				Remark = String.Format("{0}:{1}", Address, Port);
			}

			if (CountryCode == "NN")
			{
				Task.Run(() =>
				{
					try
					{
						var result = Dns.GetHostAddressesAsync(Address);
						if (!result.Wait(1000))
						{
							return;
						}

						if (result.Result.Length > 0)
						{
							CountryCode = Utils.GeoIP.GetCountryISOCode(result.Result[0]);
						}
					}
					catch (Exception)
					{
						// 跳过
					}
				});
			}

			if (Type == "S5")
			{
				return String.Format("[{0}] {1}", "S5", Remark);
			}
			else if (Type == "SS")
			{
				return String.Format("[{0}] {1}", "SS", Remark);
			}
			else if (Type == "SR")
			{
				return String.Format("[{0}] {1}", "SR", Remark);
			}
			else
			{
				return String.Format("[{0}] {1}", "V2", Remark);
			}
		}

		/// <summary>
		///		测试延迟
		/// </summary>
		/// <returns>延迟</returns>
		public int Test()
		{
			if (Type == "S5" || Type == "SS" || Type == "SR")
			{
				return TestTCP();
			}
			else
			{
				if (TransferProtocol == "tcp" || TransferProtocol == "ws" || TransferProtocol == "h2")
				{
					return TestTCP();
				}
				else
				{
					return TestICMP();
				}
			}
		}

		/// <summary>
		///		测试 TCP 延迟
		/// </summary>
		/// <returns>延迟</returns>
		public int TestTCP()
		{
			using (var client = new Socket(SocketType.Stream, ProtocolType.Tcp))
			{
				try
				{
					var destination = Dns.GetHostAddressesAsync(Address);
					if (!destination.Wait(1000))
					{
						return Delay = 460;
					}

					if (destination.Result.Length == 0)
					{
						return Delay = 460;
					}

					var watch = new Stopwatch();
					watch.Start();

					var task = client.BeginConnect(new IPEndPoint(destination.Result[0], Port), (result) =>
					{
						watch.Stop();
					}, 0);

					if (task.AsyncWaitHandle.WaitOne(460))
					{
						return Delay = (int)(watch.ElapsedMilliseconds >= 460 ? 460 : watch.ElapsedMilliseconds);
					}

					return Delay = 460;
				}
				catch (Exception)
				{
					return Delay = 460;
				}
			}
		}

		/// <summary>
		///		测试 ICMP 延迟
		/// </summary>
		/// <returns>延迟</returns>
		public int TestICMP()
		{
			using (var sender = new Ping())
			{
				try
				{
					var destination = Dns.GetHostAddressesAsync(Address);
					if (!destination.Wait(1000))
					{
						return Delay = 460;
					}

					if (destination.Result.Length == 0)
					{
						return Delay = 460;
					}

					var response = sender.Send(destination.Result[0], 460);

					return Delay = (int)(response.RoundtripTime == 0 ? 460 : response.RoundtripTime);
				}
				catch (Exception)
				{
					return Delay = 460;
				}
			}
		}
	}
}
