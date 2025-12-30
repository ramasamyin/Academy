// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all valid Spelling Bee words from the given dictionary
// ------------------------------------------------------------------------------------------------
using static System.Console;

char[] letters = { 'U', 'X', 'L', 'A', 'T', 'N', 'E' };
var allWords = File.ReadAllLines ("words.txt");
List<(string Word, int Score, bool IsPangram)> validWords = [];
foreach (var word in allWords) {
   if (!IsValidWord (word)) continue;
   var (s, p) = GetScoreAndPangram (word);
   validWords.Add ((word, s, p));
}
PrintResults (validWords);

// Check valid word: length ≥ 4, must contain required letter, only allowed letters
bool IsValidWord (string word) => word.Length >= 4
                               && word.Contains (letters[0])
                               && word.All (letters.Contains);

// Calculates score, checks if the word is a pangram and adds extra points if so
(int Score, bool IsPangram) GetScoreAndPangram (string word) {
   bool isPangram = letters.All (word.Contains);
   int score = (word.Length == 4) ? 1 : word.Length + (isPangram ? 7 : 0);
   return (score, isPangram);
}

// Prints results
void PrintResults (List<(string word, int score, bool isPangram)> words) {
   int total = 0;
   foreach (var (Word, Score, IsPangram) in words.OrderByDescending (w => w.score).ThenBy (w => w.word)) {
      if (IsPangram) ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{Score,3}. {Word}");
      ResetColor ();
      total += Score;
   }
   WriteLine ("----");
   WriteLine ($"{total} total");
}
