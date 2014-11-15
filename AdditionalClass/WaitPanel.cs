// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalClass.WaitPanel
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kalkulator1.AdditionalClass
{
  public class WaitPanel
  {
    public Panel p;
    public Label titleLab;
    public Label sLab;

    public WaitPanel(string name, int width, int height)
    {
      this.createPopup("Proszę czekać", "Proszę czekać", name, width, height);
    }

    private void createPopup(string statement, string title, string name, int width, int height)
    {
      this.p = new Panel();
      this.p.Name = name;
      int num1 = 373;
      int num2 = 196;
      if (width < num1)
        ;
      if (height < num2)
        ;
      this.p.Size = new Size(373, 196);
      this.p.BorderStyle = BorderStyle.FixedSingle;
      this.p.BackColor = SystemColors.ControlLightLight;
      this.p.Visible = false;
      int x = 0;
      if (width > this.p.Width)
        x = Convert.ToInt32((width - this.p.Width) / 2);
      int y = 0;
      if (height > this.p.Height)
        y = Convert.ToInt32((height - this.p.Height) / 2);
      this.p.Location = new Point(x, y);
      this.titleLab = new Label();
      this.titleLab.Text = title;
      this.titleLab.Name = "label_Title_WaitPanel_" + name;
      this.titleLab.AutoSize = true;
      this.titleLab.MaximumSize = new Size(200, 0);
      this.titleLab.Location = new Point(40, 20);
      this.titleLab.Font = new Font(new FontFamily("Arial"), 15f, FontStyle.Bold);
      this.p.Controls.Add((Control) this.titleLab);
      this.p.Controls["label_Title_WaitPanel_" + name].BringToFront();
      this.sLab = new Label();
      this.sLab.Name = "label_Statement_WaitPanel_" + name;
      this.sLab.Text = statement;
      this.sLab.AutoSize = true;
      this.sLab.MaximumSize = new Size(250, 0);
      this.sLab.Location = new Point(40, this.titleLab.Location.Y + this.titleLab.Height + 40);
      this.sLab.Font = new Font(new FontFamily("Arial"), 10f);
      this.p.Controls.Add((Control) this.sLab);
      this.p.Controls["label_Statement_WaitPanel_" + name].BringToFront();
    }

    public void setWaitPanel(string statement, string title)
    {
      this.titleLab.Text = title;
      this.titleLab.Refresh();
      this.sLab.Text = statement;
      this.sLab.Refresh();
    }

    public void setVisible(bool visible)
    {
      this.p.Visible = visible;
      this.sLab.Visible = visible;
      this.titleLab.Visible = visible;
      this.titleLab.Refresh();
      this.sLab.Refresh();
    }

    public void setSize(Size size)
    {
      this.p.Size = size;
      this.sLab.MaximumSize = new Size(Convert.ToInt32((double) size.Width * 0.67), 0);
      this.titleLab.MaximumSize = new Size(Convert.ToInt32((double) size.Width * 0.5), 0);
      this.p.Location = new Point(0, 0);
      this.p.Refresh();
      this.titleLab.Refresh();
      this.sLab.Refresh();
    }

    public void setLocation(Point local)
    {
      this.p.Location = local;
      this.p.Refresh();
    }

    public Panel getPanel()
    {
      return this.p;
    }

    public string getName()
    {
      return this.p.Name;
    }
  }
}
