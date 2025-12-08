// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find the 8 Queens solution
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      OutputEncoding = new UnicodeEncoding ();
      QueenPos (0);
      PrintAllSoln (solutions);
   }

   #region Implementation -------------------------------------------
   // Finds all the unique solutions and stores them in an array
   static void QueenPos (int row) {
      if (row == 8) {
         int[] solution = (int[])tempSoln.Clone ();
         if (PrevCheck (solution)) solutions[count++] = solution;
         return;
      }
      for (int column = 0; column < 8; column++) {
         if (isValid (row, column)) {
            tempSoln[row] = column;
            QueenPos (row + 1);
         }
      }
   }

   // Checks if the input position is a valid position
   static bool isValid (int row, int column) {
      for (int prevRow = 0; prevRow < row; prevRow++) {
         int prevColumn = tempSoln[prevRow];
         if (prevColumn == column || Math.Abs (prevRow - row) == Math.Abs (prevColumn - column))
            return false;
      }
      return true;
   }

   // Rotates the given array by 90 degrees clockwise
   static int[] Rotate90 (int[] arr) {
      int[] array90 = new int[8];
      for (int row = 0; row < 8; row++) array90[arr[row]] = 7 - row;
      return array90;
   }

   // Rotates the given array by 180 degrees clockwise
   static int[] Rotate180 (int[] arr) => Rotate90 (Rotate90 (arr));

   // Rotates the given array by 270 degrees clockwise
   static int[] Rotate270 (int[] arr) => Rotate90 (Rotate180 (arr));

   // Mirrors the given array along the horizontal axis
   static int[] Mirror (int[] arr) {
      int[] mirror = new int[8];
      for (int r = 0; r < 8; r++) mirror[r] = 7 - arr[r];
      return mirror;
   }

   // Checks if given 2 arrays are duplicates of each other
   static bool IsDuplicate (int[] arr1, int[] arr2) {
      for (int i = 0; i < 8; i++) if (arr1[i] != arr2[i]) return false;
      return true;
   }

   // Checks if the found solution already exists in other forms
   static bool PrevCheck (int[] sol) {
      Func<int[], int[]>[] tests = [ Rotate90, Rotate180, Rotate270, Mirror,
                                     arr => Mirror (Rotate90 (arr)),
                                     arr => Mirror (Rotate180 (arr)),
                                     arr => Mirror (Rotate270 (arr)) ];
      for (int i = 0; i < count; i++) {
         var c = solutions[i];
         foreach (var check in tests) if (IsDuplicate (check (sol), c)) return false;
      }
      return true;
   }

   // Prints the solution in a chess board
   static void PrintAllSoln (int[][] sol) {
      for (int i = 0; i < count; i++) {
         WriteLine ($"Solution {i + 1}:");
         PrintSoln (sol[i]);
         WriteLine ();
      }

      static void PrintSoln (int[] sol) {
         string empLine = "│       │       │       │       │       │       │       │       │";
         WriteLine ("┌───────┬───────┬───────┬───────┬───────┬───────┬───────┬───────┐");
         for (int r = 0; r < 8; r++) {
            WriteLine (empLine);
            for (int i = 0; i < sol[r]; i++) Write ("│       ");
            Write ($"│   ♛   ");
            for (int i = 0; i < 7 - sol[r]; i++) Write ("│       ");
            WriteLine ($"│\n{empLine}");
            WriteLine (r is 7
                       ? "└───────┴───────┴───────┴───────┴───────┴───────┴───────┴───────┘"
                       : "├───────┼───────┼───────┼───────┼───────┼───────┼───────┼───────┤");
         }
      }
   }
   #endregion

   #region Fields ---------------------------------------------------
   static int[] tempSoln = new int[8];
   static int[][] solutions = new int[12][];
   static int count = 0;
   #endregion
}
#endregion