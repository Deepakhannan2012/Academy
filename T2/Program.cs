// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find the password from the given set of instructions
// ------------------------------------------------------------------------------------------------
namespace T2;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      int currentPos = 50, finalPos, count = 0;
      var commands = File.ReadAllLines ("dial.txt").ToArray ();
      foreach (var command in commands) {
         int steps = int.Parse (command[1..]);
         if (command[0] == 'L') {
            finalPos = currentPos - steps;
            if (finalPos < 0) finalPos = 100 - (Math.Abs (finalPos) % 100);
         } else {
            finalPos = currentPos + steps;
            if (finalPos > 99) finalPos %= 100;
         }
         if (finalPos == 0) count++;
         currentPos = finalPos;
      }
      Console.WriteLine ($"The password is {count}");
   }
}
#endregion
