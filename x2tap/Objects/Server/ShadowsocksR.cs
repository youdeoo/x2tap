namespace x2tap.Objects.Server
{
    /// <summary>
    ///     ShadowsocksR
    /// </summary>
    public class ShadowsocksR
    {
        /// <summary>
        ///     地址
        /// </summary>
        public string Address = "www.baidu.com";

        /// <summary>
        ///     加密方式
        /// </summary>
        public int EncryptMethod = 0;

        /// <summary>
        ///     密码
        /// </summary>
        public string Password = "BaiduBooster";

        /// <summary>
        ///     端口
        /// </summary>
        public int Port = 443;

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark = "百度为您提供强力加速";

		/// <summary>
		///		协议
		/// </summary>
		public int Protocol = 0;

		/// <summary>
		///		协议参数
		/// </summary>
		public string ProtocolParam = "";

		/// <summary>
		///		混淆
		/// </summary>
		public int OBFS = 0;

		/// <summary>
		///		混淆参数
		/// </summary>
		public string OBFSParam = "";
    }
}