namespace A09;
class EvalException (string message) : Exception (message) {
}

class Evaluator {
   public double Evaluate (string text) {
      List<Token> tokens = [];
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next ();
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }

      // Check if this is a variable assignment
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar && tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f;
   }

   public int BasePriority { get; private set; }

   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double f)) return f;
      throw new EvalException ($"Unknown variable: {name}");
   }

   readonly Dictionary<string, double> mVars = [];
   static Token? mPrevToken;

   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TPunctuation p:
            BasePriority += p.Punct == '(' ? 10 : -10;
            break;
         case TOperator op:
            if (op is TOpArithmetic arith && arith.Op == '-') {
               if (mPrevToken == null || mPrevToken is TOperator || (mPrevToken is TPunctuation p && p.Punct == '('))
                  op = new TOpUnary (this, '-', arith.InPara);
            }
            while (mOperators.Count > 0 && mOperators.Peek ().Priority >= op.Priority) ApplyOperator ();
            mOperators.Push (op);
            break;
         default:
            throw new EvalException ($"Unknown token: {token}");
      }
      if (token is not TEnd) mPrevToken = token;
   }
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();

   void ApplyOperator () {
      if (mOperators.Count == 0) return;
      var op = mOperators.Pop ();
      var f1 = mOperands.Pop ();
      switch (op) {
         case TOpFunction func:
            mOperands.Push (func.Evaluate (f1));
            break;
         case TOpUnary unary:
            mOperands.Push (unary.Apply (f1));
            break;
         case TOpArithmetic arith:
            var f2 = mOperands.Pop ();
            mOperands.Push (arith.Evaluate (f2, f1));
            break;
         default:
            throw new EvalException ($"Unknown operator: {op}");
      }
   }
}


