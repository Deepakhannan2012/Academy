// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement the Double.Parse method
// ------------------------------------------------------------------------------------------------
using System.Transactions;
using static System.Console;

namespace A07;

class Program {
   static void Main () {
      string[] validArr = ["0","5","42","000123","+5","-5","+0","-0","0.5","5.0",".5",
         "5.","000.250","+0.5","-0.5","+.75","-.25","1e2","1e-2","1e+2","5.3e3",
         "0.5e-1","+1e2","-1e2","+.5e+3","-.5e-3","1.",".0","0e0","10e-0", "+3.14E-2"];

      string[] invalidArr = ["1e2e3","1ee2","1e","1e+","1e-","12a","3.4f",
         "1_000","NaN","Infinity",".","+.","-.","e10","+","-","e","+e10","-e10","1e2e",
         "1e2+","1e2-","1.0.0","0..1"];

      TestCases (invalidArr);
   }

   static double DoubleParse (string input) {
      int integer = 0, fraction = 0, exponent = 0, sign, expSign = 1, div = 0, num;
      bool deciPart = false, expPart = false;

      if (input.Contains ('E')) input = input.ToLower ();
      if (string.IsNullOrEmpty (input) || !input.Any (Char.IsDigit)) return -1.1;

      EChar flags = EChar.Null;

      sign = input[0] is '-' ? -1 : 1;
      input = (input[0] is '+' or '-') ? input[1..] : input;

      foreach (char a in input) {
         num = a - '0';
         switch (a) {
            case '.' when flags is EChar.Null or EChar.Integer:
               deciPart = true;
               flags |= EChar.Integer | EChar.Decimal;
               continue;
            case 'e' when ((flags & EChar.Fraction) != 0 || (flags & EChar.Integer) != 0) && !flags.HasFlag(EChar.ExpSign):
               flags |= EChar.ExpSign;
               (expPart, deciPart) = (true, false);
               continue;

            case '+' when flags.HasFlag (EChar.ExpSign) && !flags.HasFlag (EChar.Exponent):
            case '-' when flags.HasFlag (EChar.ExpSign) && !flags.HasFlag (EChar.Exponent):
               expSign = (a == '-') ? -1 : 1;
               flags |= EChar.ExpSign;
               continue;
            default:
               if (Char.IsDigit (a)) {
                  if (deciPart) {
                     flags |= EChar.Fraction;
                     fraction = (fraction * 10) + num;
                     div++;
                     continue;
                  }
                  if (expPart) {
                     flags |= EChar.Exponent;
                     exponent = (exponent * 10) + num;
                     continue;
                  }
                  integer = (integer * 10) + num;
                  flags |= EChar.Integer;
                  continue;
               }
               //if (!(((flags & EChar.ExpSign) == 0) ^ ((flags & EChar.Exponent) == 0))) ;
               if ((flags & EChar.ExpSign) != 0 )
               return -1.1;                                                                          //// invalid
               //ThrowError ();
               break;
         }
      }
      return sign * ((integer + fraction / Math.Pow (10, div)) * Math.Pow (10, expSign * exponent));
   }

   static void ThrowError () => throw new Exception ("Not in correct format");

   static void TestCases (string[] arr) {
      foreach (var item in arr) {
         WriteLine ($"> Our result: {DoubleParse (item)}");
         //WriteLine ($"  Expected result: {double.Parse (item)}");
         //if (DoubleParse (item) == double.Parse (item)) {
         //   ForegroundColor = ConsoleColor.Green;
         //   Console.WriteLine ("Answer matches");
         //} else {
         //   ForegroundColor = ConsoleColor.Red;
         //   Console.WriteLine ("Answer does not match");
         //}
         //ResetColor ();
         Console.WriteLine ();
      }
   }

   enum EChar { Null = 0, Integer = 1, Decimal = 2, Fraction = 4, ExpSign = 8, Exponent = 16 }
}
