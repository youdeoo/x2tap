using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x2tap.Objects
{
	/// <summary>
	///		模式
	/// </summary>
	public class Mode
	{
		/// <summary>
		///		名称
		/// </summary>
		public string Name;

		/// <summary>
		///		类型
		/// </summary>
		public int Type = 0;

		/// <summary>
		///		规则
		/// </summary>
		public List<string> Rule = new List<string>();
	}
}
