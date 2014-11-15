using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
namespace Kalkulator1
{
	internal class codeBar
	{
		private string code;
		private string text;
		private string textReadable;
		private string path;
		private System.Collections.Generic.Dictionary<string, string> codes;
		private System.Collections.Generic.Dictionary<char, string> codesValue;
		public codeBar()
		{
			this.code = "";
			this.text = "";
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.codes = new System.Collections.Generic.Dictionary<string, string>();
			this.codesValue = new System.Collections.Generic.Dictionary<char, string>();
			this.codes.Add("0", "100010100");
			this.codesValue.Add('0', "0");
			this.codes.Add("1", "101001000");
			this.codesValue.Add('1', "1");
			this.codes.Add("2", "101000100");
			this.codesValue.Add('2', "2");
			this.codes.Add("3", "101000010");
			this.codesValue.Add('3', "3");
			this.codes.Add("4", "100101000");
			this.codesValue.Add('4', "4");
			this.codes.Add("5", "100100100");
			this.codesValue.Add('5', "5");
			this.codes.Add("6", "100100010");
			this.codesValue.Add('6', "6");
			this.codes.Add("7", "101010000");
			this.codesValue.Add('7', "7");
			this.codes.Add("8", "100010010");
			this.codesValue.Add('8', "8");
			this.codes.Add("9", "100001010");
			this.codesValue.Add('9', "9");
			this.codes.Add("10", "110101000");
			this.codesValue.Add('A', "10");
			this.codes.Add("11", "110100100");
			this.codesValue.Add('B', "11");
			this.codes.Add("12", "110100010");
			this.codesValue.Add('C', "12");
			this.codes.Add("13", "110010100");
			this.codesValue.Add('D', "13");
			this.codes.Add("14", "110010010");
			this.codesValue.Add('E', "14");
			this.codes.Add("15", "110001010");
			this.codesValue.Add('F', "15");
			this.codes.Add("16", "101101000");
			this.codesValue.Add('G', "16");
			this.codes.Add("17", "101100100");
			this.codesValue.Add('H', "17");
			this.codes.Add("18", "101100010");
			this.codesValue.Add('I', "18");
			this.codes.Add("19", "100110100");
			this.codesValue.Add('J', "19");
			this.codes.Add("20", "100011010");
			this.codesValue.Add('K', "20");
			this.codes.Add("21", "101011000");
			this.codesValue.Add('L', "21");
			this.codes.Add("22", "101001100");
			this.codesValue.Add('M', "22");
			this.codes.Add("23", "101000110");
			this.codesValue.Add('N', "23");
			this.codes.Add("24", "100101100");
			this.codesValue.Add('O', "24");
			this.codes.Add("25", "100010110");
			this.codesValue.Add('P', "25");
			this.codes.Add("26", "110110100");
			this.codesValue.Add('Q', "26");
			this.codes.Add("27", "110110010");
			this.codesValue.Add('R', "27");
			this.codes.Add("28", "110101100");
			this.codesValue.Add('S', "28");
			this.codes.Add("29", "110100110");
			this.codesValue.Add('T', "29");
			this.codes.Add("30", "110010110");
			this.codesValue.Add('U', "30");
			this.codes.Add("31", "110011010");
			this.codesValue.Add('V', "31");
			this.codes.Add("32", "101101100");
			this.codesValue.Add('W', "32");
			this.codes.Add("33", "101100110");
			this.codesValue.Add('X', "33");
			this.codes.Add("34", "100110110");
			this.codesValue.Add('Y', "34");
			this.codes.Add("35", "100111010");
			this.codesValue.Add('Z', "35");
			this.codes.Add("36", "100101110");
			this.codesValue.Add('-', "36");
			this.codes.Add("37", "111010100");
			this.codesValue.Add('.', "37");
			this.codes.Add("38", "111010010");
			this.codesValue.Add(' ', "38");
			this.codes.Add("39", "111001010");
			this.codesValue.Add('$', "39");
			this.codes.Add("40", "101101110");
			this.codesValue.Add('/', "40");
			this.codes.Add("41", "101110110");
			this.codesValue.Add('+', "41");
			this.codes.Add("42", "110101110");
			this.codesValue.Add('%', "42");
			this.codes.Add("-", "101011110");
			this.codesValue.Add('*', "-");
			this.codes.Add("43", "100100110");
			this.codes.Add("44", "111011010");
			this.codes.Add("45", "111010110");
			this.codes.Add("46", "100110010");
		}
		public string getCode()
		{
			return this.code;
		}
		public string getText()
		{
			return this.text;
		}
		public string getTextReadable()
		{
			return this.textReadable;
		}
		public void generateCode(string text)
		{
			this.text = text.ToUpper();
			this.textReadable = "";
			for (int i = 0; i < text.Length; i++)
			{
				if (i % 4 == 0 && i != 0)
				{
					this.textReadable = this.textReadable + "-" + text[i];
				}
				else
				{
					this.textReadable += text[i];
				}
			}
			System.Collections.Generic.List<string> valueofCode = new System.Collections.Generic.List<string>();
			char[] textArray = this.text.ToCharArray();
			int C = 0;
			int iC = 1;
			int K = 0;
			int iK = 2;
			for (int i = textArray.Length - 1; i >= 0; i--)
			{
				string value = this.codesValue[textArray[i]];
				valueofCode.Add(value);
				C += iC * System.Convert.ToInt32(value);
				K += iK * System.Convert.ToInt32(value);
				iC++;
				if (iC > 20)
				{
					iC = 1;
				}
				iK++;
				if (iK > 15)
				{
					iC = 1;
				}
			}
			C %= 47;
			K += C;
			K %= 47;
			string codeOftext = this.codes["-"];
			for (int i = valueofCode.Count - 1; i >= 0; i--)
			{
				codeOftext += this.codes[valueofCode[i]];
			}
			codeOftext += this.codes[C.ToString()];
			codeOftext += this.codes[K.ToString()];
			codeOftext += this.codes["-"];
			codeOftext += "1";
			this.code = codeOftext;
		}
		public void draw(PictureBox pictureBox1)
		{
			string path = this.path + "\\tmp\\printTmp\\code.png";
			if (this.text != "" && this.code != "")
			{
				int width = this.code.Length * 2 + 20;
				pictureBox1.Size = new System.Drawing.Size(width, 70);
				System.Drawing.Bitmap DrawArea = new System.Drawing.Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
				pictureBox1.Image = DrawArea;
				pictureBox1.Image = new System.Drawing.Bitmap(width, 70);
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(pictureBox1.Image);
				g.Clear(System.Drawing.Color.White);
				pictureBox1.Refresh();
				System.Drawing.Pen brush = new System.Drawing.Pen(System.Drawing.Color.Black);
				brush.Width = 2f;
				for (int i = 0; i < this.code.Length; i++)
				{
					if (this.code[i] == '1')
					{
						int x = 2 * i + 10;
						g.DrawLine(brush, x, 0, x, 50);
					}
				}
				try
				{
					System.Drawing.Font drawFont = new System.Drawing.Font("Times New Roman", 10f);
					System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
					System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
					TextRenderer.DrawText(g, this.textReadable, drawFont, new System.Drawing.Point(12, 50), System.Drawing.Color.Black);
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message, "Uwaga");
				}
				pictureBox1.Visible = false;
				pictureBox1.Refresh();
				pictureBox1.Image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
			}
			else
			{
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}
			}
		}
	}
}
