using System;
using System.Collections.Generic;
using System.IO;

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
                File.WriteAllBytes("x2tap.ini", Properties.Resources.defaultConfig);
            }

            var parser = new IniParser.FileIniDataParser();
            var data = parser.ReadFile("x2tap.ini");
            Global.Config.v2rayLoggingLevel = int.Parse(data["x2tap"]["v2rayLoggingLevel"]);
            Global.Config.TUNTAP.Address = data["TUNTAP"]["Address"];
            Global.Config.TUNTAP.Netmask = data["TUNTAP"]["Netmask"];
            Global.Config.TUNTAP.Gateway = data["TUNTAP"]["Gateway"];

			if (File.Exists("SubscriptionLinks.json"))
			{
				Global.SubscriptionLinks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("SubscriptionLinks.json"));
			}

            if (File.Exists("v2ray.json"))
            {
                Global.V2RayProxies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Objects.Server.v2ray>>(File.ReadAllText("v2ray.json"));
            }

            if (File.Exists("Shadowsocks.json"))
            {
                Global.ShadowsocksProxies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Objects.Server.Shadowsocks>>(File.ReadAllText("Shadowsocks.json"));
            }

			if (File.Exists("ShadowsocksR.json"))
			{
				Global.ShadowsocksRProxies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Objects.Server.ShadowsocksR>>(File.ReadAllText("ShadowsocksR.json"));
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
            var parser = new IniParser.FileIniDataParser();
            var data = parser.ReadFile("x2tap.ini");
            data["x2tap"]["v2rayLoggingLevel"] = Global.Config.v2rayLoggingLevel.ToString();
            data["TUNTAP"]["Address"] = Global.Config.TUNTAP.Address;
            data["TUNTAP"]["Netmask"] = Global.Config.TUNTAP.Netmask;
            data["TUNTAP"]["Gateway"] = Global.Config.TUNTAP.Gateway;
            parser.WriteFile("x2tap.ini", data);

			File.WriteAllText("SubscriptionLinks.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.SubscriptionLinks));
            File.WriteAllText("v2ray.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.V2RayProxies));
            File.WriteAllText("Shadowsocks.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.ShadowsocksProxies));
			File.WriteAllText("ShadowsocksR.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.ShadowsocksRProxies));
        }

		/// <summary>
		///		获取 v2ray 通用配置文件
		/// </summary>
		/// <param name="bypassChina">是否需要绕过中国</param>
		/// <returns></returns>
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
				data.dns.servers.Insert(0, new Objects.v2rayConfig.DnsServer()
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

		/// <summary>
		///		取 v2ray 伪装类型
		/// </summary>
		/// <param name="type">伪装类型</param>
		/// <returns></returns>
		public static string GetFakeType(int type)
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

		public static string GetV2Ray(Objects.Server.v2ray v2ray, bool bypassChina = true)
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
					if (GetFakeType(v2ray.FakeType) == "http")
					{
						var tcpSettings = new Objects.v2rayConfig.TCPHTTPHeader()
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
						streamSettings.tcpSettings = tcpSettings;
					}
					break;
				case 1:
					streamSettings.network = "kcp";
					streamSettings.kcpSettings = new Objects.v2rayConfig.KCP()
					{
						header = new Dictionary<string, string>()
						{
							{ "type", GetFakeType(v2ray.FakeType) }
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
							{ "type", GetFakeType(v2ray.FakeType) }
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

			data.outbounds.Insert(0, new Objects.v2rayConfig.Outbound()
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
				streamSettings = streamSettings,
				tag = "defaultOutbound"
			});

			return Newtonsoft.Json.JsonConvert.SerializeObject(data);
		}

		public static string GetShadowsocks(Objects.Server.Shadowsocks shadowsocks, bool bypassChina = true)
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

			data.outbounds.Insert(0, new Objects.v2rayConfig.Outbound()
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

			return Newtonsoft.Json.JsonConvert.SerializeObject(data);
		}

		public static string GetShadowsocksR(Objects.Server.ShadowsocksR shadowsocksr)
		{
			var data = new Objects.ShadowsocksRConfig();

			data.server = shadowsocksr.Address;
			data.server_port = shadowsocksr.Port;

			switch (shadowsocksr.EncryptMethod)
			{
				case 0:
					data.method = "none";
					break;
				case 1:
					data.method = "table";
					break;
				case 2:
					data.method = "rc4";
					break;
				case 3:
					data.method = "rc4-md5";
					break;
				case 4:
					data.method = "rc4-md5-6";
					break;
				case 5:
					data.method = "aes-128-cfb";
					break;
				case 6:
					data.method = "aes-192-cfb";
					break;
				case 7:
					data.method = "aes-256-cfb";
					break;
				case 8:
					data.method = "aes-128-ctr";
					break;
				case 9:
					data.method = "aes-192-ctr";
					break;
				case 10:
					data.method = "bf-cfb";
					break;
				case 11:
					data.method = "camellia-128-cfb";
					break;
				case 12:
					data.method = "camellia-192-cfb";
					break;
				case 13:
					data.method = "camellia-256-cfb";
					break;
				case 14:
					data.method = "salsa20";
					break;
				case 15:
					data.method = "chacha20";
					break;
				case 16:
					data.method = "chacha20-ietf";
					break;
				default:
					throw new NotSupportedException(String.Format("不支持的加密方式：{0}", shadowsocksr.EncryptMethod));
			}

			data.password = shadowsocksr.Password;

			switch (shadowsocksr.Protocol)
			{
				case 0:
					data.protocol = "origin";
					break;
				case 1:
					data.protocol = "auth_sha1_v4";
					break;
				case 2:
					data.protocol = "auth_aes128_sha1";
					break;
				case 3:
					data.protocol = "auth_aes128_md5";
					break;
				case 4:
					data.protocol = "auth_chain_a";
					break;
				case 5:
					data.protocol = "auth_chain_b";
					break;
				case 6:
					data.protocol = "auth_chain_c";
					break;
				case 7:
					data.protocol = "auth_chain_d";
					break;
				case 8:
					data.protocol = "auth_chain_e";
					break;
				case 9:
					data.protocol = "auth_chain_f";
					break;
				default:
					throw new NotSupportedException(String.Format("不支持的协议：{0}", shadowsocksr.Protocol));
			}

			data.protocol_param = shadowsocksr.ProtocolParam;

			switch (shadowsocksr.OBFS)
			{
				case 0:
					data.obfs = "plain";
					break;
				case 1:
					data.obfs = "http_simple";
					break;
				case 2:
					data.obfs = "http_post";
					break;
				case 3:
					data.obfs = "http_mix";
					break;
				case 4:
					data.obfs = "tls1.2_ticket_auth";
					break;
				case 5:
					data.obfs = "tls1.2_ticket_fastauth";
					break;
				default:
					throw new NotSupportedException(String.Format("不支持的混淆方式：{0}", shadowsocksr.Protocol));
			}

			data.obfs_param = shadowsocksr.OBFSParam;

			return Newtonsoft.Json.JsonConvert.SerializeObject(data);
		}
    }
}