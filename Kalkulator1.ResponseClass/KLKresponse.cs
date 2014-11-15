using System;
namespace Kalkulator1.ResponseClass
{
	public class KLKresponse
	{
		private bool saved;
		private Code c;
		private System.Exception e;
		public KLKresponse()
		{
			this.saved = false;
			this.c = new Code();
			this.e = new System.Exception();
		}
		public void setCode(Code code)
		{
			this.c = code;
		}
		public void setSaved(bool saved)
		{
			this.saved = saved;
		}
		public void setException(System.Exception e)
		{
			this.e = e;
		}
		public bool isSaved()
		{
			return this.c.getcode() == 0 && this.saved;
		}
		public Code getCode()
		{
			return this.c;
		}
		public System.Exception getException()
		{
			return this.e;
		}
	}
}
