using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
namespace Kalkulator1
{
	internal class ValidationPatern
	{
		private ErrorType type;
		private string validationFor;
		private string paternLeft;
		private string paternRight;
		private string note;
		private string id;
		private Operation operation;
		private System.Collections.Generic.List<string[]> fields;
		private System.Collections.Generic.List<string> variable;
		public ValidationPatern()
		{
			this.validationFor = "";
			this.type = ErrorType.Null;
			this.paternLeft = "";
			this.paternRight = "";
			this.note = "";
			this.operation = Operation.Null;
			this.fields = new System.Collections.Generic.List<string[]>();
			this.variable = new System.Collections.Generic.List<string>();
			this.id = "";
		}
		public ValidationPatern(string validationFor, string paternLeft, string paternRight, string note, string id, Operation operation, ErrorType type)
		{
			this.validationFor = validationFor;
			this.type = type;
			this.paternLeft = paternLeft;
			this.paternRight = paternRight;
			this.note = note;
			this.operation = operation;
			this.fields = new System.Collections.Generic.List<string[]>();
			this.variable = new System.Collections.Generic.List<string>();
			this.id = id;
		}
		public void SetValidationPatern(string validationFor, string paternLeft, string paternRight, string note, string id, Operation operation, ErrorType type)
		{
			this.validationFor = validationFor;
			this.type = type;
			this.paternLeft = paternLeft;
			this.paternRight = paternRight;
			this.note = note;
			this.operation = operation;
			this.id = id;
		}
		public int getFieldsCount()
		{
			return this.fields.Count;
		}
		public string getNameField(int index)
		{
			return this.fields[index][0];
		}
		public string getFromField(int index)
		{
			return this.fields[index][1];
		}
		public string getNote()
		{
			return this.note;
		}
		public ErrorType getErrorType()
		{
			return this.type;
		}
		public string getId()
		{
			return this.id;
		}
		public bool containVariables(int index)
		{
			return this.fields[index].Length > 2;
		}
		public void addField(string fieldName, string from)
		{
			this.fields.Add(new string[]
			{
				fieldName,
				from
			});
		}
		public void addField(string fieldName, string from, bool forVariable)
		{
			if (forVariable)
			{
				this.fields.Add(new string[]
				{
					fieldName,
					from,
					"V"
				});
			}
			else
			{
				this.fields.Add(new string[]
				{
					fieldName,
					from
				});
			}
		}
		public void addVariable(string name)
		{
			this.variable.Add(name);
		}
		private void addField(string[] field)
		{
			this.fields.Add(field);
		}
		public bool valid(System.Collections.Generic.List<string[]> fieldsValues)
		{
			bool odp = true;
			string left = this.paternLeft;
			string right = this.paternRight;
			left = left.Replace(" and ", " AND ");
			right = right.Replace(" and ", " AND ");
			bool result;
			if (this.variable.Count == 0)
			{
				for (int i = 0; i < fieldsValues.Count; i++)
				{
					left = left.Replace(fieldsValues[i][0], fieldsValues[i][1]);
					right = right.Replace(fieldsValues[i][0], fieldsValues[i][1]);
				}
				result = this.check(left, right);
			}
			else
			{
				if (this.variable.Count == 1)
				{
					System.Collections.Generic.List<string> valueVariable = new System.Collections.Generic.List<string>();
					for (int i = 0; i < fieldsValues.Count; i++)
					{
						if (this.fields[i].Length > 2)
						{
							valueVariable.Add(fieldsValues[i][1]);
						}
						else
						{
							left = left.Replace(fieldsValues[i][0], fieldsValues[i][1]);
							right = right.Replace(fieldsValues[i][0], fieldsValues[i][1]);
						}
					}
					string lefttmp = left;
					string righttmp = right;
					if (System.Text.RegularExpressions.Regex.IsMatch(lefttmp, "sum") || System.Text.RegularExpressions.Regex.IsMatch(righttmp, "sum"))
					{
						System.Text.RegularExpressions.Match L = System.Text.RegularExpressions.Regex.Match(lefttmp, "sum\\(.+\\)");
						System.Text.RegularExpressions.Match R = System.Text.RegularExpressions.Regex.Match(righttmp, "sum\\(.+\\)");
						if (R.Success)
						{
							string sumExp = R.Value.Replace("(", "").Replace(")", "").Replace("sum", "");
							if (sumExp == this.variable[0])
							{
								int sum = 0;
								for (int j = 0; j < valueVariable.Count; j++)
								{
									try
									{
										sum += System.Convert.ToInt32(valueVariable[j]);
									}
									catch (System.FormatException)
									{
									}
									catch (System.OverflowException)
									{
									}
								}
								righttmp = righttmp.Replace(R.Value, sum.ToString());
							}
						}
						if (L.Success)
						{
							string sumExp = L.Value.Replace("(", "").Replace(")", "").Replace("sum", "");
							if (sumExp == this.variable[0])
							{
								int sum = 0;
								for (int j = 0; j < valueVariable.Count; j++)
								{
									try
									{
										sum += System.Convert.ToInt32(valueVariable[j]);
									}
									catch (System.FormatException)
									{
									}
									catch (System.OverflowException)
									{
									}
								}
								lefttmp = lefttmp.Replace(L.Value, sum.ToString());
							}
						}
						result = this.check(lefttmp, righttmp);
						return result;
					}
					for (int j = 0; j < valueVariable.Count; j++)
					{
						lefttmp = left;
						righttmp = right;
						lefttmp = lefttmp.Replace(this.variable[0], valueVariable[j]);
						righttmp = righttmp.Replace(this.variable[0], valueVariable[j]);
						if (!this.check(lefttmp, righttmp))
						{
							result = false;
							return result;
						}
					}
				}
				if (this.variable.Count == 2 && this.fields.Count == 3)
				{
					try
					{
						string a = this.variable[0].Substring(0, this.variable[0].Length - 1);
						string b = this.variable[1].Substring(0, this.variable[1].Length - 1);
						for (int i = 0; i < fieldsValues.Count; i++)
						{
							System.Text.RegularExpressions.Match A = System.Text.RegularExpressions.Regex.Match(fieldsValues[i][0], "^" + a + ".$");
							if (A.Success)
							{
								left = left.Replace(this.variable[0], fieldsValues[i][1]);
								right = right.Replace(this.variable[0], fieldsValues[i][1]);
							}
							System.Text.RegularExpressions.Match B = System.Text.RegularExpressions.Regex.Match(fieldsValues[i][0], "^" + b + ".$");
							if (B.Success)
							{
								left = left.Replace(this.variable[1], fieldsValues[i][1]);
								right = right.Replace(this.variable[1], fieldsValues[i][1]);
							}
							if (!A.Success && !B.Success)
							{
								left = left.Replace(fieldsValues[i][0], fieldsValues[i][1]);
								right = right.Replace(fieldsValues[i][0], fieldsValues[i][1]);
							}
						}
					}
					catch (System.Exception ex_566)
					{
					}
					result = this.check(left, right);
				}
				else
				{
					if (this.variable.Count == 2)
					{
						System.Collections.Generic.List<string> valueVariable2 = new System.Collections.Generic.List<string>();
						System.Collections.Generic.List<System.Collections.Generic.List<string>> valueVariable3 = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
						string[] L2 = this.paternLeft.Split(new char[]
						{
							'_'
						});
						string Left = L2[0].Replace("sumByY(", "");
						string[] R2 = this.paternRight.Split(new char[]
						{
							'_'
						});
						string Right = R2[0].Replace("sumByY(", "");
						for (int i = 0; i < fieldsValues.Count; i++)
						{
							if (this.fields[i].Length > 2)
							{
								System.Text.RegularExpressions.Match L = System.Text.RegularExpressions.Regex.Match(this.fields[i][0], Left + ".+");
								if (L.Success)
								{
									if (L2.Length > 2)
									{
										string[] b2 = this.fields[i][0].Split(new char[]
										{
											'_'
										});
										if (b2[1].Length > 1)
										{
											try
											{
												int c = System.Convert.ToInt32(b2[1].Substring(1)) - 1;
												if (valueVariable3.Count < c + 1)
												{
													while (valueVariable3.Count < c + 1)
													{
														valueVariable3.Add(new System.Collections.Generic.List<string>());
													}
												}
												valueVariable3[c].Add(fieldsValues[i][1]);
											}
											catch (System.Exception)
											{
											}
										}
									}
									else
									{
										valueVariable2.Add(fieldsValues[i][1]);
									}
								}
								else
								{
									System.Text.RegularExpressions.Match R = System.Text.RegularExpressions.Regex.Match(this.paternRight, Right + ".+_");
									if (R.Success)
									{
										if (R2.Length > 2)
										{
											string[] b2 = this.fields[i][0].Split(new char[]
											{
												'_'
											});
											if (b2[1].Length > 1)
											{
												try
												{
													int c = System.Convert.ToInt32(b2[1].Substring(1)) - 1;
													if (valueVariable3.Count < c + 1)
													{
														while (valueVariable3.Count < c + 1)
														{
															valueVariable3.Add(new System.Collections.Generic.List<string>());
														}
													}
													valueVariable3[c].Add(fieldsValues[i][1]);
												}
												catch (System.Exception)
												{
												}
											}
										}
										else
										{
											valueVariable2.Add(fieldsValues[i][1]);
										}
									}
								}
							}
							else
							{
								left = left.Replace(fieldsValues[i][0], fieldsValues[i][1]);
								right = right.Replace(fieldsValues[i][0], fieldsValues[i][1]);
							}
						}
						string lefttmp = left;
						string righttmp = right;
						if (System.Text.RegularExpressions.Regex.IsMatch(lefttmp, "sumBy") || System.Text.RegularExpressions.Regex.IsMatch(righttmp, "sumBy"))
						{
							System.Collections.Generic.List<long> sum2 = new System.Collections.Generic.List<long>();
							System.Text.RegularExpressions.Match L = System.Text.RegularExpressions.Regex.Match(lefttmp, "sumBy.\\(.+\\)");
							System.Text.RegularExpressions.Match R = System.Text.RegularExpressions.Regex.Match(righttmp, "sumBy.\\(.+\\)");
							if (R.Success)
							{
								char variableName = R.Value.Replace("sumBy", "")[0];
								string sumExp = R.Value.Replace("(", "").Replace(")", "").Replace("sumBy" + variableName.ToString(), "");
								if (sumExp == this.variable[1])
								{
									if (R2.Length > 2)
									{
										for (int i = 0; i < valueVariable3.Count; i++)
										{
											while (sum2.Count < valueVariable3.Count)
											{
												sum2.Add(0L);
											}
											for (int j = 0; j < valueVariable3[i].Count; j++)
											{
												System.Collections.Generic.List<long> list;
												int index;
												(list = sum2)[index = i] = list[index] + System.Convert.ToInt64(valueVariable3[i][j]);
											}
										}
									}
								}
								if (sum2.Count<long>() == valueVariable2.Count<string>())
								{
									odp = true;
									for (int i = 0; i < valueVariable2.Count; i++)
									{
										if (L2.Length < 3 && R2.Length > 2)
										{
											lefttmp = left;
											righttmp = right;
											lefttmp = lefttmp.Replace(lefttmp, valueVariable2[i]);
											righttmp = righttmp.Replace(R.Value, sum2[i].ToString());
											odp = this.check(lefttmp, righttmp);
											if (!odp)
											{
												result = odp;
												return result;
											}
										}
									}
								}
								else
								{
									odp = false;
								}
							}
							if (L.Success)
							{
								char variableName = R.Value.Replace("sumBy", "")[0];
								string sumExp = R.Value.Replace("(", "").Replace(")", "").Replace("sumBy" + variableName.ToString(), "");
								if (sumExp == this.variable[0])
								{
									if (L2.Length > 2)
									{
										for (int i = 0; i < valueVariable3.Count; i++)
										{
											while (sum2.Count < valueVariable3.Count)
											{
												sum2.Add(0L);
											}
											for (int j = 0; j < valueVariable3[i].Count; j++)
											{
												System.Collections.Generic.List<long> list;
												int index;
												(list = sum2)[index = i] = list[index] + System.Convert.ToInt64(valueVariable3[i][j]);
											}
										}
									}
								}
								if (sum2.Count<long>() == valueVariable2.Count<string>())
								{
									odp = true;
									for (int i = 0; i < valueVariable2.Count; i++)
									{
										if (R2.Length < 3 && L2.Length > 2)
										{
											lefttmp = left;
											righttmp = right;
											lefttmp = lefttmp.Replace(L.Value, sum2[i].ToString());
											righttmp = righttmp.Replace(righttmp, valueVariable2[i]);
											odp = this.check(lefttmp, righttmp);
											if (!odp)
											{
												result = odp;
												return result;
											}
										}
									}
								}
								else
								{
									odp = false;
								}
							}
						}
					}
					result = odp;
				}
			}
			return result;
		}
		private bool check(string left, string right)
		{
			DataTable dt = new DataTable();
			bool result;
			try
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(left, "AND") || System.Text.RegularExpressions.Regex.IsMatch(right, "AND"))
				{
					string[] leftSide = left.Split(new string[]
					{
						"AND"
					}, System.StringSplitOptions.RemoveEmptyEntries);
					string[] rightSide = right.Split(new string[]
					{
						"AND"
					}, System.StringSplitOptions.RemoveEmptyEntries);
					bool Lside = true;
					bool Rside = true;
					for (int i = 0; i < leftSide.Length; i++)
					{
						leftSide[i] = leftSide[i].Replace("less=", "<=");
						leftSide[i] = leftSide[i].Replace("more=", ">=");
						if (System.Text.RegularExpressions.Regex.IsMatch(leftSide[i], "!="))
						{
							string[] side = leftSide[i].Split(new string[]
							{
								"!="
							}, System.StringSplitOptions.RemoveEmptyEntries);
							if (side.Length != 2)
							{
								Lside = false;
								break;
							}
							side[0] = side[0].Replace("(", "");
							side[0] = side[0].Replace(")", "");
							side[1] = side[1].Replace("(", "");
							side[1] = side[1].Replace(")", "");
							try
							{
								if (System.Convert.ToInt32(side[0]) == System.Convert.ToInt32(side[1]))
								{
									Lside = false;
									break;
								}
							}
							catch (System.FormatException)
							{
								Lside = false;
								break;
							}
						}
						else
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(leftSide[i], "=="))
							{
								string[] side = leftSide[i].Split(new string[]
								{
									"=="
								}, System.StringSplitOptions.RemoveEmptyEntries);
								if (side.Length != 2)
								{
									Lside = false;
									break;
								}
								side[0] = side[0].Replace("(", "");
								side[0] = side[0].Replace(")", "");
								side[1] = side[1].Replace("(", "");
								side[1] = side[1].Replace(")", "");
								try
								{
									if (System.Convert.ToInt32(side[0]) != System.Convert.ToInt32(side[1]))
									{
										Lside = false;
										break;
									}
								}
								catch (System.FormatException)
								{
									Lside = false;
									break;
								}
							}
							else
							{
								if (!System.Convert.ToBoolean(dt.Compute(leftSide[i], "")))
								{
									Lside = false;
									break;
								}
							}
						}
					}
					for (int i = 0; i < rightSide.Length; i++)
					{
						rightSide[i] = rightSide[i].Replace("less=", "<=");
						rightSide[i] = rightSide[i].Replace("more=", ">=");
						if (System.Text.RegularExpressions.Regex.IsMatch(rightSide[i], "!="))
						{
							string[] side = rightSide[i].Split(new string[]
							{
								"!="
							}, System.StringSplitOptions.RemoveEmptyEntries);
							if (side.Length != 2)
							{
								Rside = false;
								break;
							}
							side[0] = side[0].Replace("(", "");
							side[0] = side[0].Replace(")", "");
							side[1] = side[1].Replace("(", "");
							side[1] = side[1].Replace(")", "");
							try
							{
								if (System.Convert.ToInt32(side[0]) == System.Convert.ToInt32(side[1]))
								{
									Rside = false;
									break;
								}
							}
							catch (System.FormatException)
							{
								Rside = false;
								break;
							}
						}
						else
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(rightSide[i], "=="))
							{
								string[] side = rightSide[i].Split(new string[]
								{
									"=="
								}, System.StringSplitOptions.RemoveEmptyEntries);
								if (side.Length != 2)
								{
									Rside = false;
									break;
								}
								side[0] = side[0].Replace("(", "");
								side[0] = side[0].Replace(")", "");
								side[1] = side[1].Replace("(", "");
								side[1] = side[1].Replace(")", "");
								try
								{
									if (System.Convert.ToInt32(side[0]) != System.Convert.ToInt32(side[1]))
									{
										Rside = false;
										break;
									}
								}
								catch (System.FormatException)
								{
									Rside = false;
									break;
								}
							}
							else
							{
								if (!System.Convert.ToBoolean(dt.Compute(rightSide[i], "")))
								{
									Rside = false;
									break;
								}
							}
						}
					}
					if (this.operation == Operation.Equal)
					{
						if (Lside == Rside)
						{
							result = true;
							return result;
						}
						result = false;
						return result;
					}
					else
					{
						if (this.operation == Operation.Different)
						{
							if (Lside != Rside)
							{
								result = true;
								return result;
							}
							result = false;
							return result;
						}
					}
				}
				else
				{
					if (System.Text.RegularExpressions.Regex.IsMatch(left, "max") || System.Text.RegularExpressions.Regex.IsMatch(right, "max"))
					{
						int Lside2;
						if (System.Text.RegularExpressions.Regex.IsMatch(left, "max"))
						{
							System.Text.RegularExpressions.Match a = System.Text.RegularExpressions.Regex.Match(left, "max\\(.+\\)");
							if (a.Success)
							{
								string eval = a.Value.Replace("(", "").Replace(")", "").Replace("max", "");
								string[] side = eval.Split(new char[]
								{
									','
								});
								if (side.Length != 2)
								{
									result = false;
									return result;
								}
								int s0 = System.Convert.ToInt32(dt.Compute(side[0], ""));
								int s = System.Convert.ToInt32(dt.Compute(side[1], ""));
								if (s0 >= s)
								{
									left = left.Replace(a.Value, s0.ToString());
								}
								else
								{
									left = left.Replace(a.Value, s.ToString());
								}
							}
						}
						else
						{
							Lside2 = System.Convert.ToInt32(dt.Compute(left, ""));
						}
						int Rside2;
						if (System.Text.RegularExpressions.Regex.IsMatch(right, "max"))
						{
							System.Text.RegularExpressions.Match a = System.Text.RegularExpressions.Regex.Match(right, "max\\(.+\\)");
							if (a.Success)
							{
								string eval = a.Value.Replace("(", "").Replace(")", "").Replace("max", "");
								string[] side = eval.Split(new char[]
								{
									','
								});
								if (side.Length != 2)
								{
									result = false;
									return result;
								}
								int s0 = System.Convert.ToInt32(dt.Compute(side[0], ""));
								int s = System.Convert.ToInt32(dt.Compute(side[1], ""));
								if (s0 >= s)
								{
									right = right.Replace(a.Value, s0.ToString());
								}
								else
								{
									right = right.Replace(a.Value, s.ToString());
								}
							}
						}
						else
						{
							Rside2 = System.Convert.ToInt32(dt.Compute(right, ""));
						}
						Lside2 = System.Convert.ToInt32(dt.Compute(left, ""));
						Rside2 = System.Convert.ToInt32(dt.Compute(right, ""));
						if (this.operation == Operation.Equal)
						{
							if (Lside2 == Rside2)
							{
								result = true;
								return result;
							}
							result = false;
							return result;
						}
						else
						{
							if (this.operation == Operation.Different)
							{
								if (Lside2 != Rside2)
								{
									result = true;
									return result;
								}
								result = false;
								return result;
							}
							else
							{
								if (this.operation == Operation.Less)
								{
									if (Lside2 < Rside2)
									{
										result = true;
										return result;
									}
									result = false;
									return result;
								}
								else
								{
									if (this.operation == Operation.LessOrEqual)
									{
										if (Lside2 <= Rside2)
										{
											result = true;
											return result;
										}
										result = false;
										return result;
									}
									else
									{
										if (this.operation == Operation.More)
										{
											if (Lside2 >= Rside2)
											{
												result = true;
												return result;
											}
											result = false;
											return result;
										}
										else
										{
											if (this.operation == Operation.MoreOrEqual)
											{
												if (Lside2 >= Rside2)
												{
													result = true;
													return result;
												}
												result = false;
												return result;
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (System.Text.RegularExpressions.Regex.IsMatch(left, "min") || System.Text.RegularExpressions.Regex.IsMatch(right, "min"))
						{
							int Lside2;
							if (System.Text.RegularExpressions.Regex.IsMatch(left, "min"))
							{
								System.Text.RegularExpressions.Match a = System.Text.RegularExpressions.Regex.Match(left, "min\\(.+\\)");
								if (a.Success)
								{
									string eval = a.Value.Replace("(", "").Replace(")", "").Replace("min", "");
									string[] side = eval.Split(new char[]
									{
										','
									});
									if (side.Length != 2)
									{
										result = false;
										return result;
									}
									int s0 = System.Convert.ToInt32(dt.Compute(side[0], ""));
									int s = System.Convert.ToInt32(dt.Compute(side[1], ""));
									if (s0 <= s)
									{
										left = left.Replace(a.Value, s0.ToString());
									}
									else
									{
										left = left.Replace(a.Value, s.ToString());
									}
								}
							}
							else
							{
								Lside2 = System.Convert.ToInt32(dt.Compute(left, ""));
							}
							int Rside2;
							if (System.Text.RegularExpressions.Regex.IsMatch(right, "min"))
							{
								System.Text.RegularExpressions.Match a = System.Text.RegularExpressions.Regex.Match(right, "min\\(.+\\)");
								if (a.Success)
								{
									string eval = a.Value.Replace("(", "").Replace(")", "").Replace("min", "");
									string[] side = eval.Split(new char[]
									{
										','
									});
									if (side.Length != 2)
									{
										result = false;
										return result;
									}
									int s0 = System.Convert.ToInt32(dt.Compute(side[0], ""));
									int s = System.Convert.ToInt32(dt.Compute(side[1], ""));
									if (s0 <= s)
									{
										right = right.Replace(a.Value, s0.ToString());
									}
									else
									{
										right = right.Replace(a.Value, s.ToString());
									}
								}
							}
							else
							{
								Rside2 = System.Convert.ToInt32(dt.Compute(right, ""));
							}
							Lside2 = System.Convert.ToInt32(dt.Compute(left, ""));
							Rside2 = System.Convert.ToInt32(dt.Compute(right, ""));
							if (this.operation == Operation.Equal)
							{
								if (Lside2 == Rside2)
								{
									result = true;
									return result;
								}
								result = false;
								return result;
							}
							else
							{
								if (this.operation == Operation.Different)
								{
									if (Lside2 != Rside2)
									{
										result = true;
										return result;
									}
									result = false;
									return result;
								}
								else
								{
									if (this.operation == Operation.Less)
									{
										if (Lside2 < Rside2)
										{
											result = true;
											return result;
										}
										result = false;
										return result;
									}
									else
									{
										if (this.operation == Operation.LessOrEqual)
										{
											if (Lside2 <= Rside2)
											{
												result = true;
												return result;
											}
											result = false;
											return result;
										}
										else
										{
											if (this.operation == Operation.More)
											{
												if (Lside2 >= Rside2)
												{
													result = true;
													return result;
												}
												result = false;
												return result;
											}
											else
											{
												if (this.operation == Operation.MoreOrEqual)
												{
													if (Lside2 >= Rside2)
													{
														result = true;
														return result;
													}
													result = false;
													return result;
												}
											}
										}
									}
								}
							}
						}
						else
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(left, "\\(.+\\)") || System.Text.RegularExpressions.Regex.IsMatch(right, "\\(.+\\)"))
							{
								System.Text.RegularExpressions.MatchCollection a2 = System.Text.RegularExpressions.Regex.Matches(left, "\\(.+\\)");
								for (int i = 0; i < a2.Count; i++)
								{
									string line = a2[i].Value.Replace("(", "").Replace(")", "");
									double tmp = System.Convert.ToDouble(dt.Compute(line, ""));
									left = left.Replace(a2[i].Value, tmp.ToString());
								}
								left = left.Replace(",", ".");
								System.Text.RegularExpressions.MatchCollection b = System.Text.RegularExpressions.Regex.Matches(right, "\\(.+\\)");
								for (int i = 0; i < b.Count; i++)
								{
									string line = b[i].Value.Replace("(", "").Replace(")", "");
									double tmp = System.Convert.ToDouble(dt.Compute(line, ""));
									right = right.Replace(b[i].Value, tmp.ToString());
								}
								right = right.Replace(",", ".");
							}
							int Lside2 = System.Convert.ToInt32(dt.Compute(left, ""));
							int Rside2 = System.Convert.ToInt32(dt.Compute(right, ""));
							if (this.operation == Operation.Equal)
							{
								if (Lside2 == Rside2)
								{
									result = true;
									return result;
								}
								result = false;
								return result;
							}
							else
							{
								if (this.operation == Operation.Different)
								{
									if (Lside2 != Rside2)
									{
										result = true;
										return result;
									}
									result = false;
									return result;
								}
								else
								{
									if (this.operation == Operation.Less)
									{
										if (Lside2 < Rside2)
										{
											result = true;
											return result;
										}
										result = false;
										return result;
									}
									else
									{
										if (this.operation == Operation.LessOrEqual)
										{
											if (Lside2 <= Rside2)
											{
												result = true;
												return result;
											}
											result = false;
											return result;
										}
										else
										{
											if (this.operation == Operation.More)
											{
												if (Lside2 >= Rside2)
												{
													result = true;
													return result;
												}
												result = false;
												return result;
											}
											else
											{
												if (this.operation == Operation.MoreOrEqual)
												{
													if (Lside2 >= Rside2)
													{
														result = true;
														return result;
													}
													result = false;
													return result;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (System.InvalidCastException)
			{
				result = false;
				return result;
			}
			catch (EvaluateException)
			{
				result = false;
				return result;
			}
			catch (SyntaxErrorException)
			{
				result = false;
				return result;
			}
			result = false;
			return result;
		}
	}
}
