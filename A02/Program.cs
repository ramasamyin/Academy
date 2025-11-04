// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to guess a randomly generated number between 1 to 100.
// ------------------------------------------------------------------------------------------------
int n = new Random ().Next (1, 101);
int input;
Console.Write ("Guess the number (1-100): ");
while (!int.TryParse (Console.ReadLine (), out input)) Console.Write ("Enter a valid number: ");
while (true) {
   if (n == input) {
      Console.Write ("You are correct");
      break;
   }
   Console.WriteLine (input < n ? "Your input is Low" : "Your input is high");
   Console.Write ("Give another try: ");
   while (!int.TryParse (Console.ReadLine (), out input)) ;
}






