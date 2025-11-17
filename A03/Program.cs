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
List<(string, bool, int)> validWords = [.. words.Where (x => x.Length is > 3
                                                             && x.Contains (letters[0])
                                                             && x.All (c => letters.Contains (c)))
                                          .Select(word => {
                                             bool pangram = letters.All (c => word.Contains (c));
                                             return (word, pangram, Score (word, pangram));})
                                          .OrderByDescending(x => x.Item3)];
int totalScore = 0;
foreach (var (word, pangram, score) in validWords) {
   if (pangram) ForegroundColor = ConsoleColor.Green;
   WriteLine ($"{score,3}. {word}");
   ResetColor ();
   totalScore += score;
}
WriteLine ($"----\n{totalScore} total");

// Returns the score of each word
int Score (string word, bool pangram) {
   int length = word.Length, score = length is 4 ? 1 : (pangram ? length + 7 : length);
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