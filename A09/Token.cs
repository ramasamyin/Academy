namespace A09;

abstract class Token {
}

abstract class TNumber : Token {
   public abstract double Value { get; }
}

class TLiteral (double f) : TNumber {
   public override double Value => mValue;
   public override string ToString () => $"literal:{Value}";
   readonly double mValue = f;
}

class TVariable : TNumber {
   public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   public string Name { get; private set; }
   public override double Value => mEval.GetVariable (Name);
   public override string ToString () => $"var:{Name}";
   readonly Evaluator mEval;
}

abstract class TOperator (Evaluator eval) : Token {
   public abstract int Priority { get; }
   readonly protected Evaluator mEval = eval;
}

class TOpArithmetic (Evaluator eval, char ch, bool inPara) : TOperator (eval) {
   public char Op { get; private set; } = ch;
   public bool InPara { get; private set; } = inPara;
   public override string ToString () => $"op:{Op}:{Priority}";
   public override int Priority {
      get {
         if (InPara) return sPriority[Op] + mEval.BasePriority;
         else return sPriority[Op];
      }
   }
   static Dictionary<char, int> sPriority = new () {
      ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 3, ['='] = 4,
   };
   public double Evaluate (double a, double b) {
      return Op switch {
         '+' => a + b,
         '-' => a - b,
         '*' => a * b,
         '/' => a / b,
         '^' => Math.Pow (a, b),
         _ => throw new EvalException ($"Unknown operator: {Op}"),
      };
   }
}

class TOpUnary (Evaluator eval, char op, bool inPara) : TOperator (eval) {
   public char Op { get; private set; } = op;
   public bool InPara { get; private set; } = inPara;
   public override int Priority {
      get {
         if (InPara) return 5 + mEval.BasePriority;
         else return 5;
      }
   }
   public override string ToString () => $"unary:{Op}:{Priority}";
   public double Apply (double operand) {
      return Op switch {
         '-' => -operand,
         '+' => +operand,
         _ => throw new InvalidOperationException ($"Unknown unary operator {Op}")
      };
   }
}


class TOpFunction (Evaluator eval, string name, bool inPara) : TOperator (eval) {
   public string Func { get; private set; } = name;
   public bool InPara { get; private set; } = inPara;
   public override string ToString () => $"func:{Func}:{Priority}";
   public override int Priority {
      get {
         if (InPara) return 4 + mEval.BasePriority;
         else return 4;
      }
   }
   public double Evaluate (double f) {
      return Func switch {
         "sin" => Math.Sin (D2R (f)),
         "cos" => Math.Cos (D2R (f)),
         "tan" => Math.Tan (D2R (f)),
         "sqrt" => Math.Sqrt (f),
         "log" => Math.Log (f),
         "exp" => Math.Exp (f),
         "asin" => R2D (Math.Asin (f)),
         "acos" => R2D (Math.Acos (f)),
         "atan" => R2D (Math.Atan (f)),
         _ => throw new EvalException ($"Unknown function: {Func}")
      };

      double D2R (double f) => f * Math.PI / 180;
      double R2D (double f) => f * 180 / Math.PI;
   }
}

class TPunctuation (char ch) : Token {
   public char Punct { get; private set; } = ch;
   public override string ToString () => $"punct:{Punct}";
}

class TEnd : Token {
   public override string ToString () => "end";
}

class TError (string message) : Token {
   public string Message { get; private set; } = message;
   public override string ToString () => $"error:{Message}";
}
