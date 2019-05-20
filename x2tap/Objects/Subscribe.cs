using System;

namespace x2tap.Objects
{
	public class Subscribe
	{
		/// <summary>
		///		组名
		/// </summary>
		public string GroupName = Guid.NewGuid().ToString();

		/// <summary>
		///		链接
		/// </summary>
		public string Link;
	}
}
