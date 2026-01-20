// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a file parser
// ------------------------------------------------------------------------------------------------
using static A10._1.State;
using static System.Console;
namespace A10._1;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      while (true) {
         Write ("Enter the file path: ");
         var input = ReadLine ()?.Trim () ?? "";
         // The input "EXIT" stops the program
         if (input == "EXIT") break;
         try {
            (string drive, string folder, string file, string ext) = FileParser (input);
            WriteLine ($"\nDrive: {drive}\nFolder: {folder}\nFile: {file}\nExtension: {ext}\n");
         } catch (Exception ex) {
            WriteLine (ex.Message);
         }
      }
   }

   #region Implementation -------------------------------------------
   // Returns a tuple with four strings that contains the parts of the file path
   static (string, string, string, string) FileParser (string input) {
      string drive = "", file = "", ext = "", name;
      System.Text.StringBuilder temp = new (), folder = new ();
      Action none = () => { }, todo;
      var s = A;
      // Throws an error if lowercase letters are present
      if (input.Any (Char.IsLower)) Error ();
      foreach (var ch in input + '~') {
         if (Char.IsLetter (ch)) { temp.Append (ch); continue; }
         name = temp.ToString ();
         (s, todo) = (s, ch) switch {
            (A, ':') => (B, () => drive = name),
            (B, '\\') => (C, () => { if (temp.Length != 0) Error (); }),
            (C or D, '\\') => (D, () => folder.Append (name + '\\')),
            (D, '.') => (E, () => file = name),
            (E, '~') => (H, () => ext = '.' + name),
            _ => (Z, none)
         };
         todo ();
         temp.Clear ();
      }
      if (folder.Length > 0) folder.Length--;
      if (s == H) return (drive, folder.ToString (), file, ext);
      throw Error ();

      // Throws and returns an error when the file path is not valid
      Exception Error () => throw new Exception ("Invalid file path");
   }
   #endregion
}
#endregion

#region Enums -------------------------------------------------------
// Defines the states of the Mealy machine
enum State { A, B, C, D, E, F, G, H, Z }
#endregion
