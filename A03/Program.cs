// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all valid Spelling Bee words form the given dictionary
// ------------------------------------------------------------------------------------------------
using static System.Console;

char[] letters = { 'U', 'X', 'L', 'A', 'T', 'N', 'E' };
char required = letters[0];
var allWords = File.ReadAllLines ("words.txt").Select (w => w.Trim ()).Where (w => w.Length >= 4);
var validWords = new List<(string word, int score, bool isPangram)> ();
foreach (var word in allWords) {
   if (!IsValidWord (word, letters, required))
      continue;
   var (score, isPangram) = GetScoreAndPangram (word, letters);
   validWords.Add ((word, score, isPangram));
}
PrintResults (validWords);

// Checks whether the word is valid based on the given letters and required letter
static bool IsValidWord (string word, char[] letters, char required) {
   if (!word.Contains (required)) return false;
   return word.All (letters.Contains);
}

// Returns score and checks whether the word is a pangram
static (int score, bool isPangram) GetScoreAndPangram (string word, char[] letters) {
   bool isPangram = letters.All (word.Contains);
   int score = (word.Length == 4 ? 1 : word.Length) + (isPangram ? 7 : 0);
   return (score, isPangram);
}

// Prints the results to the console
static void PrintResults (List<(string word, int score, bool isPangram)> validWords) {
   int total = 0;
   foreach (var (word, score, isPangram) in validWords.OrderByDescending (w => w.score).ThenBy (w => w.word)) {
      if (isPangram) Console.ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{score,3}. {word}");
      if (isPangram) Console.ResetColor ();
      total += score;
   }
   WriteLine ("---");
   WriteLine ($"{total} total");
}
