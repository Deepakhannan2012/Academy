namespace A09;
class Tokenizer (Evaluator eval, string text) {
   readonly Evaluator mEval = eval;  // The evaluator that owns this
   readonly string mText = text;     // The input text we're parsing through
   int mN = 0;                       // Position within the text
   public bool inBracket = false;    // Indicates if the operator is inside parenthesis so base priority can be adjusted

   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.': return GetNumber ();
            case '(':
               inBracket = true;
               return new TPunctuation (ch);
            case ')':
               inBracket = false;
               return new TPunctuation (ch);
            case '+' or '-' or '*' or '/' or '^' or '=':
               return new TOpArithmetic (mEval, ch, inBracket);
            case >= 'a' and <= 'z': return GetIdentifier ();
            default: return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub, inBracket);
      else return new TVariable (mEval, sub);
   }
   readonly string[] mFuncs = ["sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" ];

   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         mN--; break;
      }
      // Now, mN points to the first character of mText that is not part of the number
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double f)) return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }
}
