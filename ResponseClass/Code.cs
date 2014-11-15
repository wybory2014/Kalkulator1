// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ResponseClass.Code
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

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
      switch (this.code)
      {
        case -1:
          return "Utracono połączenie";
        case 0:
          return "Operacja zakończona sukcesem";
        case 1:
          return "Plik już istnieje.";
        case 2:
          return "Nie jestes autoryzowany";
        case 3:
          return "Nie oczekiwana odpowiedź z serwera";
        case 4:
          return "Nie ma danego pliku na serwerze";
        case 5:
          return "Sesja logowania wygasła";
        case 6:
          return "Nie można eksportować protokołu z innego jns";
        case 7:
          return "Nie można importować protokołu z innego jns";
        case 8:
          return "Operacja nie powiodła się";
        case 9:
          return "Ostrzeżenia twarde odrzucone";
        case 10:
          return "Ostrzeżenia twarde zaakceptowane";
        case 11:
          return "Oczekuje na odpowiedź";
        case 12:
          return "Zgłoszenie anulowane";
        case 14:
          return "Nie można wysłac protokołu. Nieprawidłowa licencja.";
        case 15:
          return "Nie można wysłac protokołu po raz drugi.";
        case 16:
          string[] strArray1 = new string[5];
          strArray1[0] = "Nie można wysłac protokołu.";
          string[] strArray2 = strArray1;
          int index1 = 1;
          char ch1 = '\n';
          string str1 = ch1.ToString();
          strArray2[index1] = str1;
          strArray1[2] = "Liczba uprawnionych do głosowania w wyborach do rady gminy, a na terenie m.st. Warszawy również w wyborach do rady dzielnicy (poza obwodami odrębnymi) musi być równa liczbie uprawnionych do głosowania odpowiednio w wyborach wójta i Prezydenta m.st. Warszawy.";
          string[] strArray3 = strArray1;
          int index2 = 3;
          ch1 = '\n';
          string str2 = ch1.ToString();
          strArray3[index2] = str2;
          strArray1[4] = "Jedynie w obwodach odrębnych na terenie m.st. Warszawy liczba uprawnionych do głosowania w wyborach do rady dzielnicy może być mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do Rady m.st. Warszawy i Prezydenta m.st. Warszawy";
          return string.Concat(strArray1);
        case 17:
          string[] strArray4 = new string[5];
          strArray4[0] = "Nie można wysłac protokołu.";
          string[] strArray5 = strArray4;
          int index3 = 1;
          char ch2 = '\n';
          string str3 = ch2.ToString();
          strArray5[index3] = str3;
          strArray4[2] = "W obwodzie głosowania, w skład którego wchodzi więcej niż jeden okręg wyborczy dla wyboru rady gminy, liczba uprawnionych do głosowania w wyborach wójta (zarówno ogółem, jak obywateli polskich i pozostałych obywateli Unii Europejskiej) musi się równać sumie odpowiednich liczb uprawnionych do głosowania w wyborach do rady gminy w okręgach wchodzących w skład obwodu głosowania.";
          string[] strArray6 = strArray4;
          int index4 = 3;
          ch2 = '\n';
          string str4 = ch2.ToString();
          strArray6[index4] = str4;
          strArray4[4] = "Jedynie gdy w jednym lub więcej z tych okręgów wyborczych dla wyboru rady gminy wyborów lub głosowania nie przeprowadza się liczby uprawnionych do głosowania w protokole głosowania w wyborach wójta muszą być większe od sumy odpowiednich liczb uprawnionych do głosowania w wyborach do rady gminy w okręgach wchodzących w skład obwodu głosowania.";
          return string.Concat(strArray4);
        case 18:
          string[] strArray7 = new string[7];
          strArray7[0] = "Nie można wysłac protokołu.";
          string[] strArray8 = strArray7;
          int index5 = 1;
          char ch3 = '\n';
          string str5 = ch3.ToString();
          strArray8[index5] = str5;
          strArray7[2] = "Liczba osób uprawnionych do głosowania w wyborach do rady powiatu i do sejmiku województwa (poza obwodami odrębnymi) musi być równa liczbie obywateli polskich uprawnionych do głosowania w wyborach do rady gminy i w wyborach wójta (z części A spisu wyborców).";
          string[] strArray9 = strArray7;
          int index6 = 3;
          ch3 = '\n';
          string str6 = ch3.ToString();
          strArray9[index6] = str6;
          strArray7[4] = "Jedynie w obwodzie głosowania, w skład którego wchodzi więcej niż jeden okręg wyborczy dla wyboru rady gminy, gdy w jednym lub więcej z tych okręgów wyborczych dla wyboru rady gminy wyborów lub głosowania nie przeprowadza się suma liczb uprawnionych do głosowania obywateli polskich w wyborach do rady gminy musi być mniejsza od liczby uprawnionych do głosowania w wyborach do rady powiatu i w wyborach do sejmiku województwa.";
          string[] strArray10 = strArray7;
          int index7 = 5;
          ch3 = '\n';
          string str7 = ch3.ToString();
          strArray10[index7] = str7;
          strArray7[6] = "Ponadto w obwodach odrębnych liczba uprawnionych do głosowania obywateli polskich w wyborach do rady gminy i wójta może być mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do rady powiatu, a liczba uprawnionych do głosowania w wyborach do rady powiatu mniejsza lub równa liczbie uprawnionych do głosowania w wyborach do sejmiku województwa.";
          return string.Concat(strArray7);
        case 19:
          return "Zgłoszenia już nie są przyjmowane";
        default:
          return "";
      }
    }
  }
}
