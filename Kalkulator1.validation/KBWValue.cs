using System;
namespace Kalkulator1.validation
{
	public class KBWValue
	{
		private string message;
		private string step;
		public KBWValue()
		{
			this.message = "";
			this.step = "";
		}
		public KBWValue(string message, string step)
		{
			this.message = message;
			this.step = step;
		}
		public string getMessage()
		{
			return this.message;
		}
		public string getStep()
		{
			return this.step;
		}
		public static bool operator ==(KBWValue c1, KBWValue c2)
		{
			return c1.getMessage() == c2.getMessage() && c1.getStep() == c2.getStep();
		}
		public static bool operator !=(KBWValue c1, KBWValue c2)
		{
			return !(c1.getMessage() == c2.getMessage()) || !(c1.getStep() == c2.getStep());
		}
	}
}
