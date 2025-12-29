using System.Text;
StringBuilder sb = new ();
int i = 0;
while (i <= 100) {
   string line = Console.ReadLine ()!;
   i++;
   string[] val = line.Split (' ');
   if (val[0].Equals ("add", StringComparison.CurrentCultureIgnoreCase)) sb.Append (val[1]);
   else if (val[0].Equals ("delete", StringComparison.CurrentCultureIgnoreCase)) {
      int delCount = int.Parse (val[1]);
      if (sb.Length > 0) sb.Remove (sb.Length - 1, delCount);
   } else if (val[0].Equals ("show", StringComparison.CurrentCultureIgnoreCase)) Console.WriteLine ($"Output: {sb}\n");
   else if (val[0].Equals ("exit", StringComparison.CurrentCultureIgnoreCase)) break;
}

