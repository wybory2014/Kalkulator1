using System;
namespace Kalkulator1.AdditionalClass
{
	internal class AttendanceOBWItem
	{
		private int id;
		private string name;
		private long lwyb;
		public string ShortName
		{
			get
			{
				return this.id.ToString();
			}
		}
		public string LongName
		{
			get
			{
				return this.name;
			}
		}
		public AttendanceOBWItem()
		{
			this.id = 0;
			this.name = "";
			this.lwyb = 0L;
		}
		public AttendanceOBWItem(int id, string name, long lwyb)
		{
			this.id = id;
			this.name = name;
			this.lwyb = lwyb;
		}
		public int getId()
		{
			return this.id;
		}
		public string getName()
		{
			return this.name;
		}
		public long getLwyb()
		{
			return this.lwyb;
		}
	}
}
