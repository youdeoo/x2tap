using System.Collections.Generic;

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
		///		类型（0. 规则内走直连 1. 规则内走代理）
		/// </summary>
		public int Type = 0;

		/// <summary>
		///		绕过中国（0. 无需额外绕过 1. 额外添加中国直连规则）
		/// </summary>
		public bool BypassChina = false;

		/// <summary>
		///		规则
		/// </summary>
		public List<string> Rule = new List<string>();
	}
}
