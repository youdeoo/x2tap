using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace x2tap
{
	public static class Utils
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
				Global.Config.V2RayLoggingLevel = int.Parse(data["x2tap"]["v2rayLoggingLevel"]);
				Global.Config.TUNTAP.Address = data["TUNTAP"]["Address"];
				Global.Config.TUNTAP.Netmask = data["TUNTAP"]["Netmask"];
				Global.Config.TUNTAP.Gateway = data["TUNTAP"]["Gateway"];
				Global.Config.TUNTAP.DNS = data["TUNTAP"]["DNS"];
				Global.Config.TUNTAP.UseCustomDNS = Boolean.Parse(data["TUNTAP"]["UseCustomDNS"]);

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
									if (splited.Length == 3)
									{
										mode.Name = splited[0].Trim();
										mode.Type = int.Parse(splited[1].Trim());
										mode.BypassChina = (int.Parse(splited[2].Trim()) == 1) ? true : false;
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

				if (File.Exists("ExceptionIPs.json"))
				{
					Global.ExceptionIPs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("ExceptionIPs.json"));
				}
			}

			/// <summary>
			///     保存到文件
			/// </summary>
			public static void SaveToFile()
			{
				var parser = new IniParser.FileIniDataParser();
				var data = parser.ReadFile("x2tap.ini");
				data["x2tap"]["v2rayLoggingLevel"] = Global.Config.V2RayLoggingLevel.ToString();
				data["TUNTAP"]["Address"] = Global.Config.TUNTAP.Address;
				data["TUNTAP"]["Netmask"] = Global.Config.TUNTAP.Netmask;
				data["TUNTAP"]["Gateway"] = Global.Config.TUNTAP.Gateway;
				data["TUNTAP"]["DNS"] = Global.Config.TUNTAP.DNS;
				data["TUNTAP"]["UseCustomDNS"] = Global.Config.TUNTAP.UseCustomDNS.ToString();
				parser.WriteFile("x2tap.ini", data);

				File.WriteAllText("SubscriptionLinks.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.SubscriptionLinks));
				File.WriteAllText("v2ray.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.V2RayProxies));
				File.WriteAllText("Shadowsocks.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.ShadowsocksProxies));
				File.WriteAllText("ShadowsocksR.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.ShadowsocksRProxies));
				File.WriteAllText("ExceptionIPs.json", Newtonsoft.Json.JsonConvert.SerializeObject(Global.ExceptionIPs));
			}

			/// <summary>
			///		初始化适配器
			/// </summary>
			public static void InitAadapter()
			{
				// 初始化适配器
				using (var client = new UdpClient("114.114.114.114", 53))
				{
					var address = ((IPEndPoint)client.Client.LocalEndPoint).Address;
					Global.Config.AdapterAddress = address.ToString();

					var addressGeted = false;

					var adapters = NetworkInterface.GetAllNetworkInterfaces();
					foreach (var adapter in adapters)
					{
						var properties = adapter.GetIPProperties();

						foreach (var information in properties.UnicastAddresses)
						{
							if (information.Address.AddressFamily == AddressFamily.InterNetwork && Equals(information.Address, address))
							{
								addressGeted = true;
							}
						}

						foreach (var information in properties.GatewayAddresses)
						{
							if (addressGeted)
							{
								Global.Config.AdapterIndex = properties.GetIPv4Properties().Index;
								Global.Config.AdapterGateway = information.Address.ToString();
								break;
							}
						}

						if (addressGeted)
						{
							break;
						}
					}
				}

				// 搜索 TUN/TAP 适配器的索引
				Global.Config.TUNTAP.ComponentID = Utils.TUNTAP.GetComponentID();
				Global.Config.TUNTAP.Name = Utils.TUNTAP.GetName(Global.Config.TUNTAP.ComponentID);
				foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (adapter.Name == Global.Config.TUNTAP.Name)
					{
						Global.Config.TUNTAP.Index = adapter.GetIPProperties().GetIPv4Properties().Index;

						break;
					}
				}
			}

			/// <summary>
			///		获取 v2ray 通用配置文件
			/// </summary>
			/// <param name="bypassChina">是否需要绕过中国</param>
			/// <returns></returns>
			public static Objects.v2rayConfig.v2rayConfig GetGeneric(bool bypassChina = true)
			{
				var data = new Objects.v2rayConfig.v2rayConfig();

				switch (Global.Config.V2RayLoggingLevel)
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
						data.method = "aes-256-ctr";
						break;
					case 11:
						data.method = "bf-cfb";
						break;
					case 12:
						data.method = "camellia-128-cfb";
						break;
					case 13:
						data.method = "camellia-192-cfb";
						break;
					case 14:
						data.method = "camellia-256-cfb";
						break;
					case 15:
						data.method = "salsa20";
						break;
					case 16:
						data.method = "chacha20";
						break;
					case 17:
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

		public static class Parse
		{
			public static Objects.Server.v2ray v2ray(string text)
			{
				var data = SimpleJSON.JSON.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(text.Remove(0, 8))));
				var v2ray = new Objects.Server.v2ray();

				v2ray.Remark = data["ps"].Value;
				v2ray.Address = data["add"].Value;
				v2ray.Port = data["port"].AsInt;
				v2ray.UserID = data["id"].Value;
				v2ray.AlterID = data["aid"].AsInt;

				if (String.IsNullOrEmpty(v2ray.Remark))
				{
					v2ray.Remark = String.Format("{0}:{1}", v2ray.Address, v2ray.Port);
				}

				switch (data["net"].Value)
				{
					case "tcp":
						v2ray.TransferProtocol = 0;
						break;
					case "kcp":
						v2ray.TransferProtocol = 1;
						break;
					case "ws":
						v2ray.TransferProtocol = 2;
						break;
					case "h2":
						v2ray.TransferProtocol = 3;
						break;
					case "quic":
						v2ray.TransferProtocol = 4;
						break;
					default:
						throw new NotSupportedException(String.Format("不支持的传输协议：{0}", data["net"].Value));
				}

				switch (data["type"].Value)
				{
					case "none":
						v2ray.FakeType = 0;
						break;
					case "http":
						v2ray.FakeType = 1;
						break;
					case "srtp":
						v2ray.FakeType = 2;
						break;
					case "utp":
						v2ray.FakeType = 3;
						break;
					case "wechat-video":
						v2ray.FakeType = 4;
						break;
					case "dtls":
						v2ray.FakeType = 5;
						break;
					case "wireguard":
						v2ray.FakeType = 6;
						break;
					default:
						throw new NotSupportedException(String.Format("不支持的伪装类型：{0}", data["type"].Value));
				}

				v2ray.FakeDomain = data["host"].Value;
				v2ray.Path = data["path"].Value == "" ? "/" : data["path"].Value;
				v2ray.TLSSecure = data["tls"].Value == "" ? false : true;

				return v2ray;
			}

            public static Objects.Server.Shadowsocks Shadowsocks(string text)
            {
                var shadowsocks = new Objects.Server.Shadowsocks();
				try
                {
					Regex finder = new Regex("^(?i)ss://([A-Za-z0-9+-/=_]+)(#(.+))?", RegexOptions.IgnoreCase), parser = new Regex("^((?<method>.+):(?<password>.*)@(?<hostname>.+?):(?<port>\\d+?))$", RegexOptions.IgnoreCase);
					var match = finder.Match(text);
                    if (!match.Success)
					{
						throw new FormatException();
					}

                    match = parser.Match(UrlSafeBase64Decode(match.Groups[1].Value));

                    shadowsocks.Password = match.Groups["password"].Value;
                    shadowsocks.Address = match.Groups["hostname"].Value;
                    shadowsocks.Port = int.Parse(match.Groups["port"].Value);

                    if (text.Contains("#"))
                    {
                        shadowsocks.Remark = Uri.UnescapeDataString(Regex.Split(text, "#", RegexOptions.IgnoreCase)[1]);
                    }
                    else
                    {
						shadowsocks.Remark = String.Format("{0}:{1}", shadowsocks.Address, shadowsocks.Port);
                    }

                    switch (match.Groups["method"].Value)
                    {
                        case "aes-256-cfb":
                            shadowsocks.EncryptMethod = 0;
                            break;
                        case "aes-128-cfb":
                            shadowsocks.EncryptMethod = 1;
                            break;
                        case "chacha20":
                            shadowsocks.EncryptMethod = 2;
                            break;
                        case "chacha20-ietf":
                            shadowsocks.EncryptMethod = 3;
                            break;
                        case "aes-256-gcm":
                            shadowsocks.EncryptMethod = 4;
                            break;
                        case "aes-128-gcm":
                            shadowsocks.EncryptMethod = 5;
                            break;
                        case "chacha20-ietf-poly1305":
                            shadowsocks.EncryptMethod = 6;
                            break;
                        default:
                            throw new NotSupportedException(String.Format("不支持的加密方式：{0}", match.Groups["method"].Value));
                    }
                }
				catch (NotSupportedException)
				{
					throw;
				}
                catch (Exception)
                {
                    var data = new Uri(text);
					var userinfo = UrlSafeBase64Decode(data.UserInfo).Split(':');

					shadowsocks.Remark = data.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                    shadowsocks.Address = data.IdnHost;
                    shadowsocks.Port = data.Port;

					if (String.IsNullOrEmpty(shadowsocks.Remark))
					{
						shadowsocks.Remark = String.Format("{0}:{1}", shadowsocks.Address, shadowsocks.Port);
					}

					switch (userinfo[0])
                    {
                        case "aes-256-cfb":
                            shadowsocks.EncryptMethod = 0;
                            break;
                        case "aes-128-cfb":
                            shadowsocks.EncryptMethod = 1;
                            break;
                        case "chacha20":
                            shadowsocks.EncryptMethod = 2;
                            break;
                        case "chacha20-ietf":
                            shadowsocks.EncryptMethod = 3;
                            break;
                        case "aes-256-gcm":
                            shadowsocks.EncryptMethod = 4;
                            break;
                        case "aes-128-gcm":
                            shadowsocks.EncryptMethod = 5;
                            break;
                        case "chacha20-ietf-poly1305":
                            shadowsocks.EncryptMethod = 6;
                            break;
                        default:
                            throw new Exception(String.Format("不支持的加密方式：{0}", userinfo[0]));
                    }

					shadowsocks.Password = userinfo[1];
				}

                return shadowsocks;
            }

            public static Objects.Server.ShadowsocksR ShadowsocksR(string text)
			{
				var data = UrlSafeBase64Decode(text.Remove(0, 6)).Split(':');
				var shadowsocksr = new Objects.Server.ShadowsocksR();

				shadowsocksr.Address = data[0];
				shadowsocksr.Port = int.Parse(data[1]);

				switch (data[2])
				{
					case "origin":
						shadowsocksr.Protocol = 0;
						break;
					case "auth_sha1_v4":
						shadowsocksr.Protocol = 1;
						break;
					case "auth_aes128_sha1":
						shadowsocksr.Protocol = 2;
						break;
					case "auth_aes128_md5":
						shadowsocksr.Protocol = 3;
						break;
					case "auth_chain_a":
						shadowsocksr.Protocol = 4;
						break;
					case "auth_chain_b":
						shadowsocksr.Protocol = 5;
						break;
					case "auth_chain_c":
						shadowsocksr.Protocol = 6;
						break;
					case "auth_chain_d":
						shadowsocksr.Protocol = 7;
						break;
					case "auth_chain_e":
						shadowsocksr.Protocol = 8;
						break;
					case "auth_chain_f":
						shadowsocksr.Protocol = 9;
						break;
					default:
						throw new NotSupportedException(String.Format("不支持的协议：{0}", data[2]));
				}

				switch (data[3])
				{
					case "none":
						shadowsocksr.EncryptMethod = 0;
						break;
					case "table":
						shadowsocksr.EncryptMethod = 1;
						break;
					case "rc4":
						shadowsocksr.EncryptMethod = 2;
						break;
					case "rc4-md5":
						shadowsocksr.EncryptMethod = 3;
						break;
					case "rc4-md5-6":
						shadowsocksr.EncryptMethod = 4;
						break;
					case "aes-128-cfb":
						shadowsocksr.EncryptMethod = 5;
						break;
					case "aes-192-cfb":
						shadowsocksr.EncryptMethod = 6;
						break;
					case "aes-256-cfb":
						shadowsocksr.EncryptMethod = 7;
						break;
					case "aes-128-ctr":
						shadowsocksr.EncryptMethod = 8;
						break;
					case "aes-192-ctr":
						shadowsocksr.EncryptMethod = 9;
						break;
					case "aes-256-ctr":
						shadowsocksr.EncryptMethod = 10;
						break;
					case "bf-cfb":
						shadowsocksr.EncryptMethod = 11;
						break;
					case "camellia-128-cfb":
						shadowsocksr.EncryptMethod = 12;
						break;
					case "camellia-192-cfb":
						shadowsocksr.EncryptMethod = 13;
						break;
					case "camellia-256-cfb":
						shadowsocksr.EncryptMethod = 14;
						break;
					case "salsa20":
						shadowsocksr.EncryptMethod = 15;
						break;
					case "chacha20":
						shadowsocksr.EncryptMethod = 16;
						break;
					case "chacha20-ietf":
						shadowsocksr.EncryptMethod = 17;
						break;
					default:
						throw new NotSupportedException(String.Format("不支持的加密方式：{0}", data[3]));
				}

				switch (data[4])
				{
					case "plain":
						shadowsocksr.OBFS = 0;
						break;
					case "http_simple":
						shadowsocksr.OBFS = 1;
						break;
					case "http_port":
						shadowsocksr.OBFS = 2;
						break;
					case "http_mix":
						shadowsocksr.OBFS = 3;
						break;
					case "tls1.2_ticket_auth":
						shadowsocksr.OBFS = 4;
						break;
					case "tls1.2_ticket_fastauth":
						shadowsocksr.OBFS = 5;
						break;
					default:
						throw new NotSupportedException(String.Format("不支持的混淆方式：{0}", data[4]));
				}

				var info = data[5].Split('/');
				shadowsocksr.Password = Utils.UrlSafeBase64Decode(info[0]);

				var dict = new Dictionary<string, string>();
				foreach (var str in info[1].Remove(0, 1).Split('&'))
				{
					var splited = str.Split('=');

					dict.Add(splited[0], splited[1]);
				}

				if (dict.ContainsKey("remarks"))
				{
					shadowsocksr.Remark = Utils.UrlSafeBase64Decode(dict["remarks"]);
				}
				else
				{
					shadowsocksr.Remark = String.Format("{0}:{1}", shadowsocksr.Address, shadowsocksr.Port);
				}

				if (dict.ContainsKey("protoparam"))
				{
					shadowsocksr.ProtocolParam = Utils.UrlSafeBase64Decode(dict["protoparam"]);
				}

				if (dict.ContainsKey("obfsparam"))
				{
					shadowsocksr.OBFSParam = Utils.UrlSafeBase64Decode(dict["obfsparam"]);
				}

				return shadowsocksr;
			}
		}

		public static class Shell
		{
			/// <summary>
			///     执行
			/// </summary>
			/// <param name="content">内容</param>
			public static Objects.ShellExitCode Execute(params string[] content)
			{
				var process = new Process();
				process.StartInfo.FileName = content[0];
				process.StartInfo.Arguments = "";
				for (var i = 1; i < content.Length; i++)
				{
					process.StartInfo.Arguments += " \"" + content[i] + "\"";
				}

				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();

				process.WaitForExit();
				return new Objects.ShellExitCode
				{
					ExitCode = process.ExitCode,

					Ok = process.ExitCode == 0
				};
			}

			/// <summary>
			///     执行命令
			/// </summary>
			/// <param name="content">内容</param>
			public static Objects.ShellExitCode ExecuteCommand(params string[] content)
			{
				var process = new Process();
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.Arguments = "/c \"";
				for (var i = 0; i < content.Length; i++)
				{
					process.StartInfo.Arguments += " " + content[i];
				}

				process.StartInfo.Arguments += "\"";

				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();

				process.WaitForExit();
				return new Objects.ShellExitCode
				{
					ExitCode = process.ExitCode,

					Ok = process.ExitCode == 0
				};
			}

			/// <summary>
			///     执行不等待
			/// </summary>
			/// <param name="content">内容</param>
			public static void ExecuteNoWait(params string[] content)
			{
				var process = new Process();
				process.StartInfo.FileName = content[0];
				process.StartInfo.Arguments = "";
				for (var i = 1; i < content.Length; i++)
				{
					process.StartInfo.Arguments += " \"" + content[i] + "\"";
				}

				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();
			}

			/// <summary>
			///     执行命令不等待
			/// </summary>
			/// <param name="content">内容</param>
			public static void ExecuteCommandNoWait(params string[] content)
			{
				var process = new Process();
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.Arguments = "/c \"";
				for (var i = 0; i < content.Length; i++)
				{
					process.StartInfo.Arguments += " " + content[i];
				}

				process.StartInfo.Arguments += "\"";

				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();
			}
		}

		public static class Route
		{
			/// <summary>
			///		创建路由规则
			/// </summary>
			/// <param name="address">地址</param>
			/// <param name="netmask">掩码 CIDR</param>
			/// <param name="gateway">网关</param>
			/// <param name="index">适配器索引</param>
			/// <param name="metric">跃点数</param>
			/// <returns></returns>
			[DllImport("x2tapCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateRoute")]
			public static extern bool Create(string address, int netmask, string gateway, int index, int metric = 100);

			/// <summary>
			///		删除路由规则
			/// </summary>
			/// <param name="address">地址</param>
			/// <param name="netmask">掩码 CIDR</param>
			/// <param name="gateway">网关</param>
			/// <param name="index">适配器索引</param>
			/// <param name="metric">跃点数</param>
			/// <returns></returns>
			[DllImport("x2tapCore.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "DeleteRoute")]
			public static extern bool Delete(string address, int netmask, string gateway, int index, int metric = 100);
		}

		public static class TUNTAP
		{
			public static string TUNTAP_COMPONENT_ID_0901 = "tap0901";
			public static string TUNTAP_COMPONENT_ID_0801 = "tap0801";
			public static string NETWORK_KEY = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}";
			public static string ADAPTER_KEY = "SYSTEM\\CurrentControlSet\\Control\\Class\\{4D36E972-E325-11CE-BFC1-08002BE10318}";

			/// <summary>
			///     获取 TUN/TAP 适配器 ID
			/// </summary>
			/// <returns>适配器 ID</returns>
			public static string GetComponentID()
			{
				var adaptersRegistry = Registry.LocalMachine.OpenSubKey(ADAPTER_KEY);

				foreach (var adapterRegistryName in adaptersRegistry.GetSubKeyNames())
				{
					if (adapterRegistryName != "Configuration" && adapterRegistryName != "Properties")
					{
						var adapterRegistry = adaptersRegistry.OpenSubKey(adapterRegistryName);

						var adapterComponentId = adapterRegistry.GetValue("ComponentId", "").ToString();
						if (adapterComponentId == TUNTAP_COMPONENT_ID_0901 || adapterComponentId == TUNTAP_COMPONENT_ID_0801)
						{
							return adapterRegistry.GetValue("NetCfgInstanceId", "").ToString();
						}
					}
				}

				return "";
			}

			/// <summary>
			///     获取 TUN/TAP 适配器名称
			/// </summary>
			/// <param name="componentId">适配器 ID</param>
			/// <returns>适配器名称</returns>
			public static string GetName(string componentId)
			{
				var registry = Registry.LocalMachine.OpenSubKey(string.Format("{0}\\{1}\\Connection", NETWORK_KEY, componentId));

				return registry.GetValue("Name", "").ToString();
			}

			/// <summary>
			///     创建 TUN/TAP 适配器
			/// </summary>
			/// <returns></returns>
			public static bool Create()
			{
				if (File.Exists("tapinstall.exe"))
				{
					return Shell.Execute("tapinstall.exe", "install", "driver\\OemVista.inf", "tap0901").Ok;
				}

				return false;
			}
		}

		/// <summary>
		///     计算流量
		/// </summary>
		/// <param name="bandwidth">流量</param>
		/// <returns>带单位的流量字符串</returns>
		public static string ComputeBandwidth(long bandwidth)
		{
			string[] units = { "KB", "MB", "GB", "TB", "PB" };
			double result = bandwidth;
			var i = -1;

			do
			{
				i++;
			} while ((result /= 1024) > 1024);

			return String.Format("{0} {1}", Math.Round(result, 2), units[i]);
		}

		/// <summary>
		///		URL 安全的 Base64 解密
		/// </summary>
		/// <param name="text">Base64 后的字符串</param>
		/// <returns>解密后的结果</returns>
		public static string UrlSafeBase64Decode(string text)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(text.Replace("-", "+").Replace("_", "/").PadRight(text.Length + (4 - text.Length % 4) % 4, '=')));
		}

		/// <summary>
		///		检查 TCP 端口是否可以连接
		/// </summary>
		/// <param name="port">端口</param>
		/// <returns>是否可以连接</returns>
		public static bool CheckTCPPortOpen(int port)
		{
			try
			{
				using (var client = new TcpClient())
				{
					var task = client.BeginConnect("127.0.0.1", port, null, null);
					if (!task.AsyncWaitHandle.WaitOne(1000))
					{
						throw new TimeoutException();
					}

					client.EndConnect(task);
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
