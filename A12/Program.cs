using System.Text;
using static System.Console;
using static System.ConsoleColor;

namespace A12;

class Program {
   const int ROWS = 6,
   COLS = 5;
   static char[,] grid = new char[ROWS, COLS];
   static ConsoleColor[,] colors = new ConsoleColor[ROWS, COLS];
   static int currentRow = 0,
   currentCol = 0;
   static string? secretWord;
   static HashSet<string>? dictionary;
   static Dictionary<char, ConsoleColor> keyboardState = [];
   static void Main () {
      OutputEncoding = Encoding.Unicode;
      CursorVisible = false;
      Clear ();
      LoadWords ();
      InitializeGame ();
      while (true) {
         DrawBoard ();
         HandleInput ();
      }
   }

   // ---------------- INITIALIZATION ----------------
   static void LoadWords () {
      var puzzleWords = File.ReadAllLines ("puzzle-5.txt")
                            .Select (w => w.Trim ().ToUpper ())
                            .ToList ();
      dictionary = [.. File.ReadAllLines ("dict-5.txt").Select (w => w.Trim ().ToUpper ())];
      secretWord = puzzleWords[new Random ().Next (puzzleWords.Count)];
   }

   static void InitializeGame () {
      for (int r = 0; r < ROWS; r++)
         for (int c = 0; c < COLS; c++) {
            grid[r, c] = '\u00b7';
            colors[r, c] = White;
         }
      for (char c = 'A'; c <= 'Z'; c++) keyboardState[c] = White;
   }

   // ---------------- INPUT ----------------
   static void HandleInput () {
      var key = ReadKey (true);
      if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.LeftArrow) {
         if (currentCol > 0) {
            currentCol--;
            grid[currentRow, currentCol] = '\u00b7';
         }
      }
      if (key.Key == ConsoleKey.Enter) {
         if (currentCol == COLS) SubmitWord ();
      }
      if (char.IsLetter (key.KeyChar) && currentCol < COLS) {
         grid[currentRow, currentCol] = char.ToUpper (key.KeyChar);
         currentCol++;
      }
   }

   // ---------------- GAME LOGIC ----------------
   static void SubmitWord () {
      string guess = "";
      for (int c = 0; c < COLS; c++) guess += grid[currentRow, c];
      if (!dictionary!.Contains (guess)) {
         WriteCentered ($"{guess} is not a word", Yellow);
         return;
      }
      EvaluateGuess (guess);
      if (guess == secretWord) EndGame (true);
      currentRow++;
      currentCol = 0;
      if (currentRow == ROWS) EndGame (false);
   }

   static void EvaluateGuess (string guess) {
      bool[] used = new bool[COLS];
      for (int i = 0; i < COLS; i++) {
         if (guess[i] == secretWord![i]) {
            colors[currentRow, i] = Green;
            keyboardState[guess[i]] = Green;
            used[i] = true;
         }
      }
      for (int i = 0; i < COLS; i++) {
         if (colors[currentRow, i] == Green) continue;
         if (secretWord!.Contains (guess[i])) {
            colors[currentRow, i] = Cyan;
            if (keyboardState[guess[i]] != Green) keyboardState[guess[i]] = Cyan;
         } else {
            colors[currentRow, i] = DarkGray;
            keyboardState[guess[i]] = DarkGray;
         }
      }
   }

   // ---------------- DISPLAY ----------------
   static void DrawBoard () {
      SetCursorPosition (0, 0);
      int keyboardWidth = "A B C D E F G".Length,
      keyboardLeft = (WindowWidth - keyboardWidth) / 2;
      for (int r = 0; r < ROWS; r++) {
         int rowLength = COLS * 2;
         int left = Math.Max (0, (WindowWidth - rowLength) / 2);
         SetCursorPosition (left, CursorTop);
         for (int c = 0; c < COLS; c++) {
            ForegroundColor =
                (r == currentRow && c == currentCol)
                ? White
                : colors[r, c];
            Write ((r == currentRow && c == currentCol) ? "\u25CC " : grid[r, c] + " ");
         }
         WriteLine ();
      }
      WriteLine ();
      DrawKeyboard (keyboardLeft);
      ResetColor ();
   }

   static void DrawKeyboard (int left) {
      string[] rows =
      [
            "ABCDEFG",
            "HIJKLMN",
            "OPQRSTU",
            "VWXYZ"
      ];
      foreach (var row in rows) {
         SetCursorPosition (left, CursorTop);
         foreach (char c in row) {
            ForegroundColor = keyboardState[c];
            Write (c + " ");
         }
         WriteLine ();
      }
   }

   static void WriteCentered (string text, ConsoleColor color) {
      int left = Math.Max (0, (WindowWidth - text.Length) / 2);
      SetCursorPosition (left, CursorTop + 1);
      ForegroundColor = color;
      WriteLine (text);
      ResetColor ();
   }

   static void EndGame (bool win) {
      Clear ();
      DrawBoard ();
      int tries = currentRow + (win ? 1 : 0);
      if (win) {
         WriteCentered (
             $"You found the word in {tries} tries",
             Green
         );
      } else {
         WriteCentered (
             $"Game over. Word was {secretWord}",
             Red
         );
      }
      WriteCentered (
          "Press any key to quit",
          White
      );
      ReadKey (true);
      Environment.Exit (0);
   }
}
