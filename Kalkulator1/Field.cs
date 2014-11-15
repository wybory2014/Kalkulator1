using System;
namespace Kalkulator1
{
	internal class Field
	{
		private string name1;
		private string name2;
		private string name2v2;
		private string name3;
		private string lista;
		private string imie1;
		private string imie2;
		private string nazwisko;
		private string dataType;
		private string save_as;
		private string komitet;
		private string plec;
		private string status;
		private string ch;
		private string fill;
		private string idCandidate;
		private string display;
		private int min;
		private int max;
		public Field()
		{
			this.name1 = "";
			this.name2 = "";
			this.name2v2 = "";
			this.name3 = "";
			this.lista = "";
			this.imie1 = "";
			this.imie2 = "";
			this.plec = "";
			this.nazwisko = "";
			this.dataType = "";
			this.save_as = "";
			this.komitet = "";
			this.ch = "";
			this.fill = "";
			this.status = "";
			this.idCandidate = "";
			this.display = "";
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string idCandidate, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = "";
			this.fill = "";
			this.idCandidate = idCandidate;
			this.min = 0;
			this.max = 0;
			this.display = display;
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string idCandidate, string isChar, string isFill, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = isChar;
			this.fill = isFill;
			this.idCandidate = idCandidate;
			this.min = 0;
			this.max = 0;
			this.display = display;
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string idCandidate, int min, int max, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = "";
			this.fill = "";
			this.idCandidate = idCandidate;
			this.min = min;
			this.max = max;
			this.display = display;
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string idCandidate, string isChar, string isFill, int min, int max, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = isChar;
			this.fill = isFill;
			this.idCandidate = idCandidate;
			this.min = min;
			this.max = max;
			this.display = display;
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = "";
			this.fill = "";
			this.idCandidate = "";
			this.min = 0;
			this.max = 0;
			this.display = display;
		}
		public Field(string name1, string name2, string name2v2, string plec, string status, string imie1, string imie2, string nazwisko, string dataType, string save_as, string komitet, string isChar, string isFill, string name3, string lista, string display)
		{
			this.name1 = name1;
			this.name2 = name2;
			this.name2v2 = name2v2;
			this.name3 = name3;
			this.lista = lista;
			this.imie1 = imie1;
			this.imie2 = imie2;
			this.plec = plec;
			this.nazwisko = nazwisko;
			this.dataType = dataType;
			this.save_as = save_as;
			this.komitet = komitet;
			this.status = status;
			this.ch = isChar;
			this.fill = isFill;
			this.idCandidate = "";
			this.min = 0;
			this.max = 0;
			this.display = display;
		}
		public string getStatus()
		{
			return this.status;
		}
		public string getDisplay()
		{
			return this.display;
		}
		public string getDataType()
		{
			return this.dataType;
		}
		public string getName1()
		{
			return this.name1;
		}
		public string getName2()
		{
			return this.name2;
		}
		public string getName3()
		{
			return this.name3;
		}
		public string getLista()
		{
			return this.lista;
		}
		public string getName2v2()
		{
			return this.name2v2;
		}
		public string getImie1()
		{
			return this.imie1;
		}
		public string getImie2()
		{
			return this.imie2;
		}
		public string getNazwisko()
		{
			return this.nazwisko;
		}
		public string getSave()
		{
			return this.save_as;
		}
		public string getKomitet()
		{
			return this.komitet;
		}
		public string getIdCandidate()
		{
			return this.idCandidate;
		}
		public string getPlec()
		{
			return this.plec;
		}
		public bool isChar()
		{
			return this.ch.ToLower() == "true";
		}
		public string getFill()
		{
			return this.fill;
		}
		public ValidationRange getRange(string name)
		{
			return new ValidationRange(name, this.min, this.max);
		}
		public bool needImportData()
		{
			return this.imie1 != "" || this.imie2 != "" || this.nazwisko != "" || this.komitet != "" || this.idCandidate != "" || this.plec != "" || this.status != "";
		}
	}
}
