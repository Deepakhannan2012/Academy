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
Write ("Enter the seven letters of Spelling Bee, starting with the mandatory letter: ");
GetInput (out char[] letters);
List<(string, int)> validWords = [.. words.Where (x => x.Length is > 3
                                                       && x.Contains (letters[0])
                                                       && x.All (c => letters.Contains (c)))
                                          .Select(word => (word,Score(word)))
                                          .OrderByDescending(x => x.Item2)];
int totalScore = 0;
foreach (var (word, score) in validWords) {
   if (IsPangram (word)) ForegroundColor = ConsoleColor.Green;
   WriteLine ($"{score,3}. {word}");
   ResetColor ();
   totalScore += score;
}
WriteLine ($"----\n{totalScore} total");

// Returns the score of each word
int Score (string word) {
   int length = word.Length, score = length is 4 ? 1 : (IsPangram (word) ? length + 7 : length);
   return score;
}

// Gets the array of characters from the user input
void GetInput (out char[] arr) {
   while (true) {
      string input = ReadLine ()?.ToUpper ().Trim () ?? "";
      if (!string.IsNullOrEmpty (input) && input.All (char.IsLetter) && input.Length is 7) {
         arr = input.ToCharArray (); return;
      }
      Write ("Invalid Input. Enter only seven letters: ");
   }
}

// Checks if the input word is a pangram
bool IsPangram (string word) => letters.All (c => word.Contains (c));