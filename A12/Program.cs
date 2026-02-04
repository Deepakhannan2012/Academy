// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to create a New - York Times style Wordle game
// ------------------------------------------------------------------------------------------------
using static System.Console;
using static System.ConsoleColor;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      OutputEncoding = System.Text.Encoding.UTF8;
      Wordle wordle = new ();
      wordle.Run ();
   }
}
#endregion

#region class Wordle ------------------------------------------------------------------------------
class Wordle () {

   #region Methods --------------------------------------------------
   // Executes the wordle program
   public void Run () {
      CursorVisible = false;
      InitialiseArrays ();
      SelectWord ();
      DisplayBoard ();
      while (!sGameOver) {
         ConsoleKeyInfo key = ReadKey (true);
         UpdateGameState (key);
         DisplayBoard ();
      }
      PrintResult ();
   }
   #endregion

   #region Implementation -------------------------------------------
   // Displays the board with the input columns and the alphabets
   void DisplayBoard () {
      CursorTop = 0;
      if (sCol == 0) sWordArray[sRow][sCol].Item1 = sCircle;
      foreach (var row in sWordArray) {
         CursorLeft = 53;
         foreach (var (letter, color) in row) {
            ForegroundColor = color;
            Write ($"{letter}  ");
            ResetColor ();
         }
         WriteLine ("\n");
      }
      CursorLeft = 45;
      PrintLine ();
      CursorLeft = 45;
      foreach (var (letter, color) in sAllLetters) {
         ForegroundColor = color;
         Write ($"{letter}    ");
         ResetColor ();
         // Displays 7 letters of the alphabet in each row
         if ((letter - 'A' + 1) % 7 == 0 || letter == 'Z') { WriteLine (); CursorLeft = 45; }
      }
      PrintLine ();
      CursorLeft = 50;
      if (!sIsWord) {
         ForegroundColor = Yellow;
         WriteLine ($"{sWord} is not a word");
         ResetColor ();
         sIsWord = true;
      }
      // The empty space replaces the already print error message if it exists
      else WriteLine ("                   ");

      // Prints a horizontal line
      void PrintLine () => WriteLine ("───────────────────────────────");
   }

   // Initialises the letter dictionary and the jagged array with default values
   void InitialiseArrays () {
      for (int i = 0; i < sWordArray.Length; i++)
         sWordArray[i] = new (char, ConsoleColor)[5];
      for (int i = 0; i < sWordArray.Length; i++)
         for (int j = 0; j < sWordArray[i].Length; j++)
            sWordArray[i][j] = (sDot, ForegroundColor);
      for (int i = 0; i < 26; i++)
         sAllLetters[(char)('A' + i)] = ForegroundColor;
   }

   // Prints the final results
   void PrintResult () {
      string result;
      (result, ForegroundColor, CursorLeft) = (sWord == sSecretWord)
                                            ? ($"You found the word in {++sRow} tries", Green, 45)
                                            : ($"Sorry - the word was {sSecretWord}", Yellow, 47);
      WriteLine (result);
      ResetColor ();
      CursorLeft = 49;
      WriteLine ("Press any key to quit");
   }

   // Selects a 5 letter word at random from the given list
   void SelectWord () {
      var words = File.ReadAllLines ("puzzle-5.txt");
      Random random = new ();
      sSecretWord = words[random.Next (words.Length)];
   }

   // Updates the inputs with its corresponding color
   void UpdateGameState (ConsoleKeyInfo key) {
      var currentCol = sWordArray[sRow];
      if (key.Key is ConsoleKey.Backspace or ConsoleKey.LeftArrow && sCol != 0) {
         // Replaces the last letter with a dot and moves the circle one step back
         currentCol[--sCol].Item1 = sDot;
         currentCol[sCol].Item1 = sCircle;
         if (sCol != 4) currentCol[sCol + 1].Item1 = sDot;
         return;
      }
      if (sCol != 5) {
         var letter = char.ToUpper (key.KeyChar);
         if (!char.IsLetter (letter)) return;
         currentCol[sCol++].Item1 = letter;
         if (sCol != 5) currentCol[sCol].Item1 = sCircle;
      }
      if (key.Key == ConsoleKey.Enter) {
         // Creates a string from the input letters and checks if it is a valid word
         sWord = new string ([.. currentCol.Select (tuple => tuple.Item1)]).ToUpper ();
         if (!sAllWords.Contains (sWord)) {
            sIsWord = false; return;
         }
         // Allocates the colors for each letter according to their position
         for (int i = 0; i < currentCol.Length; i++) {
            var letter = currentCol[i].Item1;
            sAllLetters[letter] = true switch {
               _ when !sSecretWord.Contains (letter) => DarkGray,
               _ when i == sSecretWord.IndexOf (letter) => Green,
               _ => Blue,
            };
            currentCol[i].Item2 = sAllLetters[letter];
         }
         // The game ends when either the secret word is guessed or all 6 tries are used
         if (sWord == sSecretWord || sRow == 5) { sGameOver = true; return; }
         if (sCol == 5) { sRow++; sCol = 0; }
      }
   }
   #endregion

   #region Fields ---------------------------------------------------
   // Bool that indicates if the game is over
   // and if it is a valid word respectively
   bool sGameOver, sIsWord = true;
   // Strings that stores the secret word and input of each attempt respectively
   string sSecretWord = "", sWord = "";
   // Stores the default dot and circle character
   char sDot = '\u00b7', sCircle = '\u25cc';
   // Stores the letter and color of each input
   (char, ConsoleColor)[][] sWordArray = new (char, ConsoleColor)[6][];
   // Stores every alphabet and its color
   Dictionary<char, ConsoleColor> sAllLetters = [];
   // Denotes the index of each input letter
   int sCol = 0, sRow = 0;
   // Stores all five letter words
   string[] sAllWords = File.ReadAllLines ("dict-5.txt");
   #endregion
}
#endregion