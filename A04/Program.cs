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
                         .ToDictionary (g => g.Key, g => g.Count ())
                         .OrderByDescending (g => g.Value);
      foreach (var (ch, count) in charFreq) WriteLine ($"{ch} - {count}");
      WriteLine ($"The 7 most recurring letters are: " +
                 $"{string.Join (", ", charFreq.Take (7).Select (x => x.Key))}");
   }
}
#endregion

