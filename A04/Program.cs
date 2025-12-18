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
      var words = File.ReadAllText ("words.txt");
      // Using LINQ
      var freq1 = GetFreq1 (words);
      Console.WriteLine ("Using LINQ");
      foreach (var g in freq1) Console.WriteLine ($"{g.Letter} - {g.Count}");
      // Without using LINQ
      var freq2 = GetFreq2 (words);
      Console.WriteLine ("\nWithout using LINQ");
      foreach (var g in freq2) Console.WriteLine ($"{g.Key} - {g.Value}");
   }

   static IEnumerable<(char Letter, int Count)> GetFreq1 (string words) => words.Where (char.IsLetter)
                                                                  .GroupBy (c => c)
                                                                  .Select (g => (Letter: g.Key, Count: g.Count ()))
                                                                  .OrderByDescending (g => g.Count)
                                                                  .Take (7);

   static IEnumerable<KeyValuePair<char, int>> GetFreq2 (string words) {
      Dictionary<char, int> charFrequency = [];
      foreach (char c in words) {
         if (!char.IsLetter (c)) continue;
         charFrequency[c] = charFrequency.TryGetValue (c, out int value) ? ++value : 1;
      }
      return charFrequency.OrderByDescending (c => c.Value).Take (7);
   }
}
