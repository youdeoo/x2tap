using System.Collections.Generic;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		流设置
	/// </summary>
	public class StreamSettings
	{
		/// <summary>
		///		网络
		/// </summary>
		public string network = "tcp";

		/// <summary>
		///		安全
		/// </summary>
		public string security = "none";

		/// <summary>
		///		TLS 流
		/// </summary>
		public TLS tlsSettings;

		/// <summary>
		///		TCP 流
		/// </summary>
		public object tcpSettings;

		/// <summary>
		///		KCP 流
		/// </summary>
		public KCP kcpSettings;

		/// <summary>
		///		WebSocket 流
		/// </summary>
		public WebSocket wsSettings;

		/// <summary>
		///		HTTP/2 流
		/// </summary>
		public HTTP2 httpSettings;

		/// <summary>
		///		QUIC 流
		/// </summary>
		public QUIC quicSettings;
	}

	/// <summary>
	///		TLS 流
	/// </summary>
	public class TLS
	{
		/// <summary>
		///		服务器端证书的域名
		/// </summary>
		public string serverName;

		/// <summary>
		///		是否允许不安全连接
		/// </summary>
		public bool allowInsecure = false;

		/// <summary>
		///		是否允许不安全的加密方式
		/// </summary>
		public bool allowInsecureCiphers = false;
	}

	/// <summary>
	///		不进行伪装
	/// </summary>
	public class TCPNoneHeader
	{
		/// <summary>
		///		类型
		/// </summary>
		public string type = "none";
	}

	/// <summary>
	///		伪装成 HTTP 请求
	/// </summary>
	public class TCPHTTPHeader
	{
		/// <summary>
		///		类型
		/// </summary>
		public string type = "http";

		/// <summary>
		///		HTTP 请求头
		/// </summary>
		public TCPHTTPRequestHeader request = new TCPHTTPRequestHeader();
	}

	/// <summary>
	///		HTTP 请求头
	/// </summary>
	public class TCPHTTPRequestHeader
	{
		/// <summary>
		///		HTTP 版本
		/// </summary>
		public string version = "1.1";

		/// <summary>
		///		HTTP 请求方式
		/// </summary>
		public string method = "GET";

		/// <summary>
		///		HTTP 请求路径
		/// </summary>
		public List<string> path = new List<string>()
		{
			"/"
		};

		/// <summary>
		///		HTTP 请求头
		/// </summary>
		public Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();
	}

	/// <summary>
	///		KCP 流
	/// </summary>
	public class KCP
	{
		/// <summary>
		///		最大传输单元
		/// </summary>
		public int mtu = 1350;

		/// <summary>
		///		传输时间间隔
		/// </summary>
		public int tti = 20;

		/// <summary>
		///		上行链路容量
		/// </summary>
		public int uplinkCapacity = 5;

		/// <summary>
		///		下行链路容量
		/// </summary>
		public int downlinkCapacity = 20;

		/// <summary>
		///		是否启用拥塞控制
		/// </summary>
		public bool congestion = true;

		/// <summary>
		///		单个连接的读取缓冲区大小
		/// </summary>
		public int readBufferSize = 1;

		/// <summary>
		///		单个连接的写入缓冲区大小
		/// </summary>
		public int writeBufferSize = 1;

		/// <summary>
		///		数据包头部伪装设置
		/// </summary>
		public Dictionary<string, string> header;
	}

	/// <summary>
	///		WebSocket 流
	/// </summary>
	public class WebSocket
	{
		/// <summary>
		///		HTTP 请求路径
		/// </summary>
		public string path = "/";

		/// <summary>
		///		自定义 HTTP 头
		/// </summary>
		public Dictionary<string, string> headers;
	}

	/// <summary>
	///		HTTP/2 流
	/// </summary>
	public class HTTP2
	{
		/// <summary>
		///		主机名
		/// </summary>
		public List<string> host;

		/// <summary>
		///		HTTP 请求路径
		/// </summary>
		public string path = "/";
	}

	/// <summary>
	///		QUIC 流
	/// </summary>
	public class QUIC
	{
		/// <summary>
		///		数据包头部伪装
		/// </summary>
		public Dictionary<string, string> header;
	}
}
