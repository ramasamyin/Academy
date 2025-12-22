// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to parse a string input into a double without using built-in parsing methods
// ------------------------------------------------------------------------------------------------
namespace A07;

class Program {
   static void Main () {
      Console.Write ("Enter an input: ");
      Console.WriteLine (ParseDouble (Console.ReadLine () ?? ""));
   }

   public static double ParseDouble (string input) {
      input = input.Trim ();
      if (input.Length == 0) Fatal ();
      int i = 0;
      int sign = 1;

      // Handle optional sign
      if (input[i] == '-' || input[i] == '+') {
         sign = (input[i] == '-') ? -1 : 1;
         i++;
      }
      double value = 0;
      bool hasDigits = false;

      // Integer part
      while (i < input.Length && char.IsDigit (input[i])) {
         value = value * 10 + (input[i] - '0');
         i++;
         hasDigits = true;
      }

      // Fractional part
      if (i < input.Length && input[i] == '.') {
         i++;
         double factor = 0.1;
         while (i < input.Length && char.IsDigit (input[i])) {
            value += (input[i] - '0') * factor;
            factor *= 0.1;
            i++;
            hasDigits = true;
         }
      }

      // Ensure at least one digit was found
      if (!hasDigits) Fatal ();

      // Exponent part
      if (i < input.Length && (input[i] == 'e' || input[i] == 'E')) {
         i++;
         int expSign = 1;
         if (i < input.Length && (input[i] == '-' || input[i] == '+')) {
            expSign = (input[i] == '-') ? -1 : 1;
            i++;
         }
         if (i == input.Length || !char.IsDigit (input[i])) Fatal ();
         int exponent = 0;
         while (i < input.Length && char.IsDigit (input[i])) {
            exponent = exponent * 10 + (input[i] - '0');
            i++;
         }
         value *= Math.Pow (10, expSign * exponent);
      }

      // Reject any extra characters at the end
      if (i != input.Length) Fatal ();
      return sign * value;

      static void Fatal () => throw new Exception ("Not a valid double");
   }
}
