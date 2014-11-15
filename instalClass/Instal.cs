// Decompiled with JetBrains decompiler
// Type: Kalkulator1.instalClass.Instal
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.IO;
using System.Windows.Forms;

namespace Kalkulator1.instalClass
{
  internal class Instal
  {
    public Instal(string path)
    {
      if (!Directory.Exists(path + "\\tmp"))
      {
        try
        {
          Directory.CreateDirectory(path + "\\tmp");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
        }
      }
      if (!Directory.Exists(path + "\\tmp\\printTmp"))
      {
        try
        {
          Directory.CreateDirectory(path + "\\tmp\\printTmp");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
        }
      }
      if (!Directory.Exists(path + "\\tmp\\printTmp\\css"))
      {
        try
        {
          Directory.CreateDirectory(path + "\\tmp\\printTmp\\css");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"css\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
        }
      }
      if (!Directory.Exists(path + "\\ProtocolsDef"))
      {
        try
        {
          Directory.CreateDirectory(path + "\\ProtocolsDef");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
      }
      if (!Directory.Exists(path + "\\electoralEampaign"))
      {
        try
        {
          Directory.CreateDirectory(path + "\\electoralEampaign");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
      }
      if (Directory.Exists(path + "\\Attendance"))
        return;
      try
      {
        Directory.CreateDirectory(path + "\\Attendance");
      }
      catch (ArgumentNullException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
      catch (ArgumentException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
      catch (UnauthorizedAccessException ex)
      {
        int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
      }
      catch (PathTooLongException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
      catch (DirectoryNotFoundException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
      catch (NotSupportedException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
      catch (IOException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
      }
    }
  }
}
