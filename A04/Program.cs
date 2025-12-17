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
      var allWords = File.ReadAllLines ("words.txt");
      var mostCommonChars = allWords.SelectMany (w => w)
                                    .GroupBy (c => c)
                                    .OrderByDescending (g => g.Count ())
                                    .Take (7);
      foreach (var g in mostCommonChars) Console.WriteLine ($"{g.Key}-{g.Count ()}");
   }
}
