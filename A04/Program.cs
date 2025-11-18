// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to build of frequency table of letters in a file
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A04;

class Program {
   static void Main () {
      Dictionary<char, int> charFreq = [];
      foreach (string word in File.ReadAllLines ("words.txt"))
         foreach (char ch in word)
            if (Char.IsLetter (ch))
               charFreq[ch] = charFreq.TryGetValue (ch, out int count) ? ++count : 1;
      var sorted = charFreq.OrderByDescending (x => x.Value);
      foreach (var (ch, count) in sorted) WriteLine ($"{ch} - {count}");
      WriteLine ($"The 7 most recurring letters are: " +
                 $"{string.Join (", ", sorted.Take (7).Select (x => x.Key))}");
   }
}