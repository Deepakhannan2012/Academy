// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement the Double.Parse method
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      Write ("Enter the number to be converted to a double: ");
      WriteLine ($"The converted double is {double.Parse (ReadLine () ?? "")}.");
   }

   #region Implementation -------------------------------------------
   // Converts the given input into a double
   static double DoubleParse (string input) {
      int integer = 0, frac = 0, exponent = 0, sign, expSign = 1, div = 0, num;
      (bool deciPart, bool expPart) = (false, false);
      if (string.IsNullOrEmpty (input) || !input.Any (Char.IsDigit)) ThrowError (input);
      if (input.Contains ('E')) input = input.ToLower ();
      EChar flags = EChar.Null;
      sign = input[0] == '-' ? -1 : 1;
      input = (input[0] is '+' or '-') ? input[1..] : input;
      foreach (char a in input) {
         switch (a) {
            // Checks if the decimal is present only after an integer or in the first position
            case '.' when flags is EChar.Null or EChar.Integer:
               deciPart = true;
               flags |= EChar.Integer | EChar.Decimal;
               continue;
            // Checks if the 'e' is present only after an integer or after the decimal which
            // indirectly ensures that a digit is present before the 'e'
            case 'e' when ((flags & EChar.Fraction) != 0 || (flags & EChar.Integer) != 0)
                            && (flags & EChar.ExpSign) == 0:
               flags |= EChar.ExpSign;
               (expPart, deciPart) = (true, false);
               continue;
            // Checks if the sign is present only after the 'e'
            case '+' when (flags & EChar.ExpSign) != 0 && (flags & EChar.Exponent) == 0:
            case '-' when (flags & EChar.ExpSign) != 0 && (flags & EChar.Exponent) == 0:
               expSign = (a == '-') ? -1 : 1;
               flags |= EChar.ExpSign;
               continue;
            default:
               if (Char.IsDigit (a)) {
                  num = a - '0';
                  // Adds numbers to the decimal part of the double
                  if (deciPart) {
                     flags |= EChar.Fraction;
                     frac = (frac * 10) + num;
                     div++;
                     continue;
                  }
                  // Adds numbers to the exponent part of the double
                  if (expPart) {
                     flags |= EChar.Exponent;
                     exponent *= 10 + num;
                     continue;
                  }
                  // Adds numbers to the integer part of the double
                  integer = (integer * 10) + num;
                  flags |= EChar.Integer;
                  continue;
               }
               ThrowError (input);
               break;
         }
      }
      // Checks if a digit is present after the 'e'
      if ((flags & EChar.ExpSign) != 0 && (flags & EChar.Exponent) == 0) ThrowError (input);
      return sign * ((integer + frac * Math.Pow (10, -div)) * Math.Pow (10, expSign * exponent));
   }

   // Throws an error when the input is not of the proper format
   static void ThrowError (string input)
      => throw new FormatException ($"The input string '{input}' was not in a correct format");
   #endregion

   #region Enum -----------------------------------------------------
   // Defines the multiple components of a double
   enum EChar { Null = 0, Integer = 1, Decimal = 2, Fraction = 4, ExpSign = 8, Exponent = 16 }
   #endregion
}
#endregion
