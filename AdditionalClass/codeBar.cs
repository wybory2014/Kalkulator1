// Decompiled with JetBrains decompiler
// Type: Kalkulator1.codeBar
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

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
    private Dictionary<string, string> codes;
    private Dictionary<char, string> codesValue;

    public codeBar()
    {
      this.code = "";
      this.text = "";
      this.path = Path.GetTempPath() + "KBW";
      this.codes = new Dictionary<string, string>();
      this.codesValue = new Dictionary<char, string>();
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
      for (int index = 0; index < text.Length; ++index)
      {
        if (index % 4 == 0 && index != 0)
        {
          codeBar codeBar = this;
          string str = codeBar.textReadable + (object) "-" + (string) (object) text[index];
          codeBar.textReadable = str;
        }
        else
        {
          codeBar codeBar = this;
          string str = codeBar.textReadable + (object) text[index];
          codeBar.textReadable = str;
        }
      }
      List<string> list = new List<string>();
      char[] chArray = this.text.ToCharArray();
      int num1 = 0;
      int num2 = 1;
      int num3 = 0;
      int num4 = 2;
      for (int index = chArray.Length - 1; index >= 0; --index)
      {
        string str = this.codesValue[chArray[index]];
        list.Add(str);
        num1 += num2 * Convert.ToInt32(str);
        num3 += num4 * Convert.ToInt32(str);
        ++num2;
        if (num2 > 20)
          num2 = 1;
        ++num4;
        if (num4 > 15)
          num2 = 1;
      }
      int num5 = num1 % 47;
      int num6 = (num3 + num5) % 47;
      string str1 = this.codes["-"];
      for (int index = list.Count - 1; index >= 0; --index)
        str1 = str1 + this.codes[list[index]];
      this.code = str1 + this.codes[num5.ToString()] + this.codes[num6.ToString()] + this.codes["-"] + "1";
    }

    public void draw(PictureBox pictureBox1)
    {
      string str = this.path + "\\tmp\\printTmp\\code.png";
      if (this.text != "" && this.code != "")
      {
        int width = this.code.Length * 2 + 20;
        pictureBox1.Size = new Size(width, 70);
        Bitmap bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
        pictureBox1.Image = (Image) bitmap;
        pictureBox1.Image = (Image) new Bitmap(width, 70);
        Graphics graphics = Graphics.FromImage(pictureBox1.Image);
        graphics.Clear(Color.White);
        pictureBox1.Refresh();
        Pen pen = new Pen(Color.Black);
        pen.Width = 2f;
        for (int index = 0; index < this.code.Length; ++index)
        {
          if ((int) this.code[index] == 49)
          {
            int num = 2 * index + 10;
            graphics.DrawLine(pen, num, 0, num, 50);
          }
        }
        try
        {
          Font font = new Font("Times New Roman", 10f);
          SolidBrush solidBrush = new SolidBrush(Color.Black);
          StringFormat stringFormat = new StringFormat();
          TextRenderer.DrawText((IDeviceContext) graphics, this.textReadable, font, new Point(12, 50), Color.Black);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, "Uwaga");
        }
        pictureBox1.Visible = false;
        pictureBox1.Refresh();
        pictureBox1.Image.Save(str, ImageFormat.Png);
      }
      else if (File.Exists(str))
        File.Delete(str);
    }
  }
}
