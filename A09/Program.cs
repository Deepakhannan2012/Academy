// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement unary minus in the expression evaluator
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A09;

class Program {
   static void Main () {
      var eval = new Evaluator ();
      while (true) {
         Write ("> ");
         string text = ReadLine () ?? "";
         if (text == "exit") break;
         try {
            double result = eval.Evaluate (text);
            ForegroundColor = ConsoleColor.Green;
            WriteLine (result);
         } catch (Exception e) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine (e.Message);
         }
         ResetColor ();
      }
   }
}

