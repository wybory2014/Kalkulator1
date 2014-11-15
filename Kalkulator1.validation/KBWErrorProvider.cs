using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Kalkulator1.validation
{
	public class KBWErrorProvider
	{
		private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> errors;
		private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> warning;
		private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> hardWarning;
		public KBWErrorProvider()
		{
			this.errors = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>>();
			this.warning = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>>();
			this.hardWarning = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>>();
		}
		public bool hasErrors()
		{
			return this.errors.Count<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>>>() > 0;
		}
		public bool hasError(Control c)
		{
			bool result;
			if (this.errors.ContainsKey(c.Name))
			{
				if (this.errors[c.Name].Count > 0)
				{
					result = true;
					return result;
				}
			}
			if (this.warning.ContainsKey(c.Name))
			{
				if (this.warning[c.Name].Count > 0)
				{
					result = true;
					return result;
				}
			}
			if (this.hardWarning.ContainsKey(c.Name))
			{
				if (this.hardWarning[c.Name].Count > 0)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public bool hasHardWarning()
		{
			return this.hardWarning.Count<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>>>() > 0;
		}
		public bool hasWarning()
		{
			return this.warning.Count<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>>>() > 0;
		}
		public void clearErrors()
		{
			this.errors.Clear();
			this.warning.Clear();
			this.hardWarning.Clear();
		}
		public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> getErrors()
		{
			return this.errors;
		}
		public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> getHardWarnings()
		{
			return this.hardWarning;
		}
		public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> getWarnings()
		{
			return this.warning;
		}
		public void SetErrorWithCount(Control c, string message, string code, string step)
		{
			if (message == "")
			{
				try
				{
					this.errors[c.Name].Remove(code);
					if (this.errors[c.Name].Count == 0)
					{
						this.errors.Remove(c.Name);
					}
				}
				catch (System.Collections.Generic.KeyNotFoundException)
				{
				}
			}
			else
			{
				try
				{
					this.errors[c.Name][code] = new KBWValue(message, step);
				}
				catch (System.Collections.Generic.KeyNotFoundException)
				{
					try
					{
						this.errors[c.Name].Add(code, new KBWValue(message, step));
					}
					catch (System.Collections.Generic.KeyNotFoundException)
					{
						this.errors.Add(c.Name, new System.Collections.Generic.Dictionary<string, KBWValue>());
						this.errors[c.Name].Add(code, new KBWValue(message, step));
					}
				}
			}
		}
		public void SetErrorWithCount(Control c, string message, int type, string code, string step)
		{
			if (message == "")
			{
				try
				{
					if (type == 0 || type == 1)
					{
						this.errors[c.Name].Remove(code);
						if (this.errors[c.Name].Count == 0)
						{
							this.errors.Remove(c.Name);
						}
					}
					if (type == 2)
					{
						this.hardWarning[c.Name].Remove(code);
						if (this.hardWarning[c.Name].Count == 0)
						{
							this.hardWarning.Remove(c.Name);
						}
					}
					if (type == 3)
					{
						this.warning[c.Name].Remove(code);
						if (this.warning[c.Name].Count == 0)
						{
							this.warning.Remove(c.Name);
						}
					}
				}
				catch (System.Collections.Generic.KeyNotFoundException)
				{
				}
			}
			else
			{
				try
				{
					if (type == 0 || type == 1)
					{
						this.errors[c.Name][code] = new KBWValue(message, step);
					}
					if (type == 2)
					{
						this.hardWarning[c.Name][code] = new KBWValue(message, step);
					}
					if (type == 3)
					{
						this.warning[c.Name][code] = new KBWValue(message, step);
					}
				}
				catch (System.Collections.Generic.KeyNotFoundException)
				{
					try
					{
						if (type == 0 || type == 1)
						{
							this.errors[c.Name].Add(code, new KBWValue(message, step));
						}
						if (type == 2)
						{
							this.hardWarning[c.Name].Add(code, new KBWValue(message, step));
						}
						if (type == 3)
						{
							this.warning[c.Name].Add(code, new KBWValue(message, step));
						}
					}
					catch (System.Collections.Generic.KeyNotFoundException)
					{
						if (type == 0 || type == 1)
						{
							this.errors.Add(c.Name, new System.Collections.Generic.Dictionary<string, KBWValue>());
							this.errors[c.Name].Add(code, new KBWValue(message, step));
						}
						if (type == 2)
						{
							this.hardWarning.Add(c.Name, new System.Collections.Generic.Dictionary<string, KBWValue>());
							this.hardWarning[c.Name].Add(code, new KBWValue(message, step));
						}
						if (type == 3)
						{
							this.warning.Add(c.Name, new System.Collections.Generic.Dictionary<string, KBWValue>());
							this.warning[c.Name].Add(code, new KBWValue(message, step));
						}
					}
				}
			}
		}
	}
}
