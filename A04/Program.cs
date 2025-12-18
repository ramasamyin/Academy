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
      Console.WriteLine ("Using LINQ");
      var freq1 = GetFreq1 (allWords);
      foreach (var g in freq1) Console.WriteLine ($"{g.Key}-{g.Count ()}");
      Console.WriteLine ("\nWithout using LINQ");
      var freq2 = GetFreq2 (allWords);
      int limit = Math.Min (7, freq2.Count);
      for (int i = 0; i < limit; i++) Console.WriteLine ($"{freq2[i].Key}-{freq2[i].Value}");
   }

   static IEnumerable<IGrouping<char, char>> GetFreq1 (string[] allWords) {
      var charFrequency = allWords.SelectMany (w => w)
                                    .GroupBy (c => c)
                                    .OrderByDescending (g => g.Count ())
                                    .Take (7);
      return charFrequency;
   }

   static List<KeyValuePair<char, int>> GetFreq2 (string[] allWords) {
      Dictionary<char, int> charFrequency = [];
      foreach (string word in allWords) {
         foreach (char c in word) {
            if (charFrequency.TryGetValue (c, out int value)) charFrequency[c] = ++value;
            else charFrequency[c] = 1;
         }
      }
      List<KeyValuePair<char, int>> freqList = [.. charFrequency];
      freqList.Sort ((a, b) => b.Value.CompareTo (a.Value));
      return freqList;
   }
}
