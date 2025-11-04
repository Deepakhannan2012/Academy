// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to guess a randomly generated number
// ------------------------------------------------------------------------------------------------
using static System.Console;

int guess, num = new Random ().Next (1, 101);
Write ("Guess the number between 1 and 100: ");
while (true) {
   while (!int.TryParse (ReadLine (), out guess)) Write ("Invalid input. Try again: ");
   if (guess == num) break;
   Write ($"Your guess is too {((guess > num) ? "high" : "low")}. Try again: ");
}
WriteLine ("You guessed correctly.");