namespace BowlingKata
{
    public static class RollSymbols
    {
        public const string StrikeSymbol = "X";
        public const string SpareSymbol = "/";
        public const string GutterSymbol = "-";
        public const string BlankSymbol = "";

        public static string RollToString(int roll)
        {
            switch (roll)
            {
                case 0:
                    return RollSymbols.GutterSymbol;

                case 10:
                    return RollSymbols.StrikeSymbol;

                default:
                    return System.Convert.ToString(roll);
            }

        }
    }
}
