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
      WriteLine ("Enter the number of queens: ");
      while (!int.TryParse (ReadLine (), out sQueenCount) || sQueenCount is < 1 or 2 or 3)
         Write ($"{((sQueenCount is < 1 or 2 or 3)
              ? $"Solution does not exists for n = {sQueenCount} " : "")}Enter a valid input: ");
   }

   // Returns the solution type from the user input
   static EType GetSolnType () {
      WriteLine ($"Enter the solution type for the {sQueenCount} queens problem:\n" +
                 $"-> (A)ll solutions\n-> (U)nique solutions");
      while (true)
         switch (ReadKey (true).Key) {
            case ConsoleKey.A: return EType.AllSolns;
            case ConsoleKey.U: return EType.UniqueSolns;
            default: WriteLine ("\nPlease enter only A or U: "); break;
         }
   }

   // Prints all the solutions in a chess board
   static void PrintAllSoln (List<int[]> sol) {
      OutputEncoding = System.Text.Encoding.UTF8;
      int count = sFinalSolns.Count;
      for (int i = 0; i < count; i++) {
         WriteLine ($"{((sSolnType == EType.AllSolns)
                   ? "All" : $"{count} unique")} solutions for the {sQueenCount} queens problem:" +
                    $"\n\nSolution {i + 1} of {count}:");
         PrintSoln (sol[i]);
         WriteLine ("Press any key to navigate to the next solution...");
         ReadKey ();
         Clear ();
      }

      // Prints one of the solution in a chess board
      static void PrintSoln (int[] sol) {
         string empLine = $"{string.Concat (Enumerable.Repeat ($"│       ", sQueenCount))}│";
         WriteLine (BoardRows ('┌', '┬', '┐'));
         for (int row = 0; row < sQueenCount; row++) {
            WriteLine (empLine);
            for (int col = 0; col < sQueenCount; col++)
               Write ($"│   {((col == sol[row]) ? '♛' : ' ')}   ");
            WriteLine ($"│\n{empLine}\n{(row == (sQueenCount - 1) ? BoardRows ('└', '┴', '┘')
                                                                  : BoardRows ('├', '┼', '┤'))}");
         }

         // Returns each rows of the chess board with appropriate symbols
         static string BoardRows (char left, char mid, char right)
            => $"{left}{string.Concat (Enumerable.Repeat ($"───────{mid}", sQueenCount - 1))}───────{right}";
      }
   }

   // Finds all the solutions and stores them in an list
   static void QueenPos (int row) {
      if (row == sQueenCount) {
         int[] solution = (int[])sTempSoln.Clone ();
         if (sSolnType == EType.AllSolns) sFinalSolns.Add (solution);
         else if (IsUniqueSoln (solution)) sFinalSolns.Add (solution);
         return;
      }
      for (int column = 0; column < sQueenCount; column++)
         if (IsValidPos (row, column)) {
            sTempSoln[row] = column;
            QueenPos (row + 1);
         }

      // Checks if the found solution already exists
      static bool IsUniqueSoln (int[] sol) {
         for (int i = 0; i < sFinalSolns.Count; i++) {
            var prevSoln = sFinalSolns[i];
            for (int rotateCount = 0; rotateCount < 4; rotateCount++) {
               var rotated = Rotate (sol, rotateCount);
               if (AreEqual (prevSoln, rotated) || AreEqual (prevSoln, Mirror (rotated)))
                  return false;
            }
         }
         return true;

         // Checks if two arrays are equal
         static bool AreEqual (int[] arr1, int[] arr2) => arr1.SequenceEqual (arr2);

         // Mirrors the given array along the vertical axis
         static int[] Mirror (int[] arr) {
            Array.Reverse (arr);
            return arr;
         }

         // Rotates the given array by 90° clockwise by given number of times
         static int[] Rotate (int[] arr, int count) {
            if (count == 0) return arr;
            int[] tempArr = new int[sQueenCount];
            for (int i = 0; i < count; i++) {
               if (i != 0) Array.Clear (tempArr);
               for (int row = 0; row < sQueenCount; row++) tempArr[arr[row]] = sQueenCount - 1 - row;
               arr = (int[])tempArr.Clone ();
            }
            return arr;
         }
      }

      // Checks if the column position is valid
      static bool IsValidPos (int row, int column) {
         for (int prevRow = 0; prevRow < row; prevRow++)
            // The previous queen is considered as the origin for current queen position
            // to check the diagonal and the column clash is also checked
            if (sTempSoln[prevRow] == column
                || (Math.Abs (prevRow - row) == Math.Abs (sTempSoln[prevRow] - column)))
               return false;
         return true;
      }
   }
   #endregion

   #region Enums ----------------------------------------------------
   enum EType { AllSolns, UniqueSolns }
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