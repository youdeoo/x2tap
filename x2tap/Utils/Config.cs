using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IniParser;
using Newtonsoft.Json;
using x2tap.Objects.Server;
using x2tap.Properties;

namespace x2tap.Utils
{
    public static class Config
    {
        /// <summary>
        ///     从文件初始化
        /// </summary>
        public static void InitFromFile()
        {
            // 检查配置文件是否存在，如果不存在则写入一个默认的配置
            if (!File.Exists("x2tap.ini"))
            {
                File.WriteAllBytes("x2tap.ini", Resources.defaultConfig);
            }

            var parser = new FileIniDataParser();
            var data = parser.ReadFile("x2tap.ini");
            Global.Config.v2rayLoggingLevel = int.Parse(data["x2tap"]["v2rayLoggingLevel"]);
            Global.Config.TUNTAP.Address = data["TUNTAP"]["Address"];
            Global.Config.TUNTAP.Netmask = data["TUNTAP"]["Netmask"];
            Global.Config.TUNTAP.Gateway = data["TUNTAP"]["Gateway"];
            Global.Config.TUNTAP.Metric = int.Parse(data["TUNTAP"]["Metric"]);

			if (File.Exists("SubscriptionLinks.json"))
			{
				Global.SubscriptionLinks = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("SubscriptionLinks.json"));
			}

            if (File.Exists("v2ray.json"))
            {
                Global.v2rayProxies = JsonConvert.DeserializeObject<List<Objects.Server.v2ray>>(File.ReadAllText("v2ray.json"));
            }

            if (File.Exists("Shadowsocks.json"))
            {
                Global.ShadowsocksProxies = JsonConvert.DeserializeObject<List<Shadowsocks>>(File.ReadAllText("Shadowsocks.json"));
            }

			if (Directory.Exists("mode"))
			{
				foreach (var name in Directory.GetFiles("mode", "*.txt"))
				{
					var mode = new Objects.Mode()
					{
						Name = name.Substring(5, name.Length - 9)
					};

					using (var sr = new StringReader(File.ReadAllText(name)))
					{
						var i = 0;
						var ok = true;
						string text;

						while ((text = sr.ReadLine()) != null)
						{
							if (i == 0)
							{
								var splited = text.Substring(2).Split(',');
								if (splited.Length == 2)
								{
									mode.Name = splited[0].Trim();
									mode.Type = int.Parse(splited[1].Trim());
								}
								else
								{
									ok = false;
									break;
								}
							}
							else
							{
								if (!text.StartsWith("#"))
								{
									mode.Rule.Add(text.Trim());
								}
							}

							i++;
						}

						if (!ok) break;
					}

					Global.Modes.Add(mode);
				}
			}
        }

        /// <summary>
        ///     保存到文件
        /// </summary>
        public static void SaveToFile()
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile("x2tap.ini");
            data["x2tap"]["v2rayLoggingLevel"] = Global.Config.v2rayLoggingLevel.ToString();
            data["TUNTAP"]["Address"] = Global.Config.TUNTAP.Address;
            data["TUNTAP"]["Netmask"] = Global.Config.TUNTAP.Netmask;
            data["TUNTAP"]["Gateway"] = Global.Config.TUNTAP.Gateway;
            data["TUNTAP"]["Metric"] = Global.Config.TUNTAP.Metric.ToString();
            parser.WriteFile("x2tap.ini", data);

			File.WriteAllText("SubscriptionLinks.json", JsonConvert.SerializeObject(Global.SubscriptionLinks));
            File.WriteAllText("v2ray.json", JsonConvert.SerializeObject(Global.v2rayProxies));
            File.WriteAllText("Shadowsocks.json", JsonConvert.SerializeObject(Global.ShadowsocksProxies));
        }

		public static Objects.v2rayConfig.v2rayConfig GetGeneric(bool bypassChina = true)
		{
			var data = new Objects.v2rayConfig.v2rayConfig();

			switch (Global.Config.v2rayLoggingLevel)
			{
				case 0:
					data.log.loglevel = "debug";
					break;
				case 1:
					data.log.loglevel = "info";
					break;
				case 2:
					data.log.loglevel = "warning";
					break;
				case 3:
					data.log.loglevel = "error";
					break;
				case 4:
					data.log.loglevel = "none";
					break;
				default:
					data.log.loglevel = "warning";
					break;
			}

			if (bypassChina)
			{
				data.dns.servers.Add(new Objects.v2rayConfig.DnsServer()
				{
					address = "1.2.4.8",
					port = 53,
					domains = new List<string>()
					{
						"geosite:cn"
					}
				});

				data.routing.settings.rules.Add(new Objects.v2rayConfig.RoutingRule()
				{
					domain = new List<string>()
					{
						"geosite:cn"
					},
					outboundTag = "directOutbound"
				});
				data.routing.settings.rules.Add(new Objects.v2rayConfig.RoutingRule()
				{
					ip = new List<string>()
					{
						"geoip:cn"
					},
					outboundTag = "directOutbound"
				});
			}

			return data;
		}

		public static string v2rayGetFakeType(int type)
		{
			switch (type)
			{
				case 0:
					return "none";
				case 1:
					return "http";
				case 2:
					return "srtp";
				case 3:
					return "utp";
				case 4:
					return "wechat-video";
				case 5:
					return "dtls";
				case 6:
					return "wireguard";
				default:
					return "none";
			}
		}

		public static string v2rayGet(Objects.Server.v2ray v2ray, bool bypassChina = true)
		{
			var data = GetGeneric(bypassChina);

			var user = new Objects.v2rayConfig.Protocol.Outbound.VMessUser()
			{
				id = v2ray.UserID,
				alterId = v2ray.AlterID
			};

			switch (v2ray.EncryptMethod)
			{
				case 0:
					user.security = "chacha20-poly1305";
					break;
				case 1:
					user.security = "aes-128-gcm";
					break;
				case 2:
					user.security = "auto";
					break;
				case 3:
					user.security = "none";
					break;
				default:
					user.security = "chacha20-poly1305";
					break;
			}

			var streamSettings = new Objects.v2rayConfig.StreamSettings();
			switch (v2ray.TransferProtocol)
			{
				case 0:
					streamSettings.network = "tcp";
					var tcpSettings = new Objects.v2rayConfig.TCP();

					if (v2rayGetFakeType(v2ray.FakeType) == "http")
					{
						var tcpHeader = new Objects.v2rayConfig.TCPHTTPHeader()
						{
							request = new Objects.v2rayConfig.TCPHTTPRequestHeader()
							{
								headers = new Dictionary<string, List<string>>()
								{
									{ "Host", new List<string>() { v2ray.FakeDomain } }
								},
								path = new List<string>() { v2ray.Path == "/" ? "/" : v2ray.Path }
							}
						};
						tcpSettings.header = tcpHeader;
					}
					streamSettings.tcpSettings = tcpSettings;
					break;
				case 1:
					streamSettings.network = "kcp";
					streamSettings.kcpSettings = new Objects.v2rayConfig.KCP()
					{
						header = new Dictionary<string, string>()
						{
							{ "type", v2rayGetFakeType(v2ray.FakeType) }
						}
					};
					break;
				case 2:
					streamSettings.network = "ws";
					var wsSettings = new Objects.v2rayConfig.WebSocket();

					if (v2ray.FakeDomain != "")
					{
						wsSettings.headers = new Dictionary<string, string>()
						{
							{ "Host", v2ray.FakeDomain }
						};
					}

					wsSettings.path = v2ray.Path;
					streamSettings.wsSettings = wsSettings;
					break;
				case 3:
					streamSettings.network = "http";
					var httpSettings = new Objects.v2rayConfig.HTTP2();

					if (v2ray.FakeDomain != "")
					{
						httpSettings.host = new List<string>() { v2ray.FakeDomain };
					}

					httpSettings.path = v2ray.Path;
					streamSettings.httpSettings = httpSettings;
					break;
				case 4:
					streamSettings.network = "quic";
					streamSettings.quicSettings = new Objects.v2rayConfig.QUIC()
					{
						header = new Dictionary<string, string>()
						{
							{ "type", v2rayGetFakeType(v2ray.FakeType) }
						}
					};
					break;
				default:
					streamSettings.network = "tcp";
					break;
			}

			if (v2ray.TLSSecure)
			{
				streamSettings.security = "tls";
			}

			data.outbounds.Add(new Objects.v2rayConfig.Outbound()
			{
				protocol = "vmess",
				settings = new Objects.v2rayConfig.Protocol.Outbound.VMess()
				{
					vnext = new List<Objects.v2rayConfig.Protocol.Outbound.VMessServer>()
					{
						new Objects.v2rayConfig.Protocol.Outbound.VMessServer()
						{
							address = v2ray.Address,
							port = v2ray.Port,
							users = new List<Objects.v2rayConfig.Protocol.Outbound.VMessUser>()
							{
								user
							}
						}
					}
				},
				tag = "defaultOutbound"
			});

			return JsonConvert.SerializeObject(data);
		}

		public static string ShadowsocksGet(Shadowsocks shadowsocks, bool bypassChina = true)
		{
			var data = GetGeneric(bypassChina);

			var server = new Objects.v2rayConfig.Protocol.Outbound.ShadowsocksServer()
			{
				address = shadowsocks.Address,
				port = shadowsocks.Port,
				password = shadowsocks.Password
			};

			switch (shadowsocks.EncryptMethod)
			{
				case 0:
					server.method = "aes-256-cfb";
					break;
				case 1:
					server.method = "aes-128-cfb";
					break;
				case 2:
					server.method = "chacha20";
					break;
				case 3:
					server.method = "chacha20-ietf";
					break;
				case 4:
					server.method = "aes-256-gcm";
					break;
				case 5:
					server.method = "aes-128-gcm";
					break;
				case 6:
					server.method = "chacha20-poly1305";
					break;
				default:
					server.method = "aes-256-cfb";
					break;
			}

			data.outbounds.Add(new Objects.v2rayConfig.Outbound()
			{
				protocol = "shadowsocks",
				settings = new Objects.v2rayConfig.Protocol.Outbound.Shadowsocks()
				{
					servers = new List<Objects.v2rayConfig.Protocol.Outbound.ShadowsocksServer>()
					{
						server
					}
				},
				tag = "defaultOutbound"
			});

			return JsonConvert.SerializeObject(data);
		}
    }
}