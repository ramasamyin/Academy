// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// To build a frequency table with occurrence of all the letters
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A04;

class Program {
   static void Main () {
      var words = File.ReadAllText ("words.txt");
      PrintFrequency ("Using LINQ", GetFreq1 (words));
      PrintFrequency ("\nWithout using LINQ", GetFreq2 (words));
   }

   // Using LINQ
   static IEnumerable<KeyValuePair<char, int>> GetFreq1 (string words) => words.Where (char.IsLetter)
                                                                                .GroupBy (c => c)
                                                                                .Select (g => new KeyValuePair<char, int> (g.Key, g.Count ()))
                                                                                .OrderByDescending (g => g.Value)
                                                                                .Take (7);

   // Without using LINQ
   static IEnumerable<KeyValuePair<char, int>> GetFreq2 (string words) {
      Dictionary<char, int> charFrequency = [];
      for (char c = 'A'; c <= 'Z'; c++) charFrequency[c] = 0;
      foreach (char ch in words) {
         if (ch >= 'A' && ch <= 'Z') charFrequency[ch]++;
      }
      return charFrequency.OrderByDescending (c => c.Value).Take (7);
   }

   static void PrintFrequency (string title, IEnumerable<KeyValuePair<char, int>> freq) {
      WriteLine (title);
      foreach (var (key, value) in freq) WriteLine ($"{key} - {value}");
   }
}
