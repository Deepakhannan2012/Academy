// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to solve a New - York Times style Spelling Bee game
// ------------------------------------------------------------------------------------------------
using static System.Console;

// Main program
string[] words = File.ReadAllLines ("words.txt");
(int count, int totalScore) = (0, 0);
Write ("Enter the seven letters of Spelling Bee, starting with the mandatory letter: ");
GetInput (out char[] letters);
List<(string, int)> validWords = [.. words.Where (x => x.Length is > 3 && x.Contains (letters[0]) && x.All (c => letters.Contains (c)))
                                          .Select(word => (word,Score(word)))
                                          .OrderByDescending(x => x.Item2)];
if (validWords.Count is not 0) ForegroundColor = ConsoleColor.Green;
foreach (var word in validWords) {
   if (word.Item1 == validWords[count].Item1) ResetColor ();
   WriteLine ($"{word.Item2,3}. {word.Item1}");
   totalScore += word.Item2;
}
WriteLine ($"----\n{totalScore} total");

// Returns the score of each word
int Score (string word) {
   int length = word.Length, score = length is 4 ? 1 : length;
   // Adds 7 points for a pangram and counts the number of pangrams
   if (letters.All (c => word.Contains (c))) { score += 7; count++; }
   return score;
}

// Gets the array of characters from the user input
void GetInput (out char[] arr) {
   while (true) {
      string input = ReadLine ()?.ToUpper ().Trim () ?? "";
      if (!string.IsNullOrEmpty (input) && input.All (char.IsLetter) && input.Length is 7) {
         arr = input.ToCharArray (); break;
      }
      Write ("Invalid Input. Enter only seven letters: ");
   }
}