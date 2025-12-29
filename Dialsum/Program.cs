string[] instructions = File.ReadAllLines ("C:\\Work\\Academy\\Dialsum\\dial.txt");
int start = 50;
int cnt = 0;
int currentPosition;
foreach (string line in instructions) {
   string[] val = line.Split (line[0]);
   int value = int.Parse (val[1]);
   if (line[0] == 'R') {
      currentPosition = (start + value) % 100;
      if (currentPosition == 0) cnt++;
      start = currentPosition;
   } else {
      int sub = start - value;
      currentPosition = sub < 0 ? sub + 100 : sub;
      if (currentPosition == 0) cnt++;
      start = currentPosition;
   }
}
Console.WriteLine (cnt);
