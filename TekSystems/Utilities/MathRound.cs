using System;
using System.Collections.Generic;
using System.Text;

namespace TekSystems.Utilities
{
    public static class MathRound
    {
        public static decimal MathRoundTwoDecimals(decimal number)
        {
            //decimal resp = Math.Round((Math.Round(number * 20, MidpointRounding.AwayFromZero) / 20), 2);
            decimal resp = Math.Ceiling(number * 20) / 20;
            return resp;
        }
    }
}
