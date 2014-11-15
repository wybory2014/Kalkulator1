using System;
using System.IO;
using System.Windows.Forms;
namespace Kalkulator1.instalClass
{
	internal class Instal
	{
		public Instal(string path)
		{
			if (!System.IO.Directory.Exists(path + "\\tmp"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\tmp");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(path + "\\tmp\\printTmp"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\tmp\\printTmp");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(path + "\\tmp\\printTmp\\css"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\tmp\\printTmp\\css");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"css\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(path + "\\ProtocolsDef"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\ProtocolsDef");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(path + "\\electoralEampaign"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\electoralEampaign");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(path + "\\Attendance"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(path + "\\Attendance");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
			}
		}
	}
}
