namespace A14._1;
class Program {
   static void Main () {
      var words = File.ReadAllLines ("words (1).txt");
      Dictionary<string, List<string>> groups = [];
      foreach (string txt in words) {
         int[] freq = new int[26];
         foreach (char c in txt.ToLower ()) {
            if (char.IsLetter (c)) freq[c - 'a']++;
         }
         string key = string.Join (',', freq);
         groups.TryAdd (key, []);
         groups[key].Add (txt);
      }
      Dictionary<int, List<List<string>>> result = [];
      foreach (var group in groups.Values) {
         int cnt = group.Count;
         if (cnt >= 2) {
            result.TryAdd (cnt, []);
            result[cnt].Add (group);
         }
      }
      foreach (var kvp in result.OrderByDescending (kvp => kvp.Key)) {
         foreach (var grp in kvp.Value) {
            Console.WriteLine ($"{kvp.Key} - {string.Join (" ", grp)}");
         }
      }
   }
}
