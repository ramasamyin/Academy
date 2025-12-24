// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to parse a string input into a double without using built-in parsing methods
// ------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;
using static System.Console;
namespace A07;

class Program {
   static void Main () {
      Dictionary<string, double?> tests = new () {
        // VALID
        { "123", 123 },
        { "-123.456", -123.456 },
        { "+0.001", 0.001 },
        { "3.14e2", 314 },
        { "-2.5E-3", -0.0025 },
        { "0", 0 },
        { ".5", 0.5 },
        { "5.", 5 },
        { "  +42.0  ", 42 },

        // INVALID
        { "", null },
        { "abc", null },
        { "1.2.3", null },
        { "--5", null },
        { "e10", null },
        { "5e", null },
        { "+-", null }
      };

      // To compare floating point numbers
      const double EPS = 1e-6;

      foreach (var (input, expected) in tests) {
         double actual = ParseDouble (input);
         bool match = (expected == null && double.IsNaN (actual)) ||
                      (expected != null && Math.Abs (actual - expected.Value) < EPS);
         WriteLine ($"Input: '{input}' | " +
                     $"Expected: {(expected ?? double.NaN)} | " +
                     $"Actual: {actual} | " +
                     $"PASS: {match}"
         );
      }
   }

   public static double ParseDouble (string input) {
      input = input.Trim ();
      if (input.Length == 0) return double.NaN;
      int i = 0, sign = 1, length = input.Length;

      // Regex validation
      string pattern = @"^[+-]?(\d+(\.\d*)?|\.\d+)([eE][+-]?\d+)?$";
      if (!Regex.IsMatch (input, pattern)) return double.NaN;

      // Handle optional sign
      if (input[i] == '-' || input[i] == '+') {
         sign = (input[i++] == '-') ? -1 : 1;
      }
      double value = 0;

      // Integer part
      while (i < length && char.IsDigit (input[i])) value = value * 10 + (input[i++] - '0');

      // Fractional part
      if (i < length && input[i] == '.') {
         i++;
         double factor = 0.1;
         while (i < length && char.IsDigit (input[i])) {
            value += (input[i++] - '0') * factor;
            factor *= 0.1;
         }
      }

      // Exponent part
      if (i < length && (input[i] == 'e' || input[i] == 'E')) {
         i++;
         int expSign = 1;
         if (i < length && (input[i] == '-' || input[i] == '+')) {
            expSign = (input[i++] == '-') ? -1 : 1;
         }
         int exponent = 0;
         while (i < length && char.IsDigit (input[i])) {
            exponent = exponent * 10 + (input[i++] - '0');
         }
         value *= Math.Pow (10, expSign * exponent);
      }
      return sign * value;
   }
}
