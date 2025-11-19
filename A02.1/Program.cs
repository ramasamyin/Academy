// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Guessing the number using MSB to LSB or LSB to MSB approach
// ------------------------------------------------------------------------------------------------
using System.Security.Cryptography;
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
      int guess = 0;
      int bitValue = 1;
      for (int i = 0; i < 7; i++) {
         ConsoleKey response;
         do {
            Write ($"\nDivide the number by {bitValue}. Is the quotient odd (O) or even (E): ");
            response = ReadKey ().Key;
         } while (response != ConsoleKey.O && response != ConsoleKey.E);
         if (response == ConsoleKey.O) guess += bitValue;
         bitValue <<= 1;
      }
      return guess;
   }
}