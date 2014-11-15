using System;
namespace Kalkulator1.AdditionalClass
{
	internal class AttendanceItem
	{
		private string id;
		private string name;
		public string ShortName
		{
			get
			{
				return this.id;
			}
		}
		public string LongName
		{
			get
			{
				return this.name;
			}
		}
		public AttendanceItem()
		{
			this.id = "0";
			this.name = "";
		}
		public AttendanceItem(string id, string name)
		{
			this.id = id;
			this.name = name;
		}
		public string getId()
		{
			return this.id;
		}
		public string getName()
		{
			return this.name;
		}
	}
}
