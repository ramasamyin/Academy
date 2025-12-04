// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// To build a frequency table with occurrence of all the letters
// ------------------------------------------------------------------------------------------------
namespace A04;

class Program {
   static void Main () {
      var allWords = File.ReadAllLines ("words.txt").Select (w => w.Trim ().ToLower ()).Where (w => w.Length >= 4);
      var countDict = allWords.SelectMany (w => w).GroupBy (c => c).ToDictionary (g => g.Key, g => g.Count ());
      var mostCommonChars = countDict.OrderByDescending (kvp => kvp.Value).Take (7).Select (kvp => (kvp.Key, kvp.Value)).ToList ();
      foreach (var (chr, cnt) in mostCommonChars) Console.WriteLine ($"{chr}-{cnt}");
   }
}