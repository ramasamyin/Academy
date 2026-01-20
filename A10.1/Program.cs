using static System.Console;
namespace A10._1;

enum State { A, B, C, D }

class Program {
   static void Main () {
      Write ("Enter a path to parse: ");
      string path = ReadLine ()!;
      var (drive, dir, file, ext) = ParseFile (path);
      WriteLine ($"Drive letter: {drive}\nDirectory: {dir}\nFilename: {file}\nExtension: {ext}");
   }
   static (string drive, string dir, string file, string ext) ParseFile (string path) {
      State S = State.A;
      char drive = '\0';
      string dir = "",
             file = "",
             ext = "",
             tmp = "";
      foreach (var ch in path.Trim () + "~") {
         switch (S) {
            case State.A:
               if (ch >= 'A' && ch <= 'Z') {
                  drive = ch;
                  S = State.B;
               }
               break;

            case State.B:
               if (ch == ':') S = State.C;
               break;

            case State.C:
               if (ch == '\\') {
                  if (!string.IsNullOrEmpty (tmp)) {
                     dir += (dir == "" ? "" : "\\") + tmp;
                     tmp = "";
                  }
               } else if (ch == '.') {
                  file = tmp;
                  tmp = "";
                  S = State.D;
               } else if (ch == '~') file = tmp;
               else tmp += ch;
               break;

            case State.D:
               if (ch != '~') ext += ch;
               break;
         }
      }
      return (drive.ToString (), dir, file, ext);
   }
}
