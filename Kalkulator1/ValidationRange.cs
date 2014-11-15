using System;
namespace Kalkulator1
{
	internal class ValidationRange
	{
		private string fieldName;
		private int min;
		private int max;
		private bool existRange;
		public ValidationRange(string fieldName, int min, int max)
		{
			this.fieldName = fieldName;
			this.min = min;
			this.max = max;
			this.existRange = true;
		}
		public ValidationRange(string fieldName, int min)
		{
			this.fieldName = fieldName;
			this.min = min;
			this.max = 2147483647;
			this.existRange = true;
		}
		public ValidationRange(string fieldName)
		{
			this.fieldName = fieldName;
			this.min = 0;
			this.max = 0;
			this.existRange = false;
		}
		public int getMin()
		{
			return this.min;
		}
		public int getMax()
		{
			return this.max;
		}
		public string getFieldName()
		{
			return this.fieldName;
		}
		public bool getExistRange()
		{
			return this.existRange;
		}
	}
}
