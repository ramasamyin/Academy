// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all valid Spelling Bee words from the given dictionary
// ------------------------------------------------------------------------------------------------
using static System.Console;

char[] letters = { 'U', 'X', 'L', 'A', 'T', 'N', 'E' };
char required = letters[0];
var allWords = File.ReadAllLines ("words.txt");
var validWords = new List<(string word, int score, bool isPangram)> ();
foreach (var word in allWords) {
   if (!IsValidWord (word, letters, required)) continue;
   var (s, p) = GetScoreAndPangram (word);
   validWords.Add ((word, s, p));
}
PrintResults (validWords);

// Check valid word: length ≥ 4, must contain required letter, only allowed letters
static bool IsValidWord (string word, char[] letters, char required) => word.Length >= 4
                                                                        && word.Contains (required)
                                                                        && word.All (letters.Contains);

// Calculates score, checks if the word is a pangram and adds extra points if so
(int s, bool p) GetScoreAndPangram (string word) {
   bool isPangram = letters.All (word.Contains);
   int score = (word.Length == 4) ? 1 : word.Length + (isPangram ? 7 : 0);
   return (score, isPangram);
}

// Prints results
static void PrintResults (List<(string word, int score, bool isPangram)> words) {
   int total = 0;
   foreach (var (word, score, isPangram) in words.OrderByDescending (w => w.score).ThenBy (w => w.word)) {
      if (isPangram) ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{score,3}. {word}");
      ResetColor ();
      total += score;
   }
   WriteLine ("---");
   WriteLine ($"{total} total");
}
