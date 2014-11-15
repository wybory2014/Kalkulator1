using System;
namespace Kalkulator1.ResponseClass
{
	public class Code
	{
		private int code;
		public Code()
		{
			this.code = -1;
		}
		public Code(int i)
		{
			this.code = i;
		}
		public int getcode()
		{
			return this.code;
		}
		public string getText()
		{
			string result;
			switch (this.code)
			{
			case -1:
				result = "Utracono połączenie";
				return result;
			case 0:
				result = "Operacja zakończona sukcesem";
				return result;
			case 1:
				result = "Plik już istnieje.";
				return result;
			case 2:
				result = "Nie jestes autoryzowany";
				return result;
			case 3:
				result = "Nie oczekiwana odpowiedź z serwera";
				return result;
			case 4:
				result = "Nie ma danego pliku na serwerze";
				return result;
			case 5:
				result = "Sesja logowania wygasła";
				return result;
			case 6:
				result = "Nie można eksportować protokołu z innego jns";
				return result;
			case 7:
				result = "Nie można importować protokołu z innego jns";
				return result;
			case 8:
				result = "Operacja nie powiodła się";
				return result;
			case 9:
				result = "Ostrzeżenia twarde odrzucone";
				return result;
			case 10:
				result = "Ostrzeżenia twarde zaakceptowane";
				return result;
			case 11:
				result = "Oczekuje na odpowiedź";
				return result;
			case 12:
				result = "Zgłoszenie anulowane";
				return result;
			case 14:
				result = "Nie można wysłac protokołu. Nieprawidłowa licencja.";
				return result;
			case 15:
				result = "Nie można wysłac protokołu po raz drugi.";
				return result;
			case 16:
				result = string.Concat(new string[]
				{
					"Nie można wysłac protokołu.",
					'\n'.ToString(),
					"Liczba uprawnionych do głosowania w wyborach do rady gminy, a na terenie m.st. Warszawy również w wyborach do rady dzielnicy (poza obwodami odrębnymi) musi być równa liczbie uprawnionych do głosowania odpowiednio w wyborach wójta i Prezydenta m.st. Warszawy.",
					'\n'.ToString(),
					"Jedynie w obwodach odrębnych na terenie m.st. Warszawy liczba uprawnionych do głosowania w wyborach do rady dzielnicy może być mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do Rady m.st. Warszawy i Prezydenta m.st. Warszawy"
				});
				return result;
			case 17:
				result = string.Concat(new string[]
				{
					"Nie można wysłac protokołu.",
					'\n'.ToString(),
					"W obwodzie głosowania, w skład którego wchodzi więcej niż jeden okręg wyborczy dla wyboru rady gminy, liczba uprawnionych do głosowania w wyborach wójta (zarówno ogółem, jak obywateli polskich i pozostałych obywateli Unii Europejskiej) musi się równać sumie odpowiednich liczb uprawnionych do głosowania w wyborach do rady gminy w okręgach wchodzących w skład obwodu głosowania.",
					'\n'.ToString(),
					"Jedynie gdy w jednym lub więcej z tych okręgów wyborczych dla wyboru rady gminy wyborów lub głosowania nie przeprowadza się liczby uprawnionych do głosowania w protokole głosowania w wyborach wójta muszą być większe od sumy odpowiednich liczb uprawnionych do głosowania w wyborach do rady gminy w okręgach wchodzących w skład obwodu głosowania."
				});
				return result;
			case 18:
				result = string.Concat(new string[]
				{
					"Nie można wysłac protokołu.",
					'\n'.ToString(),
					"Liczba osób uprawnionych do głosowania w wyborach do rady powiatu i do sejmiku województwa (poza obwodami odrębnymi) musi być równa liczbie obywateli polskich uprawnionych do głosowania w wyborach do rady gminy i w wyborach wójta (z części A spisu wyborców).",
					'\n'.ToString(),
					"Jedynie w obwodzie głosowania, w skład którego wchodzi więcej niż jeden okręg wyborczy dla wyboru rady gminy, gdy w jednym lub więcej z tych okręgów wyborczych dla wyboru rady gminy wyborów lub głosowania nie przeprowadza się suma liczb uprawnionych do głosowania obywateli polskich w wyborach do rady gminy musi być mniejsza od liczby uprawnionych do głosowania w wyborach do rady powiatu i w wyborach do sejmiku województwa.",
					'\n'.ToString(),
					"Ponadto w obwodach odrębnych liczba uprawnionych do głosowania obywateli polskich w wyborach do rady gminy i wójta może być mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do rady powiatu, a liczba uprawnionych do głosowania w wyborach do rady powiatu mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do sejmiku województwa."
				});
				return result;
			case 19:
				result = "Zgłoszenia już nie są przyjmowane";
				return result;
			}
			result = "";
			return result;
		}
	}
}
