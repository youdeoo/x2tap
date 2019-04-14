namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		出口
	/// </summary>
	public class Outbound
	{
		/// <summary>
		///		用于发送数据的 IP 地址
		/// </summary>
		public string sendThrough = Global.Config.adapterAddress;

		/// <summary>
		///		协议
		/// </summary>
		public string protocol;

		/// <summary>
		///		设置
		/// </summary>
		public object settings;

		/// <summary>
		///		流设置
		/// </summary>
		public StreamSettings streamSettings;

		/// <summary>
		///		标签
		/// </summary>
		public string tag;
	}
}
