using System;
using System.Collections.Generic;
using System.Text;

namespace x2tap.Utils
{
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
            var data = new Uri(Encoding.UTF8.GetString(Convert.FromBase64String(text.Remove(0, 5))));
            var shadowsocks = new Objects.Server.Shadowsocks();

            shadowsocks.Remark = Uri.UnescapeDataString(data.Fragment.Remove(0, 1));
            shadowsocks.Address = data.Host;
            shadowsocks.Port = data.Port;

            var info = Encoding.UTF8.GetString(Convert.FromBase64String(data.UserInfo)).Split(':');

            switch (info[0])
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
                case "chacha20-poly1305":
                    shadowsocks.EncryptMethod = 6;
                    break;
                default:
                    throw new Exception(String.Format("不支持的加密方式：{0}", info[0]));
            }

            shadowsocks.Password = info[1];

            return shadowsocks;
        }

		public static Objects.Server.ShadowsocksR ShadowsocksR(string text)
		{
			var data = Utils.Util.UrlSafeBase64Decode(text.Remove(0, 6)).Split(':');
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
				case "bf-cfb":
					shadowsocksr.EncryptMethod = 10;
					break;
				case "camellia-128-cfb":
					shadowsocksr.EncryptMethod = 11;
					break;
				case "camellia-192-cfb":
					shadowsocksr.EncryptMethod = 12;
					break;
				case "camellia-256-cfb":
					shadowsocksr.EncryptMethod = 13;
					break;
				case "salsa20":
					shadowsocksr.EncryptMethod = 14;
					break;
				case "chacha20":
					shadowsocksr.EncryptMethod = 15;
					break;
				case "chacha20-ietf":
					shadowsocksr.EncryptMethod = 16;
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
			shadowsocksr.Password = Util.UrlSafeBase64Decode(info[0]);

			var dict = new Dictionary<string, string>();
			foreach (var str in info[1].Remove(0, 1).Split('&'))
			{
				var splited = str.Split('=');

				dict.Add(splited[0], splited[1]);
			}

			if (dict.ContainsKey("remarks"))
			{
				shadowsocksr.Remark = Util.UrlSafeBase64Decode(dict["remarks"]);
			}

			if (dict.ContainsKey("protoparam"))
			{
				shadowsocksr.ProtocolParam = Util.UrlSafeBase64Decode(dict["protoparam"]);
			}

			if (dict.ContainsKey("obfsparam"))
			{
				shadowsocksr.OBFSParam = Util.UrlSafeBase64Decode(dict["obfsparam"]);
			}

			return shadowsocksr;
		}
    }
}