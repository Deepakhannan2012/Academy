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
      OutputEncoding = Encoding.UTF8;
      QueenPos (0);
      PrintAllSoln (sUniqueSolns);
   }

   #region Implementation -------------------------------------------
   // Checks if the found solution already exists
   static bool DuplicateCheck (int[] sol) {
      var chkArr = Tests (sol);
      for (int i = 0; i < sCount; i++)
         foreach (var check in chkArr) if (check.SequenceEqual (sUniqueSolns[i])) return false;
      return true;

      // Returns the various transformations of an arr to check duplicates
      static int[][] Tests (int[] arr) {
         // Rotated arrays by 90, 180 and 270 degrees
         int[] arr90 = Rotate (arr, 1), arr180 = Rotate (arr, 2), arr270 = Rotate (arr, 3);
         // Vertical and horizontal mirrors are equivalent with a 180° phase shift.
         // Example: vertical at 0° is same as horizontal at 180°, vertical at 90° is same
         // as horizontal at 270°
         return [arr90, arr180, arr270, Mirror (arr),
                 Mirror (arr90), Mirror (arr180), Mirror (arr270)];

         // Mirrors the given array along the vertical axis
         static int[] Mirror (int[] arr) {
            int[] mirror = new int[8];
            for (int r = 0; r < 8; r++) mirror[r] = 7 - arr[r];
            return mirror;
         }

         // Rotates the given array by 90 degrees clockwise by given number of times
         static int[] Rotate (int[] arr, int count) {
            int[] tempArr = new int[8];
            for (int i = 0; i < count; i++) {
               Array.Clear (tempArr);
               for (int row = 0; row < 8; row++) tempArr[arr[row]] = 7 - row;
               arr = (int[])tempArr.Clone ();
            }
            return arr;
         }
      }
   }

   // Checks if the column position is valid
   static bool IsValidPos (int row, int column) {
      for (int prevRow = 0; prevRow < row; prevRow++) {
         // 2 positions lie on the same diagonal if the absolute difference of row numbers is
         // equal to the absolute difference of the column numbers
         if (Math.Abs (prevRow - row) == Math.Abs (sTempSoln[prevRow] - column))
            return false;
      }
      return true;
   }

   // Prints all the solution in a chess board
   static void PrintAllSoln (int[][] sol) {
      for (int i = 0; i < sCount; i++) {
         WriteLine ($"Solution {i + 1}:");
         PrintSoln (sol[i]);
      }

      // Prints one of the solution in a chess board
      static void PrintSoln (int[] sol) {
         WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
         for (int r = 0; r < 8; r++) {
            for (int i = 0; i < 8; i++) Write ($"│ {((i == sol[r]) ? '♛' : ' ')} ");
            WriteLine ($"│\n{(r is 7 ? "└───┴───┴───┴───┴───┴───┴───┴───┘\n"
                                     : "├───┼───┼───┼───┼───┼───┼───┼───┤")}");
         }
      }
   }

   // Finds all the unique solutions and stores them in an array
   static void QueenPos (int row) {
      if (row is mMaxRow) {
         int[] solution = (int[])sTempSoln.Clone ();
         if (DuplicateCheck (solution)) sUniqueSolns[sCount++] = solution;
         return;
      }
      int availCount = sAvailableColPos.Count;
      for (int i = 0, column; i < availCount; i++) {
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
   // Stores the available columns in a solution
   static List<int> sAvailableColPos = [0, 1, 2, 3, 4, 5, 6, 7];
   // Counts the number of unique solutions found
   static int sCount = 0;
   // Stores the constant value 8
   const int mMaxRow = 8;
   // Stores the temporary solution until a unique solution is found
   static int[] sTempSoln = new int[8];
   // Stores every unique solution found
   static int[][] sUniqueSolns = new int[12][];
   #endregion
}
#endregion