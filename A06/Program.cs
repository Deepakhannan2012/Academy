// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find the 12 unique solutions for 8 queens
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      OutputEncoding = System.Text.Encoding.UTF8;
      // Initially the queen is placed at the 0th row and 0th column
      QueenPos (0);
      PrintAllSoln (sUniqueSolns);
   }

   #region Implementation -------------------------------------------
   // Checks if the found solution already exists
   static bool IsUniqueSoln (int[] sol) {
      var alterSoln = TransformSolns (sol);
      for (int i = 0; i < sCount; i++) {
         var existSoln = sUniqueSolns[i];
         foreach (var soln in alterSoln) if (soln.SequenceEqual (existSoln)) return false;
      }
      return true;

      // Returns the various transformations of an arr to check duplicates
      static int[][] TransformSolns (int[] arr) {
         // Rotated arrays by 90°, 180° and 270°
         int[] arr90 = Rotate (arr, 1), arr180 = Rotate (arr, 2), arr270 = Rotate (arr, 3);
         // Vertical mirror is equivalent with horizontal mirrors when they have a
         // difference of 180°
         // Example: vertical at 0° is same as horizontal at 180°, vertical at 90° is same
         // as horizontal at 270°
         return [arr90, arr180, arr270, Mirror (arr),
                 Mirror (arr90), Mirror (arr180), Mirror (arr270)];

         // Mirrors the given array along the vertical axis
         static int[] Mirror (int[] arr) {
            int[] mirror = new int[MAXROW];
            for (int row = 0; row < MAXROW; row++) mirror[row] = MAXROW - 1 - arr[row];
            return mirror;
         }

         // Rotates the given array by 90° clockwise by given number of times
         static int[] Rotate (int[] arr, int count) {
            int[] tempArr = new int[MAXROW];
            for (int i = 0; i < count; i++) {
               if (i is not 0) Array.Clear (tempArr);
               for (int row = 0; row < MAXROW; row++) tempArr[arr[row]] = MAXROW - 1 - row;
               arr = (int[])tempArr.Clone ();
            }
            return arr;
         }
      }
   }

   // Checks if the column position is valid
   static bool IsValidPos (int row, int column) {
      for (int prevRow = 0; prevRow < row; prevRow++) {
         // The previous queen is considered as the origin for current queen position
         // to check the diagonal
         if (Math.Abs (prevRow - row) == Math.Abs (sTempSoln[prevRow] - column))
            return false;
      }
      return true;
   }

   // Prints all the solutions in a chess board
   static void PrintAllSoln (int[][] sol) {
      WriteLine ("12 unique solutions for the 8 queens problem:\n");
      for (int i = 0; i < sCount; i++) {
         WriteLine ($"Solution {i + 1}:");
         PrintSoln (sol[i]);
      }

      // Prints one of the solution in a chess board
      static void PrintSoln (int[] sol) {
         WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
         for (int row = 0; row < MAXROW; row++) {
            for (int col = 0; col < MAXROW; col++) Write ($"│ {((col == sol[row]) ? '♛' : ' ')} ");
            WriteLine ($"│\n{(row is (MAXROW - 1) ? "└───┴───┴───┴───┴───┴───┴───┴───┘\n"
                                                  : "├───┼───┼───┼───┼───┼───┼───┼───┤")}");
         }
      }
   }

   // Finds all the unique solutions and stores them in an array
   static void QueenPos (int row) {
      if (row is MAXROW) {
         int[] solution = (int[])sTempSoln.Clone ();
         if (IsUniqueSoln (solution)) sUniqueSolns[sCount++] = solution;
         return;
      }
      int column, availCount = sAvailableColPos.Count;
      for (int i = 0; i < availCount; i++) {
         column = sAvailableColPos[i];
         if (IsValidPos (row, column)) {
            sTempSoln[row] = column;
            sAvailableColPos.RemoveAt (i);
            QueenPos (row + 1);
            sAvailableColPos.Insert (i, column);
         }
      }
   }
   #endregion

   #region Fields ---------------------------------------------------
   // Stores the available column positions in a solution
   static List<int> sAvailableColPos = [0, 1, 2, 3, 4, 5, 6, 7];
   // Counts the number of unique solutions found
   static int sCount = 0;
   // Stores the temporary solution until a unique solution is found
   static int[] sTempSoln = new int[MAXROW];
   // 12 indicates the unique solutions that are found for 8 queens
   static int[][] sUniqueSolns = new int[12][];
   // 8 indicates the maximum rows in a chess board
   const int MAXROW = 8;
   #endregion
}
#endregion