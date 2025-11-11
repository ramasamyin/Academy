// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to guess a randomly generated number between 1 to 100.
// ------------------------------------------------------------------------------------------------

using static System.Console;

int n = new Random ().Next (1, 101);
while (true) {
   Write ("Enter your guess: ");
   if (!int.TryParse (ReadLine (), out int input)) {
      WriteLine ("Invalid input! Please enter a valid number.");
      continue;
   }
   if (input == n) {
      WriteLine ("You are correct!");
      break;
   }
   WriteLine (input < n ? "Your guess is low." : "Your guess is high.");
}
