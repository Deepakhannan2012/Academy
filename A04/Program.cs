// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to build of frequency table of letters in a file
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A04;

#region class Program------------------------------------------------------------------------------
class Program {
   static void Main () {
      var charFreq = File.ReadAllText ("words.txt")
                         .Where (char.IsLetter)
                         .GroupBy (ch => ch)
                         .Select (g => new { Letter = g.Key, Count = g.Count () })
                         .OrderByDescending (g => g.Count)
                         .Take(7);
      WriteLine ($"The 7 most recurring letters are: ");
      foreach (var pair in charFreq) WriteLine ($"{pair.Letter} - {pair.Count}");
   }
}
#endregion