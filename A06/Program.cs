// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find the solutions N queens problem
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      GetNValue ();
      sSolnType = GetSolnType ();
      sTempSoln = new int[sQueenCount];
      // Initially the queen is placed at the 0th row and 0th column
      QueenPos (0);
      PrintAllSoln (sFinalSolns);
   }

   #region Implementation -------------------------------------------
   // Returns the number of queens for the problem
   static void GetNValue () {
      Write ("Enter the number of queens: ");
      while (!int.TryParse (ReadLine (), out sQueenCount) || sQueenCount < 1)
         Write ("Enter a valid input: ");
   }

   // Returns the solution type from the user input
   static EType GetSolnType () {
      WriteLine ($"Enter the solution type for the {sQueenCount} queens problem:\n" +
                 $"-> (A)ll solutions\n-> (U)nique solutions");
      while (true)
         switch (ReadKey (true).Key) {
            case ConsoleKey.A: return EType.AllSoln;
            case ConsoleKey.U: return EType.UniqueSoln;
            default: WriteLine ("\nPlease enter only A or U: "); break;
         }
   }

   // Checks if the found solution already exists
   static bool IsUniqueSoln (int[] sol) {
      for (int i = 0; i < sFinalSolns.Count; i++) {
         (var prevSoln, var rotated) = (sFinalSolns[i], sol);
         for (int rotateCount = 0; rotateCount < 4; rotateCount++) {
            rotated = Rotate (rotated);
            if (AreEqual (prevSoln, rotated) || AreEqual (prevSoln, Mirror (rotated)))
               return false;
         }
      }
      return true;

      // Checks if two arrays are equal
      static bool AreEqual (int[] arr1, int[] arr2) => arr1.SequenceEqual (arr2);

      // Mirrors the given array along the vertical axis
      static int[] Mirror (int[] arr) => [.. arr.Reverse ()];

      // Rotates the given array by 90° clockwise by given number of times
      static int[] Rotate (int[] arr) {
         int[] rotArr = new int[sQueenCount];
         for (int i = 0; i < sQueenCount; i++) rotArr[arr[i]] = sQueenCount - 1 - i;
         return rotArr;
      }
   }

   // Prints all the solutions in a chess board
   static void PrintAllSoln (List<int[]> sol) {
      OutputEncoding = System.Text.Encoding.UTF8;
      int count = sFinalSolns.Count;
      if (count == 0) {
         WriteLine ($"No solution exists for n = {sQueenCount}");
         return;
      }
      int i = 0;
      while (i < count) {
         WriteLine ($"{((sSolnType == EType.AllSoln)
                   ? "All" : $"{count} unique")} solutions for the {sQueenCount} queens problem:" +
                    $"\n\nSolution {i + 1} of {count}:");
         PrintSoln (sol[i]);
         WriteLine ("\n→ Next | ← Previous | Esc Exit");
         i = ReadKey (true).Key switch {
            ConsoleKey.RightArrow when i < count - 1 => i + 1,
            ConsoleKey.LeftArrow when i > 0 => i - 1,
            ConsoleKey.Escape => -1,  // signal exit
            _ => i
         };
         if (i == -1) return;
         Clear ();
      }

      // Prints one of the solution in a chess board
      static void PrintSoln (int[] sol) {
         const char VLINE = '│';
         const string HLINES = "───────";
         string empLine = $"{string.Concat (Enumerable.Repeat ($"{VLINE}       ", sQueenCount))}{VLINE}";
         WriteLine (BoardRows ('┌', '┬', '┐'));
         for (int row = 0; row < sQueenCount; row++) {
            WriteLine (empLine);
            for (int col = 0; col < sQueenCount; col++)
               Write ($"{VLINE}   {((col == sol[row]) ? '♛' : ' ')}   ");
            WriteLine ($"{VLINE}\n{empLine}\n{(row == (sQueenCount - 1) ? BoardRows ('└', '┴', '┘')
                                                                  : BoardRows ('├', '┼', '┤'))}");
         }

         // Returns each rows of the chess board with appropriate symbols
         static string BoardRows (char left, char mid, char right)
            => $"{left}{string.Concat (Enumerable.Repeat ($"{HLINES}{mid}", sQueenCount - 1))}{HLINES}{right}";
      }
   }

   // Finds all the solutions and stores them in an list
   static void QueenPos (int row) {
      if (row == sQueenCount) {
         int[] solution = (int[])sTempSoln.Clone ();
         if (sSolnType == EType.AllSoln || IsUniqueSoln (solution)) sFinalSolns.Add (solution);
         return;
      }
      for (int column = 0; column < sQueenCount; column++)
         if (IsValidPos (row, column)) {
            sTempSoln[row] = column;
            QueenPos (row + 1);
         }

      // Checks if the column position is valid
      static bool IsValidPos (int row, int column) {
         for (int prevRow = 0; prevRow < row; prevRow++) {
            var prevCol = sTempSoln[prevRow];
            // The previous queen is considered as the origin for current queen position
            // to check the diagonal and the column clash is also checked
            if (prevCol == column || (Math.Abs (prevRow - row) == Math.Abs (prevCol - column)))
               return false;
         }
         return true;
      }
   }
   #endregion

   #region Enums ----------------------------------------------------
   // Defines the types of solutions
   enum EType { AllSoln, UniqueSoln }
   #endregion

   #region Fields ---------------------------------------------------
   // Stores the number of queens in the problem
   static int sQueenCount;
   // Stores the method of solution
   static EType sSolnType;
   // Stores the temporary solution until the complete solution is found
   static int[] sTempSoln = null!;
   // Stores the final solution
   static List<int[]> sFinalSolns = [];
   #endregion
}
#endregion