// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// To find 12 solutions for 8 Queens Problem
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

namespace A06;

class Program {
   const int N = 8;
   static void Main () {
      OutputEncoding = new UnicodeEncoding ();
      var fundamental = GetFundamentalSolutions ();
      WriteLine ($"Total fundamental solutions = {fundamental.Count}\n");
      int index = 1;
      foreach (var sol in fundamental) {
         WriteLine ($"Solution {index++}:");
         PrintBoard (sol);
         WriteLine ();
      }
   }

   // Get all fundamental solutions by eliminating symmetric solutions
   static List<int[]> GetFundamentalSolutions () {
      List<int[]> all = [];
      int[] cols = new int[N];
      Solve (0, cols, all);
      List<int[]> unique = [];
      foreach (var sol in all)
         if (!IsSymmetricOfExisting (sol, unique)) unique.Add (sol);
      return unique;
   }

   // Backtracking to find all solutions
   static void Solve (int row, int[] cols, List<int[]> solutions) {
      if (row == N) {
         int[] s = new int[N];
         Array.Copy (cols, s, N);
         solutions.Add (s);
         return;
      }
      for (int col = 0; col < N; col++) {
         if (IsSafe (cols, row, col)) {
            cols[row] = col;
            Solve (row + 1, cols, solutions);
         }
      }
   }

   // Check if placing a queen at (row, col) is safe
   static bool IsSafe (int[] cols, int row, int col) {
      for (int r = 0; r < row; r++) {
         if (cols[r] == col) return false;
         if (Math.Abs (cols[r] - col) == Math.Abs (r - row)) return false;
      }
      return true;
   }

   // Check if the solution is symmetric to any existing fundamental solution
   static bool IsSymmetricOfExisting (int[] sol, List<int[]> fundamentals) {
      foreach (var f in fundamentals) {
         foreach (var sym in GenerateSymmetries (f))
            if (AreSame (sol, sym)) return true;
      }
      return false;
   }

   // Generate all 8 symmetries of a solution
   static IEnumerable<int[]> GenerateSymmetries (int[] sol) {
      int[] r90 = Rotate90 (sol);
      int[] r180 = Rotate90 (r90);
      int[] r270 = Rotate90 (r180);
      int[] refl = Reflect (sol);
      int[] refl90 = Rotate90 (refl);
      int[] refl180 = Rotate90 (refl90);
      int[] refl270 = Rotate90 (refl180);
      return [sol, r90, r180, r270, refl, refl90, refl180, refl270];
   }

   // Rotate the board 90 degrees clockwise
   static int[] Rotate90 (int[] sol) {
      int[] res = new int[N];
      for (int r = 0; r < N; r++) res[sol[r]] = (N - 1 - r);
      return res;
   }

   // Reflect the board horizontally
   static int[] Reflect (int[] sol) {
      int[] res = new int[N];
      for (int r = 0; r < N; r++) res[r] = (N - 1 - sol[r]);
      return res;
   }

   // Check if two solutions are the same
   static bool AreSame (int[] a, int[] b) {
      for (int i = 0; i < N; i++)
         if (a[i] != b[i]) return false;
      return true;
   }

   // Print the board with queens
   static void PrintBoard (int[] sol) {
      char queen = '\u265B';
      char empty = ' ';
      WriteLine ("┌────┬────┬────┬────┬────┬────┬────┬────┐");
      for (int r = 0; r < N; r++) {
         Write ("│");
         for (int c = 0; c < N; c++) {
            char cell = sol[r] == c ? queen : empty;
            Write ($" {cell}  │");
         }
         WriteLine ();
         if (r < 7) WriteLine ("├────┼────┼────┼────┼────┼────┼────┼────┤");
      }
      WriteLine ("└────┴────┴────┴────┴────┴────┴────┴────┘");
   }
}
