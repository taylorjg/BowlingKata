namespace BowlingKata
{
    public static class RollSymbols
    {
        public const string StrikeSymbol = "X";
        public const string SpareSymbol = "/";
        public const string GutterSymbol = "-";

        public static string RollToString(int roll)
        {
            switch (roll)
            {
                case 0:
                    return GutterSymbol;

                case 10:
                    return StrikeSymbol;

                default:
                    return System.Convert.ToString(roll);
            }

        }
    }
}
