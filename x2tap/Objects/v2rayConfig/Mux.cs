using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects.v2rayConfig
{
	/// <summary>
	///		多路复用
	/// </summary>
	public class Mux
	{
		/// <summary>
		///		启用
		/// </summary>
		public bool enabled = false;

		/// <summary>
		///		最大并发连接数
		/// </summary>
		public int concurrency = 8;
	}
}
