// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find the anagrams from a given word list
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A14._1;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      var wordList = File.ReadAllLines ("words.txt");
      PrintAnagrams (wordList);
   }

   #region Implementation -------------------------------------------
   // Prints the anagrams from a given array of words
   static void PrintAnagrams (string[] wordList) {
      Dictionary<string, List<string>> validWords = [];
      foreach (string word in wordList) {
         string sortedWord = new ([.. word.OrderBy (c => c)]);
         if (validWords.TryGetValue (sortedWord, out List<string>? words)) {
            words.Add (word);
            continue;
         }
         validWords[sortedWord] = [word];
      }
      foreach (var (count, words) in validWords.Where (x => x.Value.Count > 1)
                                               .Select (x => (x.Value.Count, x.Value.Order ()))
                                               .OrderByDescending (x => x.Count)
                                               .ThenBy(x => x.Item2.First())) {
         Write ($"{count} - ");
         foreach (var word in words) Write ($"{word} ");
         WriteLine ();
      }
   }
   #endregion
}
#endregion
