// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all valid Spelling Bee words form the given dictionary
// ------------------------------------------------------------------------------------------------
using static System.Console;

char[] letters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
char required = letters[0];
string filePath = @"C:\Users\ramasamyin\Downloads\words.txt";
if (!File.Exists (filePath)) {
   Write ("Path is not valid");
   return;
}
var allWords = File.ReadAllLines (filePath).Select (w => w.Trim ().ToLower ()).Where (w => w.Length >= 4);
var validWords = new List<(string word, int score, bool isPangram)> ();
foreach (var word in allWords) {
   if (!word.Contains (char.ToLower (required))) continue;
   if (word.Any (c => !letters.Contains (char.ToUpper (c)))) continue;
   bool isPangram = letters.All (l => word.Contains (char.ToLower (l)));
   int score = (word.Length == 4) ? 1 : word.Length;
   if (isPangram) score += 7;
   validWords.Add ((word, score, isPangram));
}
var sorted = validWords.OrderByDescending (w => w.score).ThenBy (w => w.word).ToList ();
var total = sorted.Sum (v => v.score);
foreach (var (word, score, isPangram) in sorted) {
   string scoreText = score.ToString ().PadLeft (3);
   if (isPangram) {
      ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{scoreText}. {word}");
      ResetColor ();
   } else {
      WriteLine ($"{scoreText}. {word}");
   }
}
WriteLine ("\n---");
WriteLine ($"{total} total");



