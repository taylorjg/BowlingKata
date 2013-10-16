namespace BowlingKata
{
    public class Roll
    {
        public Roll(int value, bool isSpare = false)
        {
            Value = value;
            IsSpare = isSpare;
        }

        public int Value { get; set; }
        public bool IsSpare { get; set; }

        public string Symbol {
            get
            {
                return IsSpare ? RollSymbols.SpareSymbol : RollSymbols.RollToString(Value);
            }
        }
    }
}
