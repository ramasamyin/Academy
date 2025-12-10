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
var allWords = File.ReadAllLines ("words.txt").Select (w => w.Trim ());
var validWords = new List<(string word, int score, bool isPangram)> ();
foreach (var word in allWords) {
   if (!IsValidWord (word, letters, required))
      continue;
   var (score, isPangram) = GetScoreAndPangram (word, letters);
   validWords.Add ((word, score, isPangram));
}
PrintResults (validWords);

// Check valid word: length ≥ 4, must contain required letter, only allowed letters
static bool IsValidWord (string word, char[] letters, char required) {
   return word.Length >= 4
       && word.Contains (required)
       && word.All (letters.Contains);
}


// Score and Pangram check
static (int score, bool isPangram) GetScoreAndPangram (string word, char[] letters) {
   bool isPangram = letters.All (word.Contains);
   int baseScore = (word.Length == 4) ? 1 : word.Length + (isPangram ? 7 : 0);
   return (baseScore, isPangram);
}

// Print results
static void PrintResults (List<(string word, int score, bool isPangram)> words) {
   int total = 0;
   foreach (var (word, score, isPangram) in words.OrderByDescending (w => w.score).ThenBy (w => w.word)) {
      if (isPangram) ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{score,3}. {word}");
      if (isPangram) ResetColor ();
      total += score;
   }
   WriteLine ("---");
   WriteLine ($"{total} total");
}
