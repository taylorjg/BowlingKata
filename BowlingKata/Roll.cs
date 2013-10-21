namespace BowlingKata
{
    public class Roll
    {
        public Roll(int value, bool isSpare = false)
        {
            Value = value;
            IsSpare = isSpare;
        }

        public int Value { get; private set; }
        public bool IsSpare { get; private set; }

        public string Symbol {
            get
            {
                return IsSpare ? RollSymbols.SpareSymbol : RollSymbols.RollToString(Value);
            }
        }
    }
}
