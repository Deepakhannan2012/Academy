// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to create a text editor
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

namespace T1;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      TextEditor ();
   }

   #region Implementation -------------------------------------------
   // Executes the text editor
   static void TextEditor () {
      StringBuilder sb = new ();
      Stack<(char op, string s)> undo = [];
      Stack<(char op, string s)> redo = [];
      while (true) {
         string input = ReadLine () ?? "";
         string[] temp = input.Split (' ');
         if (temp.Length > 1 && temp[1].Length > 1000) {
            WriteLine ("Text length should be <= 1000 characters !"); return;
         }
         switch (temp[0].ToLower ()) {
            case "add": Add (temp[1]); continue;
            case "delete": Delete (int.Parse (temp[1])); continue;
            case "show": Show (); continue;
            case "undo": Undo (); continue;
            case "redo": Redo (); continue;
            case "exit": break;
            default: WriteLine ("Not a valid operation !"); continue;
         }
         break;
      }

      // Adds the characters to the current text
      void Add (string s) {
         sb.Append (s);
         undo.Push (('+', s));
         redo.Clear ();
      }

      // Deletes the specified number of characters from the end
      void Delete (int n) {
         if (n > sb.Length) {
            WriteLine ("Delete count should be lower than the current text length !"); return;
         }
         string s = sb.ToString ().Substring (sb.Length - n, n);
         sb.Remove (sb.Length - n, n);
         undo.Push (('-', s));
         redo.Clear ();
      }

      // Prints the current text in the console window
      void Show () => WriteLine (sb);

      // Undo the previous operation
      void Undo () {
         var (op, s) = undo.Pop ();
         redo.Push ((op, s));
         if (op == '+') {
            int len = s.Length;
            sb.Remove (sb.Length - len, len);
         } else sb.Append (s);
      }

      // Redo the previously undone operation
      void Redo () {
         if (redo.Count == 0) {
            WriteLine ("NOTHING TO REDO !"); return;
         }
         var (op, s) = redo.Pop ();
         if (op == '+') sb.Append (s);
         else {
            int len = s.Length;
            sb.Remove (sb.Length - len, len);
         }
      }
   }
   #endregion
}
#endregion
