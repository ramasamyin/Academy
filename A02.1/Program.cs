// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Guessing the number using MSB to LSB or LSB to MSB approach
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A02._1;

class Program {
   static void Main () {
      WriteLine ("Think of a number between 1 and 100. I'll guess it");
      ConsoleKey choice;
      do {
         Write ("\nPress 'M' for MSB To LSB mode, or 'L' for LSB to MSB mode: ");
         choice = ReadKey ().Key;
      } while (choice != ConsoleKey.M && choice != ConsoleKey.L);
      if (choice == ConsoleKey.M) WriteLine ($"\nYour number is : {GuessMSB ()}");
      else if (choice == ConsoleKey.L) WriteLine ($"\nYour number is : {GuessLsb ()}");
      else WriteLine ("Invalid option. Please restart the program and choose 'M' or 'L'.");
   }

   // Guessing the number using MSB to LSB approach
   static int GuessMSB () {
      int low = 1;
      int high = 127;
      while (low < high) {
         int mid = (low + high) / 2;
         ConsoleKey response;
         do {
            Write ($"\nIs your number greater than {mid}? (Y/N): ");
            response = ReadKey ().Key;
         } while (response != ConsoleKey.Y && response != ConsoleKey.N);
         if (response == ConsoleKey.Y) low = mid + 1;
         else high = mid;
      }
      return low;
   }

   // Guessing the number using LSB to MSB approach
   static int GuessLsb () {
      int answer = 0;
      for (int bit = 0; bit < 7; bit++) {
         int div = 1 << (bit + 1),
         expectedRemainder = answer % div;
         ConsoleKey response;
         do {
            Write ($"\nIf the number is divided by {div}, is the remainder {expectedRemainder}? (Y/N): ");
            response = ReadKey ().Key;
         }
         while (response != ConsoleKey.Y && response != ConsoleKey.N);
         if (response == ConsoleKey.N) answer |= 1 << bit;
      }
      return answer;
   }
}
