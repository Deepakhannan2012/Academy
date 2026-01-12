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
   readonly int mPriority;
}

class TOpUnary (Evaluator eval, char ch, int bracketDepth) : TOperator (eval) {
   public char Op { get; private set; } = ch;
   public int BracketPriority { get; private set; } = bracketDepth * 10;
   public override string ToString () => $"op:{Op}:{Priority}";
   public override int Priority => 5 + BracketPriority;
   public double Evaluate (double a) => (Op == '-') ? -a : a;
}

class TOpArithmetic (Evaluator eval, char ch, int bracketDepth) : TOperator (eval) {
   public char Op { get; private set; } = ch;
   public int BracketPriority { get; private set; } = bracketDepth * 10;
   public override string ToString () => $"op:{Op}:{Priority}";
   public override int Priority => sPriority[Op] + BracketPriority;
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

class TOpFunction (Evaluator eval, string name, int bracketDepth) : TOperator (eval) {
   public string Func { get; private set; } = name;
   public int BracketPriority { get; private set; } = bracketDepth * 10;
   public override string ToString () => $"func:{Func}:{Priority}";
   public override int Priority => 4 + BracketPriority;

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
