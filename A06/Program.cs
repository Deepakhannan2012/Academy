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
      PrintAllSoln (sSolutions);
   }

   #region Implementation -------------------------------------------
   // Finds all the unique solutions and stores them in an array
   static void QueenPos (int row) {
      if (row is 8) {
         int[] solution = (int[])sTempSoln.Clone ();
         if (PrevCheck (solution)) sSolutions[sCount++] = solution;
         return;
      }
      for (int i = 0; i < sFreeColumns.Count; i++) {
         int column = sFreeColumns[i];
         if (IsValidPos (row, column)) {
            sTempSoln[row] = column;
            sFreeColumns.RemoveAt (i);
            QueenPos (row + 1);
            sFreeColumns.Insert (i, column);
         }
      }
   }

   // Checks if the input position is a valid position
   static bool IsValidPos (int row, int column) {
      int prevColumn;
      for (int prevRow = 0; prevRow < row; prevRow++) {
         prevColumn = sTempSoln[prevRow];
         if (Math.Abs (prevRow - row) == Math.Abs (prevColumn - column))
            return false;
      }
      return true;
   }

   // Checks if the found solution already exists in other forms
   static bool PrevCheck (int[] sol) {
      for (int i = 0; i < sCount; i++)
         foreach (var check in Tests (sol)) if (check.SequenceEqual (sSolutions[i])) return false;
      return true;

      // Returns the various transformations of an arr to check duplicates
      static int[][] Tests (int[] arr) {
         // Rotated arrays by 90, 180 and 270 degrees
         int[] arr90 = Rotate (arr, 1), arr180 = Rotate (arr, 2), arr270 = Rotate (arr, 3);
         // Mirror along the horizontal axis is same as vertical mirror rotated 180 degrees
         return [arr90, arr180, arr270, Mirror (arr),
                 Mirror (arr90), Mirror (arr180), Mirror (arr270)];

         // Rotates the given array by 90 degrees clockwise by given number of times
         static int[] Rotate (int[] arr, int count) {
            int[] rotArr = (int[])arr.Clone ();
            for (int i = 0; i < count; i++) {
               int[] tempArr = new int[8];
               for (int row = 0; row < 8; row++) tempArr[rotArr[row]] = 7 - row;
               rotArr = tempArr;
            }
            return rotArr;
         }

         // Mirrors the given array along the vertical axis
         static int[] Mirror (int[] arr) {
            int[] mirror = new int[8];
            for (int r = 0; r < 8; r++) mirror[r] = 7 - arr[r];
            return mirror;
         }
      }
   }

   // Prints all the solution in a chess board
   static void PrintAllSoln (int[][] sol) {
      for (int i = 0; i < sCount; i++) {
         WriteLine ($"Solution {i + 1}:");
         PrintSoln (sol[i]);
         WriteLine ();
      }

      // Prints one of the solution in a chess board
      static void PrintSoln (int[] sol) {
         WriteLine ("┌───┬───┬───┬───┬───┬───┬───┬───┐");
         for (int r = 0; r < 8; r++) {
            for (int i = 0; i < 8; i++) Write ($"│ {((i == sol[r]) ? '♛' : ' ')} ");
            WriteLine ($"│\n{(r is 7
                              ? "└───┴───┴───┴───┴───┴───┴───┴───┘"
                              : "├───┼───┼───┼───┼───┼───┼───┼───┤")}");
         }
      }
   }
   #endregion

   #region Fields ---------------------------------------------------
   static int[] sTempSoln = new int[8];
   static int[][] sSolutions = new int[12][];
   static int sCount = 0;
   static List<int> sFreeColumns = [0, 1, 2, 3, 4, 5, 6, 7];
   #endregion
}
#endregion